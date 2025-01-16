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
    public partial class Form7 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-P8LSDET\\SQLEXPRESS;Initial Catalog=lahoreqalanders;Integrated Security=True");
        private string currentInput;
        private string userRole;


        public Form7(string input)
        {
            InitializeComponent();
            currentInput=input;
            userRole = GetUserRole();
            LoadSponsorsData();
            CheckUserRole();
        }

        private void LoadSponsorsData()
        {
            try
            {
                con.Open();

                // SQL query to fetch data from the Sponsors table
                string query = "SELECT SponsorID, SponsorName, SponsorType, ContractDetails, StartDate, EndDate, Amount FROM Sponsors";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Bind the data to the DataGridView
                dataGridView1.DataSource = dt;

                // Hide specific columns based on user role
                if (userRole == "User")
                {
                    dataGridView1.Columns["StartDate"].Visible = false;
                    dataGridView1.Columns["EndDate"].Visible = false;
                    dataGridView1.Columns["Amount"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading sponsors data: " + ex.Message);
            }
            finally
            {
                con.Close(); // Ensure the connection is closed
            }
        }

        private string GetUserRole()
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
                    return result.ToString().Trim();
                }
                else
                {
                    MessageBox.Show("User role not found.");
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while checking the user role: " + ex.Message);
                return string.Empty;
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
                    MessageBox.Show("User Role: " + userRole); // For debugging

                    // If the role is "User", hide the buttons and columns
                    if (userRole == "User")
                    {
                        button2.Visible = false;
                        
                    }
                }
                else
                {
                    MessageBox.Show("User role not found.");
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


        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(currentInput);
            form3.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form11 form11 = new Form11(currentInput);
            form11.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
