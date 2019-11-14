using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace La_Vista_Pansol_Resort_Complex
{
    public partial class _UsersCheckOut : Form
    {

        public _UsersCheckOut()
        {
            InitializeComponent();
        }
        MySqlConnection roominfoConn;
        MySqlConnection todays_checkin;
        MySqlConnection transaction_record;
        MySqlConnection assets_availability;

        public String hostName;

        String roomName = "info_cottages";
        int idunit = 0;
        int remaining = 0;
        int guestChange;
        String paymentStatus;
        String transactionNo;
        String packageAccess;
        String assetsName = "single_cottage_2";
        int tbPrice;
        int tbDownPay;
        int additonalPay;
        int roomPrice;
        int remainUnitReserved;
        String reserved;
        String transNo;
        int downPaid;
        int pendings;
        String assetsItem;


        public void _assetsName()
        {
            if (comboBox2.Text == "Mushroom")
                assetsName = "mushroom";
            if (comboBox2.Text == "Picnic Hut")
                assetsName = "picnic_hut";
            if (comboBox2.Text == "Kubo(Air-con)")
                assetsName = "kubo";
            if (comboBox2.Text == "Pavillion")
                assetsName = "pavillion";
            if (comboBox2.Text == "Single Cottage 2")
                assetsName = "single_cottage_2";
            if (comboBox2.Text == "Bario Makiling")
                assetsName = "bario_makiling";
            if (comboBox2.Text == "Upper Rest House")
                assetsName = "upper_rest_house";
            if (comboBox2.Text == "Single Cottage 4")
                assetsName = "single_cottage_4";
            if (comboBox2.Text == "Sotano 4")
                assetsName = "sotano_4";
            if (comboBox2.Text == "Suite Room")
                assetsName = "suite_room";
            if (comboBox2.Text == "Sotano 6")
                assetsName = "sotano_6";
            if (comboBox2.Text == "Natalies")
                assetsName = "natalies";
            if (comboBox2.Text == "Lower Rest House")
                assetsName = "lower_rest_house";
            if (comboBox2.Text == "Rest House")
                assetsName = "rest_house";
            if (comboBox2.Text == "Casa Blanca")
                assetsName = "casa_blanca";
            if (comboBox2.Text == "Bucal Rest House")
                assetsName = "bucal_rest_house";
            if (comboBox2.Text == "Dormitory")
                assetsName = "dormitory";
            if (comboBox2.Text == "Ilang-Ilang")
                assetsName = "ilang_ilang";
            if (comboBox2.Text == "Family Rest House A")
                assetsName = "family_rest_house_a";
            if (comboBox2.Text == "Family Rest House B")
                assetsName = "family_rest_house_b";
            if (comboBox2.Text == "Family Rest House C")
                assetsName = "family_rest_house_c";
            if (comboBox2.Text == "Family Rest House D")
                assetsName = "family_rest_house_d";
            if (comboBox2.Text == "Ramon B. Donato Upper Ilang-Ilang (Aircon)")
                assetsName = "upper_ilang_ilang";
            if (comboBox2.Text == "Nixon B. Donato Mini Conference Hall (No-Aircon)")
                assetsName = "mini_conference_hall";
            if (comboBox2.Text == "Dr. Nicolas T. Donato Sr Conference Hall (Aircon)")
                assetsName = "conference_hall";
            if (comboBox2.Text == "Nora Luisa Hall (Aircon)")
                assetsName = "hall";
            if (comboBox2.Text == "Nicole B. Donato Multi Purpose Hall (No-Aircon)")
                assetsName = "multi_purpose_hall";
            if (comboBox2.Text == "Nilcar B. Donato Grand Convention Hall (Aircon)")
                assetsName = "convention_hall";
            if (comboBox2.Text == "Dr. Nielsen B. Donato Upper Pantalan (Aircon)")
                assetsName = "upper_pantalan";
            if (comboBox2.Text == "Nicolas B. Donato III Lower Pantalan (No-Aircon)")
                assetsName = "lower_pantalan";
        }

        ///Update Payment
        public void _updatePayment()
        {
            int c = Convert.ToInt32(tbPriceBalance.Text);
            int d = additonalPay - Convert.ToInt32(tbPriceTotal.Text);

            if (c > 0)
            {
                paymentStatus = "With Balance";
            }
            else if (d >= 0)
            {
                /////
                MessageBox.Show(Convert.ToString(d));
                paymentStatus = "Paid";
                guestChange = d;
            }
            try
            {
                transaction_record.Open();
                MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                mySqlCommand.CommandText = "UPDATE trprice SET downPayment = " + additonalPay + ", totalBalance = " + tbPriceBalance.Text + ", " +
                    "guestChange = " + guestChange + ", status = '" + paymentStatus + "' WHERE ID=" + idunit + "";
                mySqlCommand.ExecuteNonQuery();
                transaction_record.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                transaction_record.Close();
            }
        }
        ///Update CHKIN
        public void _updateReservation()
        {
            try
            {
                transaction_record.Open();
                MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "UPDATE guest_transac_info SET transacStatus='CHKOUT' WHERE transacNo='" + tbTransNo.Text + "'";
                mySqlCommand.ExecuteNonQuery();
                transaction_record.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                transaction_record.Close();
            }
            try
            {
                transaction_record.Open();
                MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "UPDATE trroom SET transacStatus='CHKOUT' WHERE transacNo='" + tbTransNo.Text + "'";
                mySqlCommand.ExecuteNonQuery();
                transaction_record.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                transaction_record.Close();
            }
        }
        ///Change of Guest
        public void _pricingTotal()
        {
            tbPriceBalance.Text = Convert.ToString(tbPrice);
            additonalPay = tbDownPay;

            int a = Convert.ToInt32(tbPriceAdvance.Text);
            int b = Convert.ToInt32(tbPriceBalance.Text);
            int c = b - a;

            additonalPay = Convert.ToInt32(tbPriceAdvance.Text) + additonalPay;

            tbPriceBalance.Text = Convert.ToString(c);
            if (Convert.ToInt32(tbPriceBalance.Text) < 0)
            {
                tbPriceBalance.Text = "0";
            }
        }
        ///Get Remaining Unit
        public void _getRoomName()
        {
            try
            {
                roominfoConn.Open();
                MySqlCommand mySqlCommand = roominfoConn.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "SELECT * FROM " + roomName + " WHERE roomName='" + tbRoomName.Text + "'";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    remaining = Convert.ToInt32(dataRow["roomUnits"].ToString());
                }
                roominfoConn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                roominfoConn.Close();
            }
        }
        ///Update Who is Check-in
        public void _updateTodaysCheckIn()
        {
            try
            {
                todays_checkin.Open();
                MySqlCommand mySqlCommand = todays_checkin.CreateCommand();
                mySqlCommand.CommandText = "UPDATE " + assetsName + " SET guestName = '', guestID = '', transacNo = '', availability ='available' WHERE unitName='" + tbRoomUnit.Text + "'";
                mySqlCommand.ExecuteNonQuery();
                todays_checkin.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                todays_checkin.Close();
            }
        }
        ///Update Room Unit
        public void _updateUnitsInc()
        {
            try
            {
                roominfoConn.Open();
                MySqlCommand mySqlCommand = roominfoConn.CreateCommand();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                remaining++;

                mySqlCommand.CommandText = "UPDATE "+roomName+" SET roomUnits=" + remaining + " WHERE roomName='" + tbRoomName.Text + "'";
                mySqlCommand.ExecuteNonQuery();
                roominfoConn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                roominfoConn.Close();
            }
        }
        ///
        public void _cbRooms1()
        {
            if (comboBox1.Text == "Cottages")
            {
                comboBox2.Visible = true;
                tbExcessRoomHour.Visible = false;
                comboBox2.Items.Clear();
                

                comboBox2.Items.Add("Mushroom");
                comboBox2.Items.Add("Picnic Hut");
                comboBox2.Items.Add("Kubo(Air-con)");
                comboBox2.Items.Add("Pavillion");
                comboBox2.Text = "Mushroom";
                roomName = "info_cottages";
            }
            if (comboBox1.Text == "Normal Rooms")
            {
                comboBox2.Visible = true;
                tbExcessRoomHour.Visible = true;
                comboBox2.Items.Clear();

                comboBox2.Items.Add("Single Cottage 2");
                comboBox2.Items.Add("Bario Makiling");
                comboBox2.Items.Add("Upper Rest House");
                comboBox2.Items.Add("Single Cottage 4");
                comboBox2.Items.Add("Sotano 4");
                comboBox2.Items.Add("Suite Room");
                comboBox2.Items.Add("Sotano 6");
                comboBox2.Items.Add("Natalies");
                comboBox2.Items.Add("Lower Rest House");
                comboBox2.Items.Add("Rest House");
                comboBox2.Items.Add("Casa Blanca");
                comboBox2.Items.Add("Bucal Rest House");
                comboBox2.Items.Add("Dormitory");
                comboBox2.Text = "Single Cottage 2";

                roomName = "info_rooms";
            }
            if (comboBox1.Text == "Private Pools")
            {
                assetsName = "private_pools";
                tbExcessRoomHour.Visible = true;
                comboBox2.Visible = false;

                roomName = "info_privatepools";
            }
            if (comboBox1.Text == "Function Halls")
            {
                tbExcessRoomHour.Visible = false;
                assetsName = "halls";
                comboBox2.Visible = false;
                roomName = "info_rooms";
            }
            _loadCHKINData();
        }
        public void _cbRooms2()
        {
            
            ///
            if (comboBox2.Text == "Mushroom")
                assetsName = "mushroom";
            if (comboBox2.Text == "Picnic Hut")
                assetsName = "picnic_hut";
            if (comboBox2.Text == "Kubo(Air-con)")
                assetsName = "kubo";
            if (comboBox2.Text == "Pavillion")
                assetsName = "pavillion";
            ///
            if (comboBox2.Text == "Single Cottage 2")
                assetsName = "single_cottage_2";
            if (comboBox2.Text == "Bario Makiling")
                assetsName = "bario_makiling";
            if (comboBox2.Text == "Upper Rest House")
                assetsName = "upper_rest_house";
            if (comboBox2.Text == "Single Cottage 4")
                assetsName = "single_cottage_4";
            if (comboBox2.Text == "Sotano 4")
                assetsName = "sotano_4";
            if (comboBox2.Text == "Suite Room")
                assetsName = "suite_room";
            if (comboBox2.Text == "Sotano 6")
                assetsName = "sotano_6";
            if (comboBox2.Text == "Natalies")
                assetsName = "natalies";
            if (comboBox2.Text == "Lower Rest House")
                assetsName = "lower_rest_house";
            if (comboBox2.Text == "Rest House")
                assetsName = "rest_house";
            if (comboBox2.Text == "Casa Blanca")
                assetsName = "casa_blanca";
            if (comboBox2.Text == "Bucal Rest House")
                assetsName = "bucal_rest_house";
            if (comboBox2.Text == "Dormitory")
                assetsName = "dormitory";
            if (comboBox2.Text == "Ilang-Ilang")
                assetsName = "ilang_ilang";
            _loadCHKINData();
        }
        ///RSV
        public void _loadCHKINData()
        {
            try
            {
                todays_checkin.Open();
                MySqlCommand mySqlCommand = todays_checkin.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.CommandText = "SELECT * FROM "+assetsName+" WHERE availability = 'not-available'";
                mySqlCommand.ExecuteNonQuery();

                mySqlDataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[2].HeaderText = "Unit";
                dataGridView1.Columns[1].HeaderText = "Guest";
                dataGridView1.Columns[3].HeaderText = "Guest ID";
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].HeaderText = "Check-Out Date-Time";
                dataGridView1.Columns[6].Visible = false;

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    transactionNo = dataRow["transacNo"].ToString();
                }
                todays_checkin.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                todays_checkin.Close();
            }
        }
        ///Get Transact No
        public void _getTransacNo()
        {
            try
            {
                transactionNo = dataGridView1.SelectedCells[4].Value.ToString();
                todays_checkin.Open();
                MySqlCommand mySqlCommand = todays_checkin.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.CommandText = "SELECT * FROM "+assetsName+" WHERE transacNo = '" + transactionNo + "'";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);
                todays_checkin.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                todays_checkin.Close();
            }
        }
        public void _loadRSVRoom()
        {
            try
            {
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
                    tbRoomUnit.Text = dataRow["roomUnit"].ToString();
                    tbPaxRange.Text = dataRow["paxRange"].ToString();
                    tbExcessChildren.Text = dataRow["kidsRange"].ToString();
                    tbExcessRoomHour.Text = dataRow["excessHour"].ToString();
                    tbExcessAdult.Text = dataRow["adultRange"].ToString();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
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
                MessageBox.Show(exc.Message);
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
                MessageBox.Show(exc.Message);
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
                    roomPrice = Convert.ToInt32(dataRow["roomRate"].ToString());
                    tbPriceAdult.Text = dataRow["adultsPrice"].ToString();
                    tbPriceChildren.Text = dataRow["kidsPrice"].ToString();
                    tbPriceAccess.Text = dataRow["accessPrice"].ToString();
                    tbPriceRental.Text = dataRow["rentalsPrice"].ToString();
                    tbPriceTotal.Text = dataRow["totalPrice"].ToString();
                    //textBox19.Text = dataRow["downPayment"].ToString();
                    tbPrice = Convert.ToInt32(dataRow["totalBalance"].ToString());
                    tbDownPay = Convert.ToInt32(dataRow["downPayment"].ToString());
                    guestChange = Convert.ToInt32(dataRow["guestChange"].ToString());
                    idunit = Convert.ToInt32(dataRow["ID"].ToString());
                    tbPriceBalance.Text = Convert.ToString(tbPrice);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
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
                    dateTimePicker1.Text = date1;
                    dateTimePicker2.MinDate = Convert.ToDateTime(date2);
                    dateTimePicker2.Text = date2;

                    tbGuestID.Text = dataRow["guestID"].ToString();
                    tbGuestName.Text = dataRow["guestName"].ToString();
                    tbTransNo.Text = dataRow["transacNo"].ToString();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbRoomType.Text = "";
            cbWavePool.Checked = false;
            cbWetnWild.Checked = false;
            _reset();

            _getTransacNo();
            _loadRSVRoom();
            _loadRSVPrice();
            _loadRSVRental();
            _loadRSVAccess();
            _loadRSVGuestTransacInfo();
            _getRoomName();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            _cbRooms1();
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            _cbRooms2();
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(tbPriceBalance.Text) <= 0)
            {
                ///Todo
                _checkReservedUnit();
                _sortPendings();
                _updateAssetAvailability();

                _updateUnitsInc();
                _updateTodaysCheckIn();
                _updateReservation();
                _updatePayment();

                _reset();
            }
            else
            {
                MessageBox.Show("There is a balance remaining.");
            }
        }

        private void tbPriceAdvance_TextChanged(object sender, EventArgs e)
        {
            _pricingTotal();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _cbRooms1();
        }
        ///On Load
        public void _CheckOut_Load(object sender, EventArgs e)
        {

            roominfoConn = new MySqlConnection("server=" + hostName + "; user=root; password= ; database=accomodation;");
            transaction_record = new MySqlConnection("server=" + hostName + "; user=root; password=; database=transaction_record;");
            todays_checkin = new MySqlConnection("server =" + hostName + "; user=root; password=; database=todays_checkin;");
            assets_availability = new MySqlConnection("server=" + hostName + "; user=root; password=; database=assets_availability;");

            cmbRental.Text = "AM";
            cmbAcess.Text = "AM";
            comboBox1.Text = "Normal Rooms";
            comboBox2.Text = "Single Cottage 2";
            btnTransNo.Enabled = false;
            _loadCHKINData();
            _reset();
        }

        private void tbPriceAdvance_Click(object sender, EventArgs e)
        {
            tbPriceAdvance.SelectAll();
        }

        private void tbPriceAdvance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void tbPriceAdvance_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char num = e.KeyChar;
            if (!char.IsDigit(num))
            {
                e.Handled = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _cbRooms2();
        }


        ///Sort Pendings
        public void _sortPendings()
        {
            try
            {
                transaction_record.Open();
                MySqlCommand command = transaction_record.CreateCommand();
                DataTable data = new DataTable();
                MySqlDataAdapter mySqlData = new MySqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                command.CommandText = "SELECT * FROM trroom WHERE roomName = '" + tbRoomName.Text + "' AND roomUnit = '" + tbRoomUnit.Text + "' AND transacStatus = 'PNDG' ORDER BY dateCheckIn ASC";
                command.ExecuteNonQuery();
                mySqlData.Fill(data);

                transaction_record.Close();
                foreach (DataRow dtr in data.Rows)
                {
                    pendings = Convert.ToInt32(data.Rows.Count.ToString());
                    transNo = dtr["transacNo"].ToString();
                }
                _updateTransStatus1();
                _updateTransStatus2();
            }
            catch (Exception exc)
            {
                transaction_record.Close();
                MessageBox.Show(exc.Message);
            }
        }
        public void _updateTransStatus1()
        {
            if (pendings > 0)
            {
                try
                {
                    transaction_record.Open();
                    MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                    mySqlCommand.CommandText = "UPDATE trroom SET transacStatus = 'RSV' WHERE transacNo='" + transNo + "'";
                    mySqlCommand.ExecuteNonQuery();
                    transaction_record.Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                    transaction_record.Close();
                }
            }
        }
        public void _updateTransStatus2()
        {
            if (pendings > 0)
            {
                try
                {
                    transaction_record.Open();
                    MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                    mySqlCommand.CommandText = "UPDATE guest_transac_info SET transacStatus = 'RSV' WHERE transacNo='" + transNo + "'";
                    mySqlCommand.ExecuteNonQuery();
                    transaction_record.Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                    transaction_record.Close();
                }
            }
        }

        ///Check Reserved Units
        public void _checkReservedUnit()
        {
            int asd1 = 0;
            int asd2 = 0;
            int asd3 = 0;
            try
            {
                transaction_record.Open();
                MySqlCommand command = transaction_record.CreateCommand();
                DataTable data = new DataTable();
                MySqlDataAdapter mySqlData = new MySqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                command.CommandText = "SELECT * FROM trroom WHERE roomName = '" + tbRoomName.Text + "' AND roomUnit = '" + tbRoomUnit.Text + "' AND transacStatus = 'RSV'";
                command.ExecuteNonQuery();
                mySqlData.Fill(data);

                transaction_record.Close();
                foreach (DataRow dtr in data.Rows)
                {
                    asd1 = Convert.ToInt32(data.Rows.Count.ToString());
                }
            }
            catch (Exception exc)
            {
                transaction_record.Close();
                MessageBox.Show(exc.Message);
            }

            try
            {
                transaction_record.Open();
                MySqlCommand command = transaction_record.CreateCommand();
                DataTable data = new DataTable();
                MySqlDataAdapter mySqlData = new MySqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                command.CommandText = "SELECT * FROM trroom WHERE roomName = '" + tbRoomName.Text + "' AND roomUnit = '" + tbRoomUnit.Text + "' AND transacStatus = 'PNDG'";
                command.ExecuteNonQuery();
                mySqlData.Fill(data);

                transaction_record.Close();
                foreach (DataRow dtr in data.Rows)
                {
                    asd2 = Convert.ToInt32(data.Rows.Count.ToString());
                }
            }
            catch (Exception exc)
            {
                transaction_record.Close();
                MessageBox.Show(exc.Message);
            }
            try
            {
                transaction_record.Open();
                MySqlCommand command = transaction_record.CreateCommand();
                DataTable data = new DataTable();
                MySqlDataAdapter mySqlData = new MySqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                command.CommandText = "SELECT * FROM trroom WHERE roomName = '" + tbRoomName.Text + "' AND roomUnit = '" + tbRoomUnit.Text + "' AND transacStatus = 'CHKIN'";
                command.ExecuteNonQuery();
                mySqlData.Fill(data);

                transaction_record.Close();
                foreach (DataRow dtr in data.Rows)
                {
                    asd3 = Convert.ToInt32(data.Rows.Count.ToString());
                }
            }
            catch (Exception exc)
            {
                transaction_record.Close();
                MessageBox.Show(exc.Message);
            }
            remainUnitReserved = asd1 + asd2 + asd3;
            //  MessageBox.Show(Convert.ToString(remainUnitReserved));
        }

        ///Update Assets Availability
        public void _updateAssetAvailability()
        {
            _assetsName();
            if (remainUnitReserved == 0)
            {
                try
                {
                    assets_availability.Open();
                    MySqlCommand mySqlCommand = assets_availability.CreateCommand();
                    mySqlCommand.CommandText = "UPDATE " + assetsName + " SET availability = 'available' WHERE unitName='" + tbRoomUnit.Text + "'";
                    mySqlCommand.ExecuteNonQuery();
                    assets_availability.Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                    assets_availability.Close();
                }
            }
            else
            {
                MessageBox.Show(Convert.ToString(remainUnitReserved));
            }
        }
    }
}
