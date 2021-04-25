using System;
using System.Collections.Generic;
using System.Text;

namespace GSBVS
{
    public class GestionDates
    {
        /// <summary>
        /// Fonction qui retourne vrai si le jour actuel est entre les deux dates entrées 
        /// en paramètres sinon il retourne faux.
        /// </summary>
        /// <param name="premierJour"></param>
        /// <param name="deuxiemeJour"></param>
        /// <returns></returns>
        public bool EntreDates(DateTime premierJour, DateTime deuxiemeJour)
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

        /// <summary>
        /// Fonction qui retourne si une date testée est entre les deux autres, tous trois 
        /// entrés en paramètres.
        /// </summary>
        /// <param name="premierJour"></param>
        /// <param name="dateTestee"></param>
        /// <param name="deuxiemeJour"></param>
        /// <returns></returns>
        public bool EntreDates(DateTime premierJour, DateTime dateTestee, DateTime deuxiemeJour)
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

        /// <summary>
        /// Fonction qui retourne le mois précédent de la date actuelle sous forme mm.
        /// </summary>
        /// <returns></returns>
        public string GetMoisPrecedent()
        {
            DateTime today = DateTime.Today;
            DateTime moisPrecedentDateTime = today.AddMonths(-1);
            string mois = ConvertMoisToString(moisPrecedentDateTime);
            return mois;
        }

        /// <summary>
        /// Fonction qui retourne le mois précédent de la date testée sous forme mm.
        /// </summary>
        /// <param name="dateTesteeDateTime"></param>
        /// <returns></returns>
        public string GetMoisPrecedent(DateTime dateTesteeDateTime)
        {
            DateTime moisPrecedentDateTime = dateTesteeDateTime.AddMonths(-1);
            string mois = ConvertMoisToString(moisPrecedentDateTime);
            return mois;
        }

        /// <summary>
        /// Fonction qui retourne le mois suivant de la date actuelle sous forme mm.
        /// </summary>
        /// <returns></returns>
        public string GetMoisSuivant()
        {
            DateTime today = DateTime.Today;
            DateTime moisSuivantDateTime = today.AddMonths(1);
            string mois = ConvertMoisToString(moisSuivantDateTime);
            return mois;
        }

        /// <summary>
        /// Fonction qui retourne le mois précédent de la date testée sous forme mm.
        /// </summary>
        /// <param name="dateTesteeDateTime"></param>
        /// <returns></returns>
        public string GetMoisSuivant(DateTime dateTesteeDateTime)
        {
            DateTime moisSuivantDateTime = dateTesteeDateTime.AddMonths(1);
            string mois = ConvertMoisToString(moisSuivantDateTime);
            return mois;
        }

        /// <summary>
        /// Fonction utilisée pour convertir le mois d'une date entrée en paramètre
        /// sous forme de string.
        /// </summary>
        /// <param name="dateTestee"></param>
        /// <returns></returns>
        public string ConvertMoisToString(DateTime dateTestee)
        {
            string date = Convert.ToString(dateTestee);
            string mois = Convert.ToString(date[3]) + Convert.ToString(date[4]);
            return mois;
        }
    }
}
