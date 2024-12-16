using PBL3.BLL;
using PBL3.DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PBL3.VIEW
{
    public partial class DetailFormTaiKhoan : Form
    {
        private int MSNV;
       
        public delegate void MyDel();
        public MyDel d;
        public DetailFormTaiKhoan(int MSNV)
        {
            InitializeComponent();
            this.MSNV = MSNV;
          
            if (MSNV != 0)
            {
                SETDetailForm(MSNV);
             
                
            }
        }
        public void SETDetailForm(int id)
        {
            QLTaiKhoanBLL bll=new QLTaiKhoanBLL();
            QLLoginBLL data=new QLLoginBLL();
            THONGTINTAIKHOAN tttk=bll.GetThongTinTaiKHoanByID(id);
            TAIKHOAN tk = bll.GetTaiKHoanByID(id);         
            textBox2.Text= tttk.Tên_nhân_viên;
            textBox3.Text = tttk.CMND;
            textBox4.Text = tttk.SĐT;
            textBox5.Text = tttk.Địa_chỉ;
            textBox6.Text = tk.Tên_tài_khoản;
            tbmk.Text = data.GiaiMa(tk.Mật_khẩu).ToString();
            if (tk.PQ == true)
            {
                comboBox1.Text = "Quản lý";
            }
            else comboBox1.Text = "Nhân viên";
            MemoryStream ms = new MemoryStream(bll.GetImageByID(id));
            pictureBox1.Image = Image.FromStream(ms);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(tbmk.Text) || string.IsNullOrEmpty(comboBox1.Text) || pictureBox1.Image == null) 
                MessageBox.Show("vui long nhap du thong tin");
            else
            {
                QLTaiKhoanBLL data = new QLTaiKhoanBLL();
                QLLoginBLL bll = new QLLoginBLL();
                QLhomeBLL qlhome = new QLhomeBLL();
                THONGTINTAIKHOAN tttk = new THONGTINTAIKHOAN();
                TAIKHOAN tk = new TAIKHOAN();
                tttk.Tên_nhân_viên = textBox2.Text;
                tttk.CMND = textBox3.Text;
                tttk.SĐT = textBox4.Text;
                tttk.Địa_chỉ = textBox5.Text;               
                tttk.Hình_ảnh = qlhome.ImageToByteArray(pictureBox1.Image);
                tk.Tên_tài_khoản = textBox6.Text;
                tk.Mật_khẩu = bll.MaHoa(tbmk.Text).ToString();
                if(comboBox1.Text == "Nhân viên") 
                tk.PQ = false;
                else tk.PQ = true;
                tk.Khóa = false;
                if (MSNV == 0)
                {
                    data.ThemTaiKhoan(tttk, tk);

                }
                else
                {
                    data.UpdateTaiKhoan(MSNV, tttk, tk);

                }
                d();
                this.Close();
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bttrue_Click(object sender, EventArgs e)
        {
            bttrue.Visible = false;
            btfalse.Visible = true;
            tbmk.PasswordChar = '*';
        }

        private void btfalse_Click(object sender, EventArgs e)
        {
            btfalse.Visible = false;
            bttrue.Visible = true;
            tbmk.PasswordChar = '\0';
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog image = new OpenFileDialog();
            if (image.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(image.FileName);
            }
        }
       

      
    }
}
