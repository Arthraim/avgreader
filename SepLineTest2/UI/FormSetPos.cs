using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SepLineTest2.UI
{
    public partial class FormSetPos : Form
    {
        private long _newIndex = 1;
        public long newIndex
        {
            get { return _newIndex; }
            set { _newIndex = value; }
        }

        private long newLineNumber = 1;

//         public FormSetPos(long LineNumber)
//         {
//             InitializeComponent();
// 
//             newIndex = 1;
//             LineNumber = 1;
//             tbPosition.Text = newIndex.ToString();
//         }

        public FormSetPos(long LineNumber, long index)
        {
            InitializeComponent();

            newIndex = index;
            newLineNumber = LineNumber;
            tbPosition.Text = newIndex.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //��ֹ�հ�
            if (tbPosition.Text == "")
            {
                MessageBox.Show("�к��ı����ڲ���Ϊ�գ�\n������Ҫ��λ���кţ�", "ע�⣡", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //��ֹ�����쳣�ַ�
            try
            {
                newIndex = Convert.ToInt64(tbPosition.Text);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + "\n��ȷ�����Ѿ����룬\n��������������֡�", "ע�⣡", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //��ֹ�������ֲ��ڽű���Χ��
            if (newIndex > newLineNumber || newIndex < 1)
            {
                MessageBox.Show("�������ַǷ���\n��ȷ���������������1���ű���󳤶�֮�䣡", "ע�⣡", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //����
            this.DialogResult = DialogResult.OK;

        }

    }
}