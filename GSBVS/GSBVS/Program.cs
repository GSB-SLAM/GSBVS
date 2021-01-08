using System;
using MySqlConnector;
using System.Collections.Generic;

namespace GSBVS
{
    class Program : AccesMySql
    {
        static void Main(string[] args)
        {
            GestionDates date = new GestionDates();
            DateTime today = DateTime.Today;
            string premiereDateString = "01/" + today.Month + "/" + today.Year;
            DateTime premiereDate = Convert.ToDateTime(premiereDateString);
            string deuxiemeDateString = "10/" + today.Month + "/" + today.Year;
            DateTime deuxiemeDate = Convert.ToDateTime(deuxiemeDateString);
            if (date.EntreDates(premiereDate, deuxiemeDate))
            {
                string table = "fichefrais";
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
                string moisPrecedent = "";
                if (date.GetMoisPrecedent(DateTime.Now) == "12")
                {
                    moisPrecedent += today.Year - 1 + date.GetMoisPrecedent(DateTime.Now);
                }
                else
                {
                    moisPrecedent += today.Year + date.GetMoisPrecedent(DateTime.Now);
                }
                string[] tabValeursConditions = { moisPrecedent };
                valeurConditions.AddRange(tabValeursConditions);
                string update = UpdateSql(table, colonnes, valeurs, colonneConditions, valeurConditions);
            }
            string troisièmeDateString = "20/" + today.Month + "/" + today.Year;
            DateTime troisiemeDate = Convert.ToDateTime(troisièmeDateString);
            int cpt = 1;
            string quatriemeDateString = "";
            if (today.Month == 01 || today.Month == 03 || today.Month == 05 || today.Month == 07 || today.Month == 08 || today.Month == 10 || today.Month == 12) 
            {
                quatriemeDateString += "31/"+today.Month+"/"+today.Year;
            }
            else if(today.Month == 02 && cpt%4!=0)
            {
                quatriemeDateString += "28/" + today.Month + "/" + today.Year;
                cpt++;
            }
            else if(today.Month == 02 && cpt % 4 == 0)
            {
                quatriemeDateString += "29/" + today.Month + "/" + today.Year;
                cpt++;
            }
            else
            {
                quatriemeDateString += "31/" + today.Month + "/" + today.Year;
            }
            DateTime quatiemeDate = Convert.ToDateTime(quatriemeDateString);
            if (date.EntreDates(troisiemeDate, quatiemeDate))
            {
                string table = "fichefrais";
                List<string> colonnes = new List<string>();
                string[] tabColonnes = { "idetat" };
                colonnes.AddRange(tabColonnes);
                List<string> valeurs = new List<string>();
                string[] tabValeurs = { "RB" };
                valeurs.AddRange(tabValeurs);
                List<string> colonneConditions = new List<string>();
                string[] tabColonnesConditions = { "idetat" };
                colonneConditions.AddRange(tabColonnesConditions);
                List<string> valeurConditions = new List<string>();
                string[] tabValeursConditions = { "VA" };
                valeurConditions.AddRange(tabValeursConditions);
                string update = UpdateSql(table, colonnes, valeurs, colonneConditions, valeurConditions);
            }
        }
    }
}