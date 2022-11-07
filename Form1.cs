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
using System.Configuration;
using System.Xml.Linq;

namespace Disconnected_Architecture_Demo
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        SqlCommandBuilder cmdBuilder;
        SqlDataAdapter adapter;
        DataSet ds;
        public Form1()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            conn = new SqlConnection(constr);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public DataSet GetAllEmployee()
        {
            adapter = new SqlDataAdapter("select *from Employee",conn);
            adapter.MissingSchemaAction=MissingSchemaAction.AddWithKey;
            cmdBuilder = new SqlCommandBuilder(adapter);
            ds=new DataSet();
            adapter.Fill(ds, "emp");
            return ds;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
        
            try
            {
                ds = GetAllEmployee();
                DataRow row = ds.Tables["emp"].NewRow();
                row["name"] = txtname.Text;
                row["salary"] = txtsalary.Text;
                ds.Tables["emp"].Rows.Add(row);
                int result = adapter.Update(ds.Tables["emp"]);
                if (result == 1)
                {
                    MessageBox.Show("Record inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmployee();
                DataRow row = ds.Tables["emp"].Rows.Find(txtid.Text);
                if (row != null)
                {
                    row["name"] = txtname.Text;
                    row["salary"] = txtsalary.Text;
                    int result = adapter.Update(ds.Tables["emp"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Record updated");
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmployee();
                DataRow row = ds.Tables["emp"].Rows.Find(txtid.Text);
                if (row != null)
                {
                    row.Delete();
                    int result = adapter.Update(ds.Tables["emp"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Record deleted");
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmployee();
                DataRow row = ds.Tables["emp"].Rows.Find(txtid.Text);
                if (row != null)
                {
                    txtname.Text = row["name"].ToString();
                    txtsalary.Text = row["salary"].ToString();
                }
                else
                {
                    MessageBox.Show("Record not found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnshowall_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmployee();
                dataGridView1.DataSource = ds.Tables["emp"];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
