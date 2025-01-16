using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Lohare_Qlander
{
    public partial class Form4 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-P8LSDET\\SQLEXPRESS;Initial Catalog=lahoreqalanders;Integrated Security=True");
        private string currentInput; 

        public Form4(string input)
        {
            InitializeComponent();
            currentInput = input;
            LoadPlayersData();
            CheckUserRole();
        }

        private void LoadPlayersData()
        {
            try
            {
                con.Open();
                string query = "SELECT * FROM Players";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No players data found.");
                }
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading players data: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void CheckUserRole()
        {

            try
            {
                con.Open();
                string query = "SELECT Role FROM Users WHERE Username = @Input OR Email = @Input";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Input", currentInput);
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    string userRole = result.ToString().Trim();
                    MessageBox.Show("User Role: " + userRole);

                    if (userRole != "Admin")
                    {
                        button2.Visible = false;
                       
                    }
                }
                else
                {
                    MessageBox.Show("User role not found.");
                    button2.Visible = false;
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while checking the user role: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

        }
        private int GetCurrentUserId()
        {
            // Implement logic to get the current user's ID
            // This is a placeholder; replace with your actual logic
            return 1; // Example user ID
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(currentInput);
            form3.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8(currentInput);
            form8.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
