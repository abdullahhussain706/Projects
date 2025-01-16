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
    public partial class Form12 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-P8LSDET\\SQLEXPRESS;Initial Catalog=lahoreqalanders;Integrated Security=True");
        private string currentInput;
        public Form12(string input)
        {
            InitializeComponent();
            currentInput = input;
            CheckUserRole();
            LoadMatchStatisticsData();
        }
        private void LoadMatchStatisticsData()
        {
            try
            {
                con.Open();

                // SQL query to fetch data from the MatchStatistics table, including player names
                string query = @"SELECT ms.StatisticID, p.PlayerName, ms.RunsScored, ms.WicketsTaken, ms.Catches
                                 FROM New_MatchStatistics ms
                                 INNER JOIN Players p ON ms.PlayerID = p.PlayerID";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Check if any data is returned
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No match statistics data found.");
                }

                // Bind the data to the DataGridView
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading match statistics data: " + ex.Message);
            }
            finally
            {
                con.Close(); // Ensure the connection is closed
            }
        }


        private void CheckUserRole()
        {
            try
            {
                con.Open();

                // SQL query to get the role of the current user
                string query = "SELECT Role FROM Users WHERE Username = @Input OR Email = @Input";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Input", currentInput);
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    string userRole = result.ToString().Trim();
                    MessageBox.Show("User Role: " + userRole); // Debugging purpose

                    // If the role is not "Admin", hide the buttons
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
                con.Close(); // Ensure the connection is closed
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form12_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(currentInput);
            form3.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form10 form10 = new Form10(currentInput);
            form10.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
