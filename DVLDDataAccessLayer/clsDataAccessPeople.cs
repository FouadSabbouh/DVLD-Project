using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsDataAccessPeople
    {
        public static bool GetPersonInfoByID(
            int ID,
            ref string NationalNumber,
            ref string FirstName,
            ref string SecondName,
            ref string ThirdName,
            ref string LastName,
            ref DateTime DateOfBirth,
            ref byte Gender,
            ref string Address,
            ref string Phone,
            ref string Email,
            ref int NationalCountryID,
            ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection =  new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM People WHERE PersonID = @ID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    NationalNumber = reader["NationalNo"].ToString();
                    FirstName = reader["FirstName"].ToString();
                    SecondName = reader["SecondName"].ToString();
                    ThirdName = reader["ThirdName"].ToString();
                    LastName = reader["LastName"].ToString();

                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = (byte)reader["Gendor"];

                    Address = reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();
                    Email = reader["Email"].ToString();

                    NationalCountryID =
                        (int)reader["NationalityCountryID"];

                    ImagePath = reader["ImagePath"] == DBNull.Value
                        ? ""
                        : reader["ImagePath"].ToString();
                }

                reader.Close();
            }
            catch(Exception ex) 
            {
                isFound = false;
                Console.WriteLine($"error: {ex}");
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
    }
}
