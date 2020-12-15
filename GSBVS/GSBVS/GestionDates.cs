using System;
using System.Collections.Generic;
using System.Text;

namespace GSBVS
{
    public abstract class GestionDates
    {
        public static bool EntreDates(DateTime premierJour, DateTime deuxiemeJour)
        {
            DateTime today = DateTime.Today;
            if (premierJour < today && today < deuxiemeJour)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool EntreDates(DateTime premierJour, DateTime dateTestee, DateTime deuxiemeJour)
        {
            if (premierJour < dateTestee && dateTestee < deuxiemeJour)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetMoisPrecedent()
        {
            DateTime today = DateTime.Today;
            DateTime moisPrecedentDateTime = today.AddMonths(-1);
            string mois = ConvertToString(moisPrecedentDateTime);
            return mois;
        }

        public static string GetMoisPrecedent(DateTime dateTesteeDateTime)
        {
            DateTime moisPrecedentDateTime = dateTesteeDateTime.AddMonths(-1);
            string mois = ConvertToString(moisPrecedentDateTime);
            return mois;
        }

        public static string GetMoisSuivant()
        {
            DateTime today = DateTime.Today;
            DateTime moisSuivantDateTime = today.AddMonths(1);
            string mois = ConvertToString(moisSuivantDateTime);
            return mois;
        }

        public static string GetMoisSuivant(DateTime dateTesteeDateTime)
        {
            DateTime moisSuivantDateTime = dateTesteeDateTime.AddMonths(1);
            string mois = ConvertToString(moisSuivantDateTime);
            return mois;
        }

        public static string ConvertToString(DateTime dateTestee)
        {
            string date = Convert.ToString(dateTestee);
            string mois = Convert.ToString(date[3]) + Convert.ToString(date[4]);
            return mois;
        }
    }
}
