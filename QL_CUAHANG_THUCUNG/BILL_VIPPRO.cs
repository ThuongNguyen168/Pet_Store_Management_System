using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace QL_CUAHANG_THUCUNG
{
    public partial class BILL_VIPPRO : Form
    {
        string MAHD;

        public BILL_VIPPRO(string MAHD)
        {
            InitializeComponent();
            this.MAHD = MAHD;
        }

        private void BILL_VIPPRO_Load(object sender, EventArgs e)
        {
            XuatHD_VIPPRO rp = new XuatHD_VIPPRO();

            ParameterValues param = new ParameterValues();
            ParameterDiscreteValue param_dis = new ParameterDiscreteValue();
            param_dis.Value = this.MAHD;
            param.Add(param_dis);

            rp.DataDefinition.ParameterFields[0].ApplyCurrentValues(param);

            crystalReportViewer.ReportSource = rp;
            crystalReportViewer.Refresh();
        }
    }
}
