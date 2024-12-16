using PBL3.BLL;
using PBL3.DAL;
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
    public partial class SplitTable : Form
    {
        private string mba;
        private int mhd;
        private int MNV;
        private Order.MyDel d1;
        public delegate void MyDel();
        public MyDel d;
        public SplitTable(string mba, int mhd,int MNV ,Order.MyDel d1)
        {
            InitializeComponent();
            this.mhd = mhd;
            this.mba = mba;
            this.MNV = MNV;
            LoadCBBTable();
            LoadDGV();
            this.d1 = d1;
        }
        public void LoadCBBTable()
        {
            QLOrderBLL bll = new QLOrderBLL();
            comboBox1.Items.AddRange(bll.SetCBBSplitTable(mba).ToArray());
        }

        public void LoadDGV()
        {
            QLOrderBLL bll = new QLOrderBLL();
            dataGridView1.DataSource = bll.ShowHoaDonByMaHoaDon(mhd);
            CultureInfo customCulture = new CultureInfo(CultureInfo.CurrentCulture.Name);
            customCulture.NumberFormat.NumberGroupSeparator = " ";
            dataGridView1.Columns["Giá_tiền"].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns["Giá_tiền"].DefaultCellStyle.FormatProvider = customCulture;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
       
                QLOrderBLL bll = new QLOrderBLL();
                string mba = bll.GetBanByMaBan(((CBBItems)comboBox1.SelectedItem).Value.ToString()).Mã_bàn_ảo;
                bll.AddHoaDon(MNV, mba);
                bll.UpDateStatusBanAo(mba, false);
                foreach (DataGridViewRow i in dataGridView1.SelectedRows)
                    {
                    bll.DeleteChiTietHoaDonByMaHoaDonAndMaSanPham(mhd, Convert.ToInt32(i.Cells["Mã_sản_phẩm"].Value));
                    bll.AddChiTietHoaDon(Convert.ToInt32(i.Cells[0].Value), bll.GetHoaDonByMaBanAo(mba)[0].Mã_hóa_đơn, Convert.ToInt32(i.Cells[3].Value));
                    }
                d1(new DetailOrder(bll.GetHoaDonByMaBanAo(mba)[0].Mã_hóa_đơn, MNV, mba, d1));
            }
                
            }
        }
    }

