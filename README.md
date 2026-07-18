import React, { useState, useRef, useEffect } from 'react';
import { Button } from 'antd';
import { UpOutlined } from '@ant-design/icons';
import { DrawerPanel } from './DragDrawer';

export default function App() {
  const containerRef = useRef<HTMLDivElement>(null);
  const [containerHeight, setContainerHeight] = useState(0);
  const [drawerHeight, setDrawerHeight] = useState<string | number>(150);
  const [isMaximized, setIsMaximized] = useState(false);
  const [showScrollTopBtn, setShowScrollTopBtn] = useState(false);
  const textContainerRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    if (containerRef.current) setContainerHeight(containerRef.current.offsetHeight);
  }, []);

  const handleResize = (size: number) => {
    if (size > containerHeight * 0.95) {
      setDrawerHeight('100%');
      setIsMaximized(true);
    } else {
      setDrawerHeight(Math.max(60, size));
      setIsMaximized(false);
    }
  };

  const toggleMaximize = () => {
    setDrawerHeight(isMaximized ? 150 : '100%');
    setIsMaximized(!isMaximized);
  };

  return (
    <div className="page-wrapper">
      <div 
        ref={containerRef} 
        className="main-70-container" 
        style={{ '--drawer-height': typeof drawerHeight === 'number' ? `${drawerHeight}px` : drawerHeight } as React.CSSProperties}
      >
        <style>{`
          .page-wrapper { display: flex; justify-content: center; align-items: center; min-height: 100vh; background-color: #f0f2f5; }
          .main-70-container { width: 70%; height: 90vh; background: #fff; border: 1px solid #d9d9d9; border-radius: 8px; display: flex; flex-direction: column; overflow: hidden; position: relative; }
          
          .text-area-scrollable { flex: 1; overflow-y: auto; padding: 24px; background: #fafafa; }
          .text-area-scrollable::-webkit-scrollbar { width: 8px; }
          .text-area-scrollable::-webkit-scrollbar-thumb { background: #c1c1c1; border-radius: 10px; border: 2px solid #fafafa; }
          .text-area-scrollable::-webkit-scrollbar-button { display: none; }

          .main-70-container .ant-drawer { position: relative !important; height: var(--drawer-height) !important; transition: height 0.1s ease !important; }
          .main-70-container .ant-drawer-content-wrapper { position: relative !important; height: 100% !important; transform: none !important; }
          
          /* תיקון הכפתור: מרווח קבוע של 10px מהמגירה */
          .scroll-to-top-btn { 
            position: absolute; right: 24px; z-index: 100; 
            box-shadow: 0 4px 10px rgba(0,0,0,0.15); 
            transition: bottom 0.1s ease; 
          }
        `}</style>

        {/* מופעל כבר ב-10 פיקסלים גלילה */}
        <div 
          ref={textContainerRef} 
          onScroll={() => setShowScrollTopBtn(textContainerRef.current!.scrollTop > 10)} 
          className="text-area-scrollable" 
          dir="rtl"
        >
          <h2>אזור מידע ראשי</h2>
          {Array.from({ length: 20 }).map((_, i) => <p key={i}>שורת טקסט {i + 1}...</p>)}
        </div>

        {showScrollTopBtn && (
          <Button 
            className="scroll-to-top-btn" 
            type="primary" 
            shape="circle" 
            icon={<UpOutlined />} 
            onClick={() => textContainerRef.current?.scrollTo({ top: 0, behavior: 'smooth' })} 
            size="large" 
            style={{ bottom: `calc(var(--drawer-height) + 10px)` }} 
          />
        )}

        <DrawerPanel height={drawerHeight} isMaximized={isMaximized} onResize={handleResize} onToggle={toggleMaximize} />
      </div>
    </div>
  );
}



import React from 'react';
import { Drawer, Button } from 'antd';
import { ArrowsAltOutlined, ShrinkOutlined } from '@ant-design/icons';

interface DrawerProps {
  height: string | number;
  isMaximized: boolean;
  onResize: (size: number) => void;
  onToggle: () => void;
}

export const DrawerPanel: React.FC<DrawerProps> = ({ height, isMaximized, onResize, onToggle }) => {
  return (
    <Drawer
      title={
        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', direction: 'rtl' }}>
          <span>כותרת המגירה</span>
          <Button type="text" icon={isMaximized ? <ShrinkOutlined /> : <ArrowsAltOutlined />} onClick={onToggle}>
            {isMaximized ? 'צמצם' : 'מסך מלא'}
          </Button>
        </div>
      }
      placement="bottom"
      closable={false}
      open={true}
      mask={false}
      getContainer={false}
      resizable={{ onResize }}
      height={height}
    >
      <div dir="rtl">
        <h4>תוכן ה-Drawer</h4>
        <p>גרירה למעלה עד הסוף תגדיר אותי למסך מלא.</p>
      </div>
    </Drawer>
  );
};
