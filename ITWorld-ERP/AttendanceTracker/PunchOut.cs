using ERP.BLL;
using ERP.BLL.HRM;
using ERP.HRM.Domain;
using Ninject;
using System;
using System.Windows.Forms;

namespace AttendanceTracker
{
    public partial class PunchOut : Form
    {
        private readonly IEmployeeAttendanceInformationService _employeeAttendanceInfoService;
        private readonly IEmployeeService _emplyeeService;

        public PunchOut()
        {
            InitializeComponent();
            IKernel kernel = BootStrapper.Initialize();
            _employeeAttendanceInfoService = kernel.GetService(typeof(EmployeeAttendanceInformationService)) as EmployeeAttendanceInformationService;
            _emplyeeService = kernel.GetService(typeof(EmployeeService)) as EmployeeService;
        }

        private void btnPunchOut_Click(object sender, EventArgs e)
        {
            if (txtEmployeeCode.Text.Trim() == String.Empty)
            {
                MessageBox.Show(@"Employee Code cannot be empty.");
                return;
            }
            try
            {
                EmployeeModel emoplyeeInfo = _emplyeeService.GetEmployeeInformation(null, txtEmployeeCode.Text.Trim());

                if (emoplyeeInfo == null)
                {
                    Clear();
                    MessageBox.Show(@"Employee is not exist.");
                }
                else
                {
                    txtName.Text = emoplyeeInfo.EmployeeName;
                    txtDesignation.Text = emoplyeeInfo.DesignationName;
                    txtDepartment.Text = emoplyeeInfo.DepartmentName;

                    var employeeAttendanceInfo = _employeeAttendanceInfoService.GetEmployeeAttendanceInformations(txtEmployeeCode.Text.Trim(), DateTime.Now);

                    if (employeeAttendanceInfo == null)
                    {
                        employeeAttendanceInfo = new EmployeeAttendanceInformationModel
                        {
                            EmployeeId = emoplyeeInfo.Id,
                            Date = DateTime.Now,
                            PunchOutTime = DateTime.Now,
                            CreatedBy = 7,
                            CreatedOn = DateTime.Now
                        };

                        _employeeAttendanceInfoService.Insert(employeeAttendanceInfo);
                    }
                    else
                    {
                        employeeAttendanceInfo.PunchOutTime = DateTime.Now;
                        _employeeAttendanceInfoService.UpdatePunchOutTime(employeeAttendanceInfo);
                    }
                    MessageBox.Show(@"Punch out information saved successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Connection error. Please contact with system administrator.");
            }
        }

        private void Clear()
        {
            txtEmployeeCode.Text = "";
            txtName.Text = "";
            txtDesignation.Text = "";
            txtDepartment.Text = "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
