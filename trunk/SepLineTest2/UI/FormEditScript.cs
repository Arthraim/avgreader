using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SepLineTest2.UI
{
    public partial class FormEditScript : Form
    {
        private string currentDir = AppDomain.CurrentDomain.BaseDirectory;
        public FormEditScript()
        {
            InitializeComponent();
            btnHelp.Text = "�﷨����";

            string FileName = currentDir + @"DATA\AVGS\story.avgs";
            richTextBox1.LoadFile(FileName, RichTextBoxStreamType.PlainText);
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            if (btnHelp.Text == "�﷨����")
            {
                btnHelp.Text = "���ر༭";
                richTextBox1.Clear();
                string FileName = currentDir + @"DATA\AVGS\rules.txt";
                richTextBox1.LoadFile(FileName, RichTextBoxStreamType.PlainText);
            }
            else if (btnHelp.Text == "���ر༭")
            {
                btnHelp.Text = "�﷨����";
                richTextBox1.Clear();
                string FileName = currentDir + @"DATA\AVGS\story.avgs";
                richTextBox1.LoadFile(FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "ȷ�������޸ģ����ز��Խ�����", "warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            string FileName = currentDir + @"DATA\AVGS\story.avgs";
            try
            {
                richTextBox1.SaveFile(FileName, RichTextBoxStreamType.PlainText);
                MessageBox.Show("����ɹ���");
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("����ʧ�ܣ�\n" + ex.Message);
            }

        }

    }
}