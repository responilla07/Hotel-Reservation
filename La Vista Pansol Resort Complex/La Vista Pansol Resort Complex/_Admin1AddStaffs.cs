using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace La_Vista_Pansol_Resort_Complex
{
    public partial class _Admin1AddStaffs : Form
    {
        public _Admin1AddStaffs()
        {
            InitializeComponent();
        }

        MySqlConnection userdbConnection;
        public String hostName;
        String existanceID = "";
        String existanceUser = "";


        public void _Admin1AddStaffs_Load(object sender, EventArgs e)
        {
            userdbConnection = new MySqlConnection("server=" + hostName + "; user=root; password= ; database=userdb;");
            _reset();
            _loadUsers();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _selectData();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            _existanceID();
            _existanceUser();
            if (existanceID == tbEmployeeID.Text || existanceUser == tbUsername.Text)
            {
                MessageBox.Show("User is already exist, check the employee for it's information.");
            }
            else
            {
                if (tbContactNo.Text.Length > 0 && tbEmail.Text.Length > 0 && tbEmployeeID.Text.Length > 0 && cmbGender.Text.Length > 0
                    && tbFullname.Text.Length > 0 && tbPassword.Text.Length > 0 && tbUsername.Text.Length > 0 && cmbLevel.Text.Length > 0)
                {
                    _saveUsers();
                    _reset();
                    _loadUsers();
                }
                else
                {
                    MessageBox.Show("Please complete all the fields below before proceeding.");
                }
            }
        }

        ///
        public void _reset()
        {
            tbEmployeeID.Text = "";
            tbFullname.Text = "";
            tbUsername.Text = "";
            tbPassword.Text = "";
            tbEmail.Text = "";
            tbContactNo.Text = "";
        }

        ///
        public void _loadUsers()
        {
            try
            {
                userdbConnection.Open();
                MySqlCommand mySqlCommand = userdbConnection.CreateCommand();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                DataTable dataTable = new DataTable();

                mySqlCommand.CommandText = "SELECT * FROM login_info ORDER BY ID DESC";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Employee ID";
                dataGridView1.Columns[2].HeaderText = "Name";
                dataGridView1.Columns[3].HeaderText = "Username";
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].HeaderText = "Email";
                dataGridView1.Columns[6].HeaderText = "Contact #";
                dataGridView1.Columns[7].HeaderText = "Gender";
                dataGridView1.Columns[8].HeaderText = "User Level";
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;

                userdbConnection.Close();
            }
            catch(Exception exc)
            {
                userdbConnection.Close();
            }

        }

        ///
        public void _saveUsers()
        {
            dateTimePicker1.ResetText();
            try
            {
                userdbConnection.Open();
                MySqlCommand mySqlCommand = userdbConnection.CreateCommand();
                mySqlCommand.CommandText = "INSERT INTO login_info(employeeID, name, user_name, user_password, email, contactNo, gender, user_level, dateCreated, lastActive) VALUES ('" + tbEmployeeID.Text + "','" + tbFullname.Text + "', '" + tbUsername.Text + "', '" + tbPassword.Text + "', '" + tbEmail.Text + "', '" + tbContactNo.Text + "', '" + cmbGender.Text + "', '" + cmbLevel.Text + "', '" + dateTimePicker1.Text + "', '" + dateTimePicker1.Text + "' )";
                mySqlCommand.ExecuteNonQuery();

                userdbConnection.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                userdbConnection.Close();
            }
        }

        ///
        public void _existanceID()
        {
            try
            {
                userdbConnection.Open();
                MySqlCommand mySqlCommand = userdbConnection.CreateCommand();
                mySqlCommand.CommandText = "SELECT * FROM login_info WHERE employeeID = '" + tbEmployeeID.Text + "'";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                DataTable dataTable = new DataTable();
                mySqlDataAdapter.Fill(dataTable);
              
                mySqlCommand.ExecuteNonQuery();

                foreach(DataRow dataRow in dataTable.Rows)
                {
                    existanceID = dataRow["employeeID"].ToString();
                }
                userdbConnection.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                userdbConnection.Close();
            }
        }
        ///
        public void _existanceUser()
        {
            try
            {
                userdbConnection.Open();
                MySqlCommand mySqlCommand = userdbConnection.CreateCommand();
                mySqlCommand.CommandText = "SELECT * FROM login_info WHERE user_name = '" + tbUsername.Text + "'";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                DataTable dataTable = new DataTable();
                mySqlDataAdapter.Fill(dataTable);

                mySqlCommand.ExecuteNonQuery();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    existanceUser = dataRow["user_name"].ToString();
                }
                userdbConnection.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                userdbConnection.Close();
            }
        }

        ///
        public void _selectData()
        {
            try
            {
                userdbConnection.Open();
                int count = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

                MySqlCommand mySqlCommand = userdbConnection.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "SELECT * FROM login_info WHERE ID=" + count + "";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);

                userdbConnection.Close();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    tbEmployeeID.Text = dataRow["employeeID"].ToString();
                    tbFullname.Text = dataRow["name"].ToString();
                    tbUsername.Text = dataRow["user_name"].ToString();
                    tbPassword.Text = dataRow["user_password"].ToString();
                    tbContactNo.Text = dataRow["contactNo"].ToString();
                    tbEmail.Text = dataRow["email"].ToString();
                    cmbGender.Text = dataRow["gender"].ToString();
                    cmbLevel.Text = dataRow["user_level"].ToString();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
