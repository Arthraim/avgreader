using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace AVGreader.Service
{
    public static class TextFrameSrv
    {
        #region ����

        static Microsoft.DirectX.Direct3D.Font font = null;
        private static string _fontFamily = "΢���ź�";
        private static float _fontSize = 20f;
        private static string _sentense = "null";
        private static Point _position = new Point(150,150);
        private static Color _color = Color.White;

        private static string framePath = Application.StartupPath + @"\DATA\IMG\frame.png";
        static Texture texFrame = null;

        #endregion

        #region ����

        public static string fontFamily
        {
            get { return _fontFamily; }
            set { _fontFamily = value; }
        }
        public static float fontSize
        {
            get { return _fontSize; }
            set { _fontSize = value; }
        }
        public static string sentense
        {
            get { return _sentense; }
            set { _sentense = value; }
        }
        public static Point position
        {
            get { return _position; }
            set { _position = value; }
        }
        public static Color color
        {
            get { return _color; }
            set { _color = value; }
        }

        #endregion

        #region ����������
        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="family">��������</param>
        /// <param name="size">�����С</param>
        public static void InitializeFont()
        {
            if (font != null)
                font = null;
            System.Drawing.Font sf = new System.Drawing.Font(fontFamily, fontSize);
            font = new Microsoft.DirectX.Direct3D.Font(EngineSrv.device, sf);

            LoadFrame();
        }

        public static void LoadFrame()
        {
            texFrame = TextureLoader.FromFile(EngineSrv.device, framePath);  //����ͼ���ڴ�
        }

        #endregion

        #region ��Ⱦ
        /// <summary>
        /// ��Ⱦ
        /// </summary>
        /// <param name="value">��������</param>
        /// <returns>��������</returns>
        public static void Render()
        {
            EngineSrv.sprite.Draw2D(texFrame,               //���Ƶ�����
                                    Rectangle.Empty,        //ѡȡ�����򣨿մ���ѡ��ȫ����
                                    new SizeF(611, 270),    //���ƵĴ�С
                                    new PointF(253, 270),   //���Ƶ�λ��
                                    Color.White);           //��ɫ����ʹ���κ���ɫ����
            
            font.DrawText(EngineSrv.sprite,                 //���Ƶ�ͼ��
                          sentense,                         //�ַ���
                          position,                         //��λ��
                          color);                           //������ɫ
        }

        #endregion
    }
}
