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

namespace PBL3.VIEW
{
    public partial class ThongKe : Form
    {
        public ThongKe()
        {
            InitializeComponent();
        }

        private void fillChart(int num)
        {
            BieuDoBLL bll = new BieuDoBLL();

            if (num == 1)
            {
                for (int i = 1; i <= 12; i++)
                {
                    chart1.Series["Hóa đơn"].Points.AddXY(i.ToString(), bll.TongDoanhThuTheoThang(i, Convert.ToInt32(comboBox1.SelectedItem)));
                }
            }

            if (num == 2)
            {
                for (int i = 1; i <= 12; i++)
                {
                    chart1.Series["Trà"].Points.AddXY(i.ToString(), bll.TongDoanhThuTra(i, Convert.ToInt32(comboBox1.SelectedItem)));
                }
            }

            if (num == 3)
            {
                for (int i = 1; i <= 12; i++)
                {
                    chart1.Series["Cà phê"].Points.AddXY(i.ToString(), bll.TongDoanhThuCaPhe(i, Convert.ToInt32(comboBox1.SelectedItem)));
                }
            }

            if (num == 4)
            {
                for (int i = 1; i <= 12; i++)
                {
                    chart1.Series["Topping"].Points.AddXY(i.ToString(), bll.TongDoanhThuTopping(i, Convert.ToInt32(comboBox1.SelectedItem)));
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }

            if (checkBox2.Checked == true)
            {
                fillChart(1);
                chart1.Series["Hóa đơn"].IsVisibleInLegend = true;
            }
            if (checkBox2.Checked == false)
            {
                chart1.Series["Hóa đơn"].IsVisibleInLegend = false;
            }

            if (checkBox3.Checked == true)
            {
                fillChart(2);
                chart1.Series["Trà"].IsVisibleInLegend = true;
            }
            if (checkBox3.Checked == false)
            {
                chart1.Series["Trà"].IsVisibleInLegend = false;
            }

            if (checkBox4.Checked == true)
            {
                fillChart(3);
                chart1.Series["Cà phê"].IsVisibleInLegend = true;
            }
            if (checkBox4.Checked == false)
            {
                chart1.Series["Cà phê"].IsVisibleInLegend = false;
            }

            if (checkBox5.Checked == true)
            {
                fillChart(4);
                chart1.Series["Topping"].IsVisibleInLegend = true;
            }
            if (checkBox5.Checked == false)
            {
                chart1.Series["Topping"].IsVisibleInLegend = false;
            }

        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            BieuDoBLL bll = new BieuDoBLL();
            //List<string> list = new List<string>();
            //list = bll.DanhSachNam();
            foreach (string s in bll.DanhSachNam())
            {
                if (comboBox1.Items.Contains(s) == false)
                    comboBox1.Items.Add(s);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Checked = true;
                checkBox3.Checked = true;
                checkBox4.Checked = true;
                checkBox5.Checked = true;
            }
            else if (checkBox1.Checked == false)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
            }
        }

     
    }
}
