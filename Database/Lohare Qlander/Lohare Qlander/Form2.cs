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
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-P8LSDET\\SQLEXPRESS;Initial Catalog=lahoreqalanders;Integrated Security=True");
        private bool isPasswordVisible = false; // Track password visibility

        public Form2()
        {
            InitializeComponent();
            pictureBox3.Image = Properties.Resources.close_eye;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            string password = textBox2.Text;

            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username/email and password.");
                return;
            }


            string query = "SELECT COUNT(1) FROM Users WHERE (Username = @Input OR Email = @Input) AND PasswordHash = @PasswordHash";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@Input", input);
                    cmd.Parameters.AddWithValue("@PasswordHash", password);  

                    try
                    {
                        // Open the connection
                        con.Open();

                        // Execute the query and get the result
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count == 1)
                        {
                            MessageBox.Show("Login successful!");
                            Form3 form3 = new Form3(input);
                            form3.Show();
                            this.Hide();

                            
                        }
                        else
                        {
                            MessageBox.Show("Login failed. Please check your email and password.");
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                    finally
                    {
                        // Close the connection
                        con.Close();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible; // Toggle visibility
            pictureBox3.Refresh();
            if (isPasswordVisible)
            {
                textBox2.PasswordChar = '\0'; // Show password
                pictureBox3.Image = Properties.Resources.eye; // Set to open eye image
            }
            else
            {
                textBox2.PasswordChar = '*'; // Hide password
                pictureBox3.Image = Properties.Resources.close_eye; // Set to closed eye image
            }
            pictureBox3.Refresh();
        }
    }
}