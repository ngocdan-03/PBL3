using PBL3.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.BLL
{
    internal class ChiTietHoaDonBLL
    {
        public HOADON LayInfoDong(int IDHOADON)
        {
            HOADON hoadon = new HOADON();
            PBL3Entities db = new PBL3Entities();
            hoadon = db.HOADON.Find(IDHOADON);
            return hoadon;
        }



        public object LayInfoBang(int IDHOADON)
        {
            PBL3Entities db = new PBL3Entities();
            var l2 = from p in db.CHITIETHOADON
                     where (p.Mã_hóa_đơn == IDHOADON)
                     select new { p.SANPHAM.Tên_sản_phẩm, p.Số_lượng, p.Giá_tiền };

            object InfoBang = l2.ToList();
            return InfoBang;
        }
    }
}
