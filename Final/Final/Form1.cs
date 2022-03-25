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

namespace Final
{
    public partial class Form1 : Form
    {
        SqlConnection cnn = new SqlConnection();
        SqlDataReader myreader;
        SqlConnectionStringBuilder s = new SqlConnectionStringBuilder("Data Source=DESKTOP-BCRF487\\DB2018980138;" +
            "Initial Catalog=Final;Integrated Security=True;Pooling=False");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.ConnectionString = s.ConnectionString;
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                string strsql = "SELECT customer_id,password FROM Customer WHERE customer_id =" + textBox1.Text;
                SqlCommand cmd = new SqlCommand(strsql, cnn);
                object res = cmd.ExecuteScalar();
                if (res != null)
                {
                    myreader = cmd.ExecuteReader();
                    myreader.Read();

                    if (textBox2.Text == myreader["password"].ToString())
                    {
                        Form2 f = new Form2(textBox2.Text);
                        f.Show();
                    }
                    else
                        MessageBox.Show("Invalid password");
                }
                else
                    MessageBox.Show("There is no customer with this ID in the table\n Please enter another customerID");
            }
            catch
            {
                MessageBox.Show("There is no customer with this ID in the table\n Please enter another customerID");
            }
            if (cnn.State == ConnectionState.Open)
                cnn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
