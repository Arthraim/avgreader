using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;

namespace AVGreader.Service
{
    public static class KeyboardSrv
    {
        #region ����

        private static Device keyDevice = new Device(new Guid());
        private static KeyboardState _keyState = null;

        #endregion

        #region ����

        public static KeyboardState keyState
        {
            get { return _keyState; }
            set { _keyState = value; }
        }

        #endregion

        #region ��ʼ��

        private static void initializeKeyboard()
        {
            keyDevice.SetCooperativeLevel(EngineSrv.parent, CooperativeLevelFlags.Exclusive | CooperativeLevelFlags.Foreground);
        }
        
        #endregion

        #region ��ز�������

        private static void FreeKeyboard()
        {
            keyDevice.Unacquire();
            keyDevice.Dispose();
            keyDevice = null;
        }
        
        public static void PressAnyKey()
        {
            initializeKeyboard();
            while ((keyState = keyDevice.GetCurrentKeyboardState()) == null)
            {
                Application.DoEvents();
            }
            FreeKeyboard();
        }

        #endregion
    }
}
