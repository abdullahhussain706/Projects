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
    public partial class Form6 : Form
    {
        
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-P8LSDET\\SQLEXPRESS;Initial Catalog=lahoreqalanders;Integrated Security=True");
        private string currentInput;

        public Form6(string input)
        {
            InitializeComponent();
            currentInput = input;
            LoadStatisticsData();
        }


        private void LoadStatisticsData()
        {
            try
            {
                // SQL query to fetch aggregated match statistics
                string query = @"
            SELECT 
                COUNT(MatchID) AS TotalMatches,
                SUM(RunsScored) AS TotalRuns,
                AVG(RunsScored) AS AverageRuns,
                SUM(WicketsTaken) AS TotalWickets,
                MAX(RunsScored) AS HighestRuns
            FROM New_MatchStatistics";

                // Open the SQL connection
                con.Open();

                // Execute the query using SqlCommand
                SqlCommand cmd = new SqlCommand(query, con);

                // Use SqlDataAdapter to fill the data
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Bind the data to DataGridView
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading match statistics: " + ex.Message);
            }
            finally
            {
                // Close the connection
                con.Close();
            }
        }




        private void label1_Click(object sender, EventArgs e)
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

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }
    }
}
