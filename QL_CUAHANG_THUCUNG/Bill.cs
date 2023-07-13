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
    public partial class Bill : Form
    {
        Customers cm = new Customers();
        DATA nv = new DATA();

        public Bill()
        {
            InitializeComponent();
        }
        private void Bill_Load(object sender, EventArgs e)
        {
            xuathoadon rpt = new xuathoadon();
            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.DisplayStatusBar = false;
            crystalReportViewer1.DisplayToolbar = true;
            crystalReportViewer1.Refresh();    
        }
        

        

    }
}
