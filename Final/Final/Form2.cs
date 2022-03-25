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
    public partial class Form2 : Form
    {
        SqlConnection cnn = new SqlConnection();
        SqlDataReader myreader;
        SqlConnectionStringBuilder s = new SqlConnectionStringBuilder("Data Source=DESKTOP-BCRF487\\DB2018980138;" +
            "Initial Catalog=Final;Integrated Security=True;Pooling=False");
        int id;
        public Form2(string str)
        {
            InitializeComponent();
            id = Convert.ToInt32(str);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            cnn.ConnectionString = s.ConnectionString;
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            string strsql="";
            if (textBox1.Text == "" &&
                textBox2.Text == "" &&
                textBox3.Text == "" &&
                textBox4.Text == "" &&
                textBox5.Text == "")
            {
                strsql = "SELECT product_id,product_name,unit_price,quantity,order_date FROM Order_ where customer_id= " + id;// WHERE product_id =" + textBox1.Text;
            }
            else if (textBox1.Text != "" && textBox2.Text != "")
            {
                strsql = "SELECT product_id,product_name,unit_price,quantity,order_date FROM Order_ WHERE product_id =" + textBox1.Text+ "and product_name Like '%" + textBox2.Text + "%'" + "and customer_id = " + id;
            }
            else if (textBox1.Text != "" && textBox2.Text == "")
                strsql = "SELECT product_id,product_name,unit_price,quantity,order_date FROM Order_ WHERE product_id =" + textBox1.Text + "and customer_id = " + id;
            else if (textBox1.Text == "" && textBox2.Text != "")
                strsql = "SELECT product_id,product_name,unit_price,quantity,order_date FROM Order_ WHERE product_name Like '%" + textBox2.Text +"%'" + "and customer_id = " + id;
            SqlCommand cmd = new SqlCommand(strsql, cnn);
            myreader = cmd.ExecuteReader();
            if (strsql != "")
            {
                while (myreader.Read())
                {
                    
                    dataGridView1.Rows.Add(myreader["product_id"].ToString(), myreader["product_name"].ToString(), myreader["unit_price"].ToString(), myreader["quantity"].ToString(), myreader["order_date"].ToString().Split(' ')[0]);
                }
            }
            else
            {
                MessageBox.Show("Error!");
            }
            if (cnn.State == ConnectionState.Open)
                cnn.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("product_id", "product_id");
            dataGridView1.Columns.Add("product_name", "product_name");
            dataGridView1.Columns.Add("unit_price", "unit_price");
            dataGridView1.Columns.Add("quantity", "quantity");
            dataGridView1.Columns.Add("order_date", "order_date");
            dataGridView1.Rows.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (fields_Check() == true)
                {
                    string strsql = "insert into Order_ values (" + textBox1.Text + "," + "'" + textBox2.Text + "'" + "," + textBox3.Text + "," +
                         textBox4.Text + "," + "'" + textBox5.Text + "'," + id + ");";
                    Execute(strsql);
                    clear();
                    show();
                }
                else
                {
                    MessageBox.Show("Please enter the required values", "Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("You have entered wrong values Or the patient who you try to enter already exist!", "Error"
                , MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
        }
        void Execute(String sqlstr1)
        {
            cnn.ConnectionString = s.ConnectionString;
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            string strsql = sqlstr1;
            SqlCommand cmd = new SqlCommand(strsql, cnn);
            int Number_of_rows_affected = cmd.ExecuteNonQuery();
            if (cnn.State == ConnectionState.Open)
                cnn.Close();
        }

        bool fields_Check()
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
                return true;
            else
                return false;
        }

        void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string strsql = "update Order_ set " + "product_id= " + textBox1.Text + ", " + " product_name='" + textBox2.Text + "'," + " unit_price=" + textBox3.Text + "," + "quantity=" + textBox4.Text + ","
                + "order_date='" + textBox5.Text + "' WHERE product_id=" + textBox1.Text + ";";
                Execute(strsql);
                clear();
                show();
            }

            catch (Exception ex)
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
                MessageBox.Show("You have entered wrong values");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string strsql = "delete From Order_ WHERE product_id=" + textBox1.Text + ";";
                Execute(strsql);
                clear();
                show();
            }
            catch
            {
                MessageBox.Show("You have entered wrong values");
            }
            if (cnn.State == ConnectionState.Open)
                cnn.Close();
        }
        void show()
        {
            dataGridView1.Rows.Clear();
            cnn.ConnectionString = s.ConnectionString;
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            string strsql = strsql = "SELECT product_id,product_name,unit_price,quantity,order_date FROM Order_ where " + "customer_id = " + id;
            SqlCommand cmd = new SqlCommand(strsql, cnn);
            myreader = cmd.ExecuteReader();
            if (strsql != "")
            {
                while (myreader.Read())
                {
                    dataGridView1.Rows.Add(myreader["product_id"].ToString(), myreader["product_name"].ToString(), myreader["unit_price"].ToString(), myreader["quantity"].ToString(), myreader["order_date"].ToString().Split(' ')[0]);
                }
            }
            if (cnn.State == ConnectionState.Open)
                cnn.Close();
        }
    }
}
