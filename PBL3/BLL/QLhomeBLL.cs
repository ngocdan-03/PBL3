using PBL3.DAL;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PBL3.BLL
{
    internal class QLhomeBLL
    {
        public THONGTINTAIKHOAN GetInforByID(int id)
        {
            THONGTINTAIKHOAN tttk = new THONGTINTAIKHOAN();
            PBL3Entities db = new PBL3Entities();

            tttk = db.THONGTINTAIKHOAN.Find(id);


            return tttk;
        }
        public void UpdateInfo(int id, string txt1, string txt2, string txt3, string txt4, string txt5, byte[] b)
        {
            using (PBL3Entities db = new PBL3Entities())
            {
                var s = db.THONGTINTAIKHOAN.Find(id);
                s.Tên_nhân_viên  = txt1;
                s.CMND = txt2;
                s.SĐT = txt3;
                s.Địa_chỉ = txt4;
                s.TAIKHOAN.Tên_tài_khoản = txt5;
                s.Hình_ảnh = b;
                db.SaveChanges();
            }
        }

        public bool CheckPass(int id,string mkc, string mkm1, string mkm2)
        {
            QLLoginBLL bll =new QLLoginBLL();
            using(PBL3Entities db=new PBL3Entities())
            {
                var s=db.TAIKHOAN.Find(id);
                if (s.Mật_khẩu != bll.MaHoa(mkc).ToString())
                {
                    MessageBox.Show("Mật khẩu cũ không đúng!!!");
                    return false;
                }
                if (mkm1 != mkm2) 
                {
                    MessageBox.Show("Mật khẩu mới nhập không giống nhau!!!");
                    return false; 
                }
                return true;
            }
        }
      public void UpdatePass(int id,string mkm)
        {
            QLLoginBLL bll=new QLLoginBLL();
            using(PBL3Entities db=new PBL3Entities())
            {
                var s=db.TAIKHOAN.Find(id);
                s.Mật_khẩu= bll.MaHoa(mkm).ToString();
                db.SaveChanges() ;
            }
        }
       public byte[] ImageToByteArray(Image img)
        {
            MemoryStream m = new MemoryStream();
            img.Save(m, System.Drawing.Imaging.ImageFormat.Png);
            return m.ToArray();
        }
    }
}
