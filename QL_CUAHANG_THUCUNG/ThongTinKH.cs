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
    public partial class ThongTinKH : Form
    {
        DATA dl = new DATA();
        public ThongTinKH()
        {
            InitializeComponent();
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            string sdt = txt_SDT.Text;
            DateTime ngsinh = datetime_ngsinh.Value;
            string gioitinh;
            if (radio_nam.Checked)
            {
                gioitinh = radio_nam.ToString();
            }
            else
            {
                gioitinh = radio_nu.ToString();
            }
            if(dl.SuaKH1(txt_MAKH.Text, txt_tenkh.Text, txt_matk.Text, sdt, txt_DiaChi.Text, ngsinh, gioitinh, txt_quyenhan.Text))
            {
                MessageBox.Show("Sửa thành công");
            }
            else
            {
                MessageBox.Show("Sửa thất bại");
            }
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ThongTinKH_Load(object sender, EventArgs e)
        {
            DataTable a =  dl.loadTTKH(QUYEN.tk, QUYEN.mk);
            foreach (DataRow r in a.Rows)
            {
                txt_MAKH.Text = r[0].ToString();
                txt_tenkh.Text = r[1].ToString();
                txt_matk.Text = r[2].ToString();
                txt_SDT.Text = r[3].ToString();
                txt_DiaChi.Text = r[4].ToString();
                datetime_ngsinh.Value = DateTime.Parse(r[5].ToString());
                
                if (radio_nam.Text == r[6].ToString())
                {
                    radio_nam.Checked.ToString();
                }
                else
                {
                    radio_nu.Checked.ToString();
                }
                txt_quyenhan.Text = r[7].ToString();
            }
        }

    }
}
