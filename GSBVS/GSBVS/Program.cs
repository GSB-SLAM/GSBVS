using System;
using MySqlConnector;
using System.Collections.Generic;

namespace GSBVS
{
    class Program : AccesMySql
    {
        static void Main(string[] args)
        {
            //string tableSelect = "visiteur";
            //List<string> colonnesSelect = new List<string>();
            //string[] tabSelect = { "id", "nom", "prenom","dateembauche" };
            //colonnesSelect.AddRange(tabSelect);
            //List<string> colonnesConditions = new List<string>();
            //List<string> valeurConditions = new List<string>();
            //string[] tabSelectC = { "id", "nom" };
            //string[] tabSelectV = { "'a100'", "'DeGaule'" };
            //colonnesConditions.AddRange(tabSelectC);
            //valeurConditions.AddRange(tabSelectV);
            //string s = SelectSql(tableSelect, colonnesSelect, colonnesConditions, valeurConditions);
            //Console.WriteLine(s);
            //Console.ReadKey();

            List<string> colonnesConditions = new List<string>();
            List<string> valeursConditions = new List<string>();
            string[] tabC = { "id", "nom", "prenom" };
            string[] tabV = { "a100", "DeGaule", "Charles" };
            colonnesConditions.AddRange(tabC);
            valeursConditions.AddRange(tabV);
            string r = Where(colonnesConditions,valeursConditions);
            Console.WriteLine(r);
            Console.ReadKey();
        }
    }
}