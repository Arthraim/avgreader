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
        #region ����

        static Device _device;
        static Control _parent;
        static Sprite _sprite = null;
        static bool _flag = true;
        static MyEnum.RunStatement _NowRunState = MyEnum.RunStatement.RUNNING;
        
        #endregion


        #region ����
        /// <summary>
        /// ������
        /// </summary>
        public static Control parent
        {
            get { return _parent; }
            set { _parent = value; }
        }
        /// <summary>
        /// ��Ⱦ�豸
        /// </summary>
        public static Device device
        {
            get { return _device; }
            set { _device = value; }
        }
        /// <summary>
        /// ��Ⱦ�ӿ�
        /// </summary>
        public static Sprite sprite
        {
            get { return EngineSrv._sprite; }
            set { EngineSrv._sprite = value; }
        }
        /// <summary>
        /// �Ƿ���ʾ�˵���
        /// </summary>
        public static bool flag
        {
            get { return EngineSrv._flag; }
            set { EngineSrv._flag = value; }
        }
        /// <summary>
        /// ���б��
        /// </summary>
        public static MyEnum.RunStatement NowRunState
        {
            get { return _NowRunState; }
            set { _NowRunState = value; }
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
        /// ����Ϸѭ��
        /// </summary>
        public static void Run ()
        {
            //�������˵�

            //�ű��Ķ�ģʽһ��һ���жϲ��ַ�ִ��
            foreach (string cmd in ScriptSrv.story.text)
            {
                if (NowRunState == MyEnum.RunStatement.RUNNING)
                {
                    ScriptSrv.QueryScriptCommand(cmd);
                    FrameMove();
                    Render();
                    Application.DoEvents();
                }
                else if (NowRunState == MyEnum.RunStatement.PAGE)
                {
                    if (KeyboardSrv.keyState == null)
                    {
                        Render();
                        Application.DoEvents();
                    }
                    else
                    {
                        NowRunState = MyEnum.RunStatement.RUNNING;
                        FrameMove();
                        Render();
                        Application.DoEvents();
                    }
                }
                else if (NowRunState == MyEnum.RunStatement.END)
                {
                    device.Dispose();
                    Application.Exit();
                }
            }
        }

        #endregion
    }
}
