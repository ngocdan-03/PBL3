using PBL3.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL3.VIEW
{
    public partial class ChangePass : Form
    {
        private int MSNV;
        public ChangePass(int MSNV)
        {
            InitializeComponent();
            this.MSNV = MSNV;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text)) MessageBox.Show("vui long nhap du thong tin");
            else
            {
                QLhomeBLL bll = new QLhomeBLL();
                if (bll.CheckPass(MSNV, textBox1.Text, textBox2.Text, textBox3.Text))
                {
                    bll.UpdatePass(MSNV, textBox2.Text);
                    MessageBox.Show("Đổi mật khẩu thành công!!!");
                    this.Close();
                }
            }
                     
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
