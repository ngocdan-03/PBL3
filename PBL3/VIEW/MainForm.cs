using PBL3.BLL;
using PBL3.DAL;
using PBL3.VIEW;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PBL3
{
    public partial class MainForm : Form
    {     
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        private Button button;
        private Form ChildForm;
        private int MSNV;
        private bool PQ;
        private void OpenChildForm(Form f)
        {
            if (ChildForm != null)
            {
                   ChildForm.Close();
            }
            ChildForm= f;
            ChildForm.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock=DockStyle.Fill;
            panel4.Controls.Add(f);
            f.BringToFront();
            f.Show();
        }
      
        public MainForm(int id,bool PQ)
        {
            InitializeComponent();
          this.WindowState = FormWindowState.Maximized;
            MSNV = id;
            this.PQ = PQ;
           
        }
    

        private void DisableButton() 
        {
            this.button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.button.ForeColor = System.Drawing.Color.Gainsboro;
            this.button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
        }
        private void EnableButton(Button b)
        {
            if (button!=null)
            {
                DisableButton();
            }
            button = b;
            this.button.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EnableButton(button1);
            foreach (Control i in panel4.Controls) {
                if (i is Form)
                    i.Visible=false;
            }
            QLhomeBLL bll=new QLhomeBLL();
            QLTaiKhoanBLL data = new QLTaiKhoanBLL();
            THONGTINTAIKHOAN SetInfo = bll.GetInforByID(MSNV);
            textBox1.Text = SetInfo.Tên_nhân_viên;
            textBox2.Text = SetInfo.CMND;
            textBox3.Text = SetInfo.SĐT;
            textBox4.Text = SetInfo.Địa_chỉ;
            textBox5.Text = SetInfo.TAIKHOAN.Tên_tài_khoản;
            MemoryStream ms = new MemoryStream(data.GetImageByID(MSNV));
            pictureBox2.Image = Image.FromStream(ms);
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
         
            button1.PerformClick();          
            if (PQ == false)
            {
                button11.Visible = false;
                button8.Visible = false;
            }
        }
        
        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;

            }
            else this.WindowState = FormWindowState.Normal;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
            Application.Exit();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            EnableButton(button2);
            OpenChildForm(new Quan_li_san_pham(PQ));
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            EnableButton(button3);
            OpenChildForm(new Quan_li_tai_khoan(PQ));
           
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            EnableButton(button4);   
            Order f=new Order(MSNV);
            f.d += new Order.MyDel(OpenChildForm);
            OpenChildForm(f);
           
        }

        private void button8_Click(object sender, EventArgs e)
        {
            EnableButton(button8);
            OpenChildForm(new DoanhThu());
        }

        

        private void button10_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text)) 
                MessageBox.Show("vui long nhap day du thon tin");
            else 
            {
                QLhomeBLL bll = new QLhomeBLL();
                bll.UpdateInfo(MSNV, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text,bll.ImageToByteArray(pictureBox2.Image));
            }
        
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ChangePass f=new ChangePass(MSNV);
            f.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            EnableButton(button11);
            OpenChildForm(new ThongKe());
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Close();
            Login f = new Login();
            f.Show();
        }

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog open =new OpenFileDialog();
            if(open.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = Image.FromFile(open.FileName);
            }
        }

       
    }
}
