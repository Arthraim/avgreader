using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using AVGreader.Domain;
using AVGreader.Service.Persistent;

namespace AVGreader.Service
{
    class ScriptSrv
    {
        #region ����

        private static AvgsReader avgsReader = new AvgsReader();
        private static StoryReader storyReader = new StoryReader();
        private static Story _story = storyReader.GetWholeStory();

        #endregion

        #region ����

        public static Story story
        {
            get { return _story; }
            set { _story = value; }
        }

        #endregion


        #region �ű�����

        /// <summary>
        /// ���ַ�������
        /// </summary>
        /// <param name="cmd">���</param>
        public static void QueryScriptCommand(string cmd)
        {
            if (cmd == "<PAGE>")
            {
                HandlePage();
            }
            else if (cmd == "<BR>")
            {
                HandleBR();
            }
            else if (cmd == "<END>")
            {
                HandleEnd();
            }
            else if (cmd[0] == '<' && cmd[cmd.Length - 1] == '>')
            {
                int i;
                string detailCMD = "";
                string id = "";
                for (i = 1; cmd[i] != ' '; i++)
                    detailCMD += cmd[i];
                for (i++; cmd[i] != '>'; i++)
                    id += cmd[i];

                switch (detailCMD)
                {
                    case "SCENE": ShowBackground(id); break;
                    case "ACTOR": ShowActor(id); break;
                    case "FACE": ShowFace(id); break;
                }
            }
            else if (cmd[0] != '<')
            {
                ShowText(cmd);
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="words">�Ի�����</param>
        private static void ShowText(string words)
        {
            //TextFrameSrv.sentense += words;
            foreach (char ch in words)
            {
                TextFrameSrv.sentense += ch;
                EngineSrv.Render();
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="id">����ID</param>
        private static void ShowBackground(string id)
        {
            Scene scene = avgsReader.GetSceneById(id);
            BackgroundSrv.Path = scene.Path;
        }

        /// <summary>
        /// �����ɫ
        /// </summary>
        /// <param name="id">��ɫID</param>
        private static void ShowActor(string id)
        {
            Actor actor = avgsReader.GetActorById(id);
            ActorSrv.Path = actor.Path;
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="id">����ID</param>
        private static void ShowFace(string id)
        {
        }

        /// <summary>
        /// ����ҳ
        /// </summary>
        private static void HandlePage()
        {
            //��������ͳͳ������������֧������
            //1������
            //KeyboardSrv.PressAnyKey();
            //2���鿴�԰�
            //3��ȥ���ı���
            //4���浵������

            TextFrameSrv.sentense = "";
        }

        /// <summary>
        /// ������
        /// </summary>
        private static void HandleBR()
        {
            TextFrameSrv.sentense += "\n";
        }

        /// <summary>
        /// �������
        /// </summary>
        private static void HandleEnd()
        {
            EngineSrv.NowRunState = MyEnum.RunStatement.END;
            MessageBox.Show("ִ����ϣ�");
        }

        #endregion
    }
}
