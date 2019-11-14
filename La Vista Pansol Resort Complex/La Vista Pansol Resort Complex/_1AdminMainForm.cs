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
    public partial class _1AdminMainForm : Form
    {

        public _1AdminMainForm()
        {
            InitializeComponent();
        }

        bool drag = false;
        Point start = new Point(0, 0);

        _Home _Home = new _Home();
        _Admin1Prices _Admin1Prices = new _Admin1Prices();
        _Admin1Reservation _Admin1Reservation = new _Admin1Reservation();
        _Admin1CheckIn _Admin1CheckIn = new _Admin1CheckIn();
        _Admin1GuestList _Admin1GuestList = new _Admin1GuestList();
        _Admin1AddStaffs _Admin1AddStaffs = new _Admin1AddStaffs();

        int click;
        public String IP_Connections = "127.0.0.1";
        public String resort_email;
        public String resort_contact;
        public String resort_pass;
        public String name = "esponilla.dev404@gmail.com";
        public String user = "yEspo.Dev404";
        public String pass = "yEspo.Dev404";
        public String employeeID = "5445678900";

        private void btnHome_Click(object sender, EventArgs e)
        {
            click = 0;
            btnClick();
            _Home.MdiParent = this;

            _Home.Show();
            _Admin1Prices.Hide();
            _Admin1Reservation.Hide();
            _Admin1CheckIn.Hide();
            _Admin1GuestList.Hide();
            _Admin1AddStaffs.Hide();
        }

        private void btnRates_Click(object sender, EventArgs e)
        {
            click = 1;
            btnClick();
            _Admin1Prices.MdiParent = this;
            _Admin1Prices.hostName = IP_Connections;
            _Admin1Prices.username = user;
            _Admin1Prices.employeeID = employeeID;
            _Admin1Prices.password = pass;

            _Home.Hide();
            _Admin1Prices.Show();
            _Admin1Reservation.Hide();
            _Admin1CheckIn.Hide();
            _Admin1GuestList.Hide();
            _Admin1AddStaffs.Hide();

            _Admin1Reservation._Admin1Reservation_Load(sender, e);
        }

        private void btnReservation_Click(object sender, EventArgs e)
        {
            click = 2;
            btnClick();
            _Admin1Reservation.MdiParent = this;
            _Admin1Reservation.hostName = IP_Connections;

            _Home.Hide();
            _Admin1Prices.Hide();
            _Admin1Reservation.Show();
            _Admin1CheckIn.Hide();
            _Admin1GuestList.Hide();
            _Admin1AddStaffs.Hide();

            _Admin1Reservation._Admin1Reservation_Load(sender, e);
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            click = 3;
            btnClick();
            _Admin1CheckIn.MdiParent = this;
            _Admin1CheckIn.hostName = IP_Connections;

            _Home.Hide();
            _Admin1Prices.Hide();
            _Admin1Reservation.Hide();
            _Admin1CheckIn.Show();
            _Admin1GuestList.Hide();
            _Admin1AddStaffs.Hide();

            _Admin1CheckIn._Admin1CheckIn_Load(sender, e);
        }
        
        private void btnGuest_Click(object sender, EventArgs e)
        {
            click = 4;
            btnClick();
            _Admin1GuestList.MdiParent = this;
            _Admin1GuestList.hostName = IP_Connections;

            _Home.Hide();
            _Admin1Prices.Hide();
            _Admin1Reservation.Hide();
            _Admin1CheckIn.Hide();
            _Admin1GuestList.Show();
            _Admin1AddStaffs.Hide();

            _Admin1GuestList._Admin1GuestList_Load(sender, e);
        }
      
        private void btnStaffs_Click(object sender, EventArgs e)
        {
            click = 5;
            btnClick();
            _Admin1AddStaffs.MdiParent = this;
            _Admin1AddStaffs.hostName = IP_Connections;

            _Home.Hide();
            _Admin1Prices.Hide();
            _Admin1Reservation.Hide();
            _Admin1CheckIn.Hide();
            _Admin1GuestList.Hide();
            _Admin1AddStaffs.Show();

            _Admin1AddStaffs._Admin1AddStaffs_Load(sender, e);
        }
        private void btnHelp_Click(object sender, EventArgs e)
        {
            //click = 1;
            //btnClick();
            //_Admin1Prices.MdiParent = this;

            //_Home.Hide();
            //_Admin1Prices.Show();
            //_Admin1Reservation.Hide();
            //_Admin1CheckIn.Hide();
            //_Admin1GuestList.Hide();
            //_Admin1AddStaffs.Hide();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            _1AdminMainForm _1AdminMainForm = (_1AdminMainForm)Application.OpenForms["_1AdminMainForm"];
            _LoginForm _LoginForm = new _LoginForm();

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to quit?", "Message", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                _1AdminMainForm.Close();
                _LoginForm.Show();
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void _AdminMainForm_Load(object sender, EventArgs e)
        {
            click = 0;
            btnClick();
            _Home.MdiParent = this;

            _Home.Show();
            _Admin1Prices.Hide();
            _Admin1Reservation.Hide();
            _Admin1CheckIn.Hide();
            _Admin1GuestList.Hide();
            _Admin1AddStaffs.Hide();

            lbName.Text = "Name : " + name;
            lbUser.Text = "Username : " + user;
            lbEmployeeID.Text = "Employee ID : " + employeeID;
        }

        public void btnClick()
        {
            if (click == 0)
            {
                btnHome.BackColor = Color.FromArgb(139, 167, 178);

                btnRates.BackColor = Color.FromArgb(176, 188, 193);
                btnCheckIn.BackColor = Color.FromArgb(176, 188, 193);
                btnGuest.BackColor = Color.FromArgb(176, 188, 193);
                btnReservation.BackColor = Color.FromArgb(176, 188, 193);
                btnGuest.BackColor = Color.FromArgb(176, 188, 193);
                btnHelp.BackColor = Color.FromArgb(176, 188, 193);
            }
            if (click == 1)
            {
                btnRates.BackColor = Color.FromArgb(139, 167, 178);

                btnHome.BackColor = Color.FromArgb(176, 188, 193);
                btnCheckIn.BackColor = Color.FromArgb(176, 188, 193);
                btnGuest.BackColor = Color.FromArgb(176, 188, 193);
                btnReservation.BackColor = Color.FromArgb(176, 188, 193);
                btnStaffs.BackColor = Color.FromArgb(176, 188, 193);
                btnHelp.BackColor = Color.FromArgb(176, 188, 193);
            }
            if (click == 2)
            {
                btnReservation.BackColor = Color.FromArgb(139, 167, 178);

                btnHome.BackColor = Color.FromArgb(176, 188, 193);
                btnRates.BackColor = Color.FromArgb(176, 188, 193);
                btnGuest.BackColor = Color.FromArgb(176, 188, 193);
                btnCheckIn.BackColor = Color.FromArgb(176, 188, 193);
                btnStaffs.BackColor = Color.FromArgb(176, 188, 193);
                btnHelp.BackColor = Color.FromArgb(176, 188, 193);
            }
            if (click == 3)
            {
                btnCheckIn.BackColor = Color.FromArgb(139, 167, 178);

                btnHome.BackColor = Color.FromArgb(176, 188, 193);
                btnRates.BackColor = Color.FromArgb(176, 188, 193);
                btnGuest.BackColor = Color.FromArgb(176, 188, 193);
                btnReservation.BackColor = Color.FromArgb(176, 188, 193);
                btnStaffs.BackColor = Color.FromArgb(176, 188, 193);
                btnHelp.BackColor = Color.FromArgb(176, 188, 193);
            }
            if (click == 4)
            {
                btnGuest.BackColor = Color.FromArgb(139, 167, 178);

                btnHome.BackColor = Color.FromArgb(176, 188, 193);
                btnRates.BackColor = Color.FromArgb(176, 188, 193);
                btnCheckIn.BackColor = Color.FromArgb(176, 188, 193);
                btnReservation.BackColor = Color.FromArgb(176, 188, 193);
                btnStaffs.BackColor = Color.FromArgb(176, 188, 193);
                btnHelp.BackColor = Color.FromArgb(176, 188, 193);
            }
            if (click == 5)
            {
                btnStaffs.BackColor = Color.FromArgb(139, 167, 178);

                btnHome.BackColor = Color.FromArgb(176, 188, 193);
                btnRates.BackColor = Color.FromArgb(176, 188, 193);
                btnCheckIn.BackColor = Color.FromArgb(176, 188, 193);
                btnGuest.BackColor = Color.FromArgb(176, 188, 193);
                btnReservation.BackColor = Color.FromArgb(176, 188, 193);
                btnHelp.BackColor = Color.FromArgb(176, 188, 193);
            }
            if (click == 6)
            {
                btnHelp.BackColor = Color.FromArgb(139, 167, 178);

                btnHome.BackColor = Color.FromArgb(176, 188, 193);
                btnRates.BackColor = Color.FromArgb(176, 188, 193);
                btnCheckIn.BackColor = Color.FromArgb(176, 188, 193);
                btnGuest.BackColor = Color.FromArgb(176, 188, 193);
                btnReservation.BackColor = Color.FromArgb(176, 188, 193);
                btnStaffs.BackColor = Color.FromArgb(176, 188, 193);
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            Point p = PointToScreen(e.Location);
            this.Location = new Point(p.X - start.X, p.Y - start.Y);
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start = new Point(e.X, e.Y);
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        
    }
}
