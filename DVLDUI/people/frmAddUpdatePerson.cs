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
using DVLDUI.Properties;
using System.Net.Mail;
using DVLDUI.Libraries;

namespace DVLDUI.people
{
    public partial class frmAddUpdatePerson : Form
    {

        public frmAddUpdatePerson()
        {
            InitializeComponent();
            _mode = enMode.AddNew;
            ;
        }

        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            _mode = enMode.Update;
            _PersonID = PersonID;

        }


        private enum enMode { AddNew = 0, Update = 1 };

        private enMode _mode;
        private clsPerson _person;
        private int _PersonID = -1;

        private void _FillContrycomboBox()
        {
            CBCountry.DataSource = clsCountry.GetAllCountries();
            CBCountry.DisplayMember = "CountryName";
            CBCountry.ValueMember = "CountryID";

        }
        private void _ResetAllComponants()
        {
            _FillContrycomboBox();
            if(_mode == enMode.AddNew)
            {
                lblAddUpdate.Text = "Add New Person";
                _person = new clsPerson();
            }
            else
            {
                lblAddUpdate.Text = "Edite Persone";
                lblPerosnID.Text = _PersonID.ToString();
            }

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            cbGender.SelectedIndex = cbGender.FindString("Male");
            pictureBox.Image = Resources.Male_512;
            txtAddress.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);
            dateTimePicker1.Value = DateTime.Now.AddYears(-18);
            dateTimePicker1.MinDate = DateTime.Now.AddYears(-100);
            CBCountry.SelectedValue = _person.Country.ID;

        }

        private void _LoadData()
        {
            _person = clsPerson.FindByID(_PersonID);
            txtFirstName.Text = _person.FirstName;
            txtSecondName.Text = _person.SecondName;
            txtThirdName.Text = _person.ThirdName;
            txtLastName.Text = _person.LastName;
            txtNationalNo.Text = _person.NationalNumber;
            string gender = _person.Gender == clsPerson.enGender.Male ? "Male" : "Female";
            cbGender.SelectedIndex = cbGender.FindString(gender);
            txtAddress.Text = _person.Address;
            txtEmail.Text = _person.Email;
            txtPhone.Text = _person.Phone;
            dateTimePicker1.Value = _person.DateOfBirth;
            CBCountry.SelectedValue = _person.Country.ID;
        }
        private void _RestDefaultValues()
        {
            _ResetAllComponants();
            if(_mode == enMode.Update)
                _LoadData();
        }

       
        


        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _RestDefaultValues();
        }

        private void cbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(pictureBox.ImageLocation == null)
            {
                if (cbGender.SelectedIndex == cbGender.FindString("Male"))
                {
                    pictureBox.Image = Resources.Male_512;
                }
                else
                {
                    pictureBox.Image = Resources.Female_512;
                }
            }
        }

     

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNationalNo.Text))
            {
                
                if (clsPerson.IsPersonExist(txtNationalNo.Text) && _mode == enMode.AddNew)
                {
                    e.Cancel= true;
                    errorProvider1.SetError(txtNationalNo, "the national no is used!!");
                }

                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txtNationalNo, "");
                }
            }
            else
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "this Feild is required!!");
            }

        }

      

        private void FeildRequired_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((TextBox)sender).Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(((TextBox)sender), "this Feild is required!!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(((TextBox)sender), "");
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                if(!clsValidate.IsValidEmail(txtEmail.Text))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtEmail, "this Email is not valid!!");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txtEmail, "");
                }
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please fix the errors before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to save this information?", "Confirm Save", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result != DialogResult.OK)
                return;

            _person.FirstName = txtFirstName.Text;
            _person.LastName = txtLastName.Text;
            _person.SecondName = txtSecondName.Text;
            _person.ThirdName = txtThirdName.Text;
            _person.Email = txtEmail.Text;
            _person.Phone = txtPhone.Text;
            _person.Address = txtAddress.Text;
            _person.NationalNumber = txtNationalNo.Text;
            _person.Gender = cbGender.SelectedIndex == cbGender.FindString("Male") ? clsPerson.enGender.Male : clsPerson.enGender.Female;
            _person.Country = clsCountry.FindCountryByID(Convert.ToInt32(CBCountry.SelectedValue));
            _person.DateOfBirth = dateTimePicker1.Value;
            
            




            if (_person.Save())
            {

                MessageBox.Show("informations is saved seccessfully","sccessfully",MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                _mode = enMode.Update;
                _PersonID = _person.PersonID;
                _RestDefaultValues();
                return;
            }
            else
            {
                MessageBox.Show("dose not save informatoin, was somthing wrong!!", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
