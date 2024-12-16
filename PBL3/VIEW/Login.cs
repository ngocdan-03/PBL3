using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PBL3.BLL;
using PBL3.DAL;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace PBL3
{
    public partial class Login : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        public Login()
        {
            InitializeComponent();
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(WindowState == FormWindowState.Normal)
            {
                this.WindowState= FormWindowState.Maximized;
                
            }else this.WindowState = FormWindowState.Normal;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QLTaiKhoanBLL   bll = new QLTaiKhoanBLL();
            QLLoginBLL baomat = new QLLoginBLL();
              bll.CheckTaiKhoanAndShowMainForm(tbtk.Text, baomat.MaHoa(tbmk.Text).ToString());
           
        }
            
        

        private void btfalse_Click(object sender, EventArgs e)
        {
            btfalse.Visible = false;
            bttrue.Visible = true;
            tbmk.PasswordChar = '\0';
        }

        private void bttrue_Click(object sender, EventArgs e)
        {
            bttrue.Visible = false;
            btfalse.Visible = true;
            tbmk.PasswordChar = '*';
        }
    }
}
