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
    public partial class Form9 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-P8LSDET\\SQLEXPRESS;Initial Catalog=lahoreqalanders;Integrated Security=True");
        private string currentInput; 

        public Form9(string input)
        {
            currentInput = input;
            InitializeComponent();
            
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            int hours = (int)numericUpDown1.Value;
    int minutes = (int)numericUpDown2.Value;
    string amPm = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : "AM";

    // Validate text boxes for required input
    if (string.IsNullOrWhiteSpace(textBox2.Text) ||
        string.IsNullOrWhiteSpace(textBox3.Text) ||
        string.IsNullOrWhiteSpace(textBox4.Text))
    {
        MessageBox.Show("Please fill out all required fields (Location, Opponent, Result).");
        return;
    }

    // Convert to 24-hour format
    if (amPm == "PM" && hours != 12)
    {
        hours += 12;
    }
    else if (amPm == "AM" && hours == 12)
    {
        hours = 0; // Midnight case
    }

    DateTime selectedTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, minutes, 0);

    // Debugging: Show input values using string concatenation
    MessageBox.Show("Date: " + dateTimePicker1.Value.Date.ToShortDateString() +
                    " | Time: " + selectedTime.ToShortTimeString() +
                    " | Location: " + textBox2.Text.Trim() +
                    " | Opponent: " + textBox3.Text.Trim() +
                    " | Result: " + textBox4.Text.Trim());

    try
    {
        using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-P8LSDET\\SQLEXPRESS;Initial Catalog=lahoreqalanders;Integrated Security=True"))
        {
            con.Open();

            string query = "INSERT INTO New_Matches (MatchDate, MatchStartTime, Location, OpponentTeam, MatchResult) " +
                           "VALUES (@MatchDate, @MatchStartTime, @Location, @OpponentTeam, @MatchResult)";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                // Add parameters to the SQL query
                cmd.Parameters.AddWithValue("@MatchDate", dateTimePicker1.Value.Date);
                cmd.Parameters.AddWithValue("@MatchStartTime", selectedTime.TimeOfDay);
                cmd.Parameters.AddWithValue("@Location", textBox2.Text.Trim()); // Trim to remove leading/trailing spaces
                cmd.Parameters.AddWithValue("@OpponentTeam", textBox3.Text.Trim()); // Trim to remove leading/trailing spaces
                cmd.Parameters.AddWithValue("@MatchResult", textBox4.Text.Trim()); // Trim to remove leading/trailing spaces

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Match added successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to add match.");
                }
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("An error occurred: " + ex.Message);
    }
        }



        private void button2_Click(object sender, EventArgs e)
        {
                    if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter a valid MatchID.");
                return;
            }

            // Validate other text boxes for required input
            if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Please fill out all required fields (Location, Opponent, Result).");
                return;
            }

            int hours = (int)numericUpDown1.Value;
            int minutes = (int)numericUpDown2.Value;
            string amPm = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : "AM";

            // Convert to 24-hour format
            if (amPm == "PM" && hours != 12)
            {
                hours += 12;
            }
            else if (amPm == "AM" && hours == 12)
            {
                hours = 0; // Midnight case
            }

            DateTime selectedTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, minutes, 0);

            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-P8LSDET\\SQLEXPRESS;Initial Catalog=lahoreqalanders;Integrated Security=True"))
                {
                    con.Open();

                    string query = "UPDATE New_Matches SET MatchDate = @MatchDate, " +
                                   "MatchStartTime = @MatchStartTime, Location = @Location, " +
                                   "OpponentTeam = @OpponentTeam, MatchResult = @MatchResult " +
                                   "WHERE MatchID = @MatchID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters to the SQL query
                        cmd.Parameters.AddWithValue("@MatchID", int.Parse(textBox1.Text));
                        cmd.Parameters.AddWithValue("@MatchDate", dateTimePicker1.Value.Date);
                        cmd.Parameters.AddWithValue("@MatchStartTime", selectedTime.TimeOfDay);
                        cmd.Parameters.AddWithValue("@Location", textBox2.Text.Trim()); // Trim to remove leading/trailing spaces
                        cmd.Parameters.AddWithValue("@OpponentTeam", textBox3.Text.Trim()); // Trim to remove leading/trailing spaces
                        cmd.Parameters.AddWithValue("@MatchResult", textBox4.Text.Trim()); // Trim to remove leading/trailing spaces

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Match updated successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to update match. Make sure the MatchID exists.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

            }

            private void button3_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Please enter a valid MatchID.");
                    return;
                }

                try
                {
                    using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-P8LSDET\\SQLEXPRESS;Initial Catalog=lahoreqalanders;Integrated Security=True"))
                    {
                        con.Open();

                        string query = "DELETE FROM New_Matches WHERE MatchID = @MatchID";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            // Add parameters to the SQL query
                            cmd.Parameters.AddWithValue("@MatchID", int.Parse(textBox1.Text));

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Match deleted successfully!");
                            }
                            else
                            {
                                MessageBox.Show("No match found with the specified ID.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(currentInput);
            form3.Show();
            this.Hide();
        }

        private void Form9_Load(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
