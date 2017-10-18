using System;
using System.Windows.Forms;
using ERP.BLL.Security;

namespace AttendanceTracker
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            if (LoginInformation.UserInformation != null)
            {
                lblName.Text = LoginInformation.UserInformation.EmployeeName;
                lblDepartment.Text = LoginInformation.UserInformation.DepartmentName;
                lblDesignation.Text = LoginInformation.UserInformation.DesignationName;
            }
        }

        private void btnEntryTime_Click(object sender, EventArgs e)
        {
            var punchIn = new PunchIn();
            punchIn.Show();
        }

        private void btnExitTime_Click(object sender, EventArgs e)
        {
            var punchOut = new PunchOut();
            punchOut.Show();
        }
    }
}
