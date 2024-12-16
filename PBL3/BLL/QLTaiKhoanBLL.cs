using PBL3.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PBL3.BLL
{
    internal class QLTaiKhoanBLL
    {
     
        public void CheckTaiKhoanAndShowMainForm(string tk, string mk)
        {
            PBL3Entities db = new PBL3Entities();
            if (string.IsNullOrEmpty(tk) || string.IsNullOrEmpty(mk))
            {
                MessageBox.Show("vui long nhap day du thong tin");
            }
            else
            {
                foreach (TAIKHOAN i in db.TAIKHOAN)
                {
                    if (i.Tên_tài_khoản == tk && i.Mật_khẩu == mk)
                    {
                        if (i.Khóa == false)
                        {
                            MessageBox.Show("Đăng nhập thành công!!!");
                            MainForm f = new MainForm(i.Mã_tài_khoản, Convert.ToBoolean(i.PQ));
                            f.Show();
                        }
                        else MessageBox.Show("Tài khoản của bạn đã bị khóa !!!");
                        return;

                    }

                }
                        MessageBox.Show("Tài khoản hoặc mật khẩu không tồn tại!!!");
                        return;
                    
                
            }
        }                                 
        public List<THONGTINTAIKHOAN> GetAllTaiKhoan()
        {
            List<THONGTINTAIKHOAN> tttk = new List<THONGTINTAIKHOAN>();
            using (PBL3Entities db = new PBL3Entities())
            {
                tttk = db.THONGTINTAIKHOAN.Select(p => p).ToList();
            }
            return tttk;
        }
        public object ShowAllTaiKhoan()
        {
            object tttk = new object();
            using (PBL3Entities db = new PBL3Entities())
            {
                tttk = db.THONGTINTAIKHOAN.Where(p=>p.TAIKHOAN.PQ == false).Select(p => new {p.Mã_nhân_viên , p.Tên_nhân_viên ,p.CMND , p.SĐT ,p.Địa_chỉ , p.TAIKHOAN.Khóa}).ToList();
            }
            return tttk;
        }
        public object getalltttkbyname(string name)
        {
            using (PBL3Entities db = new PBL3Entities())
            {
                return db.THONGTINTAIKHOAN.Where(tttk => tttk.Tên_nhân_viên.Contains(name) && tttk.TAIKHOAN.PQ == false).Select(p => new { p.Mã_nhân_viên, p.Tên_nhân_viên, p.CMND, p.SĐT, p.Địa_chỉ, p.TAIKHOAN.Khóa }).ToList();
            };
        }
        public THONGTINTAIKHOAN GetThongTinTaiKHoanByID(int  id)
        {
            THONGTINTAIKHOAN tttk = new THONGTINTAIKHOAN();
            using (PBL3Entities db = new PBL3Entities())
            {
                tttk = db.THONGTINTAIKHOAN.Find(id);
            }
            return tttk;
        }
        public TAIKHOAN GetTaiKHoanByID(int  id)
        {
            TAIKHOAN tk = new TAIKHOAN();
            using (PBL3Entities db = new PBL3Entities())
            {
                tk = db.TAIKHOAN.Find(id);
            }
            return tk;
        }

        public void KhoaTaiKhoan(int  id,bool k)
        {
            using (PBL3Entities db = new PBL3Entities())
            {             
                var s = db.TAIKHOAN.Find(id);              
                    s.Khóa = k;          
                db.SaveChanges();
            }
        }
        public void ThemTaiKhoan(THONGTINTAIKHOAN tttk, TAIKHOAN tk)
        {

            using (PBL3Entities db = new PBL3Entities())
            {
                
                db.TAIKHOAN.Add(tk);
                tttk.Mã_nhân_viên = tk.Mã_tài_khoản;
                db.THONGTINTAIKHOAN.Add(tttk);               
                db.SaveChanges();
            }                         
        }
        public byte[] GetImageByID(int id)
        {
            using (PBL3Entities db = new PBL3Entities())
            {
                var tmp = db.THONGTINTAIKHOAN.Find(id);
                return tmp.Hình_ảnh;
            }
        }


        public void UpdateTaiKhoan(int MSNV,THONGTINTAIKHOAN tttk, TAIKHOAN tk)
        {
            using (PBL3Entities db = new PBL3Entities())
            {
                var tmp1 = db.THONGTINTAIKHOAN.Find(MSNV);
                var tmp2 = db.TAIKHOAN.Find(MSNV);          
                tmp1.Tên_nhân_viên = tttk.Tên_nhân_viên;
                tmp1.CMND = tttk.CMND;
                tmp1.SĐT = tttk.SĐT;
                tmp1.Địa_chỉ = tttk.Địa_chỉ;
                tmp1.Hình_ảnh = tttk.Hình_ảnh;
                tmp2.Tên_tài_khoản = tk.Tên_tài_khoản;
                tmp2.Mật_khẩu = tk.Mật_khẩu;
                tmp2.PQ = tk.PQ;
                db.SaveChanges();
            }
        }
      public string MaHoaMatKhau(string mk)
        {
            byte[] tmp=ASCIIEncoding.ASCII.GetBytes(mk);
            byte[] hasdata = new MD5CryptoServiceProvider().ComputeHash(tmp);
            string hasPass= "";
            foreach(byte i in hasdata)
            {
                hasPass += i;               
            }
            return hasPass;
        }
       
    }
}
