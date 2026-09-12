using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayar
{
    public class clsCountry
    {
        private int _ID;
        private string _Name;
        public int ID 
        {
            get { return _ID; }
        }
        public string Name
        {
            get { return _Name; }
        }

        private clsCountry(int ID,  string Name)
        {
            _ID = ID;
            _Name = Name;
        }
        public clsCountry()
        {
            this._ID = 169;
            this._Name = "Syria";
        }

        public static clsCountry FindCountryByName(string name)
        {
            int id = -1;
            if (clsDataAccessCountries.GetCountryByName(ref id, name))
            {
                return new clsCountry(id, name);
            }
            else
            {
                return null;
            }
        }

        public static int GetContryIDByName(string Contryname)
        {
            int id = -1;
            if(clsDataAccessCountries.GetCountryByName(ref id, Contryname))
            { 
                return id;
            }else
            {
                throw new KeyNotFoundException($"the {Contryname} is Not found in Database!!");
            }
            
        }

        public static clsCountry FindCountryByID(int CountryID)
        {
            string name = string.Empty;

            if (clsDataAccessCountries.GetCountryByID(CountryID, ref name))
            {
                return new clsCountry(CountryID, name);
            }
            else
            {
                return null;
            }
        }

        public static string GetCountryNameByID(int CountryID)
        {
            string name = string.Empty;
            if (clsDataAccessCountries.GetCountryByID( CountryID, ref name))
            {
                return name;
            }
            else
            {
                throw new KeyNotFoundException($"the {CountryID} is Not found in Database!!");
            }
        }

        public static DataTable GetAllCountries()
        {
            return clsDataAccessCountries.GetAllCountries();
        }
    }
}
