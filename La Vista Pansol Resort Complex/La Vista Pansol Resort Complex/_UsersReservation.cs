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
    public partial class _UsersReservation : Form
    {
        public _UsersReservation()
        {
            InitializeComponent();
        }
        MySqlConnection roominfoConn;
        MySqlConnection transaction_record;
        MySqlConnection assets_availability;

        public String hostName;

        ///
        String roomName = "info_rooms";
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
        String assetsName;
        String assetsItem;
        String checkOutDate;
        String reserved;
        //String roomsName;
        int minPrice;
        DateTime checkIn;
        DateTime checkOut;
        TimeSpan countdays;
        int totalDays;
        int excessHour;


        ///Search ID
        public void _searchID()
        {
            label24.Text = "✘";
            label24.ForeColor = Color.Red;
            try
            {
                roominfoConn.Open();
                MySqlCommand mySqlCommand = roominfoConn.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.CommandText = "SELECT * FROM info_guest WHERE guestID LIKE('%" + tbGuestID.Text + "%')";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);

                //✔✘↻
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    guestName = dataRow["guestName"].ToString();
                    checkID = dataRow["guestID"].ToString();

                }
                if (tbGuestID.Text.Length == 15 && checkID == tbGuestID.Text)
                {
                    tbGuestName.Text = guestName;
                    label24.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                    label24.Text = "✔";
                    label24.ForeColor = Color.Green;
                }
                else
                {
                    if (tbGuestID.Text.Length == 15)
                    {
                        label24.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                        label24.Text = "No Guest";
                        tbGuestName.Text = "";
                        label24.ForeColor = Color.Red;
                    }
                    else
                    {
                        label24.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                        label24.Text = "✘";
                        tbGuestName.Text = "";
                        label24.ForeColor = Color.Red;
                    }
                }

                roominfoConn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                roominfoConn.Close();
            }
        }
        ///Update Assets
        public void _updateAssests()
        {
            try
            {
                assets_availability.Open();
                MySqlCommand mySqlCommand = assets_availability.CreateCommand();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.CommandText = "UPDATE " + assetsName + " SET availability = 'not-available', dateTimeAvailable = '" + dateTimePicker2.Text + "' WHERE unitName='" + tbRoomUnit.Text + "'";
                mySqlCommand.ExecuteNonQuery();
                assets_availability.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                assets_availability.Close();
            }
        }
        ///To Fields
        public void _toFields()
        {
            if (comboBox2.Text == "12 Hours")
            {
                tbExcessRoomHour.Text = "0D12H";
            }
            else
            {
                tbExcessRoomHour.Text = "0D22H";
            }
            if (remaining <= 0)
            {
                reserved = "PNDG";
                try
                {
                    if (roomName == "info_rooms")
                        _roomsFields();
                    if (roomName == "info_privatepools")
                        _privatepoolFields();
                    if (roomName == "info_halls")
                        _functionhallsFields();
                    assets_availability.Open();
                    MySqlCommand command = assets_availability.CreateCommand();
                    DataTable data = new DataTable();
                    MySqlDataAdapter mySqlData = new MySqlDataAdapter(command);
                    command.CommandText = "SELECT * FROM " + assetsName + " WHERE availability = 'not-available'  ORDER BY dateTimeAvailable DESC";
                    command.ExecuteNonQuery();
                    mySqlData.Fill(data);
                    assets_availability.Close();

                    foreach (DataRow dtr in data.Rows)
                    {
                        tbRoomUnit.Text = dtr["unitName"].ToString();
                        checkOutDate = dtr["dateTimeAvailable"].ToString();
                        dateTimePicker1.MinDate = Convert.ToDateTime(checkOutDate);
                    }

                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                    assets_availability.Close();
                }
                DialogResult dialogResult = MessageBox.Show(assetsItem + " is not availabe." + "\n " + "\n " + "Available at " + checkOutDate, "Message", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    dateTimePicker1.MinDate = Convert.ToDateTime(checkOutDate);
                    dateTimePicker2.MinDate = Convert.ToDateTime(checkOutDate);
                }
                else if (dialogResult == DialogResult.No)
                {
                    _reset();
                    dateTimePicker1.Text = Convert.ToString(DateTime.Today.Date);
                    dateTimePicker2.Text = Convert.ToString(dateTimePicker1.Text);
                    dateTimePicker2.MinDate = Convert.ToDateTime(dateTimePicker1.Text);
                }
            }
            else
            {
                reserved = "RSV";
                _reset();
                if (roomName == "info_rooms")
                    _roomsFields();
                if (roomName == "info_privatepools")
                    _privatepoolFields();
                if (roomName == "info_halls")
                    _functionhallsFields();
                try
                {
                    assets_availability.Open();
                    MySqlCommand command = assets_availability.CreateCommand();
                    DataTable data = new DataTable();
                    MySqlDataAdapter mySqlData = new MySqlDataAdapter(command);
                    command.CommandText = "SELECT * FROM " + assetsName + " WHERE availability = 'available'  ORDER BY dateTimeAvailable DESC";
                    command.ExecuteNonQuery();
                    mySqlData.Fill(data);
                    assets_availability.Close();
                    foreach (DataRow dtr in data.Rows)
                    {
                        tbRoomUnit.Text = dtr["unitName"].ToString();
                    }
                   
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                    assets_availability.Close();
                }
            }
        }

        ///Insert/Save Data
        public void _insertGuestTransactionInfo()
        {
            ///Guest
            try
            {
                transaction_record.Open();
                MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.CommandText = "INSERT INTO guest_transac_info (guestName, guestID, transacNo, dateTimeIn, dateTimeOut, transacStatus)VALUES" +
                    "('" + tbGuestName.Text + "','" + tbGuestID.Text + "','" + tbTransNo.Text + "','" + dateTimePicker1.Text + "','" + dateTimePicker2.Text + "','"+reserved+"')";
                mySqlCommand.ExecuteNonQuery();

                transaction_record.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                transaction_record.Close();
            }
            ///Room
            if (tbPriceRoom.Text == "0")
            {

            }
            else
            {
                int a = Convert.ToInt32(tbExcessAdult.Text);
                int b = Convert.ToInt32(tbExcessChildren.Text);
                int d = Convert.ToInt32(tbPaxRange.Text);
                int c = a + b;
                int e1 = c - d;
                if (e1 < 0) { e1 = 0; }
                try
                {
                    transaction_record.Open();
                    MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                    mySqlCommand.CommandText = "INSERT INTO trroom (guestID, transacNo, roomType, " +
                        "roomName, roomUnit, paxRange, kidsRange, adultRange, excessHour, excessPax, dateCheckIn, transacStatus)VALUES" +
                        "('" + tbGuestID.Text + "','" + tbTransNo.Text + "','" + tbRoomType.Text + "','" + tbRoomName.Text + "'," +
                        "'" + tbRoomUnit.Text + "','" + tbPaxRange.Text + "','" + tbExcessChildren.Text + "','" + tbExcessAdult.Text + "'," +
                        "'" + tbExcessRoomHour.Text + "','" + e1 + "','" + dateTimePicker1.Text + "','" + reserved + "')";
                    mySqlCommand.ExecuteNonQuery();
                    transaction_record.Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                    transaction_record.Close();
                }
                trRoom = tbRoomType.Text;
            }
            ///Access
            if (cbWavePool.Checked == true && cbWetnWild.Checked == true)
            { trAccess = "Package A"; }
            else if (cbWavePool.Checked == true && cbWetnWild.Checked == false)
            { trAccess = "Package BB"; }
            else if (cbWavePool.Checked == false && cbWetnWild.Checked == true)
            { trAccess = "Package B"; }
            else if (cbWavePool.Checked == false && cbWetnWild.Checked == false)
            { trAccess = "Package C"; }
            try
            {
                transaction_record.Open();
                MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.CommandText = "INSERT INTO traccess (guestID, transacNo, accessType, accessTime)VALUES" +
                    "('" + tbGuestID.Text + "','" + tbTransNo.Text + "','" + trAccess + "','" + cmbAcess.Text + "')";
                mySqlCommand.ExecuteNonQuery();

                transaction_record.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                transaction_record.Close();
            }
            ///Rental
            if (tbPriceRental.Text == "0")
            {

            }
            else
            {
                try
                {
                    if (cbVideoke.Checked == true)
                    {
                        transaction_record.Open();
                        MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                        mySqlCommand.CommandText = "INSERT INTO trrentals (guestID, transacNo, rentalName, rentalHour, rentalTime)VALUES" +
                            "('" + tbGuestID.Text + "','" + tbTransNo.Text + "','" + cbVideoke.Text + "','" + tbVideoke.Text + "','" + cmbRental.Text + "')";
                        mySqlCommand.ExecuteNonQuery();

                        transaction_record.Close();
                    }
                    if (cbBasketball.Checked == true)
                    {
                        transaction_record.Open();
                        MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                        mySqlCommand.CommandText = "INSERT INTO trrentals (guestID, transacNo, rentalName, rentalHour, rentalTime)VALUES" +
                            "('" + tbGuestID.Text + "','" + tbTransNo.Text + "','" + cbBasketball.Text + "','" + tbBasketball.Text + "','" + cmbRental.Text + "')";
                        mySqlCommand.ExecuteNonQuery();

                        transaction_record.Close();
                    }
                    if (cbVolleyball.Checked == true)
                    {
                        transaction_record.Open();
                        MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                        mySqlCommand.CommandText = "INSERT INTO trrentals (guestID, transacNo, rentalName, rentalHour, rentalTime)VALUES" +
                            "('" + tbGuestID.Text + "','" + tbTransNo.Text + "','" + cbVolleyball.Text + "','" + tbVolleyball.Text + "','" + cmbRental.Text + "')";
                        mySqlCommand.ExecuteNonQuery();

                        transaction_record.Close();
                    }
                    if (cbTennis.Checked == true)
                    {
                        transaction_record.Open();
                        MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                        mySqlCommand.CommandText = "INSERT INTO trrentals (guestID, transacNo, rentalName, rentalHour, rentalTime)VALUES" +
                            "('" + tbGuestID.Text + "','" + tbTransNo.Text + "','" + cbTennis.Text + "','" + tbTennis.Text + "','" + cmbRental.Text + "')";
                        mySqlCommand.ExecuteNonQuery();

                        transaction_record.Close();
                    }
                    if (cbBilliards.Checked == true)
                    {
                        transaction_record.Open();
                        MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                        mySqlCommand.CommandText = "INSERT INTO trrentals (guestID, transacNo, rentalName, rentalHour, rentalTime)VALUES" +
                            "('" + tbGuestID.Text + "','" + tbTransNo.Text + "','" + cbBilliards.Text + "','" + tbBilliards.Text + "','" + cmbRental.Text + "')";
                        mySqlCommand.ExecuteNonQuery();

                        transaction_record.Close();
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                    transaction_record.Close();
                }

            }
            ///Price
            if (tbPriceTotal.Text.Length > 0)
            {
                int a = Convert.ToInt32(tbPriceAdvance.Text);
                int b = Convert.ToInt32(tbPriceTotal.Text);
                int c = Convert.ToInt32(tbPriceBalance.Text);
                int d = a - b;

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
                String controlNo = comboBox1.Text + " - " + tbControlNo.Text;
                try
                {
                    transaction_record.Open();
                    MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                    mySqlCommand.CommandText = "INSERT INTO trprice(guestID, transacNo, adultsPrice, kidsPrice, " +
                        "roomRate, rentalsPrice, accessPrice, totalPrice, downPayment, totalBalance, guestChange, transacType, status)VALUES" +
                        "('" + tbGuestID.Text + "','" + tbTransNo.Text + "','" + tbPriceAdult.Text + "','" + tbPriceChildren.Text + "','" + tbPriceRoom.Text + "'" +
                        ",'" + tbPriceRental.Text + "','" + tbPriceAccess.Text + "','" + tbPriceTotal.Text + "','" + tbPriceAdvance.Text + "','" + tbPriceBalance.Text + "'" +
                        ",'" + guestChange + "','" + controlNo + "','" + paymentStatus + "')";
                    mySqlCommand.ExecuteNonQuery();

                    transaction_record.Close();
                }
                catch (Exception exc)
                {
                    transaction_record.Close();
                    MessageBox.Show(exc.Message);
                }
            }
        }
        ///Update Units
        public void _updateUnitsDec()
        {
            try
            {
                roominfoConn.Open();
                MySqlCommand mySqlCommand = roominfoConn.CreateCommand();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                remaining--;

                mySqlCommand.CommandText = "UPDATE " + roomName + " SET roomUnits=" + remaining + " WHERE ID=" + idunit + "";
                mySqlCommand.ExecuteNonQuery();
                roominfoConn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                roominfoConn.Close();
            }
        }
        public void _updateUnitsInc()
        {
            try
            {
                roominfoConn.Open();
                MySqlCommand mySqlCommand = roominfoConn.CreateCommand();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                remaining++;

                mySqlCommand.CommandText = "UPDATE " + roomName + " SET roomUnits=" + remaining + " WHERE ID=" + idunit + "";
                mySqlCommand.ExecuteNonQuery();
                roominfoConn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                roominfoConn.Close();
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
            comboBox1.Text = "BDO";
            tbControlNo.Text = "";
            cbVideoke.Checked = false;
            cbBasketball.Checked = false;
            cbVolleyball.Checked = false;
            cbTennis.Checked = false;
            cbBilliards.Checked = false;
            dateTimePicker1.MinDate = DateTime.Today.Date;
            dateTimePicker2.MinDate = DateTime.Today.Date;

            DateTime myDateTime = DateTime.Now;
            string myDateTimeString = myDateTime.ToString("yyyy-MM-dd H:mm");
            dateTimePicker1.Text = myDateTimeString;
            dateTimePicker2.Text = myDateTimeString;
            dateTimePicker1.MinDate = DateTime.Today.Date;
            dateTimePicker2.MinDate = DateTime.Today.Date;

            btnTransNo.Enabled = true;
            label24.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
            label24.Text = "✘";
            tbGuestName.Text = "";
            label24.ForeColor = Color.Red;

            if(comboBox2.Text=="12 Hours")
            {
                tbExcessRoomHour.Text = "0D12H";
            }
            else
            {
                tbExcessRoomHour.Text = "0D22H";
            }
        }

        ///View Data ///
        ///Rooms
        public void _loadRooms()
        {
            //textBox4.Text = "Rooms";
            try
            {
                roominfoConn.Open();

                MySqlCommand mySqlCommand = roominfoConn.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "SELECT * FROM " + roomName + "";
                mySqlCommand.ExecuteNonQuery();

                mySqlDataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].MinimumWidth = 120;
                dataGridView1.Columns[1].HeaderText = "Room Name";
                dataGridView1.Columns[2].HeaderText = "Units";
                dataGridView1.Columns[3].HeaderText = "Capacity";
                dataGridView1.Columns[4].HeaderText = "Price";
                dataGridView1.Columns[5].Visible = false;
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
        ///Private Pools
        public void _loadPrivatepools()
        {
            try
            {
                roominfoConn.Open();

                MySqlCommand mySqlCommand = roominfoConn.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "SELECT * FROM " + roomName + "";
                mySqlCommand.ExecuteNonQuery();

                mySqlDataAdapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;

                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[1].MinimumWidth = 120;
                dataGridView2.Columns[1].HeaderText = "Room Name";
                dataGridView2.Columns[2].HeaderText = "Units";
                dataGridView2.Columns[3].HeaderText = "Capacity";
                dataGridView2.Columns[4].HeaderText = "Price";
                dataGridView2.Columns[5].Visible = false;
                dataGridView2.Columns[6].Visible = false;
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
        ///Function Halls
        public void _loadFunctionHalls()
        {
            try
            {
                roominfoConn.Open();

                MySqlCommand mySqlCommand = roominfoConn.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "SELECT * FROM " + roomName + "";
                mySqlCommand.ExecuteNonQuery();

                mySqlDataAdapter.Fill(dataTable);
                dataGridView3.DataSource = dataTable;

                dataGridView3.Columns[0].Visible = false;
                dataGridView3.Columns[1].MinimumWidth = 180;
                dataGridView3.Columns[1].HeaderText = "Halls Name";
                dataGridView3.Columns[2].Visible = false;
                dataGridView3.Columns[3].HeaderText = "Capacity";
                dataGridView3.Columns[4].HeaderText = "Price";
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

        /// Data of Rooms to Fields ///
        ///Rooms
        public void _roomsFields()
        {
            try
            {
                roominfoConn.Open();
                int count = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

                MySqlCommand mySqlCommand = roominfoConn.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "SELECT * FROM " + roomName + " WHERE ID=" + count + "";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    remaining = Convert.ToInt32(dataRow["roomUnits"].ToString());
                    tbPriceRoom.Text = dataRow["priceAt12"].ToString();
                    tbRoomName.Text = dataRow["roomName"].ToString();

                    tbPaxRange.Text = dataRow["paxRange"].ToString();
                    idunit = Convert.ToInt32(dataRow["ID"].ToString());
                    assetsItem = dataRow["roomName"].ToString();
                    if (comboBox2.Text == "12 Hours")
                    {
                        ///Todo
                        minPrice = Convert.ToInt32(dataRow["priceAt12"].ToString());

                        if(minPrice == 0)
                        {
                            MessageBox.Show("This unit cannot be acommodate.");
                            _reset();
                        }
                        else
                        {
                            tbPriceRoom.Text = dataRow["priceAt12"].ToString();
                            tbExcessRoomHour.Text = "0D12H";
                        }
                    }
                    if (comboBox2.Text == "22 Hours")
                    {
                        minPrice = Convert.ToInt32(dataRow["priceAt22"].ToString());
                        ///Todo
                       if(minPrice == 0)
                        {

                        }
                        else
                        {
                            tbPriceRoom.Text = dataRow["priceAt22"].ToString();
                            tbExcessRoomHour.Text = "0D22H";
                        }
                    }
                }
                roominfoConn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                roominfoConn.Close();
            }
        }
        ///Private Pools
        public void _privatepoolFields()
        {
            try
            {
                roominfoConn.Open();
                int count = Convert.ToInt32(dataGridView2.SelectedCells[0].Value.ToString());
                MySqlCommand mySqlCommand = roominfoConn.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "SELECT * FROM " + roomName + " WHERE ID=" + count + "";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    remaining = Convert.ToInt32(dataRow["roomUnits"].ToString());

                    tbPriceRoom.Text = dataRow["packageA"].ToString();
                    tbRoomName.Text = dataRow["roomName"].ToString();
                    tbPaxRange.Text = dataRow["paxRange"].ToString();
                    assetsItem = dataRow["roomName"].ToString();

                    tbExcessRoomHour.Text = "0D8H";
                    idunit = Convert.ToInt32(dataRow["ID"].ToString());
                    if (comboBox5.Text == "Package A")
                    {
                        ///Todo
                       
                        minPrice = Convert.ToInt32(dataRow["packageA"].ToString());
                        if (minPrice == 0)
                        {
                            MessageBox.Show("This unit cannot be acommodate.");
                            _reset();
                        }
                        else
                        {
                            tbPriceRoom.Text = dataRow["packageA"].ToString();
                        }
                    }
                    if (comboBox5.Text == "Package B")
                    {
                       ///Todo

                        minPrice = Convert.ToInt32(dataRow["packageB"].ToString());
                        if (minPrice == 0)
                        {
                            MessageBox.Show("This unit cannot be acommodate.");
                            _reset();
                        }
                        else
                        {
                            tbPriceRoom.Text = dataRow["packageB"].ToString();
                        }
                    }
                    if (comboBox5.Text == "Package C")
                    {
                        ///Todo

                        minPrice = Convert.ToInt32(dataRow["packageC"].ToString());
                        if (minPrice == 0)
                        {
                            MessageBox.Show("This unit cannot be acommodate.");
                            _reset();
                        }
                        else
                        {
                            tbPriceRoom.Text = dataRow["packageC"].ToString();
                        }
                    }
                }
                roominfoConn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                roominfoConn.Close();
            }

        }
        ///Function Halls
        public void _functionhallsFields()
        {
            try
            {
                roominfoConn.Open();
                int count = Convert.ToInt32(dataGridView3.SelectedCells[0].Value.ToString());
                MySqlCommand mySqlCommand = roominfoConn.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "SELECT * FROM " + roomName + " WHERE ID=" + count + "";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);

                tbExcessRoomHour.Text = "0D10H";
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    remaining = Convert.ToInt32(dataRow["roomUnits"].ToString());
                    tbRoomName.Text = dataRow["roomName"].ToString();
                    assetsItem = dataRow["roomName"].ToString();
                    tbPaxRange.Text = dataRow["paxRange"].ToString();
                    idunit = Convert.ToInt32(dataRow["ID"].ToString());
                   
                    minPrice = Convert.ToInt32(dataRow["roomPrice"].ToString());

                    if (minPrice == 0)
                    {
                        MessageBox.Show("This unit cannot be acommodate.");
                        _reset();
                    }
                    else
                    {
                        tbPriceRoom.Text = dataRow["roomPrice"].ToString();
                    }
                }
                roominfoConn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                roominfoConn.Close();
            }
        }

        ///Assets
        public void _assets()
        {
            int count = 0;
            try
            {
                roominfoConn.Open();
                if (roomName == "info_rooms")
                    count = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
                if (roomName == "info_privatepools")
                    count = Convert.ToInt32(dataGridView2.SelectedCells[0].Value.ToString());
                if (roomName == "info_halls")
                    count = Convert.ToInt32(dataGridView3.SelectedCells[0].Value.ToString());


                MySqlCommand mySqlCommand = roominfoConn.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "SELECT * FROM " + roomName + " WHERE ID=" + count + "";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    assetsItem = dataRow["roomName"].ToString();
                }
                roominfoConn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                roominfoConn.Close();
            }

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
        ///Rentals AM & PM      ///
        ///AM
        public void _rentalAM()
        {
            try
            {
                roominfoConn.Open();
                MySqlCommand mySqlCommand = roominfoConn.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                int a = 0; int b = 0; int c = 0; int d = 0; int e = 0; int aa = 0;
                if (cbVideoke.Checked == true)
                {
                    mySqlCommand.CommandText = "SELECT * FROM info_rentals WHERE ID=1";
                    mySqlCommand.ExecuteNonQuery();
                    mySqlDataAdapter.Fill(dataTable);

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        
                       int pricemin = Convert.ToInt32(dataRow["amPrice"].ToString());

                        if (pricemin == 0)
                        {
                            MessageBox.Show("This couldn't be accommodate right now.");
                            cbVideoke.Checked = false;
                            tbVideoke.Text = "0";
                        }
                        else
                        {
                            if (Convert.ToInt32(tbVideoke.Text) > 12)
                            {
                                a = Convert.ToInt32(tbVideoke.Text) - 12;
                                aa = a * 100 + Convert.ToInt32(dataRow["amPrice"].ToString());
                            }
                            else
                            {
                                aa = Convert.ToInt32(dataRow["amPrice"].ToString());
                            }
                        }
                        
                    }
                }
                else
                {
                    cbVideoke.Checked = false;
                    tbVideoke.Text = "0";
                }
                if (cbBasketball.Checked == true)
                {
                    mySqlCommand.CommandText = "SELECT * FROM info_rentals WHERE ID=2";
                    mySqlCommand.ExecuteNonQuery();
                    mySqlDataAdapter.Fill(dataTable);

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        int pricemin;
                        pricemin = Convert.ToInt32(dataRow["amPrice"].ToString());

                        if (pricemin == 0)
                        {
                            MessageBox.Show("This couldn't be accommodate right now.");
                            cbBasketball.Checked = false;
                            tbBasketball.Text = "0";
                        }
                        else
                        {
                            b = Convert.ToInt32(dataRow["amPrice"].ToString()) * Convert.ToInt32(tbBasketball.Text);
                        }

                    }
                }
                else
                {
                    cbBasketball.Checked = false;
                    tbBasketball.Text = "0";
                }
                if (cbVolleyball.Checked == true)
                {
                    mySqlCommand.CommandText = "SELECT * FROM info_rentals WHERE ID=3";
                    mySqlCommand.ExecuteNonQuery();
                    mySqlDataAdapter.Fill(dataTable);

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        int pricemin;
                        pricemin = Convert.ToInt32(dataRow["amPrice"].ToString());

                        if (pricemin == 0)
                        {
                            MessageBox.Show("This couldn't be accommodate right now.");
                            cbVolleyball.Checked = false;
                            tbVolleyball.Text = "0";
                        }
                        else
                        {
                            c = Convert.ToInt32(dataRow["amPrice"].ToString()) * Convert.ToInt32(tbVolleyball.Text);
                        }

                    }
                }
                else
                {
                    cbVolleyball.Checked = false;
                    tbVolleyball.Text = "0";
                }
                if (cbTennis.Checked == true)
                {
                    mySqlCommand.CommandText = "SELECT * FROM info_rentals WHERE ID=4";
                    mySqlCommand.ExecuteNonQuery();
                    mySqlDataAdapter.Fill(dataTable);

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        int pricemin;
                        pricemin = Convert.ToInt32(dataRow["amPrice"].ToString());

                        if (pricemin == 0)
                        {
                            MessageBox.Show("This couldn't be accommodate right now.");
                            cbTennis.Checked = false;
                            tbTennis.Text = "0";
                        }
                        else
                        {
                            d = Convert.ToInt32(dataRow["amPrice"].ToString()) * Convert.ToInt32(tbTennis.Text);
                        }
                  
                    }
                }
                else
                {
                    cbTennis.Checked = false;
                    tbTennis.Text = "0";
                }
                if (cbBilliards.Checked == true)
                {
                    mySqlCommand.CommandText = "SELECT * FROM info_rentals WHERE ID=5";
                    mySqlCommand.ExecuteNonQuery();
                    mySqlDataAdapter.Fill(dataTable);

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        int pricemin;
                        pricemin = Convert.ToInt32(dataRow["amPrice"].ToString());

                        if (pricemin == 0)
                        {
                            MessageBox.Show("This couldn't be accommodate right now.");
                            cbBilliards.Checked = false;
                            tbBilliards.Text = "0";
                        }
                        else
                        {
                            e = Convert.ToInt32(dataRow["amPrice"].ToString()) * Convert.ToInt32(tbBilliards.Text);
                        }
                  
                    }
                }
                else
                {
                    cbBilliards.Checked = false;
                    tbBilliards.Text = "0";
                }

                tbPriceRental.Text = Convert.ToString(aa + b + c + d + e);
                roominfoConn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                roominfoConn.Close();
            }
        }
        ///PM
        public void _rentalPM()
        {
            try
            {
                roominfoConn.Open();
                MySqlCommand mySqlCommand = roominfoConn.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                int a = 0; int b = 0; int c = 0; int d = 0; int e = 0; int aa = 0;
                if (cbVideoke.Checked == true)
                {
                    mySqlCommand.CommandText = "SELECT * FROM info_rentals WHERE ID=1";
                    mySqlCommand.ExecuteNonQuery();
                    mySqlDataAdapter.Fill(dataTable);

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        int pricemin;
                        pricemin = Convert.ToInt32(dataRow["pmPrice"].ToString());

                        if (pricemin == 0)
                        {
                            MessageBox.Show("This couldn't be accommodate right now.");
                            cbVideoke.Checked = false;
                            tbVideoke.Text = "0";
                        }
                        else
                        {
                            if (Convert.ToInt32(tbVideoke.Text) > 12)
                            {
                                a = Convert.ToInt32(tbVideoke.Text) - 12;
                                aa = a * 100 + Convert.ToInt32(dataRow["pmPrice"].ToString());
                            }
                            else
                            {
                                aa = Convert.ToInt32(dataRow["pmPrice"].ToString());
                            }
                        }
                        
                    }
                }
                else
                {
                    cbVideoke.Checked = false;
                    tbVideoke.Text = "0";
                }
                if (cbBasketball.Checked == true)
                {
                    mySqlCommand.CommandText = "SELECT * FROM info_rentals WHERE ID=2";
                    mySqlCommand.ExecuteNonQuery();
                    mySqlDataAdapter.Fill(dataTable);

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        int pricemin;
                        pricemin = Convert.ToInt32(dataRow["pmPrice"].ToString());

                        if (pricemin == 0)
                        {
                            MessageBox.Show("This couldn't be accommodate right now.");
                            cbBasketball.Checked = false;
                            tbBasketball.Text = "0";
                        }
                        else
                        {
                            b = Convert.ToInt32(dataRow["pmPrice"].ToString()) * Convert.ToInt32(tbBasketball.Text);
                        }
                       
                    }
                }
                else
                {
                    cbBasketball.Checked = false;
                    tbBasketball.Text = "0";
                }
                if (cbVolleyball.Checked == true)
                {
                    mySqlCommand.CommandText = "SELECT * FROM info_rentals WHERE ID=3";
                    mySqlCommand.ExecuteNonQuery();
                    mySqlDataAdapter.Fill(dataTable);

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        int pricemin;
                        pricemin = Convert.ToInt32(dataRow["pmPrice"].ToString());

                        if (pricemin == 0)
                        {
                            MessageBox.Show("This couldn't be accommodate right now.");
                            cbVolleyball.Checked = false;
                            tbVolleyball.Text = "0";
                        }
                        else
                        {
                            c = Convert.ToInt32(dataRow["pmPrice"].ToString()) * Convert.ToInt32(tbVolleyball.Text);
                        }
                      
                    }
                }
                else
                {
                    cbVolleyball.Checked = false;
                    tbVolleyball.Text = "0";
                }
                if (cbTennis.Checked == true)
                {
                    mySqlCommand.CommandText = "SELECT * FROM info_rentals WHERE ID=4";
                    mySqlCommand.ExecuteNonQuery();
                    mySqlDataAdapter.Fill(dataTable);

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        int pricemin;
                        pricemin = Convert.ToInt32(dataRow["pmPrice"].ToString());

                        if (pricemin == 0)
                        {
                            MessageBox.Show("This couldn't be accommodate right now.");
                            cbTennis.Checked = false;
                            tbTennis.Text = "0";
                        }
                        else
                        {
                            d = Convert.ToInt32(dataRow["pmPrice"].ToString()) * Convert.ToInt32(tbTennis.Text);
                        }
    
                    }
                }
                else
                {
                    cbTennis.Checked = false;
                    tbTennis.Text = "0";
                }
                if (cbBilliards.Checked == true)
                {
                    mySqlCommand.CommandText = "SELECT * FROM info_rentals WHERE ID=5";
                    mySqlCommand.ExecuteNonQuery();
                    mySqlDataAdapter.Fill(dataTable);

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        int pricemin;
                        pricemin = Convert.ToInt32(dataRow["pmPrice"].ToString());

                        if (pricemin == 0)
                        {
                            MessageBox.Show("This couldn't be accommodate right now.");
                            cbBilliards.Checked = false;
                            tbBilliards.Text = "0";
                        }
                        else
                        {
                            e = Convert.ToInt32(dataRow["pmPrice"].ToString()) * Convert.ToInt32(tbBilliards.Text);
                        }
       
                    }
                }
                else
                {
                    cbBilliards.Checked = false;
                    tbBilliards.Text = "0";
                }

                tbPriceRental.Text = Convert.ToString(aa + b + c + d + e);
                roominfoConn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                roominfoConn.Close();
            }
        }

        ///Wavepool ++
        public void _WavePool()
        {
            try
            {

                roominfoConn.Open();
                MySqlCommand mySqlCommand = roominfoConn.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "SELECT * FROM info_entrance WHERE ID=" + access + "";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);
                roominfoConn.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {

                    int a = Convert.ToInt32(dataRow["adultAM"].ToString());
                    int b = Convert.ToInt32(dataRow["kidsAM"].ToString());
                    int c = Convert.ToInt32(dataRow["adultPM"].ToString());
                    int d = Convert.ToInt32(dataRow["kidsPM"].ToString());

                    int g = Convert.ToInt32(tbPriceAccess.Text);

                    if (cbWavePool.Checked == true && cbWavePool.Enabled == true && cmbAcess.Enabled == false)
                    {
                        int e = Convert.ToInt32(tbExcessAdult.Text) * a;
                        int f = Convert.ToInt32(tbExcessChildren.Text) * b;
                        WavePool = e + f;
                        WavePool = g + WavePool;
                    }

                    ///
                    if (cbWavePool.Checked == true && cbWavePool.Enabled == true
                        && cmbAcess.Enabled == true && cmbAcess.Text == "AM")
                    {
                        int e = Convert.ToInt32(tbExcessAdult.Text) * a;
                        int f = Convert.ToInt32(tbExcessChildren.Text) * b;
                        WavePool = e + f;
                        WavePool = g + WavePool;
                    }
                    ///
                    if (cbWavePool.Checked == true && cbWavePool.Enabled == true
                        && cmbAcess.Enabled == true && cmbAcess.Text == "PM")
                    {
                        int e = Convert.ToInt32(tbExcessAdult.Text) * c;
                        int f = Convert.ToInt32(tbExcessChildren.Text) * d;
                        WavePool = e + f;
                        WavePool = g + WavePool;
                    }
                    tbPriceAccess.Text = Convert.ToString(WavePool);
                    _wavePool();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                roominfoConn.Close();
            }
        }
        ///Wavepool --
        public void _wavePool()
        {
            try
            {

                roominfoConn.Open();
                MySqlCommand mySqlCommand = roominfoConn.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "SELECT * FROM info_entrance WHERE ID=" + access + "";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);
                roominfoConn.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {

                    int a = Convert.ToInt32(dataRow["adultAM"].ToString());
                    int b = Convert.ToInt32(dataRow["kidsAM"].ToString());
                    int c = Convert.ToInt32(dataRow["adultPM"].ToString());
                    int d = Convert.ToInt32(dataRow["kidsPM"].ToString());

                    int g = Convert.ToInt32(tbPriceAccess.Text);

                    if (cbWavePool.Checked == false && cbWavePool.Enabled == true && cmbAcess.Enabled == false)
                    {
                        int e = Convert.ToInt32(tbExcessAdult.Text) * a;
                        int f = Convert.ToInt32(tbExcessChildren.Text) * b;
                        WavePool = e + f;
                        WavePool = g - WavePool;
                    }
                    if (cbWavePool.Checked == false && cbWavePool.Enabled == true
                        && cmbAcess.Enabled == true && cmbAcess.Text == "AM")
                    {
                        int e = Convert.ToInt32(tbExcessAdult.Text) * a;
                        int f = Convert.ToInt32(tbExcessChildren.Text) * b;
                        WavePool = e + f;
                        WavePool = g - WavePool;
                    }
                    if (cbWavePool.Checked == false && cbWavePool.Enabled == true
                        && cmbAcess.Enabled == true && cmbAcess.Text == "PM")
                    {
                        int e = Convert.ToInt32(tbExcessAdult.Text) * c;
                        int f = Convert.ToInt32(tbExcessChildren.Text) * d;
                        WavePool = e + f;
                        WavePool = g - WavePool;
                    }
                    tbPriceAccess.Text = Convert.ToString(WavePool);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                roominfoConn.Close();
            }
        }

        ///WetnWild ++
        public void _WetnWild()
        {
            try
            {

                roominfoConn.Open();
                MySqlCommand mySqlCommand = roominfoConn.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "SELECT * FROM info_entrance WHERE ID=" + access + "";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);
                roominfoConn.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {

                    int a = Convert.ToInt32(dataRow["adultAM"].ToString());
                    int b = Convert.ToInt32(dataRow["kidsAM"].ToString());
                    int c = Convert.ToInt32(dataRow["adultPM"].ToString());
                    int d = Convert.ToInt32(dataRow["kidsPM"].ToString());

                    int g = Convert.ToInt32(tbPriceAccess.Text);

                    if (cbWetnWild.Checked == true && cbWetnWild.Enabled == true && cmbAcess.Enabled == false)
                    {
                        int e = Convert.ToInt32(tbExcessAdult.Text) * a;
                        int f = Convert.ToInt32(tbExcessChildren.Text) * b;
                        WetnWild = e + f;
                        WetnWild = g + WetnWild;
                    }

                    ///
                    if (cbWetnWild.Checked == true && cbWetnWild.Enabled == true
                        && cmbAcess.Enabled == true && cmbAcess.Text == "AM")
                    {
                        int e = Convert.ToInt32(tbExcessAdult.Text) * a;
                        int f = Convert.ToInt32(tbExcessChildren.Text) * b;
                        WetnWild = e + f;
                        WetnWild = g + WetnWild;
                    }
                    ///
                    if (cbWetnWild.Checked == true && cbWetnWild.Enabled == true
                        && cmbAcess.Enabled == true && cmbAcess.Text == "PM")
                    {
                        int e = Convert.ToInt32(tbExcessAdult.Text) * c;
                        int f = Convert.ToInt32(tbExcessChildren.Text) * d;
                        WetnWild = e + f;
                        WetnWild = g + WetnWild;
                    }
                    tbPriceAccess.Text = Convert.ToString(WetnWild);
                    _wetnWild();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                roominfoConn.Close();
            }
        }
        ///WetnWild --
        public void _wetnWild()
        {
            try
            {

                roominfoConn.Open();
                MySqlCommand mySqlCommand = roominfoConn.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "SELECT * FROM info_entrance WHERE ID=" + access + "";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);
                roominfoConn.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {

                    int a = Convert.ToInt32(dataRow["adultAM"].ToString());
                    int b = Convert.ToInt32(dataRow["kidsAM"].ToString());
                    int c = Convert.ToInt32(dataRow["adultPM"].ToString());
                    int d = Convert.ToInt32(dataRow["kidsPM"].ToString());

                    int g = Convert.ToInt32(tbPriceAccess.Text);

                    if (cbWetnWild.Checked == false && cbWetnWild.Enabled == true && cmbAcess.Enabled == false)
                    {
                        int e = Convert.ToInt32(tbExcessAdult.Text) * a;
                        int f = Convert.ToInt32(tbExcessChildren.Text) * b;
                        WetnWild = e + f;
                        WetnWild = g - WetnWild;
                    }
                    if (cbWetnWild.Checked == false && cbWetnWild.Enabled == true
                        && cmbAcess.Enabled == true && cmbAcess.Text == "AM")
                    {
                        int e = Convert.ToInt32(tbExcessAdult.Text) * a;
                        int f = Convert.ToInt32(tbExcessChildren.Text) * b;
                        WetnWild = e + f;
                        WetnWild = g - WetnWild;
                    }
                    if (cbWetnWild.Checked == false && cbWetnWild.Enabled == true
                        && cmbAcess.Enabled == true && cmbAcess.Text == "PM")
                    {
                        int e = Convert.ToInt32(tbExcessAdult.Text) * c;
                        int f = Convert.ToInt32(tbExcessChildren.Text) * d;
                        WetnWild = e + f;
                        WetnWild = g - WetnWild;
                    }
                    tbPriceAccess.Text = Convert.ToString(WetnWild);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                roominfoConn.Close();
            }
        }
        ///Entrance Price
        public void _accessPrice()
        {
            if (cbWavePool.Checked == true && cbWavePool.Enabled == true)
            {
                access = 1;
                tbPriceAccess.Text = "0";
                _WavePool();
            }
            if (cbWavePool.Checked == false && cbWavePool.Enabled == true)
            {
                tbPriceAccess.Text = "0";
            }
            if (cbWetnWild.Checked == true && cbWetnWild.Enabled == true)
            {
                access = 2;
                tbPriceAccess.Text = "0";
                _WetnWild();
            }
            if (cbWetnWild.Checked == false && cbWetnWild.Enabled == true)
            {
                tbPriceAccess.Text = "0";
            }
            if (cbWavePool.Checked == true && cbWavePool.Enabled == true
                && cbWetnWild.Checked == true && cbWetnWild.Enabled == true)
            {
                access = 3;
                tbPriceAccess.Text = "0";
                _WetnWild();
            }
            else if (cbWavePool.Checked == true && cbWavePool.Enabled == true
                && cbWetnWild.Checked == false && cbWetnWild.Enabled == true)
            {
                access = 1;
                tbPriceAccess.Text = "0";
                tbPriceAccess.Text = Convert.ToString(WavePool);
            }
            else if (cbWavePool.Checked == false && cbWavePool.Enabled == true
                && cbWetnWild.Checked == true && cbWetnWild.Enabled == true)
            {
                access = 2;
                tbPriceAccess.Text = "0";
                tbPriceAccess.Text = Convert.ToString(WetnWild);
            }
        }
        ///Pax Range
        private void _Paxrange()
        {
            try
            {
                int a = Convert.ToInt32(tbExcessAdult.Text);
                int b = Convert.ToInt32(tbExcessChildren.Text);
                int d = Convert.ToInt32(tbPaxRange.Text);
                int c = a + b;

                if (Convert.ToInt32(tbPaxRange.Text) > 0)
                {
                    if (c > d)
                    {
                        int e1 = c - d;
                        e1 = e1 * 300;
                        tbPriceAdult.Text = Convert.ToString(e1);
                    }
                    else if (c <= d)
                    {
                        tbPriceAdult.Text = "0";
                    }
                }
            }
            catch
            {

            }
        }

        ///Total/Balance/Payment/
        public void _payment()
        {
            int aa = Convert.ToInt32(tbPriceTotal.Text);
            int bb = Convert.ToInt32(tbPriceAdvance.Text);
            int cc = aa - bb;
            tbPriceBalance.Text = Convert.ToString(cc);
            if (Convert.ToInt32(tbPriceBalance.Text) < 0)
            {
                tbPriceBalance.Text = "0";
            }
        }
        ///Change Room Type
        public void _tabControl()
        {
            ///Rooms
            if (tabControl1.SelectedIndex == 0)
            {
                tabControl = 0;
                tbRoomType.Text = "Normal Room";
                roomName = "info_rooms";
                comboBox2.Text = "12 Hours";

                cbWavePool.Checked = false;
                cbWetnWild.Checked = true;
                cbWavePool.Enabled = true;
                cbWetnWild.Enabled = false;
                cmbAcess.Enabled = false;
                tbExcessRoomHour.Text = "0D12H";

                _loadRooms();
                cmbAcess.Enabled = false;
                tbPriceAccess.Text = "0";
                tbPriceAccess.Enabled = false;
                tbPriceAdult.Text = "0";
                tbPriceRoom.Text = "0";
                tbPaxRange.Text = "0";
                tbRoomName.Text = "";
                tbRoomUnit.Text = "";

                //_roomsFields();
            }
            ///Private Pools
            if (tabControl1.SelectedIndex == 1)
            {
                tabControl = 1;
                tbRoomType.Text = "Private Room";
                roomName = "info_privatepools";


                cbWavePool.Checked = true;
                cbWetnWild.Checked = true;
                cbWavePool.Enabled = false;
                cbWetnWild.Enabled = false;
                tbExcessRoomHour.Text = "0D8H";

                _loadPrivatepools();
                cmbAcess.Enabled = false;
                tbPriceAccess.Text = "0";
                tbPriceAccess.Enabled = false;
                tbPriceAdult.Text = "0";
                tbPriceRoom.Text = "0";
                tbPaxRange.Text = "0";
                tbRoomName.Text = "";
                comboBox5.Text = "Package A";
                tbRoomUnit.Text = "";
                //_privatepoolFields();
            }
            ///Function Rooms
            if (tabControl1.SelectedIndex == 2)
            {
                tabControl = 2;
                tbRoomType.Text = "Function Room";
                roomName = "info_halls";

                cbWavePool.Checked = false;
                cbWetnWild.Checked = false;
                cbWavePool.Enabled = true;
                cbWetnWild.Enabled = true;
                tbExcessRoomHour.Text = "0D10H";

                _loadFunctionHalls();
                cmbAcess.Enabled = true;
                tbPriceAccess.Text = "0";
                tbPriceAccess.Enabled = false;
                tbPriceAdult.Text = "0";
                tbPriceRoom.Text = "0";
                tbPaxRange.Text = "0";
                tbRoomName.Text = "";
                tbRoomUnit.Text = "";
                //_functionhallsFields();
            }
        }
        ///On Load
        public void _Reservation_Load(object sender, EventArgs e)
        {
            roominfoConn = new MySqlConnection("server=" + hostName + "; user=root; password= ; database=accomodation;");
            assets_availability = new MySqlConnection("server=" + hostName + "; user=root; password=; database=assets_availability;");
            transaction_record = new MySqlConnection("server=" + hostName + "; user=root; password=; database=transaction_record;");

            _reset();
            tabControl1.SelectedIndex = 0;
            _selectedTab();
            dateTimePicker1.MinDate = DateTime.Today.Date;
            dateTimePicker2.MinDate = DateTime.Today.Date;
            cbWavePool.Checked = false;
            cbWetnWild.Checked = true;
            cbWavePool.Enabled = true;
            cbWetnWild.Enabled = false;
            cmbAcess.Enabled = false;
            cmbRental.Text = "AM";
            cmbAcess.Text = "AM";
            tbPaxRange.Text = "0";
            comboBox2.Text = "12 Hours";

            tbRoomType.Text = "Normal Rooms";
            tbRoomName.Text = "";
            tbRoomUnit.Text = "";
            tbPaxRange.Text = "0";
            
        }
        ///Tab Control
        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            _tabControl();
        }

        ///Pricing            ///
        ///Rentals
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRental.Text == "AM")
            {
                _rentalAM();
            }
            if (cmbRental.Text == "PM")
            {
                _rentalPM();
            }
        }
        ///Rooms
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "12 Hours")
            {
                dataGridView1.Columns[4].Visible = true;
                dataGridView1.Columns[4].HeaderText = "Price";
                dataGridView1.Columns[5].Visible = false;
            }
            if (comboBox2.Text == "22 Hours")
            {
                dataGridView1.Columns[5].Visible = true;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].HeaderText = "Price";
            }
            _roomsFields();
        }
        ///Private Pools
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text == "Package A")
            {
                dataGridView2.Columns[4].Visible = true;
                dataGridView2.Columns[4].HeaderText = "Price";
                dataGridView2.Columns[5].Visible = false;
                dataGridView2.Columns[6].Visible = false;
                trAccess = comboBox5.Text;
                cbWavePool.Checked = true;
                cbWetnWild.Checked = true;
                cbWavePool.Enabled = false;
                cbWetnWild.Enabled = false;
            }
            if (comboBox5.Text == "Package B")
            {
                dataGridView2.Columns[5].Visible = true;
                dataGridView2.Columns[5].HeaderText = "Price";
                dataGridView2.Columns[4].Visible = false;
                dataGridView2.Columns[6].Visible = false;
                trAccess = comboBox5.Text;
                cbWavePool.Checked = false;
                cbWetnWild.Checked = true;
                cbWavePool.Enabled = false;
                cbWetnWild.Enabled = false;
            }
            if (comboBox5.Text == "Package C")
            {
                dataGridView2.Columns[6].Visible = true;
                dataGridView2.Columns[6].HeaderText = "Price";
                dataGridView2.Columns[4].Visible = false;
                dataGridView2.Columns[5].Visible = false;
                trAccess = comboBox5.Text;
                cbWavePool.Checked = false;
                cbWetnWild.Checked = false;
                cbWavePool.Enabled = false;
                cbWetnWild.Enabled = false;
            }
            _privatepoolFields();
        }
        ///Access
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            _accessPrice();
        }

        ///Rooms - Private Pools - Halls ///
        ///Rooms
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _assets();
            _roomsFields();
            _toFields();
        }
        ///Private Pools
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _assets();
            _privatepoolFields();
            _toFields();
        }
        ///Function Halls
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _assets();
            _functionhallsFields();
            _toFields();
        }

        ///Videoke
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            tbVideoke.Text = "12";
            if (cmbRental.Text == "AM")
            {
                _rentalAM();
            }
            if (cmbRental.Text == "PM")
            {
                _rentalPM();
            }
        }
        ///Basketball Court
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            tbBasketball.Text = "1";
            if (cmbRental.Text == "AM")
            {
                _rentalAM();
            }
            if (cmbRental.Text == "PM")
            {
                _rentalPM();
            }
        }
        ///Volleyball Court
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            tbVolleyball.Text = "1";
            if (cmbRental.Text == "AM")
            {
                _rentalAM();
            }
            if (cmbRental.Text == "PM")
            {
                _rentalPM();
            }
        }
        ///Table Tennis
        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            tbTennis.Text = "1";
            if (cmbRental.Text == "AM")
            {
                _rentalAM();
            }
            if (cmbRental.Text == "PM")
            {
                _rentalPM();
            }
        }
        ///Billiards
        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            tbBilliards.Text = "1";
            if (cmbRental.Text == "AM")
            {
                _rentalAM();
            }
            if (cmbRental.Text == "PM")
            {
                _rentalPM();
            }
        }
        ///Pax Range
        private void textBox14_KeyUp(object sender, KeyEventArgs e)
        {
            _Paxrange();
            _accessPrice();
        }

        ///Room Rate
        ///Rental Rate
        private void textBox7_KeyUp(object sender, KeyEventArgs e)
        {
            if (cmbRental.Text == "AM")
            {
                _rentalAM();
            }
            if (cmbRental.Text == "PM")
            {
                _rentalPM();
            }
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            _Paxrange();
        }
        //private void textBox23_TextChanged(object sender, EventArgs e)
        //{
        //    if (tabControl == 0)
        //    {
        //        _roomsFields();
        //    }
        //    if (tabControl == 1)
        //    {
        //        _privatepoolFields();
        //    }
        //    if (tabControl == 2)
        //    {
        //        _functionhallsFields();
        //    }
        //}

        ///Numbers Only
        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char num = e.KeyChar;
            if (!char.IsDigit(num))
            {
                e.Handled = true;
            }
        }
        ///Prevent DELETE Button
        private void textBox15_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //From generator.cs File
            String transactNo = transacNo.newTransacNo(20);
            tbTransNo.Text = transactNo;
            btnTransNo.Enabled = false;
        }

        ///Search ID
        private void tbId_KeyUp(object sender, KeyEventArgs e)
        {
            _searchID();
        }

        //////////////////////////////////////////////////////////////////////
        private void textBox23_Click(object sender, EventArgs e)
        {
            tbExcessRoomHour.SelectAll();
        }
        private void textBox13_Click(object sender, EventArgs e)
        {
            tbExcessAdult.SelectAll();
        }
        private void textBox14_Click(object sender, EventArgs e)
        {
            tbExcessChildren.SelectAll();
        }
        private void textBox21_Click(object sender, EventArgs e)
        {
            tbPriceAdvance.SelectAll();
        }
        private void textBox7_Click(object sender, EventArgs e)
        {
            tbVideoke.SelectAll();
        }
        private void textBox8_Click(object sender, EventArgs e)
        {
            tbBasketball.SelectAll();
        }
        private void textBox9_Click(object sender, EventArgs e)
        {
            tbVolleyball.SelectAll();
        }
        private void textBox10_Click(object sender, EventArgs e)
        {
            tbTennis.SelectAll();
        }
        private void tbId_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Char num = e.KeyChar;
            //if (!char.IsDigit(num))
            //{
            //    e.Handled = true;
            //}
            label24.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
            label24.Text = "↻";
            label24.ForeColor = Color.Gray;
        }

        ///Total Price
        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            int aa = Convert.ToInt32(tbPriceRoom.Text);
            int bb = Convert.ToInt32(tbPriceAdult.Text);
            int cc = Convert.ToInt32(tbPriceChildren.Text);
            int dd = Convert.ToInt32(tbPriceAccess.Text);
            int ee = Convert.ToInt32(tbPriceRental.Text);
            int eee = aa + bb + cc + dd + ee;

            tbPriceTotal.Text = Convert.ToString(eee);
        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {
            _payment();
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            _payment();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today.Date;
            dateTimePicker2.MinDate = Convert.ToDateTime(dateTimePicker1.Text);
            dateTimePicker2.Text = Convert.ToString(dateTimePicker1.Text);
        }

        ///Entrance Fee     ///
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            _accessPrice();
        }

        private void textBox11_Click(object sender, EventArgs e)
        {
            tbBilliards.SelectAll();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            int b = Convert.ToInt32(tbPriceTotal.Text);
            int c = Convert.ToInt32(tbExcessChildren.Text);
            int d = Convert.ToInt32(tbExcessAdult.Text);

            if (label24.Text == "✔" && tbTransNo.Text.Length > 0 && b > 0 && tbControlNo.Text.Length >= 8 && tbRoomUnit.Text.Length > 0)
            {
                if (c > 0 || d > 0)
                {
                    if(dateTimePicker1.Text == dateTimePicker2.Text)
                    {
                        MessageBox.Show("Check the date before proceeding", "Prompt");
                    }
                    else
                    {
                        _updateUnitsDec();
                        _updateAssests();
                        _insertGuestTransactionInfo();
                        MessageBox.Show("Transaction completed.", "Prompt");
                        _selectedTab();
                        _reset();
                    }
                }
                else
                {
                    MessageBox.Show("Kindly check your reservation form.", "Prompt");
                }
            }
            else
            {
                MessageBox.Show("Kindly check your reservation form.", "Prompt");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _reset();
            _selectedTab();
        }

        private void tbPriceRoom_MouseClick(object sender, MouseEventArgs e)
        {
            tbPriceRoom.SelectAll();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTime checkIn1 = Convert.ToDateTime(dateTimePicker1.Text);
            DateTime checkOut1 = Convert.ToDateTime(dateTimePicker2.Text);
            if (checkOut1< checkIn1)
            {
                dateTimePicker2.MinDate = dateTimePicker1.MinDate;
                dateTimePicker2.Text = Convert.ToString(dateTimePicker1.Text);
            }
            _countDateTimePrice();

        }
        ///Date Time Price
        public void _countDateTimePrice()
        {
            checkIn = Convert.ToDateTime(dateTimePicker1.Text);
            checkOut = Convert.ToDateTime(dateTimePicker2.Text);

            countdays = checkOut - checkIn;

            totalDays = countdays.Days;
            excessHour = countdays.Hours;

            _countingPrice();
        }

        ///Counting Price
        public void _countingPrice()
        {
            if (tabControl1.SelectedIndex == 0)
            {
                if(totalDays > 0)
                {
                    if (comboBox2.Text == "12 Hours")
                    {
                        ///
                        int sum1 = totalDays * 24;
                        int sum2 = sum1 + excessHour;
                        int sum3 = sum2 / 12;
                        ///
                        int total1 = sum3 * minPrice;
                        int sum4 = sum2 - sum1;
                        int total2 = sum4 * 350;
                        ///
                        int total3 = total1 + total2;
                        tbPriceRoom.Text = Convert.ToString(total3);
                        tbExcessRoomHour.Text = Convert.ToString(totalDays) + "D" + Convert.ToString(sum3) + "H";
                    }
                    else if (comboBox2.Text == "22 Hours")
                    {
                        ///
                        int sum1 = totalDays * 24;
                        int sum2 = sum1 + excessHour;
                        int sum3 = sum2 / 22;
                        int total1 = sum3 * minPrice;
                        ///
                        int sum4 = sum2 - sum1;
                        int total2 = sum4 * 350;
                        ///
                        int total3 = total1 + total2;
                        tbPriceRoom.Text = Convert.ToString(total3);
                    }
                }
                else
                {
                    if(excessHour >= 12)
                    {
                        ///
                        int sum1 = excessHour - 12;
                        int sum2 = sum1 * 350;
                        int total1 = sum2 + minPrice;
                        tbPriceRoom.Text = Convert.ToString(total1);
                    }
                }
                tbExcessRoomHour.Text = Convert.ToString(totalDays) + "D" + Convert.ToString(excessHour) + "H";
            }
            if (tabControl1.SelectedIndex == 1)
            {
                if (totalDays > 0)
                {
                    ///
                    int sum1 = totalDays * 24;
                    int sum2 = sum1 + excessHour;
                    int sum3 = sum2 / 8;
                    int total1 = sum3 * minPrice;
                    ///
                    int sum4 = sum2 - sum1;
                    int total2 = sum4 * 350;
                    ///
                    int total3 = total1 + total2;
                    tbPriceRoom.Text = Convert.ToString(total3);
                }
                else
                {
                    if (excessHour >= 8)
                    {
                        ///
                        int sum1 = excessHour / 8;
                        int total1 = sum1 * minPrice;
                        ///
                        int sum2 = sum1 * 8;
                        int sum3 = excessHour - sum2;
                        int total2 = sum3 * 350;
                        ///
                        int total3 = total1 + total2;
                        tbPriceRoom.Text = Convert.ToString(total3);
                    }
                }
                tbExcessRoomHour.Text = Convert.ToString(totalDays) + "D" + Convert.ToString(excessHour) + "H";
            }
            if (tabControl1.SelectedIndex == 2)
            {
                if (totalDays > 0)
                {
                    ///
                    int sum1 = totalDays * 24;
                    int sum2 = sum1 + excessHour;
                    int sum3 = sum2 / 10;
                    int total1 = sum3 * minPrice;
                    tbPriceRoom.Text = Convert.ToString(total1);
                }
                else
                {
                    if (excessHour >= 10)
                    {
                        ///
                        int sum1 = excessHour / 10;
                        int total1 = sum1 * minPrice;
                        tbPriceRoom.Text = Convert.ToString(total1);
                    }
                }
                tbExcessRoomHour.Text = Convert.ToString(totalDays) + "D" + Convert.ToString(excessHour) + "H";
            }
        }

        ///
        public void _selectedTab()
        {
            if(tabControl1.SelectedIndex == 0)
            {
                roomName = "info_rooms";
                _loadRooms();
            }
            if (tabControl1.SelectedIndex == 1)
            {
                roomName = "info_privatepools";
                _loadPrivatepools();
            }
            if (tabControl1.SelectedIndex == 2)
            {
                roomName = "info_halls";
                _loadFunctionHalls();
            }
        }
    }
}
