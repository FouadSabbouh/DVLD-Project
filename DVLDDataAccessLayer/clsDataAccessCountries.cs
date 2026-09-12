using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DVLDDataAccessLayer
{
    public class clsDataAccessCountries
    {

        public static bool GetCountryByName(ref int ID, string countryName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @" SELECT * FROM Countries WHERE CountryName like @countryName;";
            SqlCommand commmand = new SqlCommand(query, connection);
            commmand.Parameters.AddWithValue("@countryName", $"{countryName}");

            try
            {
                connection.Open();
                SqlDataReader reader = commmand.ExecuteReader();
                if(reader.Read())
                {
                    ID = Convert.ToInt32(reader[0]);
                    isFound = true;
                }

                else
                {
                    isFound = false;
                }

                reader.Close();
            }catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetCountryByID(int ID, ref string CountryName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @" SELECT * FROM Countries WHERE CountryID = @ID;";
            SqlCommand commmand = new SqlCommand(query, connection);
            commmand.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = commmand.ExecuteReader();
                if (reader.Read())
                {
                    CountryName = reader[1].ToString();
                    isFound = true;
                }

                else
                {
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @" SELECT * FROM Countries;";
            SqlCommand commmand = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = commmand.ExecuteReader();
                if(reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }catch(Exception ex)
            {

            }finally
            { connection.Close(); }

            return dt;
        }
    }
}
