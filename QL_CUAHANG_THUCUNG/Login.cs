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
    
    public partial class Login : Form
    {
        DATA dl = new DATA();
        string email = "", mk = "", quyen = "";
        SqlConnection cnn = new SqlConnection(@"Data Source=SU\SUPIGGY;Initial Catalog=QLTHUCUNG;User ID=sa;Password=123");
        
        public static string type;
        public static string username="";
        public Login()
        {
            InitializeComponent();
        }
        public Login(string email, string mk, string quyen)
        {
            InitializeComponent();
            this.email = email;
            this.mk = mk;
            this.quyen = quyen;
        }
        public DataTable getTK()
        {
            SqlCommand cmd = new SqlCommand("EXEC HIENTHITAIKHOAN", cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            //if (rdbWindow.Checked == true)
            //{
            //    cnn.Open();
            //    email = txtTaiKhoanLogin.Text;
            //    mk = txtMatKhauLogin.Text;
            //    string sql = "select *from TAIKHOAN where EMAIL='" + email + "' and MATKHAU='" + mk + "'";
            //    SqlCommand cmd = new SqlCommand(sql, cnn);
            //    SqlDataReader dta = cmd.ExecuteReader();
            //    if (dta.Read() == true)
            //    {
            //        MessageBox.Show("Dang nhap thanh cong", "THONG BAO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        Home f = new Home();
            //        f.Show();
            //        this.Hide(); //ẩn form DN                   
            //    }

            //}
            //else if (rdbServer.Checked == true)
            //{
            //    string s = "Data Source =" + txtSever.Text + ";Initial Catalog=" + txtData.Text + ";User ID=" + txtTaiKhoanLogin.Text + ";Password=" + txtMatKhauLogin.Text;
            //    cnn.ConnectionString = s;
            //    cnn.Open();
            //    Home f = new Home();
            //    f.Show();
            //    this.Hide(); //ẩn form DN
            //}
            //else
            //{
            //    MessageBox.Show("Dang nhap that bai", "THONG BAO", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question);
            //}
            try
            {
                using (cnn)
                {
                    SqlCommand cmd = new SqlCommand("THEMTK_LOGIN", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cnn.Open();
                    cmd.Parameters.AddWithValue("@USERNAME", txtTaiKhoanLogin.Text);
                    cmd.Parameters.AddWithValue("@MATKHAU", txtMatKhauLogin.Text);
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        rd.Read();
                        if(rd[4].ToString()=="ADMIN")
                        {
                            type = "A";
                            

                        }
                        else if (rd[4].ToString() == "USER")
                        {
                            type = "U";
                        }
                        username = txtTaiKhoanLogin.Text;
                        Home h = new Home();
                        h.Show();
                        this.Hide();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }


        private void Login_Load(object sender, EventArgs e)
        {
            getTK();
        }

        private void hệThôngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void rdbWindow_CheckedChanged(object sender, EventArgs e)
        {
            txtSever.ReadOnly = true;
            txtData.ReadOnly = true;
            txtTaiKhoanLogin.ReadOnly = false;
            txtMatKhauLogin.ReadOnly = false;
        }

        private void rdbServer_CheckedChanged(object sender, EventArgs e)
        {
            txtSever.ReadOnly = false;
            txtData.ReadOnly = false;
            txtTaiKhoanLogin.ReadOnly = false;
            txtMatKhauLogin.ReadOnly = false;
        }

        

    }
}
