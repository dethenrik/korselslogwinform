using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WinFormKørselsLog
{
    public partial class Form1 : Form
    {
        Bitmap Bitmap;

        MySqlConnection sqlconn = new MySqlConnection();
        MySqlCommand sqlcmd = new MySqlCommand();
        DataTable sqlDt = new DataTable();
        string sqlQuery;
        MySqlDataAdapter Dta = new MySqlDataAdapter();
        MySqlDataReader sqlRd;
        DataSet DS = new DataSet();

        string server = "localhost";
        string username = "root";
        string password = "abc123";
        string database = "stucon";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void upLoadData()
        {
            sqlconn.ConnectionString = "server" + server + ";" + "user id =" + username + ";" + "password" + ";" + password + ";" + "database" + database;

            sqlconn.Open();
            sqlcmd.Connection = sqlconn;
            sqlcmd.CommandText = "SELECT * FROM stucon.stucon";

            sqlRd = sqlcmd.ExecuteReader();
            sqlDt.Load(sqlRd);
            sqlRd.Close();
            sqlconn.Close();
            dataGridView1.DataSource = sqlDt;

            sdf




        }







        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult iExit;
            try
            {
                iExit = DialogResult.OK;    
            

            iExit = MessageBox.Show("confirm if you want to exit","MySql Connector",
                MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(iExit == DialogResult.Yes)
            {
                Application.Exit();
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                foreach(Control c in Reset.Controls)
                {
                    if(c is TextBox)
                        ((TextBox)c).Clear();
                }
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Print_Click(object sender, EventArgs e)
        {
            try
            {
                int height = dataGridView1.Height;
                dataGridView1.Height = dataGridView1.RowCount * dataGridView1.RowTemplate.Height * 2;
                Bitmap = new Bitmap(dataGridView1.Width, dataGridView1.Height);
                dataGridView1.DrawToBitmap(Bitmap, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
                printPreviewDialog1.PrintPreviewControl.Zoom = 1;
                printPreviewDialog1.ShowDialog();
                dataGridView1.Height = height;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                e.Graphics.DrawImage(Bitmap, 0, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
