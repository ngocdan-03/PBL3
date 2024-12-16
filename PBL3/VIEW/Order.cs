using PBL3.BLL;
using PBL3.DAL;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PBL3.VIEW
{
    public partial class Order : Form
    {
        private int MNV;
        public delegate void MyDel(Form f);
        public MyDel d { get; set; }
        public Order(int mNV)
        {
            InitializeComponent();
            LoadTableList();
            MNV = mNV;
        }
        public void LoadTableList()
        {
            QLOrderBLL bll = new QLOrderBLL();
            foreach (BAN i in bll.GetAllTable())
            {
                Button btn;
                if (i.Trạng_thái_ghép == false)
                {
                     btn = new Button() { Width = 141, Height = 141, Text = i.Tên_bàn, Name = i.Tên_bàn };
                    if (i.BANAO.Trạng_thái == false)
                    {
                        btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
                    }
                    else
                    {
                        btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
                    }
                    btn.Image = Properties.Resources._6ef87ccbe70c51373066b27fa0476722_removebg_preview;
                    btn.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    
                }
                else
                {
                     btn = new Button() { Width = 141, Height = 141, Text = i.BANAO.Tên_bàn_ảo, Name = i.Tên_bàn };
                    btn.BackColor =System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
                    btn.Image = Properties.Resources._6ef87ccbe70c51373066b27fa0476722_removebg_preview;
                    btn.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
                }
                btn.Tag = i;
                btn.Click += button_Click;
                flowLayoutPanel1.Controls.Add(btn);
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            QLOrderBLL bll = new QLOrderBLL();
            Button b1 = sender as Button;
            BAN b = b1.Tag as BAN;
            BANAO ba = bll.GetBanAoByMaBanAo(b.BANAO.Mã_bàn_ảo);
            if (ba.Trạng_thái == true)
            {
                bll.AddHoaDon(MNV, ba.Mã_bàn_ảo);
                bll.UpDateStatusBanAo(ba.Mã_bàn_ảo, false);
            }
           
                d(new DetailOrder(bll.GetHoaDonByMaBanAo(ba.Mã_bàn_ảo)[0].Mã_hóa_đơn, MNV, ba.Mã_bàn_ảo,d));
            
        }
    }
}
