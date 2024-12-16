using PBL3.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PBL3.BLL
{
    internal class QLOrderBLL
    {
        public bool CheckMHDCreateBill(int mhd)
        {
            foreach (HOADON i in GetAllHoaDon())
            {
                if (mhd == i.Mã_hóa_đơn)
                {
                    MessageBox.Show("Mã sản phẩm đã tồn tại!!!");
                    return false;
                }
            }
            return true;

        }
        public bool CheckMNVCreateBill(int mnv)
        {
            QLTaiKhoanBLL bll = new QLTaiKhoanBLL();
            foreach(THONGTINTAIKHOAN i in bll.GetAllTaiKhoan())
            {
                if (mnv == i.Mã_nhân_viên)
                {
                    return true;
                }
            }
            MessageBox.Show("Mã nhân viên không tồn tại!!!");
            return false;

        }
        public List<HOADON> GetAllHoaDon()
        {
            List<HOADON> hd = new List<HOADON>();
            using (PBL3Entities db = new PBL3Entities())
            {

                hd = db.HOADON.Select(p => p).ToList();

                return hd;
            }
        }
        public List<BAN> GetAllTable()
        {
            List<BAN> tb = new List<BAN>();
            PBL3Entities db = new PBL3Entities();

            tb = db.BAN.Select(p => p).ToList();

            return tb;
        }
        public List<CBBItems> SetCBBSanPham(int ml)
        {         
            List<CBBItems> sp = new List<CBBItems>();
            foreach (SANPHAM i in GetSanPhamByMaLoai(ml))
            {
                sp.Add(new CBBItems
                {
                    Value = i.Mã_sản_phẩm,
                    Text = i.Tên_sản_phẩm
                });
            }
            return sp;
        }
        public List<CBBItems> SetCBBLoai()
        {
          
            List<CBBItems> l = new List<CBBItems>();
            foreach (LOAISANPHAM i in GetAllLoaiSanPham())
            {
                l.Add(new CBBItems
                {
                    Value = i.Mã_loại,
                    Text = i.Tên_loại
                });
            }
            return l;
        }
        public List<CBBItems> SetCBBChangeTable()
        {
            List<CBBItems> cbb=new List<CBBItems> ();
         
                foreach(BAN i in GetAllTable())
                {
                if (i.BANAO.Trạng_thái == true)
                {
                    cbb.Add(new CBBItems
                    {
                        Value = Convert.ToInt32(i.Mã_bàn),
                        Text = i.Tên_bàn
                    });
                }         
            }
            return cbb;
        }
        public List<CBBItems> SetCBBMergeTable(string mba)
        {
            List<CBBItems> cbb = new List<CBBItems>();

            foreach (BAN i in GetAllTable())
            {
              if(mba != i.BANAO.Mã_bàn_ảo && i.Trạng_thái_ghép==false)
                    cbb.Add(new CBBItems
                    {
                        Value = Convert.ToInt32(i.Mã_bàn),
                        Text = i.Tên_bàn
                    });
                
            }
            return cbb;
        }
        public List<CBBItems> SetCBBSplitTable(string mba)
        {
            List<CBBItems> cbb = new List<CBBItems>();

            foreach (BAN i in GetAllTable())
            {
                if (i.BANAO.Trạng_thái==true)
                    cbb.Add(new CBBItems
                    {
                        Value = Convert.ToInt32(i.Mã_bàn),
                        Text = i.Tên_bàn
                    });

            }
            return cbb;
        }

       
        public void AddHoaDon( int mnv, string mba)
        {
            using (PBL3Entities db = new PBL3Entities())
            {
                db.HOADON.Add(new HOADON
                {
                    Mã_tài_khoản = mnv,
                    Mã_bàn_ảo = mba,
                    Ngày_tạo = DateTime.Now,
                    Trạng_thái = false,
                    Tổng_tiền = 0,
                });

                db.SaveChanges();
            }
        }
        public void UpDateStatusBanAo(string mba, bool tt)
        {
            using (PBL3Entities db = new PBL3Entities())
            {
                var s = db.BANAO.Find(mba);
                s.Trạng_thái = tt;
                db.SaveChanges();
            }
        }
        
        public List<ShowHoaDon> ShowHoaDonByMaHoaDon(int mhd)
        {
            PBL3Entities db = new PBL3Entities();
            List<ShowHoaDon> HDs = new List<ShowHoaDon>();
            HDs = db.CHITIETHOADON.Where(p => p.Mã_hóa_đơn == mhd && p.HOADON.Trạng_thái == false).Select(p => new ShowHoaDon
            {
                Mã_sản_phẩm= p.SANPHAM.Mã_sản_phẩm,
              Tên_loại = p.SANPHAM.LOAISANPHAM.Tên_loại,
                Tên_sản_phẩm = p.SANPHAM.Tên_sản_phẩm,
                Số_lượng = p.Số_lượng,
                Giá_tiền = p.Giá_tiền
            }).ToList();

            return HDs;
        }
        public List<HOADON> GetHoaDonByMaBanAo(string mba)
        {
            List<HOADON> tmp = new List<HOADON>();
            using (PBL3Entities db = new PBL3Entities())
            {
                tmp = db.HOADON.Where(p => p.BANAO.Mã_bàn_ảo == mba && p.Trạng_thái == false).Select(p => p).ToList();
            }
            return tmp;
        }
        public void ShowChiTietHoaDon(int  mhd,int  msp,int SL, bool MergeStatus)
        {
         
            if (GetChiTietHoaDonByMaHoaDonAndMaSanPham(mhd, msp).Count == 0)
            {
                AddChiTietHoaDon(msp, mhd, SL);
            }
            else
            {
                UpDateSoLuongAndGiaChiTietHoaDon(msp, mhd, SL, MergeStatus);
            }
            
        }
        public SANPHAM GetSanPhamByMaSanPham(int sp)
        {
            SANPHAM tmp = new SANPHAM();
            using (PBL3Entities db = new PBL3Entities())
            {
                tmp = db.SANPHAM.Find(sp);
            }
            return tmp;
        }
        public List<LOAISANPHAM> GetAllLoaiSanPham()
        {
            List<LOAISANPHAM> tmp = new List<LOAISANPHAM>();
            using (PBL3Entities db = new PBL3Entities())
            {
                tmp = db.LOAISANPHAM.Select(p => p).ToList();
            }
            return tmp;
        }
        public List<SANPHAM> GetSanPhamByMaLoai(int ml)
        {
            List<SANPHAM> tmp = new List<SANPHAM>();
            using (PBL3Entities db = new PBL3Entities())
            {
                tmp = db.SANPHAM.Where(p => p.Mã_loại == ml).Select(p => p).ToList();
            }
            return tmp;
        }
        public void AddChiTietHoaDon(int msp, int mhd, int SL)
        {
            using (PBL3Entities db = new PBL3Entities())
            {
                var tmp = db.SANPHAM.Find(msp);
                db.CHITIETHOADON.Add(new CHITIETHOADON
                {
                    Mã_sản_phẩm = msp,
                    Mã_hóa_đơn = mhd,
                    Số_lượng = SL,
                    Giá_tiền = tmp.Giá*SL
                }) ;

                db.SaveChanges();
            }

        }
        public void UpDateSoLuongAndGiaChiTietHoaDon(int msp, int mhd, int SL,bool MergeStatus)
        {
            using (PBL3Entities db = new PBL3Entities())
            {
                var temp = db.CHITIETHOADON.Find(msp, mhd);
                if (MergeStatus == false) temp.Số_lượng = SL;
                else temp.Số_lượng += SL;
                temp.Giá_tiền = temp.Số_lượng * temp.SANPHAM.Giá;
                db.SaveChanges();
            }
        }

        public void DeleteChiTietHoaDonByMaHoaDonAndMaSanPham(int  mhd, int msp)
        {
            using (PBL3Entities db = new PBL3Entities())
            {
                var s = db.CHITIETHOADON.Find(msp, mhd);
                db.CHITIETHOADON.Remove(s);
                db.SaveChanges();
            }

        }
        public void DeleteChiTietHoaDon(int mhd)
        {
            using (PBL3Entities db = new PBL3Entities())
            {
                var tmp = db.CHITIETHOADON.Where(p => p.Mã_hóa_đơn == mhd).ToList();
                db.CHITIETHOADON.RemoveRange(tmp);
                db.SaveChanges();
            }
        }
        public void DeleteHoaDon(int mhd)
        {
            using (PBL3Entities db = new PBL3Entities())
            {
                var tmp = db.HOADON.Where(p => p.Mã_hóa_đơn == mhd).FirstOrDefault();
                if(tmp != null)
                db.HOADON.Remove(tmp);
                db.SaveChanges();
            }
        }
        public BAN GetBanByMaBan(string mb)
        {
            BAN tmp = new BAN();
            PBL3Entities db = new PBL3Entities();

            tmp = db.BAN.Find(mb);

            return tmp;
        }
        public string Total(int mhd)
        {
            string tt;
            using (PBL3Entities db = new PBL3Entities())
            {
                var hd = db.HOADON.Find(mhd);
                if (db.CHITIETHOADON.Where(p => p.Mã_hóa_đơn == mhd).Count() == 0)
                {
                    tt = "0";
                    hd.Tổng_tiền = 0;
                }
                else
                {
                    tt = db.CHITIETHOADON                
                         .Where(p => p.Mã_hóa_đơn == mhd)
                         .Select(p => new { p.Giá_tiền })
                         .Sum(p => p.Giá_tiền)
                         .ToString();
                    hd.Tổng_tiền = Convert.ToDouble(tt);
                }
                db.SaveChanges();
            }
            return DinhDangTongTien(tt);
        }
        public string DinhDangTongTien(string tt)
        {
            StringBuilder stringBuilder = new StringBuilder(tt);

            if (stringBuilder.Length > 3)
            {
                int i = 1;
                while (stringBuilder.Length > (3 * i + i - 1))
                {
                    stringBuilder.Insert(stringBuilder.Length - (3 * i + i - 1), " ");
                    i++;
                }
                return stringBuilder.ToString() + " VNĐ";
            }
            else
            {
                return tt + " VNĐ";
            }
        }
        public void CanCellOrder(int mhd,string mba)
        {
            DeleteChiTietHoaDon(mhd);
            DeleteHoaDon(mhd);
            UpDateStatusBanAo(mba,true);
            UpdateMergeStatusAndMaBanAoIntoBan(mba);
        }
        public void UpdateMergeStatusAndMaBanAoIntoBan(string mba)
        {
            using (PBL3Entities db = new PBL3Entities())
            {
                List<BAN> b = db.BAN.Where(p => p.Mã_bàn_ảo == mba).ToList();
                if (b.Count > 1)
                {
                    foreach (BAN i in b)
                    {
                        i.Trạng_thái_ghép = false;
                        i.Mã_bàn_ảo = i.Mã_bàn_ảo_chính;
                    }
                    db.SaveChanges();
                }
            }
            
        }
        public BANAO GetBanAoByMaBanAo(string mba)
        {
            BANAO tmp = new BANAO();
            using (PBL3Entities db = new PBL3Entities())
            {
                tmp = db.BANAO.Find(mba);
            }
            return tmp;
        }
        public void UpDateStatusHoaDon(int  mhd, bool tt)
        {
            using (PBL3Entities db = new PBL3Entities())
            {
                var s = db.HOADON.Find(mhd);
                s.Trạng_thái = tt;
                db.SaveChanges();
            }
        }
        public void UpdateMaBanAoAndStatusIntoBan(string mb,string mba)
        {          
            using(PBL3Entities db=new PBL3Entities())
            {
                var b1 = db.BAN.Find(mb);
                var b2 = db.BAN.Where(p => p.Mã_bàn_ảo == mba).FirstOrDefault();
                var ba=db.BANAO.Where(p=>p.Mã_bàn_ảo == b1.Mã_bàn_ảo).FirstOrDefault();
                b1.Mã_bàn_ảo = mba;
                b1.Trạng_thái_ghép = true;
                b2.Trạng_thái_ghép = true;
                ba.Trạng_thái = true;
                db.SaveChanges();
            }
        }
        public int GetMaHoaDonByBan(string mb)
        {
            using(PBL3Entities db = new PBL3Entities())
            {
                var b= db.BAN.Find(mb);

                var hd = db.HOADON.Where(p => p.Mã_bàn_ảo == b.Mã_bàn_ảo).FirstOrDefault();
                if( hd != null )
                return hd.Mã_hóa_đơn;
                return 0;
            }
        }
        public List<CHITIETHOADON> GetChiTietHoaDonByMaHoaDonAndMaSanPham(int hd, int msp)
        {
            List<CHITIETHOADON> tmp = new List<CHITIETHOADON>();
            using (PBL3Entities db = new PBL3Entities())
            {
                tmp = db.CHITIETHOADON.Where(p => p.Mã_hóa_đơn == hd && p.Mã_sản_phẩm == msp).Select(p => p).ToList();
            }
            return tmp;
        }
        public void ChangeTable(int  mhd,string  mba,string mb)
        {
            BAN b = GetBanByMaBan(mb);        
            using (PBL3Entities db = new PBL3Entities())
            {
                HOADON hd = db.HOADON.Find(mhd);
                BANAO banao = db.BANAO.Find(mba);
                BANAO ba = db.BANAO.Find(b.Mã_bàn_ảo);
                hd.Mã_bàn_ảo = b.Mã_bàn_ảo;
                ba.Trạng_thái = false;
                banao.Trạng_thái = true;
                db.SaveChanges();
            }
        }
       

    }
}
