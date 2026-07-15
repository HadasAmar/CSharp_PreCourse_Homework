import React, { useState, useRef } from 'react';
import { Drawer, Button } from 'antd';
import { UpOutlined, ArrowsAltOutlined, ShrinkOutlined } from '@ant-design/icons';

export default function App() {
  // 1. ניהול גובה ה-Drawer (בפיקסלים או באחוזים)
  const [drawerHeight, setDrawerHeight] = useState<string | number>(150);
  const [isMaximized, setIsMaximized] = useState(false);
  
  // 2. ניהול כפתור ה-Scroll to Top של אזור הטקסט
  const [showScrollTopBtn, setShowScrollTopBtn] = useState(false);
  const textContainerRef = useRef<HTMLDivElement>(null);

  // פונקציית שינוי גודל ידנית (בזמן גרירה עם הידית של Antd)
  const handleResize = (size: number) => {
    setIsMaximized(false); // ביטול מצב מקסימום אם המשתמש התחיל לגרור ידנית
    
    const minHeight = 60;
    const maxHeight = 480; // מקסימום 80% מגובה הקונטיינר (600px)
    const boundedSize = Math.max(minHeight, Math.min(maxHeight, size));
    
    setDrawerHeight(boundedSize);
  };

  // פונקציית ה-Toggle (מקסום / צמצום של ה-Drawer)
  const toggleMaximize = () => {
    if (isMaximized) {
      setDrawerHeight(150); // מחזיר לגובה דיפולטיבי
      setIsMaximized(false);
    } else {
      setDrawerHeight('90%'); // מקפיץ לגובה מקסימלי יחסי לאבא
      setIsMaximized(true);
    }
  };

  // האזנה לגלילה בתוך ה-DIV של הטקסט
  const handleTextScroll = () => {
    if (!textContainerRef.current) return;
    
    // מציג את הכפתור רק אם גללו מעל 250 פיקסלים בתוך ה-DIV
    if (textContainerRef.current.scrollTop > 250) {
      setShowScrollTopBtn(true);
    } else {
      setShowScrollTopBtn(false);
    }
  };

  // גלילה חלקה חזרה למעלה של ה-DIV
  const scrollToTop = () => {
    if (textContainerRef.current) {
      textContainerRef.current.scrollTo({
        top: 0,
        behavior: 'smooth',
      });
    }
  };

  return (
    <div className="page-wrapper">
      {/* הגדרות ה-CSS הדרושות לכל הפיצ'רים */}
      <style>{`
        .page-wrapper {
          display: flex;
          justify-content: center;
          align-items: center;
          height: 100vh;
          background-color: #f0f2f5;
          width: 100%;
          font-family: system-ui, -apple-system, sans-serif;
          margin: 0;
          box-sizing: border-box;
        }

        /* ה-DIV המרכזי שתופס 70% מהמסך */
        .main-70-container {
          width: 70%;
          height: 600px;
          background-color: #ffffff;
          border: 1px solid #d9d9d9;
          border-radius: 8px;
          box-shadow: 0 4px 12px rgba(0, 0, 0, 0.08);
          position: relative; /* קריטי עבור כליאת אלמנטים ב-Absolute */
          display: flex;
          flex-direction: column; /* מסדר את הבנים אנכית (אחד תחת השני) */
          overflow: hidden;
        }

        /* אזור הטקסט - מתרחב ומצטמצם אוטומטית בעזרת ה-Flex */
        .text-area-scrollable {
          flex: 1; 
          overflow-y: auto;
          padding: 24px;
          background-color: #fafafa;
        }

        .text-area-scrollable h2 {
          margin-top: 0;
          color: #333;
        }

        .text-area-scrollable p {
          color: #555;
          line-height: 1.6;
        }

        /* אילוץ ה-Drawer של Antd להפוך לאלמנט זורם (Relative) ולא צף */
        .main-70-container .ant-drawer {
          position: relative !important; 
          height: var(--drawer-height) !important;
          transition: height 0.2s cubic-bezier(0.645, 0.045, 0.355, 1) !important; /* מעבר חלק ב-Toggle */
        }

        .main-70-container .ant-drawer-content-wrapper {
          position: relative !important;
          height: 100% !important;
          box-shadow: 0 -4px 12px rgba(0,0,0,0.05) !important;
          transform: none !important; /* ביטול האנימציה שגורמת לציפה ובריחה */
          transition: height 0.2s cubic-bezier(0.645, 0.045, 0.355, 1) !important;
        }

        /* פינוי מקום כותרת ה-Drawer לידית הגרירה של Antd */
        .main-70-container .ant-drawer-header {
          padding-top: 20px !important;
        }

        /* סטייל כפתור חזרה למעלה צף */
        .scroll-to-top-btn {
          position: absolute;
          z-index: 100; /* מבטיח שהוא יצוף מעל הכל */
          box-shadow: 0 4px 10px rgba(0,0,0,0.15);
          transition: bottom 0.2s cubic-bezier(0.645, 0.045, 0.355, 1); /* זז חלק יחד עם המגירה */
        }
      `}</style>

      {/* ה-DIV המרכזי שהוא ה-Container של הכל */}
      <div 
        className="main-70-container" 
        style={{ 
          '--drawer-height': typeof drawerHeight === 'number' ? `${drawerHeight}px` : drawerHeight 
        } as React.CSSProperties}
      >
        
        {/* 1. אזור הטקסט הנסגר והנגלל (מכיל רק את הטקסט עצמו) */}
        <div 
          ref={textContainerRef}
          onScroll={handleTextScroll}
          className="text-area-scrollable" 
          dir="rtl"
        >
          <h2>אזור מידע ראשי (בתוך ה-70%)</h2>
          <p>גללי למטה כדי לראות את כפתור ה-Scroll to Top צץ בפינה.</p>
          <p>הכפתור כעת יושב מחוץ לאזור הגלילה, כך שהוא יישאר נעוץ במקומו ולא ייעלם עם הטקסט!</p>
          
          {/* מייצר פסקאות ארוכות במיוחד כדי לייצר סקרולבר */}
          {Array.from({ length: 18 }).map((_, index) => (
            <p key={index}>שורת טקסט מספר {index + 1} לצורך הדגמת הגלילה והכפתור הצף.</p>
          ))}
          <p style={{ fontWeight: 'bold', color: '#1677ff' }}>✓ הגעת לסוף הטקסט! ה-Flexbox שומר עליו גלוי תמיד.</p>
        </div>

        {/* 2. כפתור חזרה למעלה - יושב כאח ישיר של הקונטיינר ומחשב מיקום מעל המגירה */}
        {showScrollTopBtn && (
          <Button
            className="scroll-to-top-btn"
            type="primary"
            shape="circle"
            icon={<UpOutlined />}
            onClick={scrollToTop}
            size="large"
            style={{
              right: '24px',
              // לוקח את גובה המגירה הנוכחי ומוסיף 24 פיקסלים מרווח מעליה
              bottom: `calc(var(--drawer-height) + 24px)` 
            }}
          />
        )}

        {/* 3. ה-Drawer של Ant Design */}
        <Drawer
          title={
            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', direction: 'rtl' }}>
              <span>כותרת ה-Drawer</span>
              {/* כפתור ה-Toggle הפנימי */}
              <Button 
                type="text" 
                icon={isMaximized ? <ShrinkOutlined /> : <ArrowsAltOutlined />} 
                onClick={toggleMaximize}
              >
                {isMaximized ? 'צמצם גובה' : 'מסך מלא'}
              </Button>
            </div>
          }
          placement="bottom"
          closable={false}
          open={true}
          mask={false}
          getContainer={false} // מגביל ומצמיד את המגירה רק לתוך ה-70%
          
          // תמיכה מובנית בגרירה ידנית של Antd
          resizable={{
            onResize: handleResize,
          }}
          height={drawerHeight}
        >
          <div className="drawer-inner-content" dir="rtl">
            <h4>תוכן ה-Drawer</h4>
            <p>תוכלי לגרור את הידית האפורה למעלה כדי לשנות את הגובה ידנית, או ללחוץ על כפתור ה"מסך מלא" בכותרת.</p>
          </div>
        </Drawer>

      </div>
    </div>
  );
}
