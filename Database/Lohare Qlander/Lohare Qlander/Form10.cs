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
    public partial class Form10 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-P8LSDET\\SQLEXPRESS;Initial Catalog=lahoreqalanders;Integrated Security=True");
        private string currentInput;

        public Form10(string input)
        {
            InitializeComponent();
            currentInput = input;
        }


        private void LoadPlayerData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-P8LSDET\\SQLEXPRESS;Initial Catalog=lahoreqalanders;Integrated Security=True"))
                {
                    con.Open();
                    string query = "SELECT PlayerID FROM Players"; // Only select PlayerID
                    using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        comboBox1.DataSource = dt;
                        comboBox1.DisplayMember = "PlayerID"; // Display PlayerID
                        comboBox1.ValueMember = "PlayerID"; // Bind PlayerID to ValueMember
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading players: " + ex.Message);
            }
        }

        private void LoadMatchData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-P8LSDET\\SQLEXPRESS;Initial Catalog=lahoreqalanders;Integrated Security=True"))
                {
                    con.Open();
                    string query = "SELECT MatchID FROM New_Matches"; // Select only MatchID
                    using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        comboBox2.DataSource = dt;
                        comboBox2.DisplayMember = "MatchID"; // Display MatchID
                        comboBox2.ValueMember = "MatchID"; // Bind MatchID to ValueMember
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading matches: " + ex.Message);
            }
        }


        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select both Player and Match.");
                return;
            }

            // Get the selected player and match
            var selectedPlayerID = (int)comboBox1.SelectedValue;
            var selectedMatchID = (int)comboBox2.SelectedValue;

            // Get the values from the text boxes
            int runsScored;
            int wicketsTaken;
            int catches;

            // Validate RunsScored
            if (!int.TryParse(textBox1.Text, out runsScored))
            {
                MessageBox.Show("Please enter a valid number for Runs Scored.");
                return;
            }

            // Validate WicketsTaken
            if (!int.TryParse(textBox2.Text, out wicketsTaken))
            {
                MessageBox.Show("Please enter a valid number for Wickets Taken.");
                return;
            }

            // Validate Catches
            if (!int.TryParse(textBox3.Text, out catches))
            {
                MessageBox.Show("Please enter a valid number for Catches.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-P8LSDET\\SQLEXPRESS;Initial Catalog=lahoreqalanders;Integrated Security=True"))
                {
                    con.Open();

                    string query = "INSERT INTO New_MatchStatistics (PlayerID, MatchID, RunsScored, WicketsTaken, Catches) " +
                                   "VALUES (@PlayerID, @MatchID, @RunsScored, @WicketsTaken, @Catches)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PlayerID", selectedPlayerID);
                        cmd.Parameters.AddWithValue("@MatchID", selectedMatchID);
                        cmd.Parameters.AddWithValue("@RunsScored", runsScored);
                        cmd.Parameters.AddWithValue("@WicketsTaken", wicketsTaken);
                        cmd.Parameters.AddWithValue("@Catches", catches);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Statistics added successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select both Player and Match.");
                return;
            }

            // Get the selected player and match
            var selectedPlayerID = (int)comboBox1.SelectedValue;
            var selectedMatchID = (int)comboBox2.SelectedValue;

            // Get the values from the text boxes
            int runsScored;
            int wicketsTaken;
            int catches;

            // Validate RunsScored
            if (!int.TryParse(textBox1.Text, out runsScored))
            {
                MessageBox.Show("Please enter a valid number for Runs Scored.");
                return;
            }

            // Validate WicketsTaken
            if (!int.TryParse(textBox2.Text, out wicketsTaken))
            {
                MessageBox.Show("Please enter a valid number for Wickets Taken.");
                return;
            }

            // Validate Catches
            if (!int.TryParse(textBox3.Text, out catches))
            {
                MessageBox.Show("Please enter a valid number for Catches.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-P8LSDET\\SQLEXPRESS;Initial Catalog=lahoreqalanders;Integrated Security=True"))
                {
                    con.Open();

                    string query = "UPDATE New_MatchStatistics SET RunsScored = @RunsScored, WicketsTaken = @WicketsTaken, Catches = @Catches " +
                                   "WHERE PlayerID = @PlayerID AND MatchID = @MatchID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PlayerID", selectedPlayerID);
                        cmd.Parameters.AddWithValue("@MatchID", selectedMatchID);
                        cmd.Parameters.AddWithValue("@RunsScored", runsScored);
                        cmd.Parameters.AddWithValue("@WicketsTaken", wicketsTaken);
                        cmd.Parameters.AddWithValue("@Catches", catches);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record updated successfully!");
                        }
                        else
                        {
                            MessageBox.Show("No matching record found to update.");
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
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select both Player and Match.");
                return;
            }

            // Get the selected player and match
            var selectedPlayerID = (int)comboBox1.SelectedValue;
            var selectedMatchID = (int)comboBox2.SelectedValue;

            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-P8LSDET\\SQLEXPRESS;Initial Catalog=lahoreqalanders;Integrated Security=True"))
                {
                    con.Open();

                    string query = "DELETE FROM New_MatchStatistics WHERE PlayerID = @PlayerID AND MatchID = @MatchID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PlayerID", selectedPlayerID);
                        cmd.Parameters.AddWithValue("@MatchID", selectedMatchID);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record deleted successfully!");
                        }
                        else
                        {
                            MessageBox.Show("No matching record found to delete.");
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

        private void Form10_Load(object sender, EventArgs e)
        {
            LoadMatchData();
            LoadPlayerData();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
