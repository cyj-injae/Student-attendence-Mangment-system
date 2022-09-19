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
using System.Data.SqlClient;
namespace rr
{
    public partial class Form1 : Form
    {
        string ordb = "Data source=orcl;User Id=scott;Password=tiger;";
        OracleConnection conn;
        SqlDataAdapter da;
        DataTable dt= new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select week from Teach_Std ";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
           
            dr.Close(); 

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }


        //close connectoin
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose(); 
        }

        private void flowLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        ///view student info (SINGle row ) using prcoedure
        private void button1_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "STD_INFORMATION";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("sid", sid_txtbox.Text);
            cmd.Parameters.Add("namee", OracleDbType.Varchar2,30000).Direction=ParameterDirection.Output;
            cmd.Parameters.Add("gender", OracleDbType.Varchar2, 30000).Direction= ParameterDirection.Output; 
            cmd.Parameters.Add("email", OracleDbType.Varchar2, 30000).Direction= ParameterDirection.Output;
            cmd.Parameters.Add("sec", OracleDbType.Int32, 30000).Direction = ParameterDirection.Output;
             cmd.ExecuteNonQuery();
             

                studentname_txtbox.Text = cmd.Parameters["namee"].Value.ToString();
                gender_txtbox.Text = cmd.Parameters["gender"].Value.ToString();
                mail_txtbox.Text = cmd.Parameters["email"].Value.ToString();
                sec_txtbox.Text = cmd.Parameters["sec"].Value.ToString();
              
            

            
        }

       
        //insert student attendeance 
        private void button2_Click(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into Teach_Std values (:tid,:sid,:weeek,:corse,:warn) ";
            cmd.Parameters.Add("tid",tid_txtbox.Text);
            cmd.Parameters.Add("sid",sid_txtbox.Text);
            cmd.Parameters.Add("weeek",week_txtbox.Text);
            cmd.Parameters.Add("corse",coursename_txtbox.Text);
            cmd.Parameters.Add("warn", warning_txtbox.Text);
            int n = cmd.ExecuteNonQuery();
            if (n != -1)
            {
                MessageBox.Show("the Student Attendance has been added !");
            }


        }

        private void label5_Click(object sender, EventArgs e)
        {
            

        }
        // 
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select course from Teach_Std where TeacherID=:tid";
            cmd.Parameters.Add("tid", tid_txtbox.Text);
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                coursename_txtbox.Text = dr[0].ToString();
            }
        }
        //select multipe rows (  WHo attend in certain week and with whm teachwer )
        private void button2_Click_1(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            studentid_cmb.Items.Clear();
            cmd.CommandText = "select StudentID from Teach_Std where week=:wk and TeacherID=:tid";
            cmd.Parameters.Add("wk", week_txtbox.Text);
            cmd.Parameters.Add("tid", tid_txtbox.Text);
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();  
            while (dr.Read())
            {
                studentid_cmb.Items.Add(dr[0].ToString());
            }
        }
        // show weeks that teacher take aattendance in 
        private void button3_Click(object sender, EventArgs e)
        {
            week_cmb.Items.Clear();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "PROCEDURE2";
             cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.Add("id", tid_txtbox.Text);
            cmd.Parameters.Add("week", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                week_cmb.Items.Add(dr[0].ToString());
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
