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

namespace La_Vista_Pansol_Resort_Complex
{
    public partial class _LoginForm : Form
    {
        public _LoginForm()
        {
            InitializeComponent();
        }

        MySqlConnection connection;

        private void button1_Click(object sender, EventArgs e)
        {

            _UsersMainForm _MainForm = new _UsersMainForm();
            _1AdminMainForm _Admin = new _1AdminMainForm();
            connection = new MySqlConnection("server=" + textBox1.Text + "; user=root; password= ; database=userdb;");
            int messages = 1;

            try
            {
                connection.Open();
                MySqlCommand mySqlCommand = connection.CreateCommand();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                DataTable dataTable = new DataTable();
                
                mySqlCommand.CommandText = "SELECT * FROM login_info WHERE user_name = '"+textBox2.Text+"'";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    String a = dataRow["user_name"].ToString();
                    String b = dataRow["user_password"].ToString();
                    String c = dataRow["name"].ToString();
                    String d = dataRow["user_level"].ToString();
                    String f = dataRow["employeeID"].ToString();
                    
                    if(textBox2.Text ==a && textBox3.Text == b)
                    {
                        messages = 1;
                        this.Hide();
                    }
                    else
                    {
                        messages = 0;
                        MessageBox.Show("Wrong username or password");
                        break;
                    }
                    ///////////
                    if (messages == 1)
                    {
                        MessageBox.Show("Welcome!");
                        if (d == "admin")
                        {
                            _Admin.name = c;
                            _Admin.user = a;
                            _Admin.pass = b;
                            _Admin.employeeID = f;
                            _Admin.IP_Connections = textBox1.Text;
                            _Admin.Show();
                        }
                        else
                        {
                            _MainForm.name = c;
                            _MainForm.user = a;
                            _Admin.employeeID = f;
                            _MainForm.IP_Connections = textBox1.Text;
                            _MainForm.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Wrong username or password");
                    }
                    break;
                }
                connection.Close();
            }
            catch(Exception exc)
            {
                MessageBox.Show("Can't connect to server or wrong server connection");
                connection.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Please contact your administrator.");
        }

        private void _LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
           Application.Exit();
        }
    }
}
