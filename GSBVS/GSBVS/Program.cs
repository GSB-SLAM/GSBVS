using System;
using MySqlConnector;
using System.Collections.Generic;

namespace GSBVS
{
    class Program : AccesMySql
    {
        static void Main(string[] args)
        {
            string visiteur = "visiteur";
            List<string> visiteurs = new List<string>();
            visiteurs.Add("id");
            string n = SelectSql(visiteur, visiteurs);
            Console.WriteLine(n);
            Console.ReadKey();
        }
    }
}