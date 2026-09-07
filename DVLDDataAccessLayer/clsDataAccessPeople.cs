using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsDataAccessPeople
    {
        public static bool GetPersonInfoByID(int ID,ref string NationalNumber, ref string FirstName, ref string SecondName,
            ref string ThirdName,ref string LastName,ref DateTime DateOfBirth, ref byte Gender,ref string Address,
            ref string Phone,ref string Email, ref int NationalCountryID, ref string ImagePath)
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

        public static int AddNewPerson(string NationalNumber,string FirstName, string SecondName, string ThirdName,
            string LastName, DateTime DateOfBirth, byte Gender, string Address, string Phone,string Email,
            int NationalCountryID, string ImagePath)

        {
            int newPersonID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [dbo].[People]([NationalNo],[FirstName],[SecondName],[ThirdName],[LastName],[DateOfBirth]
                                               ,[Gendor],[Address],[Phone],[Email],[NationalityCountryID],[ImagePath])
                                         VALUES
                                               (@NationalNumber, @FirstName, @SecondName, @ThirdName,@LastName, @DateOfBirth,
                                               @Gender, @Address, @Phone, @Email, @NationalCountryID, @ImagePath);
                                    SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNumber", NationalNumber);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@ThirdName", ThirdName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@NationalCountryID", NationalCountryID);
            command.Parameters.AddWithValue("@ImagePath", string.IsNullOrEmpty(ImagePath) ? (object)DBNull.Value : ImagePath);



            try
            {             
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int id))
                    {
                        newPersonID = id;
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
            finally
            {
                connection.Close();
            }


            return newPersonID;

        }


        public static bool UpdatePerson(int PersonID, string NationalNumber, string FirstName, string SecondName, string ThirdName,
            string LastName, DateTime Date, byte Gender, string Address, string Phone, string Email,
            int NationalCountryID, string ImagePath)
        {
            bool isUpdated = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE [dbo].[People]
                         SET [NationalNo] = @NationalNumber
                            ,[FirstName] = @FirstName
                            ,[SecondName] = @SecondName
                            ,[ThirdName] = @ThirdName
                            ,[LastName] = @LastName
                            ,[DateOfBirth] = @DateOfBirth
                            ,[Gendor] = @Gender
                            ,[Address] = @Address
                            ,[Phone] = @Phone
                            ,[Email] = @Email
                            ,[NationalityCountryID] = @NationalCountryID
                            ,[ImagePath] = @ImagePath
                       WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNumber", NationalNumber);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@ThirdName", ThirdName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", Date);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@NationalCountryID", NationalCountryID);
            command.Parameters.AddWithValue("@ImagePath", string.IsNullOrEmpty(ImagePath) ? (object)DBNull.Value : ImagePath);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    isUpdated = true;
                }
                else
                {
                    isUpdated = false;
                }

            }
            catch (Exception ex)
            {
                isUpdated = false;
                Console.WriteLine($"Error: {ex}");
            }
            finally
            {
                connection.Close();

            }

            return isUpdated;
        }


        public static DataTable GetAllPeople()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM People";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable dataTable = new DataTable();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    dataTable.Load(reader);
                }
                reader.Close();

            }catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
            finally
            {
                connection.Close();
            }

            return dataTable;
        }
       

        public static bool IsPersonExist(int PersonID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT Found=1 FROM People WHERE PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsFound = reader.HasRows;
                reader.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("error", ex);
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }
    }
}
