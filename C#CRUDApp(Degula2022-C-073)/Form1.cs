using ADODB;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_CRUDApp_Degula2022_C_073_
{
    public partial class Form1 : Form
    {
        public ADODB.Connection con = new ADODB.Connection();
        public ADODB.Command cmd = new ADODB.Command();
        public ADODB.Recordset rs = new ADODB.Recordset();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
                con.Open("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"C:\\Users\\PC\\Documents\\MS Access\\C#CRUDApp_DB.mdb\"");
                cmd.ActiveConnection = con;  
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {               
                cmd.CommandText = $"SELECT * FROM UsersTable where uname = '{txtUsername.Text}' and pword = '{txtPass.Text}'";
                rs = cmd.Execute(out object recordsAffected, Type.Missing, -1);

                if (rs.EOF == false)
                {
                    MessageBox.Show("Logged in successfully!", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.None);
                    cleartxt();
                    this.Hide();
                    var crudform = new crudform();
                    crudform.Show();
                }
                else if (txtUsername.Text == "" && txtPass.Text == "")
                {
                    MessageBox.Show("Username and Password cannot be blank.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtUsername.Text == "")
                {
                    MessageBox.Show("Username cannot be blank.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtPass.Text == "")
                {
                    MessageBox.Show("Password cannot be blank.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtPass.Text != "from UsersTable where pword  = txtPass.Text" && txtUsername.Text != "from UsersTable where uname  = txtUsername.Text")
                {
                    MessageBox.Show("Username and Password does not exist. Register first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cleartxt();
                }
                else if (txtPass.Text != "from UsersTable where pword  =  txtPass.Text")
                {
                    MessageBox.Show("Password does not match any username. Try again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cleartxt();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            this.Hide();
            register register = new register();
            register.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                this.Show();
            }
            else
            {
                con.Close();
                Environment.Exit(0);
            }
        }
        private void cleartxt()
        {
            txtUsername.Clear();
            txtPass.Clear();
        }
    }
}
