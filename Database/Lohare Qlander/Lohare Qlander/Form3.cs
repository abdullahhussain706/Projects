using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lohare_Qlander
{
    public partial class Form3 : Form
    {
        private int charIndex = 0;
        private string fullTextLabel1;
        private string fullTextLabel2;
        private string _input;

        public Form3(string input)
        {
            InitializeComponent();
            _input = input;

            fullTextLabel1 = label1.Text; // Store the full text for label1
            fullTextLabel2 = label2.Text; // Store the full text for label2

            label1.Text = ""; // Clear the label1 text initially
            label2.Text = ""; // Clear the label2 text initially

            timer1.Interval = 100; // Set the timer interval (e.g., 100 ms)
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (charIndex < fullTextLabel1.Length)
            {
                label1.Text += fullTextLabel1[charIndex];
            }

            // Reveal characters one by one in label2
            if (charIndex < fullTextLabel2.Length)
            {
                label2.Text += fullTextLabel2[charIndex];
            }

            charIndex++;

            // Stop the timer when the text is fully revealed
            if (charIndex >= fullTextLabel1.Length && charIndex >= fullTextLabel2.Length)
            {
                timer1.Stop();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Form4 form4 = new Form4(_input);
            form4.Show();
            this.Hide();
            Form8 form8 = new Form8(_input);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Form5 form5 = new Form5(_input);
            form5.Show();
            this.Hide();
            Form9 form9 = new Form9(_input);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form12 form12 = new Form12(_input);
            form12.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7(_input);
            form7.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            Form6 form6 = new Form6(_input);
            form6.Show();
            this.Hide();
            Form10 form10 = new Form10(_input);
        }
    }
}
