using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QL_CUAHANG_THUCUNG
{
    public partial class HOADON : Form
    {
        DATA dl = new DATA();
        SqlConnection cnn = new SqlConnection(@"Data Source=SU\SUPIGGY;Initial Catalog=QLTHUCUNG;User ID=sa;Password=123");
        public HOADON()
        {
            InitializeComponent();
        }
        public void dataBingDing(DataTable hoadon)
        {

            txt_hd.DataBindings.Clear();
            cbo_kh.DataBindings.Clear();
            cbo_nv.DataBindings.Clear();
            dtNgayLap.DataBindings.Clear();
            dtNgayGiao.DataBindings.Clear();
            txt_thanhtien.DataBindings.Clear();

            txt_hd.DataBindings.Add("text", hoadon, "MAHD");
            cbo_kh.DataBindings.Add("text", hoadon, "MAKH");
            cbo_nv.DataBindings.Add("text", hoadon, "MANV");
            dtNgayLap.DataBindings.Add("text", hoadon, "NGAYLAP");
            dtNgayGiao.DataBindings.Add("text", hoadon, "NGAYGIAO");
            txt_thanhtien.DataBindings.Add("text", hoadon, "THANHTIEN");

            DataTable dt1 = getNV();
            cbo_nv.DataSource = dt1;
            cbo_nv.DisplayMember = "MANV";
            cbo_nv.ValueMember = "MANV";

            DataTable dt2 = getKH();
            cbo_kh.DataSource = dt2;
            cbo_kh.DisplayMember = "MAKH";
            cbo_kh.ValueMember = "MAKH";

        }
        public void getInsertList()
        {
            SqlCommand cmd = new SqlCommand("EXEC HIENTHIHOADON", cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv_hd.DataSource = dt;
            dataBingDing(dt);
        }
        public DataTable getNV()
        {
            SqlCommand cmd = new SqlCommand("EXEC hienthiTHEM", cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public DataTable getKH()
        {

            SqlCommand cmd = new SqlCommand("EXEC HIENTHIKHACHHANG", cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        private void HOADON_Load(object sender, EventArgs e)
        {
            getInsertList();
        }

        

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (dl.XoaHD(txt_hd.Text))
            {
                MessageBox.Show("Xóa thành công");
                getInsertList();
            }
            else
            {
                MessageBox.Show("Xóa thất bại");
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            DateTime ngaylap = dtNgayLap.Value;
            DateTime ngaygiao = dtNgayGiao.Value;
            string thanhtien = txt_thanhtien.Text;
            if (dl.SuaHD(txt_hd.Text, cbo_kh.ToString(), cbo_nv.Text, ngaylap, ngaygiao, thanhtien))
            {
                MessageBox.Show("Sửa Thành Công");
                getInsertList();

            }
            else
            {
                MessageBox.Show("Sửa Thất Bại");
            }
        }
        
        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            string str = string.Format("EXEC FINDHD '{0}'", txt_hd.Text);
            SqlCommand cmd = new SqlCommand(str, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv_hd.DataSource = dt;
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            BILL_VIPPRO form = new BILL_VIPPRO(txt_hd.Text);
            form.ShowDialog();
            //this.Visible = true;
        }

        

        private void btn_allbill_Click(object sender, EventArgs e)
        {
            Bill bill = new Bill();
            bill.Show();
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            DateTime ngaylap = dtNgayLap.Value;
            DateTime ngaygiao = dtNgayGiao.Value;
            string thanhtien = txt_thanhtien.Text;
            if (dl.ThemHD(txt_hd.Text, cbo_kh.ToString(), cbo_nv.ToString(), ngaylap, ngaygiao, thanhtien))
            {
                
                MessageBox.Show("Thêm Thành Công");
                getInsertList();
            }
            else
            {
                MessageBox.Show("Thêm Thất Bại");
            }
        }
    }
}
