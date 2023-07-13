using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace QL_CUAHANG_THUCUNG
{
    public class DATA
    {
        SqlConnection cnn = new SqlConnection(@"Data Source=SU\SUPIGGY;Initial Catalog=QLTHUCUNG;User ID=sa;Password=123");
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        DataSet dt_QLThuCung = new DataSet();


        // NHÂN VIÊN

        
        public bool ThemNV(string MANV, string hoten, string TAIKHOAN, DateTime ngaysinh, string sdt, string diachi, string luong)
        {
            
            try
            {
                cnn.Open();
                string str = string.Format("EXEC THEM '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'",
                    MANV, hoten, TAIKHOAN, ngaysinh.ToString("yyyy/MM/dd"), sdt, diachi, luong);
                SqlCommand cmd = new SqlCommand(str, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        public bool XoaNV(string MANV)
        {
            try
            {
                cnn.Open();
                string str = string.Format("EXEC XOANV '{0}'", MANV);
                SqlCommand cmd = new SqlCommand(str, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SuaNV(string MANV, string hoten, string TAIKHOAN, DateTime ngaysinh, string sdt, string diachi, string luong)
        {
            try
            {
                cnn.Open();
                string str = string.Format("EXEC SUANV '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'",
                    MANV, hoten, TAIKHOAN, ngaysinh.ToString("yyyy/MM/dd"), sdt, diachi, luong);
                SqlCommand cmd = new SqlCommand(str, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool TimMaNV(string MANV)
        {
            try
            {
                string str = string.Format("EXEC FINDNV '{0}'", MANV);
                SqlCommand cmd = new SqlCommand(str, cnn);
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                
                return true;
            }
            catch
            {
                return false;
            }
        }


        // THÚ CƯNG
        
            
        
        public bool ThemTC(string matc, string tentc, string gia, string chitiet, string maloai, string soluong)
        {

            try
            {
                cnn.Open();
                string str = string.Format("EXEC THEMTC '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'",
                    matc, tentc, gia, chitiet, maloai, soluong);
                SqlCommand cmd = new SqlCommand(str, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool XoaTC(string MATC)
        {
            try
            {
                cnn.Open();
                string str = string.Format("EXEC XOATC '{0}'", MATC);
                SqlCommand cmd = new SqlCommand(str, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SuaTC(string matc, string tentc, string gia, string chitiet, string maloai, string soluong)
        {
            try
            {
                cnn.Open();
                string str = string.Format("EXEC SUATC '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'",
                    matc, tentc, gia, chitiet, maloai, soluong);
                SqlCommand cmd = new SqlCommand(str, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool TimMaTC(string MATC)
        {
            try
            {
                string str = string.Format("EXEC FINDTC '{0}'", MATC);
                SqlCommand cmd = new SqlCommand(str, cnn);
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                return true;
            }
            catch
            {
                return false;
            }
        }

        // KHÁCH HÀNG

        public bool ThemKH(string makh, string tenkh, string matk, string sdt, string diachi, DateTime ngaysinh, string gioitinh)
        {
            try
            {
                cnn.Open();
                string str = string.Format("EXEC THEMKH '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'",
                    makh, tenkh, matk, sdt, diachi, ngaysinh.ToString("yyyy/MM/dd"), gioitinh);
                SqlCommand cmd = new SqlCommand(str, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool XoaKH(string MAKH)
        {
            try
            {
                cnn.Open();
                string str = string.Format("EXEC XOAKH '{0}'", MAKH);
                SqlCommand cmd = new SqlCommand(str, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SuaKH(string makh, string tenkh, string matk, string sdt, string diachi, DateTime ngaysinh, string gioitinh)
        {
            try
            {
                cnn.Open();
                string str = string.Format("EXEC SUAKH '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'",
                    makh, tenkh, matk, sdt, diachi, ngaysinh.ToString("yyyy/MM/dd"), gioitinh);
                SqlCommand cmd = new SqlCommand(str, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool TimMaKH(string MAKH)
        {
            try
            {
                string str = string.Format("EXEC FINDKH '{0}'", MAKH);
                SqlCommand cmd = new SqlCommand(str, cnn);
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ktraDangNhap(string tk, string mk)
        {
            foreach (DataRow r in ds.Tables["dbo.TAIKHOAN"].Rows)
            {
                if (r["EMAIL"].ToString() == tk && r["MATKHAU"].ToString() == mk)
                {
                    QUYEN.tk = r["USERNAME"].ToString();
                    QUYEN.mk = r["MATKHAU"].ToString();
                    QUYEN.loaiQuyen = r["QUYENHAN"].ToString();                   
                    return true;
                }

            }
            return false;
        }
        public DataTable LoadKH()
        {
            SqlCommand cmd = new SqlCommand("EXEC HIENTHIKHACHHANG", cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);           
            return dt;
        }
        
        
        public bool capNhatThanhTien()
        {
            try
            {
                SqlCommand cm = new SqlCommand("updatettHoaDon", cnn);
                cm.CommandType = CommandType.StoredProcedure;
                cnn.Open();
                cm.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //Xóa hóa đơn có dùng thủ tục
        public bool xoaHoaDon(string maHD)
        {
            try
            {
                SqlCommand cm = new SqlCommand("DELHD", cnn);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@MAHD", maHD);
                cnn.Open();
                cm.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }
        

        // Hàm nhập vào mã nhân viên và trả về bảng các hóa đơn mà nhân viên đó đã lập
        public DataTable NHANVIENHD(string manv)
        {
            DataTable a = new DataTable();
            string cauLenh = "select * from NHANVIENHD ('" + manv + "')";
            SqlDataAdapter b = new SqlDataAdapter(cauLenh, cnn);
            b.Fill(a);
            return a;

        }
        public DataTable loadTTNV(string tk, string mk)
        {

            string str = string.Format("EXEC THONGTINNV '{0}', '{1}'", tk, mk);
            SqlCommand cmd = new SqlCommand(str, cnn);
            da = new SqlDataAdapter(cmd);
            DataTable a = new DataTable();
            da.Fill(a);
            return a;

        }
        public bool SuaNV1(string MANV, string hoten, string TAIKHOAN, DateTime ngaysinh, string sdt, string diachi, string luong, string quyenhan)
        {
            try
            {
                cnn.Open();
                string str = string.Format("EXEC SUANV '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}'",
                    MANV, hoten, TAIKHOAN, ngaysinh.ToString("yyyy/MM/dd"), sdt, diachi, luong, quyenhan);
                SqlCommand cmd = new SqlCommand(str, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public DataTable loadTTKH(string tk, string mk)
        {

            string str = string.Format("EXEC THONGTINKH '{0}', '{1}'", tk, mk);
            SqlCommand cmd = new SqlCommand(str, cnn);
            da = new SqlDataAdapter(cmd);
            DataTable a = new DataTable();
            da.Fill(a);
            return a;

        }
        public bool SuaKH1(string makh, string tenkh, string matk, string sdt, string diachi, DateTime ngaysinh, string gioitinh, string quyenhan)
        {
            try
            {
                cnn.Open();
                string str = string.Format("EXEC SUAKH '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}'",
                    makh, tenkh, matk, sdt, diachi, ngaysinh.ToString("yyyy/MM/dd"), gioitinh, quyenhan);
                SqlCommand cmd = new SqlCommand(str, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        // HÓA ĐƠN

        public bool ThemHD(string mahd, string makh, string manv, DateTime ngaylap, DateTime ngaygiao, string thanhtien)
        {
            try
            {
                cnn.Open();
                string str = string.Format("EXEC THEMHD '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'",
                    mahd, makh, manv, ngaylap.ToString("yyyy/MM/dd"), ngaygiao.ToString("yyyy/MM/dd"), thanhtien);
                SqlCommand cmd = new SqlCommand(str, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool XoaHD(string MAHD)
        {
            try
            {
                cnn.Open();
                string str = string.Format("EXEC XOAHD '{0}'", MAHD);
                SqlCommand cmd = new SqlCommand(str, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SuaHD(string mahd, string makh, string manv, DateTime ngaylap, DateTime ngaygiao, string thanhtien)
        {
            try
            {
                cnn.Open();
                string str = string.Format("EXEC SUAHD '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'",
                    mahd, makh, manv, ngaylap.ToString("yyyy/MM/dd"), ngaygiao.ToString("yyyy/MM/dd"), thanhtien);
                SqlCommand cmd = new SqlCommand(str, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool TimMaHD(string MAHD)
        {
            try
            {
                string str = string.Format("EXEC FINDHD '{0}'", MAHD);
                SqlCommand cmd = new SqlCommand(str, cnn);
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
