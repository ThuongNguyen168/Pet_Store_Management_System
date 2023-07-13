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
    public partial class Pets : Form
    {
        SqlConnection cnn = new SqlConnection(@"Data Source=SU\SUPIGGY;Initial Catalog=QLTHUCUNG;User ID=sa;Password=123");
        DATA dl = new DATA();
        public Pets()
        {
            InitializeComponent();
        }
        void PhanQuyen()
        {
            if(Login.type=="A")
            {
                btnThemTC.Visible = true;
                btnXoaTC.Visible = true;
                btnSuaTC.Visible = true;
                btn_timkiem.Visible = true;
            }
            else if (Login.type=="U")
            {
                btnThemTC.Visible = false;
                btnXoaTC.Visible = false;
                btnSuaTC.Visible = false;
                btn_timkiem.Visible = true;
            }
        }

        public void dataBingDing(DataTable Pet)
        {
            txtMaThuCung.DataBindings.Clear();
            txtTenThuCung.DataBindings.Clear();
            txtGiaThuCung.DataBindings.Clear();
            txtChiTietTC.DataBindings.Clear();
            cboLoaiThuCung.DataBindings.Clear();
            txt_soluong.DataBindings.Clear();

            txtMaThuCung.DataBindings.Add("text", Pet, "MATHUCUNG");
            txtTenThuCung.DataBindings.Add("text", Pet, "TENTHUCUNG");
            txtGiaThuCung.DataBindings.Add("text", Pet, "GIA");
            txtChiTietTC.DataBindings.Add("text", Pet, "CHITIET");
            cboLoaiThuCung.DataBindings.Add("text", Pet, "MALOAI");
            txt_soluong.DataBindings.Add("text", Pet, "SOLUONG");

            DataTable dt1 = getLoaiTC();
            cboLoaiThuCung.DataSource = dt1;
            cboLoaiThuCung.DisplayMember = "MALOAI";
            cboLoaiThuCung.ValueMember = "MALOAI";

        }
        private void Pets_Load(object sender, EventArgs e)
        {
            PhanQuyen();
            getInsertList();
        }
                
        
        private void btnXoaTC_Click(object sender, EventArgs e)
        {
            if (dl.XoaTC(txtMaThuCung.Text))
            {
                MessageBox.Show("Xóa thành công");
                getInsertList();
            }
            else
            {
                MessageBox.Show("Xóa thất bại");
            }
            
        }

        private void btnSuaTC_Click(object sender, EventArgs e)
        {
            string GIA = txtGiaThuCung.Text;
            if (dl.SuaTC(txtMaThuCung.Text, txtTenThuCung.Text, GIA, txtChiTietTC.Text, cboLoaiThuCung.Text, txt_soluong.Text))
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
            string str = string.Format("EXEC FINDTC '{0}'", txtMaThuCung.Text);
            SqlCommand cmd = new SqlCommand(str, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvTC.DataSource = dt;
        }
        
        private void btnThemTC_Click(object sender, EventArgs e)
        {
           
            string GIA = txtGiaThuCung.Text;
            if (dl.ThemTC(txtMaThuCung.Text, txtTenThuCung.Text, GIA, txtChiTietTC.Text, cboLoaiThuCung.Text, txt_soluong.Text))
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
            SqlCommand cmd = new SqlCommand("EXEC hienthiThuCUNG", cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvTC.DataSource = dt;
            dataBingDing(dt);
        }
        public DataTable getLoaiTC()
        {
            SqlCommand cmd = new SqlCommand("EXEC hienthiLOAITC", cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

       
       

        
       
    }
}
