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




    public partial class Form11 : Form
    {
        private string currentInput;
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-P8LSDET\\SQLEXPRESS;Initial Catalog=lahoreqalanders;Integrated Security=True");

        public Form11(string input)
        {
            InitializeComponent();
            currentInput = input;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(currentInput);
            form3.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sponsorName = textBox2.Text;
            string sponsorType = textBox3.Text;
            string contractDetails = textBox4.Text; // This should be contract details, not amount
            DateTime startDate;
            DateTime endDate;
            decimal amount;

            // Validate Amount
            if (!decimal.TryParse(textBox5.Text, out amount)) // This should be textBox5 for amount
            {
                MessageBox.Show("Please enter a valid amount.");
                return;
            }

            // Validate StartDate
            if (!DateTime.TryParse(dateTimePicker1.Text, out startDate))
            {
                MessageBox.Show("Please enter a valid Start Date.");
                return;
            }

            // Validate EndDate
            if (!DateTime.TryParse(dateTimePicker2.Text, out endDate))
            {
                MessageBox.Show("Please enter a valid End Date.");
                return;
            }

            // Validate other fields
            if (string.IsNullOrWhiteSpace(sponsorName) || string.IsNullOrWhiteSpace(sponsorType) || string.IsNullOrWhiteSpace(contractDetails))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-P8LSDET\\SQLEXPRESS;Initial Catalog=lahoreqalanders;Integrated Security=True"))
                {
                    con.Open();
                    string query = "INSERT INTO Sponsors (SponsorName, SponsorType, ContractDetails, StartDate, EndDate, Amount) VALUES (@SponsorName, @SponsorType, @ContractDetails, @StartDate, @EndDate, @Amount)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SponsorName", sponsorName);
                        cmd.Parameters.AddWithValue("@SponsorType", sponsorType);
                        cmd.Parameters.AddWithValue("@ContractDetails", contractDetails);
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                        cmd.Parameters.AddWithValue("@Amount", amount); // Ensure this matches the decimal value
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Sponsor added successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding the sponsor: " + ex.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int sponsorID;
            if (!int.TryParse(textBox1.Text, out sponsorID))
            {
                MessageBox.Show("Please enter a valid Sponsor ID.");
                return;
            }

            string sponsorName = textBox2.Text;
            string sponsorType = textBox3.Text;
            string contractDetails = textBox4.Text;
            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;
            decimal amount;

            // Validate Amount
            if (!decimal.TryParse(textBox5.Text, out amount))
            {
                MessageBox.Show("Please enter a valid amount.");
                return;
            }

            // Validate other fields
            if (string.IsNullOrWhiteSpace(sponsorName) || string.IsNullOrWhiteSpace(sponsorType) || string.IsNullOrWhiteSpace(contractDetails))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-P8LSDET\\SQLEXPRESS;Initial Catalog=lahoreqalanders;Integrated Security=True"))
                {
                    con.Open();
                    string query = "UPDATE Sponsors SET SponsorName = @SponsorName, SponsorType = @SponsorType, ContractDetails = @ContractDetails, StartDate = @StartDate, EndDate = @EndDate, Amount = @Amount WHERE SponsorID = @SponsorID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SponsorName", sponsorName);
                        cmd.Parameters.AddWithValue("@SponsorType", sponsorType);
                        cmd.Parameters.AddWithValue("@ContractDetails", contractDetails);
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                        cmd.Parameters.AddWithValue("@Amount", amount);
                        cmd.Parameters.AddWithValue("@SponsorID", sponsorID); // Use SponsorID from textBox1

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Sponsor updated successfully!");
                        }
                        else
                        {
                            MessageBox.Show("No sponsor found with the provided ID.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating the sponsor: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int sponsorID;
            if (!int.TryParse(textBox1.Text, out sponsorID))
            {
                MessageBox.Show("Please enter a valid Sponsor ID.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-P8LSDET\\SQLEXPRESS;Initial Catalog=lahoreqalanders;Integrated Security=True"))
                {
                    con.Open();
                    string query = "DELETE FROM Sponsors WHERE SponsorID = @SponsorID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SponsorID", sponsorID);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Sponsor deleted successfully!");
                        }
                        else
                        {
                            MessageBox.Show("No sponsor found with the provided ID.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the sponsor: " + ex.Message);
            }
        }

        private void Form11_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
