using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GSBVS
{
    public class application : AccesMySql
    {
        public void MajEtat()
        {
            GestionDates date = new GestionDates();
            DateTime today = DateTime.Today;
            string premiereDateString = "01/" + today.Month +"/"+ today.Year;
            DateTime premiereDate = Convert.ToDateTime(premiereDateString);
            string deuxiemeDateString = "10/" + today.Month + "/" + today.Year;
            DateTime deuxiemeDate = Convert.ToDateTime(deuxiemeDateString);
            if (date.EntreDates(premiereDate, deuxiemeDate))
            {
                string table = "etat";
                List<string> colonnes = new List<string>();
                string[] tabColonnes = { "idetat" };
                colonnes.AddRange(tabColonnes);
                List<string> valeurs = new List<string>();
                string[] tabValeurs = { "CL" };
                valeurs.AddRange(tabValeurs);
                List<string> colonneConditions = new List<string>();
                string[] tabColonnesConditions = { "mois" };
                colonneConditions.AddRange(tabColonnesConditions);
                List<string> valeurConditions = new List<string>();
                string moisPrecedent = date.GetMoisPrecedent(DateTime.Now);
                string[] tabValeursConditions = { moisPrecedent };
                valeurConditions.AddRange(tabValeursConditions);
                string update = UpdateSql(table, colonnes, valeurs, colonneConditions, valeurConditions);
            }
        }

    }
}
