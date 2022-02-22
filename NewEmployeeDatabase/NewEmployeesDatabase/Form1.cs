using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewEmployeesDatabase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DatabaseConnection objConnect;
        string conString;

        DataSet ds;
        DataRow drow;

        int MaxRows;
        int inc = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                objConnect = new DatabaseConnection();
                conString = Properties.Settings.Default.EmployeeConnectionString;

                objConnect.connection_string = conString;
                objConnect.Sql = Properties.Settings.Default.SQL;

                ds = objConnect.GetConnection;
                MaxRows = ds.Tables[0].Rows.Count;

                NavigateRecords();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void NavigateRecords()
        {
            drow = ds.Tables[0].Rows[inc];
            txtFirstName.Text = drow.ItemArray.GetValue(1).ToString();
            txtSurname.Text = drow.ItemArray.GetValue(2).ToString();
            txtJobTitle.Text = drow.ItemArray.GetValue(3).ToString();
            txtDepartment.Text = drow.ItemArray.GetValue(4).ToString();
            labelUpdate();
        }

        private void labelUpdate()
        {
            label5.Text = "Record" + (inc + 1) + "of" + MaxRows;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (inc != MaxRows -1)
            {

                inc++;
                NavigateRecords();
            }
            else
            {
                MessageBox.Show("No More Rows");
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (inc>0)
            {
                inc--;
                NavigateRecords();
            }
            else
            {
                MessageBox.Show("First Record");
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (inc != MaxRows - 1)
            {
                inc = MaxRows - 1;
                NavigateRecords();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (inc != 0)
            {
                inc = 0;
                NavigateRecords();
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            txtFirstName.Clear();
            txtSurname.Clear();
            txtJobTitle.Clear();
            txtDepartment.Clear();

            btnAddNew.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigateRecords();

            btnCancel.Enabled = false;
            btnSave.Enabled = false;
            btnAddNew.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables[0].NewRow();
            row[1] = txtFirstName.Text;
            row[2] = txtSurname.Text;
            row[3] = txtJobTitle.Text;
            row[4] = txtDepartment.Text;

            ds.Tables[0].Rows.Add(row);

            try
            {
                objConnect.UpdateDatabase(ds);
                MaxRows = MaxRows + 1;
                inc = MaxRows - 1;

                MessageBox.Show("Database updated");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            btnCancel.Enabled = false;
            btnSave.Enabled = false;
            btnAddNew.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables[0].Rows[inc];

            row[1] = txtFirstName.Text;
            row[2] = txtSurname.Text;
            row[3] = txtJobTitle.Text;
            row[4] = txtDepartment.Text;

            try
            {
                objConnect.UpdateDatabase(ds);

                MessageBox.Show("Record Updated");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds.Tables[0].Rows[inc].Delete();
                objConnect.UpdateDatabase(ds);

                MaxRows = ds.Tables[0].Rows.Count;
                inc--;
                NavigateRecords();

                MessageBox.Show("Record Deleted");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
