using PBL3.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.BLL
{
    internal class DoanhThuBLL
    {
        
        public object ShowHoaDon(DateTime ngaydau, DateTime ngaycuoi)
        {
            PBL3Entities db = new PBL3Entities();

            var l1 = from p in db.HOADON
                     where (p.Ngày_tạo >= ngaydau && p.Ngày_tạo <= ngaycuoi && p.Trạng_thái == true)
                     select new { p.Mã_hóa_đơn, p.Mã_tài_khoản, p.TAIKHOAN.THONGTINTAIKHOAN.Tên_nhân_viên, p.Tổng_tiền, p.Ngày_tạo };
            object hd = l1.ToList();
            return hd;
        }
        public object ShowAllHoaDon()
        {
            PBL3Entities db = new PBL3Entities();

            var l1 = from p in db.HOADON
                     where ( p.Trạng_thái == true)
                     select new { p.Mã_hóa_đơn, p.Mã_tài_khoản, p.TAIKHOAN.THONGTINTAIKHOAN.Tên_nhân_viên, p.Tổng_tiền, p.Ngày_tạo };
            object hd = l1.ToList();
            return hd;
        }


        public string LayTongTienCuaCacHoaDon(DateTime ngaydau, DateTime ngaycuoi)
        {
            double tongtien = 0;
            List<HOADON> tmp = new List<HOADON>();
            using (PBL3Entities db = new PBL3Entities())
            {
                tmp = db.HOADON.Where(p => p.Ngày_tạo >= ngaydau && p.Ngày_tạo <= ngaycuoi && p.Trạng_thái == true).Select(p => p).ToList();

            }

            foreach (HOADON i in tmp)
            {
                tongtien += i.Tổng_tiền;
            }
            QLOrderBLL bll = new QLOrderBLL();
            return bll.DinhDangTongTien(tongtien.ToString());
           
            
        }
    }
}
