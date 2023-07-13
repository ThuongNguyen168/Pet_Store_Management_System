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
    public partial class Employees : Form
    {
        DATA dl = new DATA();
        public Employees()
        {
            InitializeComponent();
        }
        public void dataBingDing(DataTable Employee)
        {

            txtMaNV.DataBindings.Clear();
            txtHoTenNV.DataBindings.Clear();
            cbo_matk.DataBindings.Clear();
            mtxtNgSinhNV.DataBindings.Clear();
            txtSdtNV.DataBindings.Clear();
            txtDiachiNV.DataBindings.Clear();
            txtLuongNV.DataBindings.Clear();


            txtMaNV.DataBindings.Add("text", Employee, "MANV");
            txtHoTenNV.DataBindings.Add("text", Employee, "TENNV");
            cbo_matk.DataBindings.Add("text", Employee, "MATK");
            mtxtNgSinhNV.DataBindings.Add("text", Employee, "NGSINH");
            txtSdtNV.DataBindings.Add("text", Employee, "SDT");
            txtDiachiNV.DataBindings.Add("text", Employee, "DIACHI");
            txtLuongNV.DataBindings.Add("text", Employee, "LUONG");

            DataTable dt1 = getTK();
            cbo_matk.DataSource = dt1;
            cbo_matk.DisplayMember = "MATK";
            cbo_matk.ValueMember = "MATK";

        }
        public DataTable getTK()
        {
            SqlCommand cmd = new SqlCommand("EXEC HIENTHITAIKHOAN", cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        private void Employees_Load(object sender, EventArgs e)
        {
            
            getInsertList();
        }

        SqlConnection cnn = new SqlConnection(@"Data Source=SU\SUPIGGY;Initial Catalog=QLTHUCUNG;User ID=sa;Password=123");
        private void btnThemNV_Click(object sender, EventArgs e)
        {
            DateTime ngaysinh = mtxtNgSinhNV.Value;
            string sdt = txtSdtNV.Text;
            string luong = txtLuongNV.Text;
            if (dl.ThemNV(txtMaNV.Text, txtHoTenNV.Text, cbo_matk.Text, ngaysinh, sdt, txtDiachiNV.Text, luong))
            {
                
                MessageBox.Show("Thêm Thành Công");
                getInsertList();
                
            }
            else
            {
                MessageBox.Show("Thêm Thất Bại");
            }
           

        }
        public void getInsertList()
        {
            SqlCommand cmd = new SqlCommand("EXEC hienthiTHEM", cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvNV.DataSource = dt;
            dataBingDing(dt);
        }

        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            DateTime ngaysinh = mtxtNgSinhNV.Value;
            string sdt = txtSdtNV.Text;
            string luong = txtLuongNV.Text;
            if (dl.SuaNV(txtMaNV.Text, txtHoTenNV.Text, cbo_matk.Text, ngaysinh, sdt, txtDiachiNV.Text, luong))
            {
                
                MessageBox.Show("Sửa Thành Công");
                getInsertList();

            }
            else
            {
                MessageBox.Show("Sửa Thất Bại");
            }
           
        }
        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            if (dl.XoaNV(txtMaNV.Text))
            {
                MessageBox.Show("Xóa thành công");
                getInsertList();
            }
            else
            {
                MessageBox.Show("Xóa thất bại");
            }
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            string str = string.Format("EXEC FINDNV '{0}'", txtMaNV.Text);
            SqlCommand cmd = new SqlCommand(str, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvNV.DataSource = dt;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ThongTinNV ttnv = new ThongTinNV();
            ttnv.Show();
        }

        




    }
}
