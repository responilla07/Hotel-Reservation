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
    public partial class _UsersCheckIn : Form
    {
        public _UsersCheckIn()
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
        int cottagePrice =1;
        int tbPrice;
        int tbDownPay;
        int additonalPay;
        String excessHours;
        String transNo;
        int downPaid;
        int pendings;
        int remainUnitReserved;
        String reserved;
        bool isAvailable;

        String asdTransNo;
        //String timeIn, timeOut;



        ///Update Who is Check-in
        public void _updateTodaysCheckIn()
        {
            _assetsName();
            try
            {
                todays_checkin.Open();
                MySqlCommand mySqlCommand = todays_checkin.CreateCommand();
                mySqlCommand.CommandText = "UPDATE " + assetsName + " SET guestName = '" + tbGuestName.Text + "', guestID = '" + tbGuestID.Text + "', transacNo = '" + tbTransNo.Text + "', dateTimeCheckOut = '" + dateTimePicker2.Text + "', availability = 'not-available'  WHERE unitName='" + tbRoomUnit.Text + "'";
                mySqlCommand.ExecuteNonQuery();
                todays_checkin.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                todays_checkin.Close();
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
        ///Update Price
        public void _updatePrice()
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
                    "guestChange = " + guestChange + ", status = '" +paymentStatus+ "' WHERE ID=" + idunit + "";
                mySqlCommand.ExecuteNonQuery();
                transaction_record.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                transaction_record.Close();
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
                    "('" + tbGuestName.Text + "','" + tbGuestID.Text + "','" + tbTransNo.Text + "','" + dateTimePicker1.Text + "','" + dateTimePicker2.Text + "','" + reserved + "')";
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
                String controlNo = "Cash" + tbPriceAdvance.Text;
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
        ///Assets
        public void _assets()
        {
            int count = 0;
            try
            {
                roominfoConn.Open();
                count = Convert.ToInt32(dataGridView2.SelectedCells[0].Value.ToString());

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
            _assetsName();
        }
        ///To Fields
        public void _toFields()
        {
            if (remaining <= 0)
            {
                //reserved = "PNDG";
                try
                {
                    _cottagesFields();
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
                _reset();
                MessageBox.Show("There is no available unit for this time" +"\n" +"\n"+ assetsItem + " is not availabe." + "\n " + "\n " + "Available at " + checkOutDate);
                //DialogResult dialogResult = MessageBox.Show(assetsItem + " is not availabe." + "\n " + "\n " + "Available at " + checkOutDate, "Message", MessageBoxButtons.YesNo);
                //if (dialogResult == DialogResult.Yes)
                //{
                //    dateTimePicker1.MinDate = Convert.ToDateTime(checkOutDate);
                //    dateTimePicker2.MinDate = Convert.ToDateTime(checkOutDate);
                //}
                //else if (dialogResult == DialogResult.No)
                //{
                //    _reset();
                //    dateTimePicker1.Text = Convert.ToString(DateTime.Today.Date);
                //    dateTimePicker2.Text = Convert.ToString(dateTimePicker1.Text);
                //}
            }
            else
            {
                reserved = "CHKIN";
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
        ///View Data ///
        ///Coattages
        public void _loadCottages()
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
                dataGridView2.Columns[1].MinimumWidth = 250;
                dataGridView2.Columns[1].HeaderText = "Cottages Name";
                dataGridView2.Columns[2].HeaderText = "Units";
                dataGridView2.Columns[5].Visible = false;
                dataGridView2.Columns[3].HeaderText = "Capacity";
                dataGridView2.Columns[4].HeaderText = "Price";

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
        public void _Paxrange()
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

        /// Data of Rooms to Fields ///

        ///Cottategs
        public void _cottagesFields()
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
                    tbRoomName.Text = dataRow["roomName"].ToString();
                    tbPaxRange.Text = dataRow["paxRange"].ToString();
                    idunit = Convert.ToInt32(dataRow["ID"].ToString());
                    assetsItem = dataRow["roomName"].ToString();
                    if (comboBox4.Text == "AM")
                    {
                        if (Convert.ToInt32(tbExcessRoomHour.Text) > 0)
                        {
                            int a = Convert.ToInt32(dataRow["amPrice"].ToString());
                            int b = Convert.ToInt32(tbExcessRoomHour.Text) * 350;

                            if (a == 0)
                            {
                                MessageBox.Show("Cannot be accomodate.");
                                _reset();
                                a = 1;
                            }
                            else
                            {
                                int c = a + b;
                                tbPriceRoom.Text = Convert.ToString(c);
                            }
                        }
                        else
                        {
                            tbPriceRoom.Text = dataRow["amPrice"].ToString();
                        }
                    }
                    if (comboBox4.Text == "PM")
                    {
                        if (Convert.ToInt32(tbExcessRoomHour.Text) > 0)
                        {
                            cottagePrice = Convert.ToInt32(dataRow["pmPrice"].ToString());
                            int b = Convert.ToInt32(tbExcessRoomHour.Text) * 350;

                            if (cottagePrice == 0)
                            {
                                MessageBox.Show("Cannot be accomodate.");
                                _reset();
                                cottagePrice = 1;
                            }
                            else
                            {
                                int c = cottagePrice + b;
                                tbPriceRoom.Text = Convert.ToString(c);
                            }
                        }
                        else
                        {
                           cottagePrice = Convert.ToInt32(dataRow["pmPrice"].ToString());
                            if (cottagePrice == 0)
                            {
                                MessageBox.Show("Cannot be accomodate.");
                                _reset();
                                cottagePrice = 1;
                            }
                            else
                            {
                                tbPriceRoom.Text = Convert.ToString(cottagePrice);
                            }
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
        ///Update Reservation
        public void _updateReservationCHKIN1()
        {
            try
            {
                transaction_record.Open();
                MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "UPDATE guest_transac_info SET transacStatus='CHKIN' WHERE transacNo='" + tbTransNo.Text + "'";
                mySqlCommand.ExecuteNonQuery();
                transaction_record.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                transaction_record.Close();
            }
        }
        ///Update Reservation
        public void _updateReservationCHKIN2()
        {
            try
            {
                transaction_record.Open();
                MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "UPDATE trroom SET transacStatus='CHKIN' WHERE transacNo='" + tbTransNo.Text + "'";
                mySqlCommand.ExecuteNonQuery();
                transaction_record.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                transaction_record.Close();
            }
        }

        ///Update Cancel
        public void _updateReservationCNCL1()
        {
            try
            {
                transaction_record.Open();
                MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "UPDATE guest_transac_info SET transacStatus='CNCL' WHERE transacNo='" + asdTransNo + "'";
                mySqlCommand.ExecuteNonQuery();
                transaction_record.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                transaction_record.Close();
            }
        }
        public void _updateReservationCNCL2()
        {
            try
            {
                transaction_record.Open();
                MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "UPDATE trroom SET transacStatus='CNCL' WHERE transacNo='" + asdTransNo + "'";
                mySqlCommand.ExecuteNonQuery();
                transaction_record.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                transaction_record.Close();
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
        ///Update Room Unit
        public void _updateUnitsInc()
        {
            try
            {
                roominfoConn.Open();
                MySqlCommand mySqlCommand = roominfoConn.CreateCommand();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                
                if (tbRoomType.Text == "Normal Rooms")
                {
                    roomName = "info_rooms";
                }
                if (tbRoomType.Text == "Private Pools")
                {
                    roomName = "info_privatepools";
                }
                if (tbRoomType.Text == "Function Halls")
                {
                    roomName = "info_halls";
                }
                remaining++;
                mySqlCommand.CommandText = "UPDATE " + roomName + " SET roomUnits=" + remaining + " WHERE roomName='" + tbRoomName.Text + "'";
                mySqlCommand.ExecuteNonQuery();
                roominfoConn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                roominfoConn.Close();
            }
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
                        if (Convert.ToInt32(tbVideoke.Text) > 12)
                        {
                            a = Convert.ToInt32(tbVideoke.Text) - 12;
                            aa = a * 100 + Convert.ToInt32(dataRow["amPrice"].ToString());
                        }
                        else
                        {
                            aa = Convert.ToInt32(dataRow["pmPrice"].ToString());
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
                        b = Convert.ToInt32(dataRow["amPrice"].ToString()) * Convert.ToInt32(tbBasketball.Text);
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
                        c = Convert.ToInt32(dataRow["amPrice"].ToString()) * Convert.ToInt32(tbVolleyball.Text);
                    }
                }
                else
                {
                    cbVolleyball.Checked = false;
                    tbVolleyball.Text = "0";
                }
                if (cbTennis.Checked == true)
                {
                    mySqlCommand.CommandText = "SELECT * FROM info_rentals  WHERE ID=4";
                    mySqlCommand.ExecuteNonQuery();
                    mySqlDataAdapter.Fill(dataTable);

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        d = Convert.ToInt32(dataRow["amPrice"].ToString()) * Convert.ToInt32(tbTennis.Text);
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
                        e = Convert.ToInt32(dataRow["amPrice"].ToString()) * Convert.ToInt32(tbBilliards.Text);
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
                        b = Convert.ToInt32(dataRow["pmPrice"].ToString()) * Convert.ToInt32(tbBasketball.Text);
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
                        c = Convert.ToInt32(dataRow["pmPrice"].ToString()) * Convert.ToInt32(tbVolleyball.Text);
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
                        d = Convert.ToInt32(dataRow["pmPrice"].ToString()) * Convert.ToInt32(tbTennis.Text);
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
                        e = Convert.ToInt32(dataRow["pmPrice"].ToString()) * Convert.ToInt32(tbBilliards.Text);
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

        ///
        
        public void _comboBox4TextChange()
        {
            if (roomName == "info_cottages")
            {
                if (comboBox4.Text == "AM")
                {
                    dataGridView2.Columns[4].Visible = true;
                    dataGridView2.Columns[4].HeaderText = "Price";
                    dataGridView2.Columns[5].Visible = false;
                }
                if (comboBox4.Text == "PM")
                {
                    dataGridView2.Columns[5].Visible = true;
                    dataGridView2.Columns[5].HeaderText = "Price";
                    dataGridView2.Columns[4].Visible = false;
                }
                _cottagesFields();
            }
            
        }
        ///Tab Control
        public void _tabControl()
        {
            if (tabControl1.SelectedIndex == 0)
            {
                _reset();
                tbGuestID.Enabled = false;
                tbGuestName.Enabled = false;
                tbTransNo.Enabled = false;
                tbVideoke.Enabled = false;
                tbBasketball.Enabled = false;
                tbVolleyball.Enabled = false;
                tbTennis.Enabled = false;
                tbBilliards.Enabled = false;
                tbRoomType.Enabled = false;
                tbRoomName.Enabled = false;
                tbRoomUnit.Enabled = false;
                tbPaxRange.Enabled = false;
                tbExcessRoomHour.Enabled = false;
                tbExcessAdult.Enabled = false;
                tbExcessChildren.Enabled = false;
                tbPriceAdult.Enabled = false;
                tbPriceChildren.Enabled = false;
                tbPriceAccess.Enabled = false;
                tbPriceRental.Enabled = false;
                tbPriceTotal.Enabled = false;
                tbPriceBalance.Enabled = false;
                tbPriceAdvance.Enabled = true;
                cbBasketball.Enabled = false;
                cbBilliards.Enabled = false;
                cbTennis.Enabled = false;
                cbVideoke.Enabled = false;
                cbVolleyball.Enabled = false;
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                cmbAcess.Enabled = false;
                cmbRental.Enabled = false;
                cbWavePool.Enabled = false;
                cbWetnWild.Enabled = false;
                cbWetnWild.Checked = false;
                cbWavePool.Checked = false;
                label24.Visible = false;
                btnTransNo.Visible = false;

                btnCancel.SetBounds(3, 3, 110, 22);
                btnCancel.Text = "Cancel Reservation";
                tbRoomType.Text = "";
                tbRoomUnit.Text = "";
                tbPaxRange.Text = "";
                tbRoomName.Text = "";
                _loadRSVData();
            }
            if (tabControl1.SelectedIndex == 1)
            {
                _reset();
                tbRoomType.Text = "Cottages";
                tbRoomUnit.Text = "";
                tbPaxRange.Text = "";
                tbRoomName.Text = "";
                _loadCottages();
                comboBox4.Items.Remove("12 Hours");
                comboBox4.Items.Remove("22 Hours");
                comboBox4.Items.Remove("Package A");
                comboBox4.Items.Remove("Package B");
                comboBox4.Items.Remove("Package C");
                comboBox4.Text = "AM";
                comboBox4.Enabled = true;
                label24.Visible = true;
                btnTransNo.Visible = true;

                tbGuestID.Enabled = true;
                tbVideoke.Enabled = true;
                tbBasketball.Enabled = true;
                tbVolleyball.Enabled = true;
                tbTennis.Enabled = true;
                tbBilliards.Enabled = true;
                tbExcessAdult.Enabled = true;
                tbExcessChildren.Enabled = true;
                cbWavePool.Enabled = true;
                cbWetnWild.Enabled = true;
                cbWavePool.Checked = false;
                cbWetnWild.Checked = false;
                cbVideoke.Enabled = true;
                cbBasketball.Enabled = true;
                cbVolleyball.Enabled = true;
                cbTennis.Enabled = true;
                cbBilliards.Enabled = true;
                cmbRental.Enabled = true;
                cmbAcess.Enabled = true;

                btnCancel.SetBounds(3, 3, 102, 22);
                btnCancel.Text = "Cancel";
            }
        }
        ///RSV
        public void _loadRSVData()
        {
            try
            {
                transaction_record.Open();
                MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.CommandText = "SELECT * FROM guest_transac_info WHERE transacStatus = 'RSV'";
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


                foreach(DataRow dataRow in dataTable.Rows)
                {
                    transactionNo = dataRow["transacNo"].ToString();
                    //dateTimePicker1.Text = dataRow["dateTimeIn"].ToString();
                    //dateTimePicker2.Text = dataRow["dateTimeOut"].ToString();
                }
                transaction_record.Close();
            }
            catch(Exception exc)
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
                mySqlCommand.CommandText = "SELECT * FROM trroom WHERE transacNo = '"+transactionNo+"'";
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
                    if(rentalName=="Basketball Court")
                    {
                        cbBasketball.Checked = true;
                        tbBasketball.Text = rentalHour;
                    }
                    if(rentalName=="Volleyball Court")
                    {
                        cbVolleyball.Checked = true;
                        tbVolleyball.Text = rentalHour;
                    }
                    if(rentalName=="Table Tennis")
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
                    if(packageAccess=="Package A")
                    {
                        cbWavePool.Checked = true;
                        cbWetnWild.Checked = true;
                    }
                    if(packageAccess=="Package BB")
                    {
                        cbWavePool.Checked = true;
                    }
                    if(packageAccess=="Package B")
                    {
                        cbWetnWild.Checked = true;
                    }
                    if(packageAccess=="Package C")
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
                    downPaid = Convert.ToInt32(dataRow["downPayment"].ToString());
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
                    dateTimePicker2.MinDate = Convert.ToDateTime(date2);

                    tbGuestID.Text = dataRow["guestID"].ToString();
                    tbGuestName.Text = dataRow["guestName"].ToString();
                    tbTransNo.Text = dataRow["transacNo"].ToString();
                    asdTransNo = dataRow["transacNo"].ToString();
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
            dateTimePicker2.MinDate = DateTime.Today.Date;
            label24.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
            label24.Text = "✘";
            tbGuestName.Text = "";
            label24.ForeColor = Color.Red;
            if (tabControl1.SelectedIndex == 0)
            {
                btnTransNo.Enabled = false;
            }
            else
            {
                btnTransNo.Enabled = true;
            }
        }
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
        ///Search
        public void _searchRSV()
        {
            try
            {
                transaction_record.Open();
                MySqlCommand mySqlCommand = transaction_record.CreateCommand();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                DataTable dataTable = new DataTable();
                mySqlCommand.CommandText = "SELECT * FROM guest_transac_info WHERE transacStatus = 'RSV' AND guestID LIKE ('%" + textBox24.Text + "%')";
                mySqlCommand.ExecuteNonQuery();

                mySqlDataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                transaction_record.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                transaction_record.Close();
            }
        }
        ///On Load
        public void _CheckIn_Load(object sender, EventArgs e)
        {
            roominfoConn = new MySqlConnection("server=" + hostName + "; user=root; password= ; database=accomodation;");
            transaction_record = new MySqlConnection("server=" + hostName + "; user=root; password=; database=transaction_record;");
            todays_checkin = new MySqlConnection("server =" + hostName + "; user=root; password=; database=todays_checkin;");
            assets_availability = new MySqlConnection("server=" + hostName + "; user=root; password=; database=assets_availability;");

            tabControl1.SelectedIndex = 0;
            btnCancel.SetBounds(3, 3, 110, 22);
            btnCancel.Text = "Cancel Reservation";

            cmbRental.Text = "AM";
            cmbAcess.Text = "AM";
            btnTransNo.Enabled = false;
            _loadRSVData();
            _tabControl();
        }
        //Data Grid View
        public void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            tbRoomType.Text = "";
            cbWavePool.Checked = false;
            cbWetnWild.Checked = false;
            _reset();

            _loadRSVRoom();
            _loadRSVPrice();
            _loadRSVRental();
            _loadRSVAccess();
            _roomsFields();
            _assetsName();
            _loadRSVGuestTransacInfo();
            _checkRoomTodayAvailability();

            ///Todo
            ///_checkReservedUnit();
            _sortPendings();
            
            

        }
        public void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _assets();
            _cottagesFields();
            _toFields();
        }

        public void textBox7_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true;
            }
        }
        public void textBox7_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Char num = e.KeyChar;
            if (!char.IsDigit(num))
            {
                e.Handled = true;
            }
        }
        public void textBox7_KeyUp(object sender, KeyEventArgs e)
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
        //Combo Box
        public void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            _comboBox4TextChange();
        }
        public void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            _accessPrice();
        }
        public void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            _accessPrice();
        }
        public void textBox23_KeyUp(object sender, KeyEventArgs e)
        {
            _Paxrange();
            _accessPrice();
        }

        public void btnCheckIn_Click(object sender, EventArgs e)
        {
            int b = Convert.ToInt32(tbPriceTotal.Text);
            int c = Convert.ToInt32(tbExcessChildren.Text);
            int d = Convert.ToInt32(tbExcessAdult.Text);
            if (tabControl1.SelectedIndex == 0)
            {
                if(isAvailable == true)
                {
                    ///Todo
                    _checkReservedUnit();
                    _sortPendings();
                    _updateAssetAvailability();

                    _updateTodaysCheckIn();
                    _updatePrice();

                    _updateReservationCHKIN1();
                    _updateReservationCHKIN2();

                    _loadRSVData();
                    MessageBox.Show("Reservation has been checked-in.");
                    _UsersMainForm _UsersMainForm = (_UsersMainForm)Application.OpenForms["_UsersMainForm"];
                    _UsersMainForm.Close();
                    _UsersMainForm asdasd = new _UsersMainForm();
                    asdasd.Show();
                }
                else
                {
                    MessageBox.Show("Room unit is not available right now.");
                }
            }
            if (tabControl1.SelectedIndex == 1)
            {
                if (label24.Text == "✔" && tbTransNo.Text.Length > 0 && b > 0 && tbRoomUnit.Text.Length>0)
                {
                    if (c > 0 || d > 0)
                    {
                        if(cbWavePool.Checked == true || cbWetnWild.Checked)
                        {
                            ///Todo
                            _checkReservedUnit();
                            _sortPendings();
                            _updateAssetAvailability();

                            _assets();
                            //_loadCottages();
                            _updateTodaysCheckIn();
                            _updateUnitsDec();
                            _updateAssests();
                            _insertGuestTransactionInfo();
                            _loadCottages();
                            MessageBox.Show("Transaction completed.", "Prompt");
                            _reset();
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
                else
                {
                    MessageBox.Show("Kindly check your reservation form.", "Prompt");
                }
            }
           
        }

        public void tbVideoke_Click(object sender, EventArgs e)
        {
            tbVideoke.SelectAll();
        }

        public void tbBasketball_Click(object sender, EventArgs e)
        {
            tbBasketball.SelectAll();
        }

        public void tbVolleyball_Click(object sender, EventArgs e)
        {
            tbVolleyball.SelectAll();
        }

        public void tbTennis_Click(object sender, EventArgs e)
        {
            tbTennis.SelectAll();
        }

        public void tbBilliards_Click(object sender, EventArgs e)
        {
            tbBilliards.SelectAll();
        }

        public void tbExcessRoomHour_Click(object sender, EventArgs e)
        {
            tbExcessRoomHour.SelectAll();
        }

        public void tbExcessAdult_Click(object sender, EventArgs e)
        {
            tbExcessAdult.SelectAll();
        }

        public void tbExcessChildren_Click(object sender, EventArgs e)
        {
            tbExcessChildren.SelectAll();
        }

        public void tbPriceAdvance_Click(object sender, EventArgs e)
        {
            tbPriceAdvance.SelectAll();
        }

        public void tbGuestID_KeyUp(object sender, KeyEventArgs e)
        {
            _searchID();
        }

        public void tbGuestID_KeyPress(object sender, KeyPressEventArgs e)
        {
            label24.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
            label24.Text = "↻";
            label24.ForeColor = Color.Gray;
        }

        public void btnTransNo_Click(object sender, EventArgs e)
        {
            String transactNo = transacNo.newTransacNo(20);
            tbTransNo.Text = transactNo;
            btnTransNo.Enabled = false;
        }

        public void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today.Date;
            dateTimePicker2.MinDate = Convert.ToDateTime(dateTimePicker1.Text);
            dateTimePicker2.Text = Convert.ToString(dateTimePicker1.Text);
        }

        public void cbVideoke_CheckedChanged(object sender, EventArgs e)
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

        public void cbBasketball_CheckedChanged(object sender, EventArgs e)
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

        public void cbVolleyball_CheckedChanged(object sender, EventArgs e)
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

        public void cbTennis_CheckedChanged(object sender, EventArgs e)
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

        public void cbBilliards_CheckedChanged(object sender, EventArgs e)
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

        public void cmbRental_SelectedIndexChanged(object sender, EventArgs e)
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

        public void tbPriceRental_TextChanged(object sender, EventArgs e)
        {
            int aa = Convert.ToInt32(tbPriceRoom.Text);
            int bb = Convert.ToInt32(tbPriceAdult.Text);
            int cc = Convert.ToInt32(tbPriceChildren.Text);
            int dd = Convert.ToInt32(tbPriceAccess.Text);
            int ee = Convert.ToInt32(tbPriceRental.Text);
            int eee = aa + bb + cc + dd + ee;

            tbPriceTotal.Text = Convert.ToString(eee);
        }

        public void tbPriceRoom_TextChanged(object sender, EventArgs e)
        {
            int aa = Convert.ToInt32(tbPriceRoom.Text);
            int bb = Convert.ToInt32(tbPriceAdult.Text);
            int cc = Convert.ToInt32(tbPriceChildren.Text);
            int dd = Convert.ToInt32(tbPriceAccess.Text);
            int ee = Convert.ToInt32(tbPriceRental.Text);
            int eee = aa + bb + cc + dd + ee;

            tbPriceTotal.Text = Convert.ToString(eee);
        }

        public void tbPriceTotal_TextChanged(object sender, EventArgs e)
        {
            ///Bago
            if (tabControl1.SelectedIndex == 0)
            {
                _pricingTotal();
            }
            else
            {
                _payment();
            }
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            ///Do Something
            if(btnCancel.Text == "Cancel Reservation")
            {
                if (downPaid > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("The reservation cannot be cancel.", "Alert Message", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //do something
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //do something else
                    }
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure to cancel this reservation?", "Alert Message", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        _updateUnitsInc();
                         _updateReservationCNCL1();
                        _updateReservationCNCL2();

                        ///Todo
                        _checkReservedUnit();
                        _sortPendings();
                        _updateAssetAvailability();

                        _updatePrice();
                        _loadRSVData();
                       
                        _reset();
                        MessageBox.Show("Reservation successfully cancelled");
                        _UsersMainForm _UsersMainForm = (_UsersMainForm)Application.OpenForms["_UsersMainForm"];
                        _UsersMainForm.Close();
                        _UsersMainForm asdasd = new _UsersMainForm();
                        asdasd.Show();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //do something else
                    }
                    
                }
                
            }
            else
            {
                _reset();
            }
        }

        public void textBox24_KeyUp(object sender, KeyEventArgs e)
        {
            _searchRSV();
        }

        private void tbPriceRoom_Click(object sender, EventArgs e)
        {
            tbPriceRoom.SelectAll();
        }

        public void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            _tabControl();
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTime checkIn1 = Convert.ToDateTime(dateTimePicker1.Text);
            DateTime checkOut1 = Convert.ToDateTime(dateTimePicker2.Text);
            if (checkOut1 < checkIn1)
            {
                dateTimePicker2.MinDate = dateTimePicker1.MinDate;
                dateTimePicker2.Text = Convert.ToString(dateTimePicker1.Text);
            }
        }

        ///Rooms
        public void _roomsFields()
        {
            if (tbRoomType.Text == "Normal Rooms")
            {
                roomName = "info_rooms";
            }
            if (tbRoomType.Text == "Private Pools")
            {
                roomName = "info_privatepools";
            }
            if (tbRoomType.Text == "Function Halls")
            {
                roomName = "info_halls";
            }
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
                roominfoConn.Close();
                MessageBox.Show(exc.Message);
            }
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
            catch(Exception exc)
            {
                transaction_record.Close();
                MessageBox.Show(exc.Message);
            }
        }
        public void _updateTransStatus1()
        {
            if(pendings > 0)
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
                    asd3= Convert.ToInt32(data.Rows.Count.ToString());
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
                    mySqlCommand.CommandText = "UPDATE "+assetsName+" SET availability = 'available' WHERE unitName='" + tbRoomUnit.Text+ "'";
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

        public void _checkRoomTodayAvailability()
        {
            _assetsName();
            try
            {
                todays_checkin.Open();
                MySqlCommand command = todays_checkin.CreateCommand();
                DataTable data = new DataTable();
                MySqlDataAdapter mySqlData = new MySqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                command.CommandText = "SELECT * FROM "+ assetsName + " WHERE unitName = '"+tbRoomUnit.Text+"'";
                command.ExecuteNonQuery();
                mySqlData.Fill(data);

                todays_checkin.Close();
                foreach (DataRow dtr in data.Rows)
                {
                    String availableunit = dtr["availability"].ToString();
                    if(availableunit == "not-available")
                    {
                        isAvailable = false;
                    }
                    else
                    {
                        isAvailable = true;
                    }
                }
            }
            catch (Exception exc)
            {
                todays_checkin.Close();
                MessageBox.Show(exc.Message);
            }
        }
    }
}
