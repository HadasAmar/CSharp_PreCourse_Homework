
import { Injectable } from "@nestjs/common";
import { InjectDataSource } from "@nestjs/typeorm";
import { DataSource } from "typeorm";
import { PostLinkEntity } from "./post-link.entity";
import { GetLinkedPostsInput } from "./dto/get-linked-posts.input";
import { LinkedPostsPaginatedResponse } from "./dto/linked-posts-paginated.response";

@Injectable()
export class PostLinksService {
  constructor(
    @InjectDataSource()
    private readonly dataSource: DataSource
  ) {}

  async getLinkedPosts(input: GetLinkedPostsInput): Promise<LinkedPostsPaginatedResponse> {
    const { versionId, limit, offset } = input;

    // 1. קישורים יוצאים
    const outgoingQuery = this.dataSource
      .createQueryBuilder()
      .select("link.id", "id")
      .addSelect("link.postId", "postId")
      .addSelect("link.postVersionId", "postVersionId")
      .addSelect("link.linkedPostId", "linkedPostId")
      .addSelect("link.linkedPostVersionId", "linkedPostVersionId")
      .addSelect("'outgoing'", "direction")
      .from(PostLinkEntity, "link")
      .where("link.postVersionId = :versionId");

    // 2. תת-שאילתה לסינון גרסה מקסימלית עבור קישורים נכנסים
    const maxIncomingSubQuery = this.dataSource
      .createQueryBuilder()
      .select("MAX(sub.id)", "maxId")
      .from(PostLinkEntity, "sub")
      .where("sub.linkedPostVersionId = :versionId")
      .groupBy("sub.postId");

    // 3. קישורים נכנסים
    const incomingQuery = this.dataSource
      .createQueryBuilder()
      .select("link.id", "id")
      .addSelect("link.postId", "postId")
      .addSelect("link.postVersionId", "postVersionId")
      .addSelect("link.linkedPostId", "linkedPostId")
      .addSelect("link.linkedPostVersionId", "linkedPostVersionId")
      .addSelect("'incoming'", "direction")
      .from(PostLinkEntity, "link")
      .where(`link.id IN (${maxIncomingSubQuery.getQuery()})`);

    // 4. שאילתת UNION מאוחדת + Paging + COUNT(*) OVER()
    const rawResults = await this.dataSource
      .createQueryBuilder()
      .select("combined.id", "id")
      .addSelect("combined.postId", "postId")
      .addSelect("combined.postVersionId", "postVersionId")
      .addSelect("combined.linkedPostId", "linkedPostId")
      .addSelect("combined.linkedPostVersionId", "linkedPostVersionId")
      .addSelect("combined.direction", "direction")
      .addSelect("COUNT(*) OVER()", "totalCount")
      .from(`(${outgoingQuery.getQuery()} UNION ALL ${incomingQuery.getQuery()})`, "combined")
      .setParameters({
        versionId,
        limit,
        offset,
      })
      .orderBy("combined.id", "DESC")
      .offset(offset)
      .limit(limit)
      .getRawMany();

    const totalCount = rawResults.length > 0 ? parseInt(rawResults[0].totalCount, 10) : 0;
    const items = rawResults.map(({ totalCount, ...item }) => item);

    return {
      items,
      totalCount,
    };
  }
}
