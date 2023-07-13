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
    public partial class ThongTinNV : Form
    {
        DATA dl = new DATA();
        public ThongTinNV()
        {
            InitializeComponent();
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            string sdt = txt_SDT.Text;
            DateTime ngsinh = datetime_ngaysinh.Value;
            if (dl.SuaNV1(txt_MANV.Text, txt_tennv.Text, txt_tk.Text, ngsinh, sdt, txt_DiaChi.Text, txt_luong.Text, txt_quyenhan.Text))
            {
                MessageBox.Show("Sửa thành công");
            }
            else
            {
                MessageBox.Show("Sửa thất bại");
            }
        }

        private void ThongTinNV_Load(object sender, EventArgs e)
        {
            DataTable a = dl.loadTTNV(QUYEN.tk, QUYEN.mk);
            foreach (DataRow r in a.Rows)
            {
                txt_MANV.Text = r[0].ToString();
                txt_tennv.Text = r[1].ToString();
                txt_tk.Text = r[2].ToString();
                datetime_ngaysinh.Value = DateTime.Parse(r[3].ToString());
                txt_SDT.Text = r[4].ToString();
                txt_DiaChi.Text = r[5].ToString();
                txt_luong.Text = r[6].ToString();
                txt_quyenhan.Text = r[7].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
