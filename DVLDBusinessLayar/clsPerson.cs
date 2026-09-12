using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayar
{
    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enum enSearchFeild { All, PersonID, NationalNumber, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Phone, Email, CountryName };
        private enMode _Mode = enMode.AddNew;

        public enum enGender { Male = 0, Female = 1 };

        public int PersonID { get; set; }
        public string NationalNumber { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public enGender Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalCountryID { get; set; }
        public string ImagePath { get; set; }

        public clsPerson()
        {
            this.PersonID = -1;
            this.NationalNumber = string.Empty;
            this.FirstName = string.Empty;
            this.SecondName = string.Empty;
            this.ThirdName = string.Empty;
            this.LastName = string.Empty;
            this.DateOfBirth = DateTime.Now;
            this.Gender = enGender.Male;
            this.Address = string.Empty;
            this.Phone = string.Empty;
            this.Email = string.Empty;
            this.NationalCountryID = 169;
            this.ImagePath = null;

            this._Mode = enMode.AddNew;
        }

        private clsPerson(int PersonID, string NationalNumber, string FirstName, string SecondName,
            string ThirdName, string LastName, DateTime DateOfBirth, enGender gender, string Address,
            string Phone, string Email, int NationalCountryID, string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNumber = NationalNumber;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.ImagePath = ImagePath;
            this.NationalCountryID = NationalCountryID;

            this._Mode = enMode.Update;

        }

        private static string _GetFeilName(enSearchFeild Feild)
        {
            switch(Feild)
            {
                case enSearchFeild.PersonID:return "PersonID";
                case enSearchFeild.NationalNumber: return "NationalNo";
                case enSearchFeild.FirstName: return "FirstName";
                case enSearchFeild.SecondName: return "SecondName";
                case enSearchFeild.ThirdName: return "ThirdName";
                case enSearchFeild.LastName: return "LastName";
                case enSearchFeild.Email: return "Email";
                case enSearchFeild.Gender: return "Gendor";
                case enSearchFeild.CountryName: return "CountryName";
                default: return string.Empty;
            }
        }

        private bool _AddNewPerson()
        {
            
            this.PersonID = clsDataAccessPeople.AddNewPerson(this.NationalNumber, this.FirstName, this.SecondName,
                this.ThirdName, this.LastName, this.DateOfBirth, (byte)this.Gender, this.Address, this.Phone,
                this.Email, this.NationalCountryID, this.ImagePath);

            return this.PersonID > 0;
        }

        private bool _UpdatePerson()
        {
            return clsDataAccessPeople.UpdatePerson(this.PersonID, this.NationalNumber, this.FirstName, this.SecondName,
                this.ThirdName, this.LastName, this.DateOfBirth, (byte)this.Gender, this.Address, this.Phone,
                this.Email, this.NationalCountryID, this.ImagePath);
        }

        public static clsPerson FindByID(int PersonID)
        {
            string NationalNumber = string.Empty;
            string FirstName = string.Empty;
            string SecondName = string.Empty;
            string ThirdName = string.Empty;
            string LastName = string.Empty;
            DateTime DateOfBirth = DateTime.Now;
            byte gender = 0;
            string Address = string.Empty;
            string Phone = string.Empty;
            string Email = string.Empty;
            int NationalCountryID = 0;
            string ImagePath = null;

            // Here you would typically retrieve the data from a database or other data source
            if (clsDataAccessPeople.GetPersonInfoByID(PersonID, ref NationalNumber, ref FirstName, ref SecondName, ref ThirdName,
                ref LastName, ref DateOfBirth, ref gender, ref Address, ref Phone,
                ref Email, ref NationalCountryID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNumber, FirstName, SecondName, ThirdName,
                    LastName, DateOfBirth, (enGender)gender, Address, Phone, Email,
                    NationalCountryID, ImagePath);
            }
            else
            {
                return null;

            }
        }

        public bool Save()
        {
            if (_Mode == enMode.AddNew)
            {
                bool result = _AddNewPerson();
                if (result)
                    _Mode = enMode.Update; // Change mode to Update after adding a new person
                return result;
            }
            else if (_Mode == enMode.Update)
            {
                return _UpdatePerson();

            }
            else
            {
                throw new InvalidOperationException("Invalid mode for saving person.");
            }
        }

        public static DataTable GetAllPeople(enSearchFeild Feild, string ValueSearch= null)
        {
               string FeildName = _GetFeilName(Feild);
               return clsDataAccessPeople.GetAllPeople(FeildName, ValueSearch);
        }

        
        public static bool IsPersonExist(int PersonID)
        {
            return clsDataAccessPeople.IsPersonExist(PersonID);
        }

    }
}
