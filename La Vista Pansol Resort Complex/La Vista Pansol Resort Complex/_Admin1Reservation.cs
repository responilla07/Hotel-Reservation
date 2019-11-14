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
    public partial class _Admin1Reservation : Form
    {
        public _Admin1Reservation()
        {
            InitializeComponent();
        }
        MySqlConnection roominfoConn;
        MySqlConnection todays_checkin;
        MySqlConnection transaction_record;
        MySqlConnection assets_availability;

        public String hostName;


        ///
        String roomName = "info_cottages";
        int idunit = 0;
        int access = 0;
        //int date, time;
        int rentalID = 0;
        int remaining = 0;
        int tabControl = 0;
        int WavePool, WetnWild = 0;
        String checkID;
        String guestName;
        String trRoom;
        String trRental;
        String trAccess;
        int guestChange;
        String paymentStatus;
        String transactionNo;
        String packageAccess;
        String assetsName;
        String assetsItem;
        String checkOutDate;
        int cottagePrice = 1;
        int tbPrice;
        int tbDownPay;
        int additonalPay;
        String excessHours;
        int downPaid;
        String transacStatus = "all";


        public void _Admin1Reservation_Load(object sender, EventArgs e)
        {
            roominfoConn = new MySqlConnection("server=" + hostName + "; user=root; password= ; database=accomodation;");
            transaction_record = new MySqlConnection("server=" + hostName + "; user=root; password=; database=transaction_record;");
            todays_checkin = new MySqlConnection("server =" + hostName + "; user=root; password=; database=todays_checkin;");
            assets_availability = new MySqlConnection("server=" + hostName + "; user=root; password=; database=assets_availability;");

            _reset();
            cmbRental.Text = "AM";
            cmbAcess.Text = "AM";
            cmbStatus.Text = "All Transaction";
            btnTransNo.Enabled = false;
            _loadRSV_AllData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbRoomType.Text = "";
            cbWavePool.Checked = false;
            cbWetnWild.Checked = false;
            _reset();

            _loadRSVRoom();
            _loadRSVPrice();
            _loadRSVRental();
            _loadRSVAccess();
            _loadRSVGuestTransacInfo();

            _assetsName();
        }
    
        ///RSV
        public void _loadRSV_AllData()
        {
            try
            {
                transaction_record.Open();
                MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.CommandText = "SELECT * FROM guest_transac_info";
                mySqlCommand.ExecuteNonQuery();

                mySqlDataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Guest Name";
                dataGridView1.Columns[1].MinimumWidth = 155;
                dataGridView1.Columns[2].HeaderText = "Guest ID";
                dataGridView1.Columns[2].MinimumWidth = 100;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[3].MinimumWidth = 155;
                dataGridView1.Columns[4].HeaderText = "Check-In";
                dataGridView1.Columns[5].HeaderText = "Check-Out";


                foreach (DataRow dataRow in dataTable.Rows)
                {
                    transactionNo = dataRow["transacNo"].ToString();
                    //dateTimePicker1.Text = dataRow["dateTimeIn"].ToString();
                    //dateTimePicker2.Text = dataRow["dateTimeOut"].ToString();
                }
                transaction_record.Close();
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.Message);
                transaction_record.Close();
            }
        }
        public void _loadRSV_ReservedData()
        {
            try
            {
                transaction_record.Open();
                MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.CommandText = "SELECT * FROM guest_transac_info WHERE transacStatus = '" + transacStatus + "'";
                mySqlCommand.ExecuteNonQuery();

                mySqlDataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Guest Name";
                dataGridView1.Columns[1].MinimumWidth = 155;
                dataGridView1.Columns[2].HeaderText = "Guest ID";
                dataGridView1.Columns[2].MinimumWidth = 100;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[3].MinimumWidth = 155;
                dataGridView1.Columns[4].HeaderText = "Check-In";
                dataGridView1.Columns[5].HeaderText = "Check-Out";


                foreach (DataRow dataRow in dataTable.Rows)
                {
                    transactionNo = dataRow["transacNo"].ToString();
                    //dateTimePicker1.Text = dataRow["dateTimeIn"].ToString();
                    //dateTimePicker2.Text = dataRow["dateTimeOut"].ToString();
                }
                transaction_record.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                transaction_record.Close();
            }
        }
        public void _loadRSVRoom()
        {
            try
            {
                transactionNo = dataGridView1.SelectedCells[3].Value.ToString();
                transaction_record.Open();
                MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.CommandText = "SELECT * FROM trroom WHERE transacNo = '" + transactionNo + "'";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);
                transaction_record.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    tbRoomType.Text = dataRow["roomType"].ToString();
                    tbRoomName.Text = dataRow["roomName"].ToString();
                    assetsItem = dataRow["roomName"].ToString();
                    tbRoomUnit.Text = dataRow["roomUnit"].ToString();
                    tbPaxRange.Text = dataRow["paxRange"].ToString();
                    tbExcessChildren.Text = dataRow["kidsRange"].ToString();
                    tbExcessAdult.Text = dataRow["adultRange"].ToString();
                    tbExcessRoomHour.Text = dataRow["excessHour"].ToString();
                }
            }
            catch (Exception exc)
            {
                ////MessageBox.Show(exc.Message);
                transaction_record.Close();
            }
        }
        public void _loadRSVRental()
        {
            try
            {
                transaction_record.Open();
                MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.CommandText = "SELECT * FROM trrentals WHERE transacNo = '" + transactionNo + "'";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);
                transaction_record.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    String rentalName = dataRow["rentalName"].ToString();
                    String rentalHour = dataRow["rentalHour"].ToString();
                    cmbRental.Text = dataRow["rentalTime"].ToString();

                    if (rentalName == "Videoke")
                    {
                        cbVideoke.Checked = true;
                        tbVideoke.Text = rentalHour;
                    }
                    if (rentalName == "Basketball Court")
                    {
                        cbBasketball.Checked = true;
                        tbBasketball.Text = rentalHour;
                    }
                    if (rentalName == "Volleyball Court")
                    {
                        cbVolleyball.Checked = true;
                        tbVolleyball.Text = rentalHour;
                    }
                    if (rentalName == "Table Tennis")
                    {
                        cbTennis.Checked = true;
                        tbTennis.Text = rentalHour;
                    }
                    if (rentalName == "Billiards")
                    {
                        cbBilliards.Checked = true;
                        tbBilliards.Text = rentalHour;
                    }
                }
            }
            catch (Exception exc)
            {
                ////MessageBox.Show(exc.Message);
                transaction_record.Close();
            }
        }
        public void _loadRSVAccess()
        {
            try
            {
                transaction_record.Open();
                MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.CommandText = "SELECT * FROM traccess WHERE transacNo = '" + transactionNo + "'";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);
                transaction_record.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    cmbAcess.Text = dataRow["accessTime"].ToString();
                    packageAccess = dataRow["accessType"].ToString();
                    ///Do Something
                    if (packageAccess == "Package A")
                    {
                        cbWavePool.Checked = true;
                        cbWetnWild.Checked = true;
                    }
                    if (packageAccess == "Package BB")
                    {
                        cbWavePool.Checked = true;
                    }
                    if (packageAccess == "Package B")
                    {
                        cbWetnWild.Checked = true;
                    }
                    if (packageAccess == "Package C")
                    {
                        cbWavePool.Checked = false;
                        cbWavePool.Checked = false;
                    }
                }
            }
            catch (Exception exc)
            {
                ////MessageBox.Show(exc.Message);
                transaction_record.Close();
            }
        }
        public void _loadRSVPrice()
        {
            try
            {
                transaction_record.Open();
                MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.CommandText = "SELECT * FROM trprice WHERE transacNo = '" + transactionNo + "'";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);
                transaction_record.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    tbPriceRoom.Text = dataRow["roomRate"].ToString();
                    tbPriceAdult.Text = dataRow["adultsPrice"].ToString();
                    tbPriceChildren.Text = dataRow["kidsPrice"].ToString();
                    tbPriceAccess.Text = dataRow["accessPrice"].ToString();
                    tbPriceRental.Text = dataRow["rentalsPrice"].ToString();
                    tbPriceTotal.Text = dataRow["totalPrice"].ToString();
                    tbGuestChange.Text = dataRow["guestChange"].ToString();
                    tbPriceAdvance.Text = dataRow["downPayment"].ToString();
                    //textBox19.Text = dataRow["downPayment"].ToString();
                    tbPrice = Convert.ToInt32(dataRow["totalBalance"].ToString());
                    tbDownPay = Convert.ToInt32(dataRow["downPayment"].ToString());
                    guestChange = Convert.ToInt32(dataRow["guestChange"].ToString());
                    idunit = Convert.ToInt32(dataRow["ID"].ToString());
                    tbPriceBalance.Text = Convert.ToString(tbPrice);
                    downPaid = Convert.ToInt32(dataRow["downPayment"].ToString());
                }
            }
            catch (Exception exc)
            {
                ////MessageBox.Show(exc.Message);
                transaction_record.Close();
            }
        }
        public void _loadRSVGuestTransacInfo()
        {
            try
            {
                transaction_record.Open();
                MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.CommandText = "SELECT * FROM guest_transac_info WHERE transacNo = '" + transactionNo + "'";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);
                transaction_record.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    String date1 = dataRow["dateTimeIn"].ToString();
                    String date2 = dataRow["dateTimeOut"].ToString();
                    dateTimePicker1.MinDate = Convert.ToDateTime(date1);
                    dateTimePicker2.MinDate = Convert.ToDateTime(date2);

                    tbGuestID.Text = dataRow["guestID"].ToString();
                    tbGuestName.Text = dataRow["guestName"].ToString();
                    tbTransNo.Text = dataRow["transacNo"].ToString();
                }
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.Message);
                transaction_record.Close();
            }
        }
        ///Reset
        public void _reset()
        {
            tbGuestID.Text = "";
            tbGuestName.Text = "";
            tbTransNo.Text = "";
            tbRoomName.Text = "";
            tbRoomUnit.Text = "";
            tbPaxRange.Text = "";
            tbVideoke.Text = "0";
            tbBasketball.Text = "0";
            tbVolleyball.Text = "0";
            tbTennis.Text = "0";
            tbBilliards.Text = "0";
            tbExcessRoomHour.Text = "0";
            tbExcessAdult.Text = "0";
            tbExcessChildren.Text = "0";
            tbPriceRoom.Text = "0";
            tbPriceAdult.Text = "0";
            tbPriceChildren.Text = "0";
            tbPriceAccess.Text = "0";
            tbPriceRental.Text = "0";
            tbPriceTotal.Text = "0";
            tbPriceAdvance.Text = "0";
            tbPriceBalance.Text = "0";
            cbVideoke.Checked = false;
            cbBasketball.Checked = false;
            cbVolleyball.Checked = false;
            cbTennis.Checked = false;
            cbBilliards.Checked = false;
            dateTimePicker1.MinDate = DateTime.Today.Date;
            dateTimePicker1.Text = Convert.ToString(DateTime.Today.Date);
            dateTimePicker2.MinDate = DateTime.Today.Date;
            dateTimePicker2.Text = Convert.ToString(DateTime.Today.Date);
            label24.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
            label24.Text = "✘";
            tbGuestName.Text = "";
            label24.ForeColor = Color.Red;
        }
        ///Search
        public void _searchRSV()
        {
            try
            {
                transaction_record.Open();
                MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                DataTable dataTable = new DataTable();
                mySqlCommand.CommandText = "SELECT * FROM guest_transac_info WHERE transacStatus = '"+transacStatus+"' AND guestID LIKE ('%" + textBox24.Text + "%')";
                mySqlCommand.ExecuteNonQuery();

                mySqlDataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                transaction_record.Close();
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.Message);
                transaction_record.Close();
            }
        }
        public void _searchRSVAll()
        {
            try
            {
                transaction_record.Open();
                MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                DataTable dataTable = new DataTable();
                mySqlCommand.CommandText = "SELECT * FROM guest_transac_info WHERE guestID LIKE ('%" + textBox24.Text + "%')";
                mySqlCommand.ExecuteNonQuery();

                mySqlDataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                transaction_record.Close();
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.Message);
                transaction_record.Close();
            }
        }

        private void textBox24_KeyUp(object sender, KeyEventArgs e)
        {
            
            if(transacStatus == "all")
            {
                _searchRSVAll(); 
            }
            else
            {
                _searchRSV();
            }
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStatus.Text == "Reserved")
            {
                transacStatus = "RSV";
                _loadRSV_ReservedData();
            }
            if (cmbStatus.Text == "Checked-In")
            {
                transacStatus = "CHKIN";
                _loadRSV_ReservedData();
            }
            if (cmbStatus.Text == "Checked-Out")
            {
                transacStatus = "CHKOUT";
                _loadRSV_ReservedData();
            }
            if (cmbStatus.Text == "Cancelled")
            {
                transacStatus = "CNCL";
                _loadRSV_ReservedData();
            }
            if (cmbStatus.Text == "All Transaction")
            {
                transacStatus = "all";
                _loadRSV_AllData();
            }
            if (cmbStatus.Text == "Pending")
            {
                transacStatus = "PNDG";
                _loadRSV_ReservedData();
            }
        }



        ///
        public void _assetsName()
        {
            if (assetsItem == "Mushroom")
                assetsName = "mushroom";
            if (assetsItem == "Picnic Hut")
                assetsName = "picnic_hut";
            if (assetsItem == "Kubo(Air-con)")
                assetsName = "kubo";
            if (assetsItem == "Pavillion")
                assetsName = "pavillion";
            if (assetsItem == "Single Cottage 2")
                assetsName = "single_cottage_2";
            if (assetsItem == "Bario Makiling")
                assetsName = "bario_makiling";
            if (assetsItem == "Upper Rest House")
                assetsName = "upper_rest_house";
            if (assetsItem == "Single Cottage 4")
                assetsName = "single_cottage_4";
            if (assetsItem == "Sotano 4")
                assetsName = "sotano_4";
            if (assetsItem == "Suite Room")
                assetsName = "suite_room";
            if (assetsItem == "Sotano 6")
                assetsName = "sotano_6";
            if (assetsItem == "Natalies")
                assetsName = "natalies";
            if (assetsItem == "Lower Rest House")
                assetsName = "lower_rest_house";
            if (assetsItem == "Rest House")
                assetsName = "rest_house";
            if (assetsItem == "Casa Blanca")
                assetsName = "casa_blanca";
            if (assetsItem == "Bucal Rest House")
                assetsName = "bucal_rest_house";
            if (assetsItem == "Dormitory")
                assetsName = "dormitory";
            if (assetsItem == "Ilang-Ilang")
                assetsName = "ilang_ilang";
            if (assetsItem == "Family Rest House A")
                assetsName = "family_rest_house_a";
            if (assetsItem == "Family Rest House B")
                assetsName = "family_rest_house_b";
            if (assetsItem == "Family Rest House C")
                assetsName = "family_rest_house_c";
            if (assetsItem == "Family Rest House D")
                assetsName = "family_rest_house_d";
            if (assetsItem == "Ramon B. Donato Upper Ilang-Ilang (Aircon)")
                assetsName = "upper_ilang_ilang";
            if (assetsItem == "Nixon B. Donato Mini Conference Hall (No-Aircon)")
                assetsName = "mini_conference_hall";
            if (assetsItem == "Dr. Nicolas T. Donato Sr Conference Hall (Aircon)")
                assetsName = "conference_hall";
            if (assetsItem == "Nora Luisa Hall (Aircon)")
                assetsName = "hall";
            if (assetsItem == "Nicole B. Donato Multi Purpose Hall (No-Aircon)")
                assetsName = "multi_purpose_hall";
            if (assetsItem == "Nilcar B. Donato Grand Convention Hall (Aircon)")
                assetsName = "convention_hall";
            if (assetsItem == "Dr. Nielsen B. Donato Upper Pantalan (Aircon)")
                assetsName = "upper_pantalan";
            if (assetsItem == "Nicolas B. Donato III Lower Pantalan (No-Aircon)")
                assetsName = "lower_pantalan";
        }
    }
}
