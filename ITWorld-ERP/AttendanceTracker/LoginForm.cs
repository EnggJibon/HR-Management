using ERP.BLL.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ninject;
using ERP.BLL;

namespace AttendanceTracker
{
    public partial class LoginForm : Form
    {
        private readonly IUserInformationService _userInformationService;

        public LoginForm()
        {
            InitializeComponent();
            IKernel kernel = BootStrapper.Initialize();
            _userInformationService = kernel.GetService(typeof(UserInformationService)) as UserInformationService;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var userInformation = _userInformationService.GetUserDetails(null, txtUsername.Text.Trim());
                if (userInformation == null)
                {
                    MessageBox.Show(@"Username or password is incorrect. Please enter your valid credentials.");
                    return;
                }

                if (Authenticator.ValidatePassword(txtPassword.Text.Trim(), userInformation.Password))
                {
                    if (!userInformation.IsActive || userInformation.IsLocked)
                    {
                        MessageBox.Show(@"User is not active now. Please contact with your system administrator.");
                    }

                    if (userInformation.IsPasswordChanged)
                    {
                        LoginInformation.UserInformation = userInformation;

                        Hide();
                        var dashboard = new Dashboard();
                        dashboard.Closed += (o, args) => Close();
                        dashboard.Show();
                       
                        MessageBox.Show(@"Login Successful.");
                    }
                }
            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("The underlying provider failed on Open"))
                {
                    MessageBox.Show(@"Failed to connect database server. Please contact with your system administrator.");
                }
            }
        }
    }
}
