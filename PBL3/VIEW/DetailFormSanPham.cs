using PBL3.BLL;
using PBL3.DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PBL3.VIEW
{
    public partial class DetailFormSanPham : Form
    {
        private int msp ;
        public delegate void MyDel();
        public MyDel d;
        public DetailFormSanPham(int msp)
        {
            InitializeComponent();
            this.msp = msp;
            
            LoadCBBLoai();
            
            if (msp != 0)
            {
                LoadDetailForm(msp);
            }
                
        }
        
        public void LoadDetailForm(int msp)
        {
            QLSanPhamBLL data = new QLSanPhamBLL();
            List<SANPHAM> sp= data.getsanphambymsp(msp);
            textBox2.Text = sp[0].Tên_sản_phẩm;
            textBox3.Text = sp[0].Mã_loại.ToString();
            textBox4.Text = sp[0].Giá.ToString();
            comboBox1.Text = sp[0].LOAISANPHAM.Tên_loại;
            MemoryStream ms = new MemoryStream(data.GetImageByID(msp));
            pictureBox2.Image = Image.FromStream(ms);
        }
        public void LoadCBBLoai()
        {
            QLSanPhamBLL data = new QLSanPhamBLL();
            comboBox1.Items.AddRange(data.SetCBBLoaifdt().ToArray());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox3.Text = ((CBBItems)comboBox1.SelectedItem).Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QLSanPhamBLL data = new QLSanPhamBLL();
            QLhomeBLL bll=new QLhomeBLL();
            if ( string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text)||pictureBox2.Image == null)
            {
                MessageBox.Show("vui long nhap du thong tin");
            }
            else
            {

                SANPHAM sp = new SANPHAM
                {

                    Tên_sản_phẩm = textBox2.Text,
                    Mã_loại = Convert.ToInt32(textBox3.Text),
                    Giá = Convert.ToDouble(textBox4.Text),
                    Hình_ảnh = bll.ImageToByteArray(pictureBox2.Image),
                    };

                    if (msp == 0)
                    {
                        data.themsanpham(sp);
                    }
                    else
                    {
                        data.capnhatsanpham(msp,sp);

                    }
                    d();
                    this.Close();
                }

            }
        

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog image = new OpenFileDialog();
            if(image.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = Image.FromFile(image.FileName);
            }
        }
        
        
    }
}
