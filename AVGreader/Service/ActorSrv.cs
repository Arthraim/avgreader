using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace AVGreader.Service
{
    public static class ActorSrv
    {
        #region ����

        static string _Path;
        static Texture texRole1 = null;
        //static Texture texRole2 = null;
        
        #endregion

        #region ����

        public static string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }

        #endregion

        #region ������Դ
        /// <summary>
        /// ������Դ
        /// </summary>
        public static void LoadResX()
        {
            if (Path == null)
                return;
            texRole1 = TextureLoader.FromFile(EngineSrv.device, Path);  //����ͼ���ڴ�
            //texRole2 = TextureLoader.FromFile(EngineSrv.device, Path + "Test.png");  //����ͼ���ڴ�
        }

        #endregion

        #region ��Ⱦ
        public static void Render()
        {
            if (texRole1 == null)
                return;
            //ͼ�񣬲ü�λ�ã����ֳ����Ĵ�С������λ�ã���ɫ����ɫ����Ӧ���κι��ˣ�
            EngineSrv.sprite.Draw2D(texRole1,               //���Ƶ�����
                                    Rectangle.Empty,        //�ü������մ���ȫ����
                                    new SizeF(315, 540),    //���ƵĴ�С
                                    new PointF(96, 0),      //���Ƶ�λ��
                                    Color.White);           //��ɫ���ˣ���ɫ����Ӧ���κι��ˣ�
            //EngineSrv.sprite.Draw2D(texRole2, Rectangle.Empty, new SizeF(512, 512), new PointF(0, 0), Color.White);
        }

        #endregion
    }
}
