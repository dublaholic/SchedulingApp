using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedulingApp
{
    public partial class Homepage : Form
    {
       
        public User loginUser; 
        public Homepage(User user)
        {
            InitializeComponent();
            FormatDGV(dgvSchedule);
            loginUser = user;
            GetSchedule(loginUser.userID, monthCalendar.SelectionStart, monthCalendar.SelectionStart);
            
            
        }
        private void FormatDGV(DataGridView d)
        {

            d.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            d.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow;
            d.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            d.RowHeadersVisible = false;
        }

        private void Homepage_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void GetSchedule(int userId, DateTime startDate, DateTime endDate)
        {
            DataAction appointmentData = new DataAction();
            dgvSchedule.DataSource = appointmentData.GetAppointments(userId, startDate, endDate);
            dgvSchedule.Refresh();
        }

        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            GetSchedule(loginUser.userID, monthCalendar.SelectionStart, monthCalendar.SelectionEnd); 
        }

        private void dgvSchedule_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnModifyAppointment_Click(object sender, EventArgs e)
        {
            Appointment modAppointment = (Appointment)dgvSchedule.CurrentRow.DataBoundItem;
            new ManageAppointment(modAppointment).ShowDialog(); 
        }

        private void mnuCustomers_Click(object sender, EventArgs e)
        {
            Form Customers = new Customers(loginUser);
            Customers.ShowDialog();
        }
    }
}
         
    
            
            
    

