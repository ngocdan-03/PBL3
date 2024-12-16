using PBL3.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PBL3.VIEW
{
    public partial class ChiTietHoaDon : Form
    {
        public ChiTietHoaDon(int idhoadon)
        {
            InitializeComponent();
            setValue(idhoadon);
        }

        public void setValue(int IDHOADON)
        {
            ChiTietHoaDonBLL bll = new ChiTietHoaDonBLL();
            QLOrderBLL data = new QLOrderBLL();
            textBox1.Text = bll.LayInfoDong(IDHOADON).Mã_hóa_đơn.ToString();
            textBox2.Text = bll.LayInfoDong(IDHOADON).Mã_tài_khoản.ToString();
            textBox3.Text = bll.LayInfoDong(IDHOADON).TAIKHOAN.THONGTINTAIKHOAN.Tên_nhân_viên;
            textBox4.Text = bll.LayInfoDong(IDHOADON).Ngày_tạo.ToString();
            textBox6.Text = data.DinhDangTongTien(bll.LayInfoDong(IDHOADON).Tổng_tiền.ToString());                    
            dataGridView1.DataSource = bll.LayInfoBang(IDHOADON);
            CultureInfo customCulture = new CultureInfo(CultureInfo.CurrentCulture.Name);
            customCulture.NumberFormat.NumberGroupSeparator = " ";
            dataGridView1.Columns["Giá_tiền"].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns["Giá_tiền"].DefaultCellStyle.FormatProvider = customCulture;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        
    }
}
