using PBL3.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.BLL
{
    internal class BieuDoBLL
    {
        public string TongDoanhThuTheoThang(int thang, int nam)
        {
            double tongtien = 0;
            PBL3Entities db = new PBL3Entities();
            foreach (HOADON i in db.HOADON)
            {
                if (i.Ngày_tạo.Month == thang && i.Ngày_tạo.Year == nam && i.Trạng_thái == true)
                {
                    tongtien += i.Tổng_tiền;
                }
            }

            return tongtien.ToString();

        }

        public string TongDoanhThuTra(int thang, int nam)
        {
            double tongtien = 0;
            PBL3Entities db = new PBL3Entities();
            foreach (CHITIETHOADON i in db.CHITIETHOADON)
            {
                if (i.SANPHAM.Mã_loại == 300 && i.HOADON.Ngày_tạo.Month == thang && i.HOADON.Ngày_tạo.Year == nam && i.HOADON.Trạng_thái == true)
                {
                    tongtien += i.Giá_tiền;
                }
            }

            return tongtien.ToString();
        }

        public string TongDoanhThuCaPhe(int thang, int nam)
        {
            double tongtien = 0;
            PBL3Entities db = new PBL3Entities();
            foreach (CHITIETHOADON i in db.CHITIETHOADON)
            {
                if (i.SANPHAM.Mã_loại == 301 && i.HOADON.Ngày_tạo.Month == thang && i.HOADON.Ngày_tạo.Year == nam && i.HOADON.Trạng_thái == true)
                {
                    tongtien += i.Giá_tiền;
                }
            }

            return tongtien.ToString();
        }

        public string TongDoanhThuTopping(int thang, int nam)
        {
            double tongtien = 0;
            PBL3Entities db = new PBL3Entities();
            foreach (CHITIETHOADON i in db.CHITIETHOADON)
            {
                if (i.SANPHAM.Mã_loại == 302 && i.HOADON.Ngày_tạo.Month == thang && i.HOADON.Ngày_tạo.Year == nam && i.HOADON.Trạng_thái == true)
                {
                    tongtien += i.Giá_tiền;
                }
            }

            return tongtien.ToString();
        }

        public List<string> DanhSachNam()
        {
            List<string> list = new List<string>();
            PBL3Entities db = new PBL3Entities();
            foreach (HOADON i in db.HOADON)
            {
                list.Add(i.Ngày_tạo.Year.ToString());
            }

            return list;
        }
    }
}
