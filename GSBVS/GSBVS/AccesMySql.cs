using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace GSBVS
{
    /// <summary>
    /// Condition d'utilisation de TOUTES les fonctions contenant des listes ou tableaux:L'ordre des paramètres est important, il doit être relatif à la manière 
    /// dont on écrit la fonction.
    /// ex:le paramètre colonnes[0] devra correspondre au paramètre valeurs[0] et ainsi de suite pour toutes les listes, tous les tableaux.
    /// </summary>
    abstract public class AccesMySql
    {
        /// <summary>
        /// Fonction qui permet de se connecter à MySql à l'aide des paramètres en tant que logs.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="database"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static MySqlConnection DBConnecter(string host, int port, string database, string username, string password)
        {
            string connection = "Server=" + host + ";Database=" + database + ";port=" + port + ";User id=" + username + ";password=" + password;
            MySqlConnection connectio = new MySqlConnection(connection);
            return connectio;
        }
        /// <summary>
        /// Fonction qui rentre les paramètres pour se connecter à l'aide de la fonction DBconnecter à MySql.
        /// </summary>
        /// <returns></returns>
        public static MySqlConnection DBConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "gsb_frais";
            string username = "root";
            string password = "";
            return DBConnecter(host, port, database, username, password);
        }
        /// <summary>
        /// Fonction qui permet de connecter l'objet MySqlConnection en paramètre
        /// </summary>
        /// <param name="connexion"></param>
        public static void OpenConnection(MySqlConnection connexion)
        {
            connexion.Open();
            Console.WriteLine("La connexion est établie");
        }
        /// <summary>
        /// Fonction qui permet de déconnecter l'objet MySqlConnection en paramètre
        /// </summary>
        /// <param name="connexion"></param>
        public static void CloseConnection(MySqlConnection connexion)
        {
            connexion.Close();
            Console.WriteLine("La connexion est fermée");
        }
        /// <summary>
        /// Fonction qui permet d'effectuer un insert dans la BD.
        /// Conditions d'utilisation des paramètres:
        /// table:string de la table dans laquelle on va ajouter les valeurs entrées en paramètres.
        /// colonnes:Liste de string qui contient la ou les colonnes dans lesquelles on va ajouter les valeurs en paramètres.
        /// valeurs:Liste de string qui contient la ou les valeurs qu'on ajoute dans les colonnes de la table, toutes deux entrées en paramètres.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="colonnes"></param>
        /// <param name="valeurs"></param>
        public static string InsertSql(string table, List<string> colonnes, List<string> valeurs)
        {
            MySqlConnection connexion = new MySqlConnection();
            MySqlCommand commande = connexion.CreateCommand();
            string colonnesString = "";
            if (colonnes.Count > 1)
            {


                for (int i = 0; i < colonnes.Count; i++)
                {
                    colonnesString += colonnes[i] + ",";
                }
            }
            else
            {
                colonnesString += colonnes[0];
            }
            string valeursString = "";
            if (valeurs.Count > 1)
            {


                for (int i = 0; i < valeurs.Count - 1; i++)
                {
                    valeursString += "'" + valeurs[i] + "',";
                }
                int l = valeurs.Count - 1;
                valeursString += "'" + valeurs[l] + "'";
            }
            else
            {
                valeursString += valeurs[0];
            }
            commande.CommandText = "INSERT INTO " + table + " (" + colonnesString + ") VALUES (" + valeursString + ")";
            string n = "Les valeurs " + valeursString + " ont bien été ajoutées dans la table " + table;
            return n;
        }
        /// <summary>
        /// Fonction qui permet d'update la BD.
        /// Condition d'utilisation des paramètres:
        /// table:string de la table pour laquelle on va modifier les valeurs entrées en paramètres.
        /// colonnes:Liste de string qui contient la ou les colonnes dans lesquelles on va modifier les valeurs en paramètres.
        /// valeursListe de string qui contient la ou les valeurs qu'on modifie dans les colonnes de la table, toutes deux entrées en paramètres.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="colonnes"></param>
        /// <param name="valeurs"></param>
        public static string UpdateSql(string table, List<string> colonnes, List<string> valeurs)
        {
            MySqlConnection connexion = new MySqlConnection();
            MySqlCommand commande = connexion.CreateCommand();
            string colonnesvaleursString = "";
            if (colonnes.Count > 1 && valeurs.Count > 1)
            {
                if (colonnes.Count == valeurs.Count)
                {
                    for (int i = 0; i < colonnes.Count; i++)
                    {
                        colonnesvaleursString += colonnes[i] + "='" + valeurs[i] + "',";
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                colonnesvaleursString += colonnes[0] + "='" + valeurs[0] + "',";
            }
            commande.CommandText = "UPDATE " + table + " SET " + colonnesvaleursString;
            string n = "La table " + table + " a bien été modifiée";
            return n;
        }
        /// <summary>
        /// Fonction qui permet d'update la BD, surcharge d'UpdateSql pour laquelle on ajoute les conditions(where).
        /// Condition d'utilisation des paramètres:
        /// table:string de la table pour laquelle on va modifier les valeurs entrées en paramètres.
        /// colonnes:Liste de string qui contient la ou les colonnes dans lesquelles on va modifier les valeurs en paramètres.
        /// valeursListe de string qui contient la ou les valeurs qu'on modifie dans les colonnes de la table, toutes deux entrées en paramètres.
        /// colonneConditions:Liste de string qui contient la ou les colonnes qu'on entre en condition dans le where.
        /// valeurConditions:Liste de string qui contient la ou les valeurs qu'on entre en condition dans le where
        /// </summary>
        /// <param name="table"></param>
        /// <param name="colonnes"></param>
        /// <param name="valeurs"></param>
        /// <param name="colonneConditions"></param>
        /// <param name="valeurConditions"></param>
        public static string UpdateSql(string table, List<string> colonnes, List<string> valeurs, List<string> colonneConditions, List<string> valeurConditions)
        {
            MySqlConnection connexion = new MySqlConnection();
            MySqlCommand commande = connexion.CreateCommand();
            string colonnesvaleursString = "";
            if (colonnes.Count == valeurs.Count)
            {
                for (int i = 0; i < colonnes.Count; i++)
                {
                    colonnesvaleursString += colonnes[i] + "='" + valeurs[i] + "',";
                }
            }
            else
            {
                throw new Exception();
            }
            string whereString = "";
            string valeursString = "";
            if (colonneConditions.Count > 1 && valeurConditions.Count == colonneConditions.Count)
            {
                for (int i = 0; i < colonneConditions.Count; i++)
                {
                    if (i == colonneConditions.Count - 1)
                    {
                        whereString += " WHERE" + colonneConditions[i] + "=" + valeurConditions[i];
                    }
                    else
                    {
                        whereString += " WHERE" + colonneConditions[i] + "=" + valeurConditions[i] + " AND  ";
                    }
                    valeursString += "'" + valeurConditions[i] + "' ";
                }
            }
            else
            {
                valeursString += "'" + valeurConditions[0] + "'";
                whereString += " WHERE" + colonneConditions[0] + "=" + valeurConditions[0];
            }
            commande.CommandText = "UPDATE " + table + " SET " + whereString;
            string n = "Les valeurs " + valeursString + "de la table " + table + " ont bien été modifiées";
            return n;
        }
        /// <summary>
        /// Fonction qui permet de delete dans la BD
        /// Condition d'utilisation des paramètres:
        /// table:string qui contient la table dont on veut delete toutes les lignes.
        /// </summary>
        /// <param name="table"></param>
        public static string DeleteSql(string table)
        {
            MySqlConnection connexion = new MySqlConnection();
            MySqlCommand commande = connexion.CreateCommand();
            commande.CommandText = "DELETE FROM " + table;
            string n = "La table " + table + " a bien été supprimée";
            return n;
        }
        /// <summary>
        /// Fonction qui permet de delete dans la BD, surcharge de DeleteSql mais pour delete des valeurs d'une table.
        /// Condition d'utilisation des paramètres:
        /// table:string qui contient la table dont on veut delete une ligne.
        /// colonne:string qui contient la colonne dont on veut delete une ligne.
        /// valeur:string qui contient la valeur qu'on veut delete dans la colonne de la table, toutes deux entrées en paramètres.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="colonne"></param>
        /// <param name="valeur"></param>
        public static string DeleteSql(string table, string colonne, string valeur)
        {
            MySqlConnection connexion = new MySqlConnection();
            MySqlCommand commande = connexion.CreateCommand();
            commande.CommandText = "DELETE FROM " + table + " WHERE " + colonne + " = " + valeur;
            string n = "La valeur " + valeur + " de la table " + table + " a bien été supprimée";
            return n;
        }
        /// <summary>
        /// Fonction qui permet de select dans la BD
        /// Condition d'utilisation des paramètres:
        /// table:string de la table pour laquelle on veut select des colonnes.
        /// colonnes:List de string des colonnes qu'on veut select.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="colonnes"></param>
        public static string SelectSql(string table, List<string> colonnes)
        {
            MySqlConnection connexion = DBConnection();
            connexion.Open();
            MySqlCommand commande = connexion.CreateCommand();
            string selectString = "";
            if (colonnes.Count > 1)
            {
                for (int i = 0; i < colonnes.Count; i++)
                {
                    selectString += colonnes[i] + ",";
                }
            }
            else
            {
                selectString += colonnes[0];
            }
            commande.CommandText = "SELECT " + selectString + " FROM " + table;

            string requete = ReadRequetes(commande);
            connexion.Close();
            return requete;
        }
        /// <summary>
        /// Fonction qui permet de select dans la BD, surcharge de SelectSql mais avec une ou des conditions(where).
        /// Condition d'utilisation des paramètres:
        /// table:string de la table pour laquelle on veut select des colonnes.
        /// colonnes:Liste de string des colonnes qu'on veut select.
        /// colonneConditions:Liste de string qui contient la ou les colonnes qu'on entre en condition dans le where.
        /// valeurConditions:Liste de string qui contient la ou les valeurs qu'on entre en condition dans le where
        /// </summary>
        /// <param name="table"></param>
        /// <param name="colonnes"></param>
        /// <param name="colonneConditions"></param>
        /// <param name="valeurConditions"></param>
        public static string SelectSql(string table, List<string> colonnes, List<string> colonneConditions, List<string> valeurConditions)
        {
            MySqlConnection connexion = new MySqlConnection();
            MySqlCommand commande = connexion.CreateCommand();
            string selectString = "";
            if (colonnes.Count > 1)
            {
                for (int i = 0; i < colonnes.Count; i++)
                {
                    selectString += colonnes[i] + ",";
                }
            }
            else
            {
                selectString += colonnes[0];
            }
            string whereString = "";
            if (colonneConditions.Count > 1 && valeurConditions == colonneConditions)
            {
                for (int i = 0; i < colonneConditions.Count; i++)
                {
                    if (i == colonneConditions.Count - 1)
                    {
                        whereString += " WHERE " + colonneConditions[i] + " = " + valeurConditions[i];
                    }
                    else
                    {
                        whereString += " WHERE " + colonneConditions[i] + " = " + valeurConditions[i] + " AND ";
                    }
                }
            }
            else
            {
                whereString += " WHERE " + colonneConditions + "=" + valeurConditions;
            }

            commande.CommandText = "SELECT " + selectString + " FROM " + table + whereString;
            string requete = ReadRequetes(commande);
            return requete;
        }
        /// <summary>
        /// Fonction qui permet de select dans la BD, surcharge de SelectSql mais avec un ou des inner join et une ou des conditions(where).
        /// Condition d'utilisation des paramètres:
        /// tables:tableau de string des tables et des id utilisées dans le select (et les inner join).
        /// tablesAmbigu:Liste de string dans qui contient les tables à entrer pour éviter des erreurs d'ambiguïté.
        /// colonnes:Liste de string des colonnes qu'on veut select.
        /// colonneConditions:Liste de string qui contient la ou les colonnes qu'on entre en condition dans le where.
        /// valeurConditions:Liste de string qui contient la ou les valeurs qu'on entre en condition dans le where
        /// </summary>
        /// <param name="tables"></param>
        /// <param name="tablesAmbigu"></param>
        /// <param name="colonnes"></param>
        /// <param name="colonneConditions"></param>
        /// <param name="valeurConditions"></param>
        public static string SelectSql(string[,] tables, List<string> tablesAmbigu, List<string> colonnes, List<string> colonneConditions, List<string> valeurConditions)
        {
            MySqlConnection connexion = new MySqlConnection();
            MySqlCommand commande = connexion.CreateCommand();
            string selectString = "";
            if (colonnes.Count > 1)
            {

                for (int i = 0; i < colonnes.Count; i++)
                {
                    if ((colonnes[i].Contains("id") || colonnes[i].Contains("mois")) && !colonnes[i].Contains("montantValide"))
                    {
                        selectString += tablesAmbigu + "." + colonnes[i] + ",";
                    }
                    else
                    {
                        selectString += colonnes[i] + ",";
                    }
                }
            }
            else
            {
                selectString += colonnes[0];
            }
            string innerJoinString = "";
            for (int i = 0; i < tables.Length; i++)
            {
                if (i == 0)
                {
                    innerJoinString += tables[i, 0];
                }
                else
                {
                    innerJoinString += tables[i, 0] + " INNER JOIN " + tables[i + 1, 0] + " ON " + tables[i, 0] + "." + tables[i, 1] + "=" + tables[i + 1, 0] + "." + tables[i + 1, 1];
                }

            }
            string whereString = "";
            if (colonneConditions.Count > 1 && valeurConditions == colonneConditions)
            {
                for (int i = 0; i < colonneConditions.Count; i++)
                {
                    if (i == colonneConditions.Count - 1)
                    {
                        whereString += " WHERE " + colonneConditions[i] + " = " + valeurConditions[i];
                    }
                    else
                    {
                        whereString += " WHERE " + colonneConditions[i] + " = " + valeurConditions[i] + " AND ";
                    }
                }
            }
            else
            {
                whereString += " WHERE " + colonneConditions + "=" + valeurConditions;
            }

            commande.CommandText = "SELECT " + selectString + " FROM " + innerJoinString + whereString;
            string requete = ReadRequetes(commande);
            return requete;
        }
        /// <summary>
        /// Fonction locale à la cette classe permettant de renvoyer le résultat des requêtes sous forme
        /// de string et avec une MySqlCommand en paramètre qui contient ladite requête. 
        /// </summary>
        /// <param name="commande"></param>
        /// <returns></returns>
        private static string ReadRequetes(MySqlCommand commande)
        {
            DbDataReader reader = commande.ExecuteReader();
            int y = 0;
            string requete = "";
            reader.Read();
            while (y <= reader.Depth)
            {

                requete += reader.GetString(y);
                y++;
            }
            reader.Close();
            return requete;
        }
    }
}