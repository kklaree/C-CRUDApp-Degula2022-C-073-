using System;
using System.Collections.Generic;
using System.ComponentModel;    
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace C_CRUDApp_Degula2022_C_073_
{
    public partial class crudform : Form
    {
        public ADODB.Connection con = new ADODB.Connection();
        public ADODB.Command cmd = new ADODB.Command();
        public ADODB.Recordset rs = new ADODB.Recordset();
        string userstr;
        public crudform()
        {
            InitializeComponent();
        }

        private void crudform_Load(object sender, EventArgs e)
        {
            con.Open("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"C:\\Users\\PC\\Documents\\MS Access\\C#CRUDApp_DB.mdb\"");
            cmd.ActiveConnection = con;
            LoadData();
        }
        private void LoadData()
        {
            DataGridView1.Rows.Clear();
            cmd.CommandText = "Select uname from UsersTable";
            rs = cmd.Execute(out object recordsAffected, Type.Missing, -1);

            while (!rs.EOF)
            {
                DataGridView1.Rows.Add(rs.Fields[0].Value);
                rs.MoveNext();
            }
        }
        private void cleartxt()
        {
            txtUsername.Clear();
            txtPass.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.CommandText = $"SELECT * FROM UsersTable WHERE uname = '{txtUsername.Text}'";
                rs = cmd.Execute(out object recordsAffectedSelect, Type.Missing, -1);

                if (!rs.EOF)
                {
                    MessageBox.Show("Username already exists! Try another.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cleartxt();
                    LoadData();
                }
                else if (string.IsNullOrEmpty(txtUsername.Text) && string.IsNullOrEmpty(txtPass.Text))
                {
                    MessageBox.Show("Username and Password cannot be blank.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrEmpty(txtPass.Text))
                {
                    MessageBox.Show("Password cannot be blank.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrEmpty(txtUsername.Text))
                {
                    MessageBox.Show("Username cannot be blank.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    cmd.CommandText = $"INSERT INTO UsersTable (uname, pword) VALUES ('{txtUsername.Text}', '{txtPass.Text}')";
                    cmd.Execute(out object recordsAffectedInsert, Type.Missing, -1);

                    MessageBox.Show("Record added successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cleartxt();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete the account?", "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                this.Show();
            }
            else
            {
                try
                {
                    cmd.CommandText = $"delete from UsersTable where uname = '{txtUsername.Text}'";
                    cmd.Execute(out object recordsAffected, Type.Missing, -1);

                    MessageBox.Show("Record deleted successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cleartxt();
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUdate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to edit your password?", "Edit confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                this.Show();
            }
            else
            {
                try
                {
                    if (string.IsNullOrEmpty(txtUsername.Text) && string.IsNullOrEmpty(txtPass.Text))
                    {
                        MessageBox.Show("Username and Password cannot be blank.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (string.IsNullOrEmpty(txtPass.Text))
                    {
                        MessageBox.Show("Password cannot be blank.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (string.IsNullOrEmpty(txtUsername.Text))
                    {
                        MessageBox.Show("Username cannot be blank.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        cmd.CommandText = $"update UsersTable set pword = '{txtPass.Text}' where uname = '{txtUsername.Text}'";
                        cmd.Execute(out object recordsAffected, Type.Missing, -1);
                        MessageBox.Show("Password updated successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cleartxt();
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to Logout?", "Logout confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                this.Show();
            }
            else
            {
                this.Hide();
                Form1 form1 = new Form1();
                form1.Show();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cleartxt();
        }

        private void CmdExit_Click(object sender, EventArgs e)
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

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                if (DataGridView1.SelectedRows.Count > 0)
                {
                    userstr = DataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    txtUsername.Text = userstr;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
