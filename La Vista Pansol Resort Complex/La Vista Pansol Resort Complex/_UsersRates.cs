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
    public partial class _UsersRates : Form
    {
        public _UsersRates()
        {
            InitializeComponent();
        }
        MySqlConnection accomodation;

        public String hostName;
        String packageA, packageB, packageC;
        public String username, password, employeeID;

        public void _UsersRates_Load(object sender, EventArgs e)
        {
            accomodation = new MySqlConnection("server=" + hostName + "; user=root; password= ; database=accomodation;");

            _reset();
        }

        ///
        public void _reset()
        {
            cmbRooms.Text = "Single Cottage 2";
            cmbCottages.Text = "Muchroom";
            cmbPrivatePools.Text = "Ilang-Ilang";
            cmbHalls.Text = "Ramon B. Donato Upper Ilang-Ilang (Aircon)";
            cmbRentals.Text = "Videoke";
            cmbPoolAccess.Text = "Wave Pool";
            cmbPackage.Text = "Package A";

            tbRoomPax.Text = "";
            tbRoomUnits.Text = "";
            tbRoomPrice12.Text = "";
            tbRoomPrice22.Text = "";

            tbCottagesUnits.Text = "";
            tbCottagesPax.Text = "";
            tbCottagesPriceAM.Text = "";
            tbCottagesPricePM.Text = "";

            tbPrivatePoolsPax.Text = "";
            tbPrivatePoolsPrice.Text = "";
            tbPrivatePoolsUnits.Text = "";

            tbHallsUnits.Text = "";
            tbHallsUnits.Text = "";
            tbHallsPrice.Text = "";

            tbRentalsHour.Text = "";
            tbRentalsPriceAM.Text = "";
            tbRentalsPricePM.Text = "";

            tbAccessAdultAM.Text = "";
            tbAccessAdultPM.Text = "";
            tbAccessKidsAM.Text = "";
            tbAccessKidsPM.Text = "";
        }
        ///
        public void _selectRooms()
        {
            int count = 1;

            if (cmbRooms.Text == "Single Cottage 2")
                count = 1;
            if (cmbRooms.Text == "Bario Makiling")
                count = 2;
            if (cmbRooms.Text == "Upper Rest House")
                count = 3;
            if (cmbRooms.Text == "Single Cottage 4")
                count = 4;
            if (cmbRooms.Text == "Sotano 4")
                count = 5;
            if (cmbRooms.Text == "Suite Room")
                count = 6;
            if (cmbRooms.Text == "Sotano 6")
                count = 7;
            if (cmbRooms.Text == "Natalies")
                count = 8;
            if (cmbRooms.Text == "Lower Rest House")
                count = 9;
            if (cmbRooms.Text == "Rest House")
                count = 10;
            if (cmbRooms.Text == "Casa Blanca")
                count = 11;
            if (cmbRooms.Text == "Bucal Rest House")
                count = 12;
            if (cmbRooms.Text == "Dormitory")
                count = 13;

            try
            {
                accomodation.Open();

                MySqlCommand mySqlCommand = accomodation.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "SELECT * FROM info_rooms WHERE ID=" + count + "";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    tbRoomUnits.Text = dataRow["roomUnits"].ToString();
                    tbRoomPax.Text = dataRow["paxRange"].ToString();
                    tbRoomPrice12.Text = dataRow["priceAt12"].ToString();
                    tbRoomPrice22.Text = dataRow["priceAt22"].ToString();
                }
                accomodation.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                accomodation.Close();
            }
        }
        ///
        public void _selectCottages()
        {
            int count = 1;

            if (cmbCottages.Text == "Mushroom")
                count = 1;
            if (cmbCottages.Text == "Picnic Hut")
                count = 2;
            if (cmbCottages.Text == "Kubo(Air-con)")
                count = 3;
            if (cmbCottages.Text == "Pavillion")
                count = 4;

            try
            {
                accomodation.Open();

                MySqlCommand mySqlCommand = accomodation.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "SELECT * FROM info_cottages WHERE ID=" + count + "";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    tbCottagesUnits.Text = dataRow["roomUnits"].ToString();
                    tbCottagesPax.Text = dataRow["paxRange"].ToString();
                    tbCottagesPriceAM.Text = dataRow["amPrice"].ToString();
                    tbCottagesPricePM.Text = dataRow["pmPrice"].ToString();
                }
                accomodation.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                accomodation.Close();
            }
        }
        ///
        public void _selectPrivatePools()
        {
            int count = 1;

            if (cmbPrivatePools.Text == "Ilang-Ilang")
                count = 1;
            if (cmbPrivatePools.Text == "Family Rest House A")
                count = 2;
            if (cmbPrivatePools.Text == "Family Rest House B")
                count = 3;
            if (cmbPrivatePools.Text == "Family Rest House C")
                count = 4;
            if (cmbPrivatePools.Text == "Family Rest House D")
                count = 5;

            try
            {
                accomodation.Open();

                MySqlCommand mySqlCommand = accomodation.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "SELECT * FROM info_privatepools WHERE ID=" + count + "";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    tbPrivatePoolsUnits.Text = dataRow["roomUnits"].ToString();
                    tbPrivatePoolsPax.Text = dataRow["paxRange"].ToString();
                    packageA = dataRow["packageA"].ToString();
                    packageB = dataRow["packageB"].ToString();
                    packageC = dataRow["packageC"].ToString();
                    _packageType();
                }
                accomodation.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                accomodation.Close();
            }
        }

        private void cmbRooms_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            _selectRooms();
        }

        private void cmbCottages_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            _selectCottages();
        }

        private void cmbPrivatePools_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            _packageType();
            _selectPrivatePools();
        }

        private void cmbPackage_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            _packageType();
            _selectPrivatePools();
        }

        private void cmbRentals_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            _selectRentals();
        }

        private void cmbPoolAccess_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            _selectPoolAccess();
        }

        private void cmbHalls_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            _selectHalls();
        }

        public void _packageType()
        {
            if (cmbPackage.Text == "Package A")
            {
                tbPrivatePoolsPrice.Text = packageA;
                packageA = tbPrivatePoolsPrice.Text;
            }
            if (cmbPackage.Text == "Package B")
            {
                tbPrivatePoolsPrice.Text = packageB;
                packageB = tbPrivatePoolsPrice.Text;
            }
            if (cmbPackage.Text == "Package C")
            {
                tbPrivatePoolsPrice.Text = packageC;
                packageC = tbPrivatePoolsPrice.Text;
            }
        }
        ///
        public void _selectHalls()
        {
            int count = 1;

            if (cmbHalls.Text == "Ramon B. Donato Upper Ilang-Ilang (Aircon)")
                count = 1;
            if (cmbHalls.Text == "Nixon B. Donato Mini Conference Hall (No-Aircon)")
                count = 2;
            if (cmbHalls.Text == "Dr. Nicolas T. Donato Sr Conference Hall (Aircon)")
                count = 3;
            if (cmbHalls.Text == "Nora Luisa Hall (Aircon)")
                count = 4;
            if (cmbHalls.Text == "Nicole B. Donato Multi Purpose Hall (No-Aircon)")
                count = 5;
            if (cmbHalls.Text == "Nilcar B. Donato Grand Convention Hall (Aircon)")
                count = 6;
            if (cmbHalls.Text == "Dr. Nielsen B. Donato Upper Pantalan (Aircon)")
                count = 7;
            if (cmbHalls.Text == "Nicolas B. Donato III Lower Pantalan (No-Aircon)")
                count = 8;

            try
            {
                accomodation.Open();

                MySqlCommand mySqlCommand = accomodation.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "SELECT * FROM info_halls WHERE ID=" + count + "";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    tbHallsUnits.Text = dataRow["roomUnits"].ToString();
                    tbHallsPax.Text = dataRow["paxRange"].ToString();
                    tbHallsPrice.Text = dataRow["roomPrice"].ToString();
                }
                accomodation.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                accomodation.Close();
            }

        }
        ///
        public void _selectRentals()
        {
            int count = 1;

            if (cmbRentals.Text == "Videoke")
                count = 1;
            if (cmbRentals.Text == "Basketball Court")
                count = 2;
            if (cmbRentals.Text == "Volleyball Court")
                count = 3;
            if (cmbRentals.Text == "Table Tennis")
                count = 4;
            if (cmbRentals.Text == "Billiards")
                count = 5;

            try
            {
                accomodation.Open();

                MySqlCommand mySqlCommand = accomodation.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "SELECT * FROM info_rentals WHERE ID=" + count + "";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    tbRentalsHour.Text = dataRow["minHour"].ToString();
                    tbRentalsPriceAM.Text = dataRow["amPrice"].ToString();
                    tbRentalsPricePM.Text = dataRow["pmPrice"].ToString();
                }
                accomodation.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                accomodation.Close();
            }
        }
        ///
        public void _selectPoolAccess()
        {
            int count = 1;

            if (cmbRentals.Text == "Wave Pool")
                count = 1;
            if (cmbRentals.Text == "Wet n' Wild")
                count = 2;
            if (cmbRentals.Text == "Both Access")
                count = 3;
            if (cmbRentals.Text == "Excess Pax Price")
                count = 4;

            try
            {
                accomodation.Open();

                MySqlCommand mySqlCommand = accomodation.CreateCommand();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.CommandText = "SELECT * FROM info_entrance WHERE ID=" + count + "";
                mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.Fill(dataTable);

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    tbAccessAdultAM.Text = dataRow["adultAM"].ToString();
                    tbAccessAdultPM.Text = dataRow["adultPM"].ToString();
                    tbAccessKidsAM.Text = dataRow["kidsAM"].ToString();
                    tbAccessKidsPM.Text = dataRow["kidsPM"].ToString();
                }
                accomodation.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                accomodation.Close();
            }
        }
    }
}
