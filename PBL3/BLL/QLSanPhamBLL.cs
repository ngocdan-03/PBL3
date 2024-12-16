using PBL3.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace PBL3.BLL
{
    public class QLSanPhamBLL
    {
    
        public List<SANPHAM> getallsanpham()
        {
            using (PBL3Entities db = new PBL3Entities())
            {
                return db.SANPHAM.Select(s => s).ToList();

            };

        }



        public List<SANPHAM> getsanphambyml(int ml)
        {
            using (PBL3Entities db = new PBL3Entities())
            {
                return db.SANPHAM.Where(p => p.Mã_loại == ml).Select(p => p).ToList();


            }
        }
        public List<SANPHAM> getsanphambymsp(int msp)
        {
            PBL3Entities db = new PBL3Entities();

            return db.SANPHAM.Where(p => p.Mã_sản_phẩm == msp).Select(p => p).ToList();

        }
        public LOAISANPHAM getlspbyml(int ml)
        {
            using (PBL3Entities db = new PBL3Entities())
            {

                return db.LOAISANPHAM.Find(ml);


            }
        }
        public List<LOAISANPHAM> getallloaisanpham()
        {
            List<LOAISANPHAM> lsp = new List<LOAISANPHAM>();
            using (PBL3Entities db = new PBL3Entities())
            {
                lsp = db.LOAISANPHAM.Select(p => p).ToList();

            }
            return lsp;
        }
      
        public void themsanpham(SANPHAM sp)
        {

        
               
                using (PBL3Entities db = new PBL3Entities())
                {                                 
                        db.SANPHAM.Add(sp);
                        db.SaveChanges();                 
                  
                }
                
               
            }
        public byte[] GetImageByID(int id)
        {
            using (PBL3Entities db = new PBL3Entities())
            {
                var tmp = db.SANPHAM.Find(id);
                return tmp.Hình_ảnh;
            }                
        }   
        public void capnhatsanpham(int msp,SANPHAM sp)
        {
            using (PBL3Entities db = new PBL3Entities())
            {
                var tmp = db.SANPHAM.Find(msp);
                tmp.Tên_sản_phẩm = sp.Tên_sản_phẩm;
                tmp.Mã_loại = sp.Mã_loại;
                tmp.Giá = sp.Giá;
                tmp.Hình_ảnh = sp.Hình_ảnh;
                db.SaveChanges();
            }
        }
        public List<CBBItems> SetCBBLoai()
        {
            List<CBBItems> cbb=new List<CBBItems> ();
            cbb.Add(new CBBItems
            {
                Value = 0,
                Text = "All"
            }) ;
            foreach (LOAISANPHAM i in getallloaisanpham())
            {
                cbb.Add(new CBBItems
                {
                    Value=i.Mã_loại,Text=i.Tên_loại
                });
            }
            return cbb;
        }
        public List<CBBItems> SetCBBLoaifdt()
        {
            List<CBBItems> cbb = new List<CBBItems>();
            foreach (LOAISANPHAM i in getallloaisanpham())
            {
                cbb.Add(new CBBItems
                {
                    Value = i.Mã_loại,
                    Text = i.Tên_loại
                });
            }
            return cbb;
        }
        public List<CBBItems> SetCBBSanPham(int ml)
        {
            List<CBBItems> cbb = new List<CBBItems>();
            foreach (SANPHAM i in getsanphambyml(ml))
            {
                cbb.Add(new CBBItems
                {
                    Value = i.Mã_sản_phẩm,
                    Text = i.Tên_sản_phẩm
                });
            }
            return cbb;
        }      
    }
}
