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
    public partial class register : Form
    {
        public ADODB.Connection con = new ADODB.Connection();
        public ADODB.Command cmd = new ADODB.Command();
        public ADODB.Recordset rs = new ADODB.Recordset();
        public register()
        {
            InitializeComponent();
        }

        private void register_Load(object sender, EventArgs e)
        {
            con.Open("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"C:\\Users\\PC\\Documents\\MS Access\\C#CRUDApp_DB.mdb\"");
            cmd.ActiveConnection = con;
        }
        private void cleartxt()
        {
            txtUsername.Clear();
            txtPass.Clear();
            txtConfirm.Clear();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.CommandText = "SELECT * FROM UsersTable WHERE uname = '" + txtUsername.Text + "'";
                rs = cmd.Execute(out object recordsAffectedSelect, Type.Missing, -1);

                if (!rs.EOF)
                {
                    MessageBox.Show("Account already exists! Try another.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cleartxt();
                }
                else if (string.IsNullOrEmpty(txtUsername.Text) && string.IsNullOrEmpty(txtPass.Text))
                {
                    MessageBox.Show("Username and Password cannot be blank.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrEmpty(txtUsername.Text))
                {
                    MessageBox.Show("Username cannot be blank.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cleartxt();
                }
                else if (string.IsNullOrEmpty(txtPass.Text))
                {
                    MessageBox.Show("Password cannot be blank.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cleartxt();
                }
                else if (string.IsNullOrEmpty(txtConfirm.Text))
                {
                    MessageBox.Show("Password cannot be blank.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
                else
                {
                    cmd.CommandText = $"INSERT INTO UsersTable (uname, pword) VALUES ('{txtUsername.Text}', '{txtPass.Text}')";
                    cmd.Execute(out object recordsAffectedInsert, Type.Missing, -1);

                    MessageBox.Show("Record added successfully! Please proceed to the Login form", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cleartxt();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void txtConfirm_TextChanged(object sender, EventArgs e)
        {
            if (!txtConfirm.Text.Equals(txtPass.Text))
            {
                lblMatch.Text = "Password does not match!";
            }
            else
            {
                lblMatch.Text = "";
            }
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
    }
}
