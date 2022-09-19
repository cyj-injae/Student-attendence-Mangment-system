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

namespace rr
{
    public partial class Repport_form : Form
    {
        attendence report1;
        Warning report2;
        public Repport_form()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void Repport_form_Load(object sender, EventArgs e)
        {
            report1 = new attendence();
            report2 = new Warning();
            foreach (ParameterDiscreteValue item in report1.ParameterFields[0].DefaultValues)
            {
                comboBox1.Items.Add(item.Value);
            }
            foreach (ParameterDiscreteValue item in report2.ParameterFields[0].DefaultValues)
            {
                Teach_id.Items.Add(item.Value);
            }
        }

        private void report_button_Click(object sender, EventArgs e)
        {
            report1.SetParameterValue(0, comboBox1.Text);
            crystalReportViewer1.ReportSource = report1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            report2.SetParameterValue(0, Teach_id.Text);
            crystalReportViewer1.ReportSource = report2;
        }
    }
}
