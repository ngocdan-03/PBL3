using PBL3.BLL;
using PBL3.DAL;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace PBL3.VIEW
{
    public partial class Quan_li_san_pham : Form
    {
        private bool PQ;
        public Quan_li_san_pham(bool PQ)
        {
            InitializeComponent();
            LoadGUI();
            LoadCBBLoai();
            this.PQ = PQ;
            if (PQ == false)
            {
                button2.Visible = false;           
                dataGridView1.CellDoubleClick -= dataGridView1_CellDoubleClick;
            }
        }
        public void LoadGUI()
        {
            QLSanPhamBLL data = new QLSanPhamBLL();
            dataGridView1.DataSource =data.getallsanpham();
            dataGridView1.Columns["CHITIETHOADON"].Visible= false;
            dataGridView1.Columns["LOAISANPHAM"].Visible = false;
            dataGridView1.Columns["Mã_loại"].Visible = false;
            dataGridView1.Columns["Hình_ảnh"].Visible = false;
            CultureInfo customCulture = new CultureInfo(CultureInfo.CurrentCulture.Name);
            customCulture.NumberFormat.NumberGroupSeparator = " ";        
            dataGridView1.Columns["Giá"].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns["Giá"].DefaultCellStyle.FormatProvider = customCulture;

        }

      public void LoadCBBLoai()
        {
            QLSanPhamBLL data = new QLSanPhamBLL();
            comboBox1.Items.AddRange(data.SetCBBLoai().ToArray());
        }

        public void LoadCBBSanPham(int ml)
        {
            QLSanPhamBLL data = new QLSanPhamBLL();
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(data.SetCBBSanPham(ml).ToArray());
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            QLSanPhamBLL data = new QLSanPhamBLL();
            int ml = ((CBBItems)comboBox1.SelectedItem).Value;
            if (ml == 0)
            {
                comboBox2.Items.Clear();
                comboBox2.Text = null;
                dataGridView1.DataSource = data.getallsanpham();
                
            }
            else
            {
                LoadCBBSanPham(ml);
                comboBox2.Text = null;
                dataGridView1.DataSource = data.getsanphambyml(ml);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            QLSanPhamBLL data = new QLSanPhamBLL();
            int msp = ((CBBItems)comboBox2.SelectedItem).Value;
            dataGridView1.DataSource = data.getsanphambymsp(msp);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int msp = 0;
            DetailFormSanPham f=new DetailFormSanPham(msp);
            f.d += new DetailFormSanPham.MyDel(LoadGUI);
            f.Show();
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int msp =Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            DetailFormSanPham f=new DetailFormSanPham(msp);
            f.d += new DetailFormSanPham.MyDel(LoadGUI);
            f.Show();
        }       
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {       if (dataGridView1.SelectedRows.Count == 1) {
                QLSanPhamBLL bll=new QLSanPhamBLL();
                int ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                MemoryStream ms =new MemoryStream(bll.GetImageByID(ID));
                pictureBox2.Image = Image.FromStream(ms);
            }
        }
    }
}
