using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO.Ports;
using System.Threading;

namespace La_Vista_Pansol_Resort_Complex
{
    public partial class _UsersNewGuest : Form
    {
        public _UsersNewGuest()
        {
            InitializeComponent();
        }

        MySqlConnection mySqlConnection;
        MySqlConnection connection;
        public String hostName;
        public String resortPassword;
        public String resortEmail;
        public String resortContact;

        public void _SaveData()
        {
            try
            {
                mySqlConnection.Open();
                MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
                mySqlCommand.CommandText = "INSERT INTO info_guest (guestID, guestName, contactNo, emailAddress, address, gender)VALUES" +
                    "('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + comboBox1.Text + "')";
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                mySqlConnection.Close();
            }
        }

        public void _random()
        {
            Random generator = new Random();
            String r = generator.Next(0, 9999999).ToString("D15");
            textBox1.Text = Convert.ToString(r);
        }

        public void _reset()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _random();
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && textBox3.Text.Length> 0 && textBox4.Text.Length > 0
                && textBox5.Text.Length > 0 && comboBox1.Text.Length > 0)
            {
                _SaveData();
                _sendEmail();
               // _sendSMS();
                MessageBox.Show("New guest information has been added.");
                _reset();
            }
            else
            {
                MessageBox.Show("Please complete all the information above.");
            }
        }

        ///On Load
        private void _NewGuest_Load(object sender, EventArgs e)
        {
            mySqlConnection = new MySqlConnection("server="+ hostName + "; user=root; password=; database=accomodation;");
            connection = new MySqlConnection("server=" + hostName + "; user=root; password= ; database=userdb;");
            _getInfo();
        }

        ///Get Credentials
        public void _getInfo()
        {
            _UsersMainForm _MainForm = new _UsersMainForm();
            try
            {
                connection.Open();
                MySqlCommand mySqlCommand = connection.CreateCommand();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                DataTable dataTable = new DataTable();

                mySqlCommand.CommandText = "SELECT * FROM login_info WHERE ID = 1";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    resortEmail = dataRow["email"].ToString();
                    resortContact = dataRow["contactNo"].ToString();
                    resortPassword = dataRow["user_password"].ToString();

                    textBox4.Text = resortEmail;
                    break;
                }
                connection.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        ///Send Email
        public void _sendEmail()
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;

                smtpClient.Credentials = new NetworkCredential(resortEmail, resortPassword);

                MailMessage mailMessage = new MailMessage(resortEmail, textBox4.Text,

                    "La Vista Pansol Resort",
                    "Dear, Mr./Ms. " + textBox2.Text + "\n" + "\n" +
                    "Welcome to La Vista Pansol Resort," + "\n" + "\n" +
                    "It is our pleasure to have you visit our resort,  " +
                    "we will surely ensure that you have great stay." + "\n" + "\n" +

                    "   Your guest ID is: " + textBox1.Text + "\n" +
                    "   Contact Tel. No. / Cell phone: " + textBox3.Text + "\n" +
                    "   Address: " + textBox5.Text + "\n" + "\n"+ "\n" +

                    "E-Mail:" + "\n" + "   " + resortEmail + "\n" + "\n" +
                    "Address:" + "\n" + "   Norville Subdivision,Barangay Pansol Calamba, Laguna "+ "\n" + "\n" +
                    "Contact Numbers:" + "\n" + 
                    "   Laguna Office:" + "(049) 834-1121 / (049) 545-1850 " + "\n" +
                    "   Manila Office:" + "(02) 519-4215" + "\n" +
                    "   Mobile: " + "0915-211-0737 / 0923-179-2014 ");

                mailMessage.Priority = MailPriority.High;
                smtpClient.Send(mailMessage);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        ///Send SMS
        public void _sendSMS()
        {
            SerialPort _serialPort;
            try
            {
                string number = textBox3.Text;
                string message = "Dear, Mr./Ms. " + textBox2.Text + "\n" + "\n" +
                                 "Welcome to La Vista Pansol Resort," + "\n" + "\n" +
                                 "   Thank you so much for choosing La Vista Pansol Resort. " +
                                 "It is our pleasure to have you here with us, " +
                                 "As we want to ensure that you will have a pleasant and comfortable stay." + "\n" + "\n" +

                                 "   Your guest ID is: " + textBox1.Text + "\n" +
                                 "   Contact Tel. No. / Cell phone: " + textBox3.Text + "\n" +
                                 "   Address: " + textBox5.Text + "\n" + "\n" +

                                 "   Again we want to welcome you and we can't wait to hear " +
                                 "about your experience with our service at La Vista Pansol Resort." + "\n" + "\n" + "\n" +

                                 "E-Mail:" + "\n" + "   " + resortEmail + "\n" + "\n" +
                                 "Address:" + "\n" + "   Norville Subdivision,Barangay Pansol Calamba, Laguna " + "\n" + "\n" +
                                 "Contact Numbers:" + "\n" +
                                 "   Laguna Office:" + "(049) 834-1121 / (049) 545-1850 " + "\n" +
                                 "   Manila Office:" + "(02) 519-4215" + "\n" +
                                 "   Mobile: " + "0915-211-0737 / 0923-179-2014 ";

                _serialPort = new SerialPort("COM8", 115200);
                Thread.Sleep(1000);
                _serialPort.Open();
                Thread.Sleep(1000);
                _serialPort.Write("AT+CMGF=1\r");
                Thread.Sleep(1000);
                _serialPort.Write("AT+CMGS=\"" + number + "\"\r\n");
                Thread.Sleep(1000);
                _serialPort.Write(message + "\x1A");
                Thread.Sleep(1000);
                _serialPort.Close();

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
