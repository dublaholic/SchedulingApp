using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace SchedulingApp
{

    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            LoginLanguage();
        }
        public void LoginLanguage()
        {
            switch (CultureInfo.CurrentUICulture.EnglishName)
            {
                case "English (United States)":
                    lblUsername.Text = "Username:";
                    lblPasswaord.Text = "Password:";
                    lblErrorText.Text = "The username and password did not match";
                    btnSubmit.Text = "Submit";
                    btnExit.Text = "Exit";
                    btnRegister.Text = "Register";
                    break;
                case "Spanish (Mexico)":
                    lblUsername.Text = "Nombre de usuario:";
                    lblPasswaord.Text = "Contraseña:";
                    lblErrorText.Text = "El nombre de usuario y la contraseña no coinciden";
                    btnSubmit.Text = "Enviar";
                    btnExit.Text = "Salida";
                    btnRegister.Text = "Registrarse";
                    break;
                default:
                    lblUsername.Text = "Username:";
                    lblPasswaord.Text = "Password:";
                    lblErrorText.Text = "Error";
                    btnSubmit.Text = "Submit";
                    btnExit.Text = "Exit";
                    btnRegister.Text = "Register";
                    break;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            DataAction verifyUser = new DataAction();
            User loginUser = new User(txtUsername.Text, txtPassword.Text);
 
            
            loginUser.userID = verifyUser.VerifyUser(loginUser.username, loginUser.password);

            if(loginUser.userID > 0)
            {
                DateTime timestamp = DateTime.Now;
                verifyUser.LogUserActivity($"Username: {loginUser.username} Log in: {timestamp}");

                Form homepage = new Homepage(loginUser);
                homepage.Show();
                this.Close();
                
            }
            else 
            {
                lblErrorText.Visible = true;
            }


        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
