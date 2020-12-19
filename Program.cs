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
            colonnes.AddRange(tab);
            string n = SelectSql(table, colonnes);
            Console.WriteLine(n);
            Console.ReadKey();

            //string table = "visiteur";
            //List<string> colonnes = new List<string>();
            //string[] tab = { "id", "nom", "prenom", "login", "mdp", "adresse", "cp", "ville", "dateembauche" };
            //colonnes.AddRange(tab);
            //string n = SelectSql(table, colonnes);
            //Console.WriteLine(n);
            //Console.ReadKey();

            //string tableInsert = "visiteur";
            //List<string> colonnesInsert = new List<string>();
            //string[] tabInsert = { "id", "nom", "prenom" };
            //colonnesInsert.AddRange(tabInsert);
            //List<string> valeursInsert = new List<string>();
            //string[] tab2Insert = { "a134", "villechak", "Luizsk" };
            //valeursInsert.AddRange(tab2Insert);
            //string l = InsertSql(tableInsert, colonnesInsert, valeursInsert);
            //Console.WriteLine(l);
            //Console.ReadKey();


        }
    }
}