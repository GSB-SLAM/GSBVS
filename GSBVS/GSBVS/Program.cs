using System;
using MySqlConnector;
using System.Collections.Generic;

namespace GSBVS
{
    class Program : AccesMySql
    {
        static void Main(string[] args)
        {
            string table = "etat";
            List<string> colonnes = new List<string>();
            string[] tab = { "id", "libelle" };
            //colonnes.AddRange(tab);
            colonnes.Add("id");
            colonnes.Add("nom");
            string n = SelectSql(table, colonnes);
            Console.WriteLine(n);
            Console.ReadKey();
        }
    }
}