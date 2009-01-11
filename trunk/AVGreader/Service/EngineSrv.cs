using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using AVGreader.Domain;
using AVGreader.Service.Persistent;

namespace AVGreader.Service
{
    public static class EngineSrv
    {
        #region ˽�б�������

        
        #endregion


        #region ����


        private static Control _parent;
        /// <summary>
        /// ������
        /// </summary>
        public static Control parent
        {
            get { return _parent; }
            set { _parent = value; }
        }


        private static Device _device;
        /// <summary>
        /// ��Ⱦ�豸
        /// </summary>
        public static Device device
        {
            get { return _device; }
            set { _device = value; }
        }


        private static Sprite _sprite = null;
        /// <summary>
        /// ��Ⱦ�ӿ�
        /// </summary>
        public static Sprite sprite
        {
            get { return EngineSrv._sprite; }
            set { EngineSrv._sprite = value; }
        }


        private static bool _flag = true;
        /// <summary>
        /// �Ƿ���ʾ�˵���
        /// </summary>
        public static bool flag
        {
            get { return EngineSrv._flag; }
            set { EngineSrv._flag = value; }
        }


        private static MyEnum.RunStatement _nowRunState = MyEnum.RunStatement.RUNNING;
        /// <summary>
        /// ���б��
        /// </summary>
        public static MyEnum.RunStatement nowRunState
        {
            get { return _nowRunState; }
            set { _nowRunState = value; }
        }

        private static long _nowIndex = 1;
        /// <summary>
        /// ����Ϸѭ��������
        /// </summary>
        public static long nowIndex
        {
            get { return _nowIndex; }
            set { _nowIndex = value; }
        }

        #endregion


        #region ��ʼ��
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns>True</returns>
        public static bool Initialize()
        {
            try
            {
                PresentParameters pp = new PresentParameters();
                pp.Windowed = true;  //����ģʽ
                pp.SwapEffect = SwapEffect.Discard;
                device = new Device(0, DeviceType.Hardware, parent, CreateFlags.SoftwareVertexProcessing, pp);

                sprite = new Sprite(device);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        #endregion


        #region ������Դ

        /// <summary>
        /// �л���ʾ����
        /// </summary>
        public static void FrameMove()
        {
            BackgroundSrv.LoadResX();
            ActorSrv.LoadResX();
            TextFrameSrv.InitializeFont();
        }

        #endregion
        

        #region ��Ⱦ

        /// <summary>
        /// ��Ⱦ
        /// </summary>
        public static void Render()
        {
            device.Clear(ClearFlags.Target, Color.Blue, 0, 0);
            device.BeginScene();
            sprite.Begin(SpriteFlags.AlphaBlend);

            BackgroundSrv.Render();
            ActorSrv.Render();
            if (flag == true)
                TextFrameSrv.Render();

            sprite.End();
            device.EndScene();
            device.Present();
        }

        #endregion
        

        #region ��ѭ��

        /// <summary>
        /// ��Ϸ��ѭ��
        /// </summary>
        private static void MainLoop()
        {
            //һ��һ���жϲ��ַ�ִ��
            while (nowIndex <= ScriptSrv.story.LineNumber)
            {
                if (nowRunState == MyEnum.RunStatement.RUNNING)
                {
                    ScriptSrv.QueryScriptCommand(nowIndex);
                    FrameMove();
                    Render();
                    Application.DoEvents();
                }
                else if (nowRunState == MyEnum.RunStatement.PAGE)
                {
                    if (KeyboardSrv.keyState == null)
                    {
                        Render();
                        Application.DoEvents();
                    }
                    else
                    {
                        nowRunState = MyEnum.RunStatement.RUNNING;
                        ScriptSrv.QueryScriptCommand(nowIndex);
                        FrameMove();
                        Render();
                        Application.DoEvents();
                    }
                }
                else if (nowRunState == MyEnum.RunStatement.END)
                {
                    break;  //�籾ִ����� �����籾ѭ��
                }

                // ָ����һ��
                nowIndex++;
            }
        }

        /// <summary>
        /// ��Ϸ����
        /// </summary>
        public static void Run ()
        {
            //�˵�

            //��ѭ��
            MainLoop();

            //��Ϸ�������ͷ�
            ExitGame();
        }

        /// <summary>
        /// �˳���Ϸ
        /// </summary>
        private static void ExitGame()
        {
            device.Dispose();
            Application.Exit();
        }

        #endregion
    }
}
