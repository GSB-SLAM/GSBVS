using System;
using MySqlConnector;
using System.Collections.Generic;

namespace GSBVS
{
    class Program : AccesMySql
    {
        static void Main(string[] args)
        {
            List<string> tables = new List<string>();
            List<string> tablesAmbigues = new List<string>();
            List<string> colonnes = new List<string>();
            List<string> colonneConditions = new List<string>();
            List<string> valeurConditions = new List<string>();
            List<string> tablesAmbiguesConditions = new List<string>();
            string[] table = { "VisiteurFicheFrais", "FicheFraisEtat" };
            string[] tableAmbigue = { "visiteur"};
            string[] colonne = { "id", "nom", "prenom" };
            string[] colonneCondition = { "id" };
            string[] valeurCondition = { "CL" };
            string[] tableAmbigueCondition = { "etat" };
            tables.AddRange(table);
            tablesAmbigues.AddRange(tableAmbigue);
            colonnes.AddRange(colonne);
            colonneConditions.AddRange(colonneCondition);
            valeurConditions.AddRange(valeurCondition);
            tablesAmbiguesConditions.AddRange(tableAmbigueCondition);
            string distinct = "DISTINCT";
            string tableFrom = "visiteur";
            string s = SelectSql(tableFrom,tables, tablesAmbigues,colonnes,colonneConditions,valeurConditions,tablesAmbiguesConditions,distinct);
            Console.WriteLine(s);
            Console.ReadKey();
            //string tableSelect = "visiteur";
            //List<string> colonnesSelect = new List<string>();
            //string[] tabSelect = { "id", "nom", "prenom", "dateembauche" };
            //colonnesSelect.AddRange(tabSelect);
            //List<string> colonnesConditions = new List<string>();
            //List<string> valeurConditions = new List<string>();
            //string[] tabSelectC = { "id", "nom" };
            //string[] tabSelectV = { "a100", "DeGaule" };
            //string distinct = "DISTINCT";
            //colonnesConditions.AddRange(tabSelectC);
            //valeurConditions.AddRange(tabSelectV);
            //string s = SelectSql(tableSelect, colonnesSelect, colonnesConditions, valeurConditions,distinct);
            //Console.WriteLine(s);
            //Console.ReadKey();

            //List<string> colonnesConditions = new List<string>();
            //List<string> valeursConditions = new List<string>();
            //string[] tabC = { "id", "nom", "prenom" };
            //string[] tabV = { "a100", "DeGaule", "Charles" };
            //colonnesConditions.AddRange(tabC);
            //valeursConditions.AddRange(tabV);
            //string r = Where(colonnesConditions,valeursConditions);
            //Console.WriteLine(r);
            //Console.ReadKey();


        }
    }
}