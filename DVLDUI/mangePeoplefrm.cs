using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayar;

namespace DVLDUI
{
    public partial class mangePeoplefrm : Form
    {

        DataTable Countries = clsCountry.GetAllCountries();
        DataTable People = clsPerson.GetAllPeople(clsPerson.enSearchFeild.All);

        private void _PerformPeopleDataOptions()
        {
            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    People = clsPerson.GetAllPeople(clsPerson.enSearchFeild.PersonID, numericUpDown1.Value.ToString());
                    dataGridView1.DataSource = People;
                    break;
                case 2:
                    People = clsPerson.GetAllPeople(clsPerson.enSearchFeild.NationalNumber, textBox1.Text);
                    dataGridView1.DataSource = People;
                    break;
                case 3:
                    People = clsPerson.GetAllPeople(clsPerson.enSearchFeild.CountryName, comboBox3.Text);
                    dataGridView1.DataSource = People;
                    break;
                case 4:
                    People = clsPerson.GetAllPeople(clsPerson.enSearchFeild.Email, textBox1.Text);
                    dataGridView1.DataSource = People;
                    break;
                case 5:
                    People = clsPerson.GetAllPeople(clsPerson.enSearchFeild.FirstName, textBox1.Text);
                    dataGridView1.DataSource = People;
                    break;
                case 6:
                    People = clsPerson.GetAllPeople(clsPerson.enSearchFeild.LastName, textBox1.Text);
                    dataGridView1.DataSource = People;
                    break;
                case 7:
                    People = clsPerson.GetAllPeople(clsPerson.enSearchFeild.Gender, comboBox2.Text);
                    dataGridView1.DataSource = People;
                    break;
            }
        }
        public mangePeoplefrm()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           switch(comboBox1.SelectedIndex)
            {
                case 0:
                    textBox1.Visible = false;
                    numericUpDown1.Visible = false;
                    comboBox2.Visible = false;
                    comboBox3.Visible = false;
                    break;
                case 1:
                    textBox1.Visible = false;
                    numericUpDown1.Visible = true;
                    comboBox2.Visible = false;
                    comboBox3.Visible = false;

                    break;

                case 3:
                    textBox1.Visible = false;
                    numericUpDown1.Visible = false;
                    comboBox2.Visible = false;
                    comboBox3.Visible = true;
                    break;
                case 2:
                case 4:
                case 5:
                case 6:
                    textBox1.Visible = true;
                    numericUpDown1.Visible = false;
                    comboBox2.Visible = false;
                    comboBox3.Visible = false;
                    break;

                case 7:
                    textBox1.Visible = false;
                    numericUpDown1.Visible = false;
                    comboBox2.Visible = true;
                    comboBox3.Visible = false;
                    break;
            }
        }

        private void mangePeoplefrm_Load(object sender, EventArgs e)
        {
            comboBox3.DataSource = Countries;
            comboBox3.DisplayMember = "CountryName";
            comboBox3.ValueMember = "CountryID";
            comboBox2.SelectedIndex = 0;
            dataGridView1.DataSource = People;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _PerformPeopleDataOptions();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            _PerformPeopleDataOptions();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            _PerformPeopleDataOptions();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _PerformPeopleDataOptions();
        }
    }
}
