using PBL3.DAL;
using PBL3.BLL;
using PBL3.VIEW;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace PBL3.VIEW
{
    public partial class DoanhThu : Form
    {
        public DoanhThu()
        {
            InitializeComponent();
            ShowDGV();
        }
        public void ShowDGV()
        {
            DoanhThuBLL bll=new DoanhThuBLL();
            dataGridView1.DataSource = bll.ShowAllHoaDon();
            CultureInfo customCulture = new CultureInfo(CultureInfo.CurrentCulture.Name);
            customCulture.NumberFormat.NumberGroupSeparator = " ";
            dataGridView1.Columns["Tổng_tiền"].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns["Tổng_tiền"].DefaultCellStyle.FormatProvider = customCulture;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Hien thi cac hoa don len datagridview
            DoanhThuBLL bll = new DoanhThuBLL();
            dataGridView1.DataSource = bll.ShowHoaDon(dateTimePicker1.Value, dateTimePicker2.Value);

            //Tinh tong tien cua cac hoa don
            textBox1.Text = bll.LayTongTienCuaCacHoaDon(dateTimePicker1.Value, dateTimePicker2.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                ChiTietHoaDon f = new ChiTietHoaDon( Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Mã_hóa_đơn"].Value));

                f.Show();
            }
        }

      
    }
}
