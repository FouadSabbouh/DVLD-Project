using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DVLDBusinessLayar;

namespace Tests
{
    internal class Program
    {

      static void TestFindByID(int ID)
        {
            clsPerson person = clsPerson.FindByID(ID);
            if (person != null)
            {
                Console.WriteLine($"Person ID: {person.PersonID}");
                Console.WriteLine($"National Number {person.NationalNumber}");
                Console.WriteLine($"First Name {person.FirstName}");
                Console.WriteLine($"Secound Name{person.SecondName}");
                Console.WriteLine($"Third Name: {person.ThirdName}");
                Console.WriteLine($"Last Name: {person.LastName}");
                Console.WriteLine(person.DateOfBirth);
                Console.WriteLine(person.Gender);
                Console.WriteLine(person.Address);
                Console.WriteLine(person.Email);
                Console.WriteLine(person.Phone);
                Console.WriteLine(person.ImagePath);
                Console.WriteLine(person.NationalCountryID);
            }
            else
            {
                Console.WriteLine("The personID was not Found");

            }
        }

        static void TestAddNewPerson()
        {
            clsPerson person = new clsPerson();
            person.NationalNumber = "N22";
            person.FirstName = "Fouad";
            person.SecondName = "Yahya";
            person.ThirdName = "Kamel";
            person.LastName = "Sabbouh";
            person.DateOfBirth = new DateTime(2003, 6, 28);
            person.Gender = clsPerson.enGender.Male;
            person.Address = "Syria, Tartous";
            person.Email = "fwadsbwh@gmail.com";
            person.Phone = "00963996945525";

            person.Save();
            
        }
        static void Main(string[] args)
        {


            clsCountry country = clsCountry.FindCountryByName("JORAN");

            try
            {
                Console.WriteLine(clsCountry.GetContryIDByName("sy"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            


        }
    }
}
