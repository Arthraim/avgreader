using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace AVGreader.Service
{
    public static class BackgroundSrv
    {
        #region ����

        static string _Path;
        static Texture Background1 = null;

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
            Background1 = TextureLoader.FromFile(EngineSrv.device, Path);  //����ͼ���ڴ�
        }

        #endregion

        #region ��Ⱦ
        /// <summary>
        /// ��Ⱦ
        /// </summary>
        public static void Render()
        {
            //Engine.sprite.Draw2D(Background1, new PointF(0, 0), 0, new PointF(0, 0), Color.White);
            EngineSrv.sprite.Draw2D(Background1, 
                                    new Rectangle(0, 0, 1024, 768), //������Ϊ�ü�ͼƬ��������ֵ��ͼ���С����
                                    new SizeF(EngineSrv.parent.ClientSize.Width, EngineSrv.parent.ClientSize.Height), 
                                    new PointF(0, 0), 
                                    Color.White);  

        }

        #endregion
    }
}
