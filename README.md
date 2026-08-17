import React, { useState } from 'react';
import { Button, Modal, Input, Form, ConfigProvider } from 'antd';
import styled from 'styled-components';

// ==========================================
// 1. אזור ה-Styled Components (כל העיצובים כאן)
// ==========================================

const PageWrapper = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100vh;
  background-color: #f0f2f5;
`;

const MainContainer = styled.div`
  width: 70%;
  height: 90vh;
  background: #fff;
  border: 1px solid #d9d9d9;
  border-radius: 8px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  padding: 24px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.05);
`;

const FormRow = styled.div`
  display: flex;
  width: 100%;
  align-items: center;
`;

const FormLabel = styled.span`
  width: 130px;
  text-align: right;
  flex-shrink: 0;
  margin-left: 12px;
  font-weight: 500;
`;

const StyledInput = styled(Input)`
  flex: 1;
`;

const StyledForm = styled(Form)`
  margin-top: 20px;
`;

// ==========================================
// 2. רכיב ה-App הראשי
// ==========================================

export default function App() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [form] = Form.useForm();

  const handleModalOk = () => {
    form.validateFields().then((values) => {
      console.log('Submitted values:', values);
      setIsModalOpen(false);
      form.resetFields();
    }).catch((info) => {
      console.log('Validate Failed:', info);
    });
  };

  const handleModalCancel = () => {
    setIsModalOpen(false);
    form.resetFields();
  };

  return (
    <ConfigProvider direction="rtl">
      <PageWrapper>
        <MainContainer>
          <Button type="primary" size="large" onClick={() => setIsModalOpen(true)}>
            הוספת משתמש חדש
          </Button>

          <Modal 
            title="הוספת משתמש חדש" 
            open={isModalOpen} 
            onOk={handleModalOk} 
            onCancel={handleModalCancel}
            okText="אישור"
            cancelText="ביטול"
            centered
          >
            <StyledForm form={form} layout="horizontal">
              <Form.Item 
                name="name" 
                required={false}
                rules={[{ required: true, message: 'נא להזין שם' }]}
                style={{ marginBottom: 16 }}
              >
                <FormRow>
                  <FormLabel>שם:</FormLabel>
                  <StyledInput placeholder="הכנס שם מלא" />
                </FormRow>
              </Form.Item>
              
              <Form.Item 
                name="idNumber" 
                required={false}
                rules={[{ required: true, message: 'נא להזין תעודת זהות' }]}
                style={{ marginBottom: 12 }}
              >
                <FormRow>
                  <FormLabel>מספר תעודת זהות:</FormLabel>
                  <StyledInput placeholder="הכנס מספר תעודת זהות" />
                </FormRow>
              </Form.Item>
            </StyledForm>
          </Modal>
        </MainContainer>
      </PageWrapper>
    </ConfigProvider>
  );
}
