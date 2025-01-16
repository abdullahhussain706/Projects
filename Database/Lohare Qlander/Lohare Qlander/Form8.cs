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
     
    public partial class Form8 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-P8LSDET\\SQLEXPRESS;Initial Catalog=lahoreqalanders;Integrated Security=True");
        private string currentInput; 
        
        public Form8(string input)
        {
            InitializeComponent();
            currentInput = input;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

         private void button1_Click(object sender, EventArgs e)
        {

     

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Player Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method to prevent further processing
            }

            // Retrieve values from the form controls
            string playerName = textBox2.Text;
            DateTime? dateOfBirth = dateTimePicker1.Value; // Assuming you have a DateTimePicker control
            string role = textBox3.Text;
            string battingStyle = textBox4.Text;
            string bowlingStyle = textBox5.Text;
            string nationality = textBox6.Text;
            string currentTeam = textBox7.Text;
            bool isCaptain = checkBox1.Checked;

            try
            {
                con.Open();

                // SQL query to insert data into the Players table
                string query = "INSERT INTO Players (PlayerName, DateOfBirth, Role, BattingStyle, BowlingStyle, Nationality, CurrentTeam, IsCaptain) " +
                               "VALUES (@PlayerName, @DateOfBirth, @Role, @BattingStyle, @BowlingStyle, @Nationality, @CurrentTeam, @IsCaptain)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PlayerName", playerName);
                cmd.Parameters.AddWithValue("@DateOfBirth", (object)dateOfBirth ?? DBNull.Value); // Handle null value for DateOfBirth
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@BattingStyle", battingStyle);
                cmd.Parameters.AddWithValue("@BowlingStyle", bowlingStyle);
                cmd.Parameters.AddWithValue("@Nationality", nationality);
                cmd.Parameters.AddWithValue("@CurrentTeam", currentTeam);
                cmd.Parameters.AddWithValue("@IsCaptain", isCaptain);

                // Execute the command
                cmd.ExecuteNonQuery();

                MessageBox.Show("Player added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding the player: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close(); // Ensure the connection is closed
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("PlayerID (TextBox1) cannot be null or empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method to prevent further processing
            }

            // Retrieve the PlayerID
            int playerId;
            if (!int.TryParse(textBox1.Text, out playerId))
            {
                MessageBox.Show("Invalid PlayerID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method to prevent further processing
            }

            // Check if textBox2 is empty or contains only white space
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Player Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method to prevent further processing
            }

            // Retrieve values from the form controls
            string playerName = textBox2.Text;
            DateTime dateOfBirth = dateTimePicker1.Value; // Assuming you have a DateTimePicker control
            string role = textBox3.Text;
            string battingStyle = textBox4.Text;
            string bowlingStyle = textBox5.Text;
            string nationality = textBox6.Text;
            string currentTeam = textBox7.Text;
            bool isCaptain = checkBox1.Checked;

            try
            {
                con.Open();

                // SQL query to update data in the Players table
                string query = "UPDATE Players SET PlayerName = @PlayerName, DateOfBirth = @DateOfBirth, Role = @Role, " +
                               "BattingStyle = @BattingStyle, BowlingStyle = @BowlingStyle, Nationality = @Nationality, " +
                               "CurrentTeam = @CurrentTeam, IsCaptain = @IsCaptain WHERE PlayerID = @PlayerID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PlayerName", playerName);
                cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth); // Handle DateTime directly
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@BattingStyle", battingStyle);
                cmd.Parameters.AddWithValue("@BowlingStyle", bowlingStyle);
                cmd.Parameters.AddWithValue("@Nationality", nationality);
                cmd.Parameters.AddWithValue("@CurrentTeam", currentTeam);
                cmd.Parameters.AddWithValue("@IsCaptain", isCaptain);
                cmd.Parameters.AddWithValue("@PlayerID", playerId); // Add the PlayerID parameter

                // Execute the command
                cmd.ExecuteNonQuery();

                MessageBox.Show("Player updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating the player: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close(); // Ensure the connection is closed
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("PlayerID (TextBox1) cannot be null or empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method to prevent further processing
            }

            // Retrieve the PlayerID
            int playerId;
            if (!int.TryParse(textBox1.Text, out playerId))
            {
                MessageBox.Show("Invalid PlayerID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method to prevent further processing
            }

            try
            {
                con.Open();

                // SQL query to delete data from the Players table
                string query = "DELETE FROM Players WHERE PlayerID = @PlayerID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PlayerID", playerId); // Add the PlayerID parameter

                // Execute the command
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Player deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No player found with the given PlayerID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the player: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close(); // Ensure the connection is closed
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            Form3 form3 = new Form3(currentInput);
            form3.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

       
    }
}
