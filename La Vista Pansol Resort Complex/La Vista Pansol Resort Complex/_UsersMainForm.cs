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
    public partial class _UsersMainForm : Form
    {

        public _UsersMainForm()
        {
            InitializeComponent();
        }

        bool drag = false;
        Point start = new Point(0, 0);

        
        //New Guest
        _UsersNewGuest _NewGuest = new _UsersNewGuest();
        //Reservation
        _UsersReservation _Reservation = new _UsersReservation();
        //CheckIN
        _UsersCheckIn _CheckIn = new _UsersCheckIn();
        //CheckOut
        _UsersCheckOut _CheckOut = new _UsersCheckOut();
        //Rooms
        _UsersRates _Rates = new _UsersRates();

        //Home
        _Home _Home = new _Home();

        int click;
        public String IP_Connections = "127.0.0.1";
        public String resort_email;
        public String resort_contact;
        public String resort_pass;
        public String name = "esponilla.dev404@gmail.com";
        public String user = "yEspo.Dev404";
        public String employeeID = "5445678900";

        private void btnSettings_Click(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            _UsersMainForm _UsersMainForm = (_UsersMainForm)Application.OpenForms["_UsersMainForm"];
            _LoginForm _LoginForm = new _LoginForm();

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to quit?", "Message", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                _UsersMainForm.Close();
                _LoginForm.Show();
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }
            //Application.Exit();
        }

        public void btnClick()
        {
            if (click == 0)
            {
                btnHome.BackColor = Color.FromArgb(139, 167, 178);

                btnRooms.BackColor = Color.FromArgb(176, 188, 193);
                btnCheckIn.BackColor = Color.FromArgb(176, 188, 193);
                btnCheckOut.BackColor = Color.FromArgb(176, 188, 193);
                btnReservation.BackColor = Color.FromArgb(176, 188, 193);
                btnGuest.BackColor = Color.FromArgb(176, 188, 193);
                btnHelp.BackColor = Color.FromArgb(176, 188, 193);
            }
            if (click == 1)
            {
                btnRooms.BackColor = Color.FromArgb(139, 167, 178);

                btnHome.BackColor = Color.FromArgb(176, 188, 193);
                btnCheckIn.BackColor = Color.FromArgb(176, 188, 193);
                btnCheckOut.BackColor = Color.FromArgb(176, 188, 193);
                btnReservation.BackColor = Color.FromArgb(176, 188, 193);
                btnGuest.BackColor = Color.FromArgb(176, 188, 193);
                btnHelp.BackColor = Color.FromArgb(176, 188, 193);
            }
            if (click == 2)
            {
                btnReservation.BackColor = Color.FromArgb(139, 167, 178);

                btnHome.BackColor = Color.FromArgb(176, 188, 193);
                btnRooms.BackColor = Color.FromArgb(176, 188, 193);
                btnCheckOut.BackColor = Color.FromArgb(176, 188, 193);
                btnCheckIn.BackColor = Color.FromArgb(176, 188, 193);
                btnGuest.BackColor = Color.FromArgb(176, 188, 193);
                btnHelp.BackColor = Color.FromArgb(176, 188, 193);
            }
            if (click == 3)
            {
                btnCheckIn.BackColor = Color.FromArgb(139, 167, 178);

                btnHome.BackColor = Color.FromArgb(176, 188, 193);
                btnRooms.BackColor = Color.FromArgb(176, 188, 193);
                btnCheckOut.BackColor = Color.FromArgb(176, 188, 193);
                btnReservation.BackColor = Color.FromArgb(176, 188, 193);
                btnGuest.BackColor = Color.FromArgb(176, 188, 193);
                btnHelp.BackColor = Color.FromArgb(176, 188, 193);
            }
            if (click == 4)
            {
                btnCheckOut.BackColor = Color.FromArgb(139, 167, 178);

                btnHome.BackColor = Color.FromArgb(176, 188, 193);
                btnRooms.BackColor = Color.FromArgb(176, 188, 193);
                btnCheckIn.BackColor = Color.FromArgb(176, 188, 193);
                btnReservation.BackColor = Color.FromArgb(176, 188, 193);
                btnGuest.BackColor = Color.FromArgb(176, 188, 193);
                btnHelp.BackColor = Color.FromArgb(176, 188, 193);
            }
            if (click == 5)
            {
                btnGuest.BackColor = Color.FromArgb(139, 167, 178);

                btnHome.BackColor = Color.FromArgb(176, 188, 193);
                btnRooms.BackColor = Color.FromArgb(176, 188, 193);
                btnCheckIn.BackColor = Color.FromArgb(176, 188, 193);
                btnCheckOut.BackColor = Color.FromArgb(176, 188, 193);
                btnReservation.BackColor = Color.FromArgb(176, 188, 193);
                btnHelp.BackColor = Color.FromArgb(176, 188, 193);
            }
            if (click == 6)
            {
                btnHelp.BackColor = Color.FromArgb(139, 167, 178);

                btnHome.BackColor = Color.FromArgb(176, 188, 193);
                btnRooms.BackColor = Color.FromArgb(176, 188, 193);
                btnCheckIn.BackColor = Color.FromArgb(176, 188, 193);
                btnCheckOut.BackColor = Color.FromArgb(176, 188, 193);
                btnReservation.BackColor = Color.FromArgb(176, 188, 193);
                btnGuest.BackColor = Color.FromArgb(176, 188, 193);
            }
        }

        private void _MainForm_Load(object sender, EventArgs e)
        {
            click = 0;
            btnClick();
            _Home.MdiParent = this;
            _Home.Show();
            _Reservation.Hide();
            _NewGuest.Hide();
            _CheckIn.Hide();
            _CheckOut.Hide();
            _Rates.Hide();

            lbName.Text = "Name : " + name;
            lbUser.Text = "Username : " + user;
            lbEmployeeID.Text = "Employee ID : " + employeeID;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start = new Point(e.X, e.Y);
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
         
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start.X, p.Y - start.Y);
            
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        private void btnHome_Click_1(object sender, EventArgs e)
        {
            click = 0;
            btnClick();
            _Home.MdiParent = this;
            _Home.Show();
            _Reservation.Hide();
            _NewGuest.Hide();
            _CheckIn.Hide();
            _CheckOut.Hide();
            _Rates.Hide();
        }

        private void btnRooms_Click_1(object sender, EventArgs e)
        {
            click = 1;
            btnClick();
            _Rates.MdiParent = this;
            _Rates.hostName = IP_Connections;

            _Rates.Show();
            _Reservation.Hide();
            _NewGuest.Hide();
            _CheckIn.Hide();
            _CheckOut.Hide();
            _Home.Hide();

            _Rates._UsersRates_Load(sender, e);
        }

        private void btnReservation_Click_1(object sender, EventArgs e)
        {
            click = 2;
            btnClick();
            _Reservation.MdiParent = this;
            _Reservation.hostName = IP_Connections;
            _Reservation.Show();
            _NewGuest.Hide();
            _CheckIn.Hide();
            _CheckOut.Hide();
            _Home.Hide();
            _Rates.Hide();
            _Reservation._Reservation_Load(sender, e);
        }

        private void btnCheckIn_Click_1(object sender, EventArgs e)
        {
            click = 3;
            btnClick();
            _CheckIn.MdiParent = this;
            _CheckIn.hostName = IP_Connections;
            _CheckIn.Show();
            _NewGuest.Hide();
            _Reservation.Hide();
            _CheckOut.Hide();
            _Home.Hide();
            _Rates.Hide();
            _CheckIn._CheckIn_Load(sender, e);
        }

        private void btnCheckOut_Click_1(object sender, EventArgs e)
        {
            click = 4;
            btnClick();
            _CheckOut.MdiParent = this;
            _CheckOut.hostName = IP_Connections;
            _CheckOut.Show();
            _CheckIn.Hide();
            _NewGuest.Hide();
            _Reservation.Hide();
            _Home.Hide();
            _Rates.Hide();
            _CheckOut._CheckOut_Load(sender, e);
        }

        private void btnGuest_Click_1(object sender, EventArgs e)
        {
            click = 5;
            btnClick();
            _NewGuest.MdiParent = this;
            _NewGuest.hostName = IP_Connections;

            _NewGuest.Show();
            _Reservation.Hide();
            _CheckIn.Hide();
            _CheckOut.Hide();
            _Home.Hide();
            _Rates.Hide();
        }

        private void btnHelp_Click_1(object sender, EventArgs e)
        {

        }
    }
}
