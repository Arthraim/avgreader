using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using SepLineTest2.Domain;
using SepLineTest2.UI;
using SepLineTest2.Service.Persistent;

namespace SepLineTest2.UI
{
    public partial class FormMain : Form
    {
        StoryReader2 storyReader = new StoryReader2();
        private Story2 myStory = new Story2();

        static long index = 1;

        public FormMain()
        {
            InitializeComponent();
            OpenDefaultAVGS();
        }

        private void EnabledGroupBoxes(bool flag)
        {
            groupBox1.Enabled = flag;
            groupBox2.Enabled = flag;
            groupBox3.Enabled = flag;
            groupBox4.Enabled = flag;
            groupBox6.Enabled = flag;

            ReplaceToolStripMenuItem.Enabled = flag;
            ResetToolStripMenuItem.Enabled = flag;
        }

        private void OpenDefaultAVGS()
        {
            myStory = storyReader.GetWholeStory();

            EnabledGroupBoxes(true);
            tsTextStatus.ForeColor = Color.Green;
            tsTextStatus.Text = "�Ѽ���Ĭ��AVGS";
            RefreshIndex();
            tbLength.Text = myStory.LineNumber.ToString();
        }

        private void OpenOtherFils()
        {
            openFileDialog1.Multiselect = false;
            openFileDialog1.Title = "���ı��ļ�";
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "AVGS�ű��ļ�(.avgs)|*.avgs|�ı��ļ�(.txt)|*.txt|�����ļ�|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                myStory = GetOtherFile(openFileDialog1.FileName);
            }

            EnabledGroupBoxes(true);
            tsTextStatus.ForeColor = Color.DarkOrange;
            tsTextStatus.Text = "�Ѽ���" + openFileDialog1.FileName.ToString();
            RefreshIndex();
            tbLength.Text = myStory.LineNumber.ToString();
        }

        private void CloseFile()
        {
            myStory = new Story2();

            tsTextStatus.ForeColor = Color.Red;
            tsTextStatus.Text = "�ļ��ѹر�";
            tbNowIndex.Text = "";
            tbLength.Text = "";
            index = 1;
            EnabledGroupBoxes(false);
            ProgressBar.Value = 0;
            textFakeConsole.Clear();
        }

        private void RefreshIndex()
        {
            tbNowIndex.Text = index.ToString();

            ProgressBar.Maximum = (int)myStory.LineNumber;
            ProgressBar.Minimum = 0;
            ProgressBar.Value = (int)index;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void printLn (long index, string str)
        {
            string tempStr;
            tempStr = string.Format("{0:[0000] }", index);
            tempStr += str + "\n";
            textFakeConsole.Text += tempStr;
        }

        private void btnOneLine_Click(object sender, EventArgs e)
        {
            if (index <= myStory.LineNumber)
            {
                printLn(index, myStory.GetTextByIndex(index));
                RefreshIndex();
                index++;
            }
            else
            {
                MessageBox.Show("�Ѿ�����ű�ĩ�ˣ�", "ע�⣡", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnThisLine_Click(object sender, EventArgs e)
        {
            //��ֹ�հ�
            if (tbIndex.Text == "")
            {
                MessageBox.Show("�к��ı����ڲ���Ϊ�գ�\n������Ҫ��λ���кţ�", "ע�⣡", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            long thisIndex;
            //��ֹ�����쳣�ַ�
            try
            {
                thisIndex = Convert.ToInt64(tbIndex.Text);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + "\n��ȷ�����Ѿ����룬\n��������������֡�", "ע�⣡", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //��ֹ�������ֲ��ڽű���Χ��
            if (thisIndex > myStory.LineNumber || thisIndex < 1)
            {
                MessageBox.Show("�������ַǷ���\n��ȷ���������������1���ű���󳤶�֮�䣡", "ע�⣡", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //����
            printLn(thisIndex, myStory.GetTextByIndex(thisIndex));
        }

        private void btnAllLines_Click(object sender, EventArgs e)
        {
            textFakeConsole.Clear();
            foreach (Line l in myStory.content)
            {
                printLn(l.index, l.text);
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            textFakeConsole.Clear();
        }

        private void btnZeroIndex_Click(object sender, EventArgs e)
        {
            index = 1;
            RefreshIndex();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout frmAbout = new FormAbout();
            frmAbout.ShowDialog();
        }

        private void ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            index = 1;
            RefreshIndex();
        }

        private void CLSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textFakeConsole.Clear();
        }

        private void EditScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEditScript frmEdit = new FormEditScript();
            frmEdit.ShowDialog();
        }

        private void ReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetPos setpos = new FormSetPos(myStory.LineNumber, Convert.ToInt64(tbNowIndex.Text));
            setpos.ShowDialog(this);
            if (setpos.DialogResult == DialogResult.OK)
            {
                index = setpos.newIndex;
                RefreshIndex();
            }
        }

        private void btnSetPos_Click(object sender, EventArgs e)
        {
            FormSetPos setpos = new FormSetPos(myStory.LineNumber, Convert.ToInt64(tbNowIndex.Text));
            setpos.ShowDialog(this);
            if (setpos.DialogResult == DialogResult.OK)
            {
                index = setpos.newIndex;
                RefreshIndex();
            }
        }

        private void OpenScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDefaultAVGS();
        }

        public Story2 GetOtherFile(string path)
        {
            Story2 story = new Story2();
            StreamReader SR = new StreamReader(path, Encoding.GetEncoding("GB2312"));
            string tempStr;
            int index = 1;
            while ((tempStr = SR.ReadLine()) != null)
            {
                Line newLine = new Line();
                newLine.index = index;
                newLine.text = tempStr;
                story.content.Add(newLine);
                index++;
            }
            story.LineNumber = index - 1;
            return story;
        }

        private void OpenFIletoolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenOtherFils();
        }

        private void CloseFiletoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CloseFile();
        }




    }
}