using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace rr
{
    public partial class Form3 : Form
    {
        
        OracleDataAdapter adapter;
        OracleCommandBuilder cmd;
        DataSet ds;
        public Form3()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        string ordb = "Data Source = orcl; User ID = scott; Password = tiger;";

        private void load_btn_Click(object sender, EventArgs e)
        {
            string cmd_str = "select * from teach_std where teacherid = :t and studentid = :s";

            adapter = new OracleDataAdapter(cmd_str, ordb);
            adapter.SelectCommand.Parameters.Add("t", teacherID.Text);
            //adapter.SelectCommand.Parameters.Add("w", week.Text);
            adapter.SelectCommand.Parameters.Add("s", studentID.Text);

            ds = new DataSet();
            adapter.Fill(ds);
            data.DataSource = ds.Tables[0];
        }

        private void data_CellContentClick (object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void save_btn_Click(object sender, EventArgs e)
        {
            cmd = new OracleCommandBuilder(adapter);
            adapter.Update(ds.Tables[0]);
        }

        private void data_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string message = "Do you want to delete this attendance?";
            string title = "Delete attendance";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(this, message, title, buttons);
            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in data.SelectedRows)
                {
                    data.Rows.RemoveAt(row.Index);
                    data.Update();
                }

            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
