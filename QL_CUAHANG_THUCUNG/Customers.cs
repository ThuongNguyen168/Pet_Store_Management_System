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
    public partial class Customers : Form
    {
        DATA dl = new DATA();
        SqlConnection cnn = new SqlConnection(@"Data Source=SU\SUPIGGY;Initial Catalog=QLTHUCUNG;User ID=sa;Password=123");
        public Customers()
        {
            InitializeComponent();
        }
        public void dataBingDing(DataTable khachhang)
        {
            txtMaKH.DataBindings.Clear();
            txtHoTenKH.DataBindings.Clear();
            cbo_matk.DataBindings.Clear();
            txtSdtKH.DataBindings.Clear();
            txtDiachiKH.DataBindings.Clear();
            datetime_ngaysinh.DataBindings.Clear();
            txtGIoiTinh.DataBindings.Clear();

            txtMaKH.DataBindings.Add("text", khachhang, "MAKH");
            txtHoTenKH.DataBindings.Add("text", khachhang, "TENKH");
            cbo_matk.DataBindings.Add("text", khachhang, "MATK");
            txtSdtKH.DataBindings.Add("text", khachhang, "SDT");
            txtDiachiKH.DataBindings.Add("text", khachhang, "DIACHI");
            datetime_ngaysinh.DataBindings.Add("text", khachhang, "NGSINH");
            txtGIoiTinh.DataBindings.Add("text", khachhang, "GIOITINH");

            DataTable dt1 = getTK();
            cbo_matk.DataSource = dt1;
            cbo_matk.DisplayMember = "MATK";
            cbo_matk.ValueMember = "MATK";

        }


        private void Customers_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có muốn thoát không?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            string str = string.Format("EXEC FINDKH '{0}'", txtMaKH.Text);
            SqlCommand cmd = new SqlCommand(str, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvKH.DataSource = dt;
        }

        public void getInsertList()
        {
            SqlCommand cmd = new SqlCommand("EXEC HIENTHIKHACHHANG", cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvKH.DataSource = dt;
            dataBingDing(dt);
        }
        public DataTable getTK()
        {
            SqlCommand cmd = new SqlCommand("EXEC HIENTHITAIKHOAN", cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        
        private void btnThemKH_Click(object sender, EventArgs e)
        {
            DateTime ngaysinh = datetime_ngaysinh.Value;
            string sdt = txtSdtKH.Text;
            if (dl.ThemKH(txtMaKH.Text, txtHoTenKH.Text, cbo_matk.Text, sdt, txtDiachiKH.Text, ngaysinh, txtGIoiTinh.Text))
            {
                MessageBox.Show("Thêm Thành Công");
                getInsertList();
            }
            else
            {
                MessageBox.Show("Thêm Thất Bại");
            }
        }

        private void Customers_Load(object sender, EventArgs e)
        {
            getInsertList();
        }

        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            if (dl.XoaKH(txtMaKH.Text))
            {
                MessageBox.Show("Xóa thành công");
                getInsertList();
            }
            else
            {
                MessageBox.Show("Xóa thất bại");
            }
        }

        private void btnSuaKH_Click(object sender, EventArgs e)
        {
            DateTime ngaysinh = datetime_ngaysinh.Value;
            string sdt = txtSdtKH.Text;
            if (dl.SuaKH(txtMaKH.Text, txtHoTenKH.Text, cbo_matk.Text, sdt, txtDiachiKH.Text, ngaysinh, txtGIoiTinh.Text))
            {

                MessageBox.Show("Sửa Thành Công");
                getInsertList();

            }
            else
            {
                MessageBox.Show("Sửa Thất Bại");
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ThongTinKH thongtinkh = new ThongTinKH();
            thongtinkh.Show();
        }


    }
}
