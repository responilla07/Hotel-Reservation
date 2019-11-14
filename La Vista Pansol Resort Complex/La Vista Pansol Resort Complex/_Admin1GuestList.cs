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
    public partial class _Admin1GuestList : Form
    {
        public _Admin1GuestList()
        {
            InitializeComponent();
        }

        MySqlConnection roominfoConn;

        public String hostName;

        public void _Admin1GuestList_Load(object sender, EventArgs e)
        {
            roominfoConn = new MySqlConnection("server=" + hostName + "; user=root; password= ; database=accomodation;");

            _loadGuest();
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if(textBox2.Text.Length > 0 && textBox1.Text.Length > 0)
            {
                _searchNameID();
            }
            else if (textBox1.Text.Length > 0)
            {
                _searchID();
            }
            else
            {
                _searchName();
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox2.Text.Length > 0 && textBox1.Text.Length > 0)
            {
                _searchNameID();
            }
            else if (textBox2.Text.Length > 0)
            {
                _searchName();
            }
            else
            {
                _searchID();
            }
        }

        public void _searchName()
        {
            try
            {
                roominfoConn.Open();
                MySqlCommand mySqlCommand = roominfoConn.CreateCommand();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                DataTable dataTable = new DataTable();
                mySqlCommand.CommandText = "SELECT * FROM info_guest WHERE guestName LIKE ('%" + textBox2.Text + "%')";
                mySqlCommand.ExecuteNonQuery();

                mySqlDataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                roominfoConn.Close();
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.Message);
                roominfoConn.Close();
            }
        }

        public void _searchID()
        {
            try
            {
                roominfoConn.Open();
                MySqlCommand mySqlCommand = roominfoConn.CreateCommand();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                DataTable dataTable = new DataTable();
                mySqlCommand.CommandText = "SELECT * FROM info_guest WHERE guestID LIKE ('%" + textBox1.Text + "%')";
                mySqlCommand.ExecuteNonQuery();

                mySqlDataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                roominfoConn.Close();
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.Message);
                roominfoConn.Close();
            }
        }

        public void _searchNameID()
        {
            try
            {
                roominfoConn.Open();
                MySqlCommand mySqlCommand = roominfoConn.CreateCommand();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                DataTable dataTable = new DataTable();
                mySqlCommand.CommandText = "SELECT * FROM info_guest WHERE guestName LIKE ('%" + textBox2.Text + "%') AND guestID LIKE ('%" + textBox1.Text + "%')";
                mySqlCommand.ExecuteNonQuery();

                mySqlDataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                roominfoConn.Close();
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.Message);
                roominfoConn.Close();
            }
        }

        public void _loadGuest()
        {
            try
            {
                roominfoConn.Open();
                MySqlCommand mySqlCommand = roominfoConn.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.CommandText = "SELECT * FROM info_guest";
                mySqlCommand.ExecuteNonQuery();

                mySqlDataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Guest ID";
                dataGridView1.Columns[2].HeaderText = "Guest Name";
                dataGridView1.Columns[3].HeaderText = "Contact No.";
                dataGridView1.Columns[4].HeaderText = "Email";
                dataGridView1.Columns[5].HeaderText = "Address";
                dataGridView1.Columns[6].HeaderText = "Gender";

                roominfoConn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                roominfoConn.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int count = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
                roominfoConn.Open();
                MySqlCommand mySqlCommand = roominfoConn.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.CommandText = "SELECT * FROM info_guest WHERE ID = '" + count + "'";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);
                roominfoConn.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    tbGuestID.Text = dataRow["guestID"].ToString();
                    tbGuestName.Text = dataRow["guestName"].ToString();
                    tbGuestContactNo.Text = dataRow["contactNo"].ToString();
                    tbGuestEmail.Text = dataRow["emailAddress"].ToString();
                    tbGuestAddress.Text = dataRow["address"].ToString();
                    tbGuestGender.Text = dataRow["gender"].ToString();
                }
            }
            catch (Exception exc)
            {
                ////MessageBox.Show(exc.Message);
                roominfoConn.Close();
            }
        }
    }
}
