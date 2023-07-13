using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_CUAHANG_THUCUNG
{
    public partial class Home : Form
    {
       
        public bool isThoat = true;
        public Home()
        {
            InitializeComponent();
            
        }
        string username = "";
        private void label5_Click(object sender, EventArgs e) // đăng xuất
        {
            this.Close();
            Login a = new Login();
            a.Show();
        }

        void PhanQuyen()
        {
            if(Login.type=="U")
            {
                label4.Enabled = false;
            }
        }
        private void Home_Load(object sender, EventArgs e)
        {
             PhanQuyen();
            username=Login.username;
             lblXinChao.Text = "Xin chào, "+ username;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Home a = new Home();
            a.Show();
            
        }

       

        private void label2_Click(object sender, EventArgs e)
        {
            Customers a = new Customers();
            a.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Employees a = new Employees();
            a.Show();
        }

        

        private void label7_Click(object sender, EventArgs e)
        {
            Pets a = new Pets();
            a.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            HOADON a = new HOADON();
            a.Show();
        }
    }
}
