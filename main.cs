using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Chatapp
{
    public partial class main : Form
    {
        
        public main()
        {
            InitializeComponent();
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // getting the values from from
            String username = usernameRegister.Text;
            String password = passwordRegister.Text;
            String email = emailRegister.Text;
            String confirm = confirmPassword.Text;

            // adding a new user
            dataBase db = new dataBase();
            MySqlCommand command = new MySqlCommand("INSERT INTO `registration`(`Username`, `Email`, `Password`, `ActiveStatus`) VALUES (@user, @mail, @pass, @active)", db.getConnection());

            command.Parameters.Add("@user", MySqlDbType.VarChar).Value = username;
            command.Parameters.Add("@mail", MySqlDbType.VarChar).Value = email;
            command.Parameters.Add("@pass", MySqlDbType.Text).Value = password;
            command.Parameters.Add("@active", MySqlDbType.Int32).Value = 1;

            if (!(username == "Username" || password == "Password" || email == "Email"))
            {
                if (password == confirm)
                {
                    if (!check())
                    {
                        // connection open
                        db.openConnection();

                        // execute command
                        if (command.ExecuteNonQuery() == 1)
                        {
                            this.Hide();
                            mainPage mp = new mainPage();
                            mp.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Error");
                        }

                        // close connection
                        db.closeConnection();
                    }
                    else
                    {
                        MessageBox.Show("Username Already Taken!", "Duplicate user",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Passwords does not match!");
                }

            }
            else
            {
                MessageBox.Show("Enter Valid values!");
            }
        }

        // checking if user exists or not!
        public Boolean check()
        {
            dataBase db = new dataBase();

            String username = usernameRegister.Text;

            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `registration` WHERE `Username` = @user", db.getConnection());

            command.Parameters.Add("@user", MySqlDbType.VarChar).Value = username;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            usernameRegister.Visible = false;
            emailRegister.Visible = false;
            passwordRegister.Visible = false;
            confirmPassword.Visible = false;
            button1.Visible = false;
            button2.Visible = false;

            Username.Visible = true;
            Password.Visible = true;
            LoginButton.Visible = true;
            RegisterButton.Visible = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void usernameRegister_Enter(object sender, EventArgs e)
        {
            if (usernameRegister.Text == "User name")
            {
                usernameRegister.Text = "";
                usernameRegister.ForeColor = Color.Black;
            }
        }

        private void usernameRegister_Leave(object sender, EventArgs e)
        {
            if (usernameRegister.Text == "")
            {
                usernameRegister.Text = "User name";
                usernameRegister.ForeColor = Color.Gray;
            }
        }

        private void emailRegister_Enter(object sender, EventArgs e)
        {
            if (emailRegister.Text == "Email")
            {   
                emailRegister.Text = "";
                emailRegister.ForeColor = Color.Black;
            }
        }

        private void emailRegister_Leave(object sender, EventArgs e)
        {
            if (emailRegister.Text == "")
            {   
                emailRegister.Text = "Email";
                emailRegister.ForeColor = Color.Gray;
            }
        }

        private void passwordRegister_Enter(object sender, EventArgs e)
        {
            if (passwordRegister.Text == "Password")
            {
                passwordRegister.Text = "";
                passwordRegister.PasswordChar = '*';
                passwordRegister.ForeColor = Color.Black;
            }
        }

        private void passwordRegister_Leave(object sender, EventArgs e)
        {
            if (passwordRegister.Text == "")
            {
                passwordRegister.Text = "Password";
                passwordRegister.ForeColor = Color.Gray;
                passwordRegister.PasswordChar = '\0';
            }
        }

        private void confirmPassword_Enter(object sender, EventArgs e)
        {
            if (confirmPassword.Text == "Confirm Password")
            {
                confirmPassword.Text = "";
                confirmPassword.PasswordChar = '*';
                confirmPassword.ForeColor = Color.Black;
            }
        }

        private void confirmPassword_Leave(object sender, EventArgs e)
        {
            if (confirmPassword.Text == "")
            {
                confirmPassword.Text = "Confirm Password";
                confirmPassword.ForeColor = Color.Gray;
                confirmPassword.PasswordChar = '\0';
            }
        }

        private void confirmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2.PerformClick();
            }
        }
        private bool dragging = false;
        private Point startPoint = new Point(0, 0);
        private void main_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void main_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.startPoint.X, p.Y - this.startPoint.Y);
            }
        }

        private void main_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            Username.Visible = false;
            Password.Visible = false;
            LoginButton.Visible = false;
            RegisterButton.Visible = false;

            emailRegister.Visible = true;
            usernameRegister.Visible = true;
            passwordRegister.Visible = true;
            confirmPassword.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Username_Enter(object sender, EventArgs e)
        {
            if (Username.Text == "User name")
            {
                Username.Text = "";
                Username.ForeColor = Color.Black;
            }
        }

        private void Username_Leave(object sender, EventArgs e)
        {
            if (Username.Text == "")
            {
                Username.Text = "User name";
                Username.ForeColor = Color.Gray;
            }
        }

        private void Password_Enter(object sender, EventArgs e)
        {
            if (Password.Text == "Password")
            {
                Password.Text = "";
                Password.PasswordChar = '*';
                Password.ForeColor = Color.Black;
            }
        }

        private void Password_Leave(object sender, EventArgs e)
        {
            if (Password.Text == "")
            {
                Password.Text = "Password";
                Password.ForeColor = Color.Gray;
                Password.PasswordChar = '\0';
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            dataBase db = new dataBase();

            String username = Username.Text;
            String password = Password.Text;

            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `registration` WHERE `Username` = @user and `Password` = @pass", db.getConnection());
            command.Parameters.Add("@user", MySqlDbType.VarChar).Value = username;
            command.Parameters.Add("@pass", MySqlDbType.Text).Value = password;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            // check if username exists or not
            if (table.Rows.Count > 0)
            {
                this.Hide();
                mainPage mp = new mainPage();
                mp.ShowDialog();
                this.Close();
            }
            else
            {
                if (username.Trim().Equals(""))
                {
                    MessageBox.Show("Enter Username to Continue!", "Empty Username");
                }
                else if (password.Trim().Equals(""))
                {
                    MessageBox.Show("Enter Password to Continue!", "Empty Password");
                }
                else
                {
                    MessageBox.Show("Wrong Username or Password!", "No User Found");
                }
            }
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoginButton.PerformClick();
            }
        }

    }
}
