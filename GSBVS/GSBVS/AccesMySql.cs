﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
        protected const string innerJoinVisiteurFicheFrais = "VisiteurFicheFrais";
        protected const string innerJoinFicheFraisVisiteur = "FicheFraisVisiteur";
        protected const string innerJoinVisiteurLigneFraisForfait = "VisiteurLigneFraisForfait";
        protected const string innerJoinLigneFraisForfaitVisiteur = "LigneFraisForfaitVisiteur";
        protected const string innerJoinVisiteurLigneFraisHorsForfait = "VisiteurLigneFraisHorsForfait";
        protected const string innerJoinLigneFraisHorsForfaitVisiteur = "LigneFraisHorsForfaitVisiteur";
        protected const string innerJoinFraisForfaitLigneFraisForfait = "FraisForfaitLigneFraisForfait";
        protected const string innerJoinLigneFraisForfaitFraisForfait = "LigneFraisForfaitFraisForfait";
        protected const string innerJoinFicheFraisEtat = "FicheFraisEtat";
        protected const string innerJoinEtatFicheFrais = "EtatFicheFrais";
        protected const string innerJoinFicheFraisTypeVehicule = "FicheFraisTypeVehicule";
        protected const string innerJoinTypeVehiculeFicheFrais = "TypeVehiculeFicheFrais";
        protected const string innerJoinFicheFraisLigneFraisForfait = "FicheFraisLigneFraisForfait";
        protected const string innerJoinLigneFraisForfaitFicheFrais = "LigneFraisForfaitFicheFrais";
        protected const string innerJoinFicheFraisLigneFraisHorsForfait = "FicheFraisLigneFraisHorsForfait";
        protected const string innerJoinLigneFraisHorsForfaitFicheFrais = "LigneFraisHorsForfaitFicheFrais";
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
            MySqlConnection connexion = DBConnection();
            OpenConnection(connexion);
            MySqlCommand commande = connexion.CreateCommand();
            string colonnesString = "";
            if (colonnes.Count > 1)
            {


                for (int i = 0; i < colonnes.Count; i++)
                {
                    if (i == colonnes.Count - 1)
                    {
                        colonnesString += colonnes[i];
                    }
                    else
                    {
                        colonnesString += colonnes[i] + ",";
                    }
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
            commande.ExecuteReader();
            CloseConnection(connexion);
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
            MySqlConnection connexion = DBConnection();
            OpenConnection(connexion);
            MySqlCommand commande = connexion.CreateCommand();
            string colonnesvaleursString = "";
            if (colonnes.Count > 1 && valeurs.Count > 1)
            {
                if (colonnes.Count == valeurs.Count)
                {
                    for (int i = 0; i < colonnes.Count; i++)
                    {
                        if (i == colonnes.Count - 1)
                        {
                            colonnesvaleursString += colonnes[i] + "='" + valeurs[i] + "'";
                        }
                        else
                        {
                            colonnesvaleursString += colonnes[i] + "='" + valeurs[i] + "',";
                        }
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
            commande.ExecuteReader();
            CloseConnection(connexion);
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
            MySqlConnection connexion = DBConnection();
            OpenConnection(connexion);
            MySqlCommand commande = connexion.CreateCommand();
            string colonnesvaleursString = "";
            if (colonnes.Count == valeurs.Count)
            {
                for (int i = 0; i < colonnes.Count; i++)
                {
                    if (i == colonnes.Count - 1)
                    {
                        colonnesvaleursString += colonnes[i] + "='" + valeurs[i]+ "'";
                    }
                    else
                    {
                        colonnesvaleursString += colonnes[i] + "='" + valeurs[i] + "',";
                    }
                }
            }
            else
            {
                throw new Exception();
            }
            string whereString = Where(colonneConditions, valeurConditions);
            commande.CommandText = "UPDATE " + table + " SET " +colonnesvaleursString+" "+ whereString;
            MySqlDataReader reader = commande.ExecuteReader();
            reader.Read();
            reader.Close();
            string n = "Les valeurs de la table " + table + " ont bien été modifiées";
            CloseConnection(connexion);
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
            MySqlConnection connexion = DBConnection();
            OpenConnection(connexion);
            MySqlCommand commande = connexion.CreateCommand();
            commande.CommandText = "DELETE FROM " + table;
            string n = "La table " + table + " a bien été supprimée";
            CloseConnection(connexion);
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
            MySqlConnection connexion = DBConnection();
            OpenConnection(connexion);
            MySqlCommand commande = connexion.CreateCommand();
            commande.CommandText = "DELETE FROM " + table + " WHERE " + colonne + " = " + valeur;
            string delete = "La valeur " + valeur + " de la table " + table + " a bien été supprimée";
            CloseConnection(connexion);
            return delete;
        }
        /// <summary>
        /// Fonction qui permet de select dans la BD
        /// Condition d'utilisation des paramètres:
        /// table:string de la table pour laquelle on veut select des colonnes.
        /// colonnes:List de string des colonnes qu'on veut select.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="colonnes"></param>
        public static string SelectSql(string table, List<string> colonnes, string distinct)
        {
            MySqlConnection connexion = DBConnection();
            OpenConnection(connexion);
            MySqlCommand commande = connexion.CreateCommand();
            string selectString = SelectColonnes(colonnes);
            if (distinct != "")
            {
                commande.CommandText = "SELECT " + distinct + " " + selectString + " FROM " + table;
            }
            else
            {
                commande.CommandText = "SELECT " + selectString + " FROM " + table;
            }
            string requete = ReadRequetes(commande);
            CloseConnection(connexion);
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
        public static string SelectSql(string table, List<string> colonnes, List<string> colonneConditions, List<string> valeurConditions, string distinct)
        {
            MySqlConnection connexion = DBConnection();
            OpenConnection(connexion);
            MySqlCommand commande = connexion.CreateCommand();
            string selectString = SelectColonnes(colonnes);
            string whereString = Where(colonneConditions, valeurConditions);
            if (distinct != "")
            {
                commande.CommandText = "SELECT " + distinct + " " + selectString + " FROM " + table + whereString;
            }
            else
            {
                commande.CommandText = "SELECT " + selectString + " FROM " + table + whereString;
            }
            string requete = ReadRequetes(commande);
            CloseConnection(connexion);
            return requete;
        }
        /// <summary>
        /// Fonction qui permet de select dans la BD, surcharge de SelectSql mais avec une ou plusieurs jointures(inner join) et une ou plusieurs conditions(where).
        /// Condition d'utilisation des paramètres:
        /// tables:Liste de string des tables utilisées dans le select (et les inner join). Modèle : TableTable (la première à laquelle on inner join la deuxième).
        /// tablesAmbigues:Liste de string dans qui contient les tables à entrer pour éviter des erreurs d'ambiguïté.
        /// colonnes:Liste de string des colonnes qu'on veut select.
        /// colonneConditions:Liste de string qui contient la ou les colonnes qu'on entre en condition dans le where.
        /// valeurConditions:Liste de string qui contient la ou les valeurs qu'on entre en condition dans le where
        /// tablesAmbiguesConditions:Liste de string qui contient la ou les tables pour lesquelles il peut y avoir des ambiguités de clé primaire
        /// </summary>
        /// <param name="tables"></param>
        /// <param name="tablesAmbigues"></param>
        /// <param name="colonnes"></param>
        /// <param name="colonneConditions"></param>
        /// <param name="valeurConditions"></param>
        /// <param name="tablesAmbiguesConditions"></param>
        public static string SelectSql(string tableFrom, List<string> tables, List<string> tablesAmbigues, List<string> colonnes, List<string> colonneConditions,
            List<string> valeurConditions, List<string> tablesAmbiguesConditions, string distinct)
        {
            MySqlConnection connexion = DBConnection();
            OpenConnection(connexion);
            MySqlCommand commande = connexion.CreateCommand();
            string selectString = SelectColonnesAmbigues(colonnes, tablesAmbigues);
            string innerJoinString = tableFrom;
            for (int i = 0; i < tables.Count; i++)
            {
                switch (tables[i])
                {

                    case innerJoinVisiteurFicheFrais:
                        innerJoinString += " INNER JOIN fichefrais on visiteur.id=fichefrais.idvisiteur ";
                        break;
                    case innerJoinVisiteurLigneFraisForfait:
                        innerJoinString += " INNER JOIN lignefraisforfait on visiteur.id=lignefraisforfait.idvisiteur ";
                        break;
                    case innerJoinVisiteurLigneFraisHorsForfait:
                        innerJoinString += " INNER JOIN lignefraishorsforfait on visiteur.id=lignefraishorsforfait.idvisiteur ";
                        break;
                    case innerJoinFraisForfaitLigneFraisForfait:
                        innerJoinString += " INNER JOIN lignefraisforfait on fraisforfait.id=lignefraisforfait.idfraisforfait ";
                        break;
                    case innerJoinFicheFraisEtat:
                        innerJoinString += " INNER JOIN etat on fichefrais.idetat=etat.id ";
                        break;
                    case innerJoinFicheFraisTypeVehicule:
                        innerJoinString += " INNER JOIN typevehicule on fichefrais.idtypevehicule=typevehicule.id ";
                        break;
                    case innerJoinFicheFraisLigneFraisForfait:
                        innerJoinString += " INNER JOIN lignefraisforfait on fichefrais.mois=lignefraisforfait.mois ";
                        break;
                    case innerJoinFicheFraisLigneFraisHorsForfait:
                        innerJoinString += " INNER JOIN lignefraishorsforfait on fichefrais.mois=lignefraishorsforfait.mois ";
                        break;
                    case innerJoinFicheFraisVisiteur:
                        innerJoinString += " INNER JOIN visiteur on fichefrais.idvisiteur=visiteur.id ";
                        break;
                    case innerJoinLigneFraisForfaitVisiteur:
                        innerJoinString += " INNER JOIN visiteur on lignefraisforfait.idvisiteur=visiteur.id ";
                        break;
                    case innerJoinLigneFraisHorsForfaitVisiteur:
                        innerJoinString += " INNER JOIN visiteur on lignefraishorsforfait.idvisiteur=visiteur.id ";
                        break;
                    case innerJoinEtatFicheFrais:
                        innerJoinString += " INNER JOIN fiche frais on etat.id=fichefrais.idetat ";
                        break;
                    case innerJoinTypeVehiculeFicheFrais:
                        innerJoinString += " INNER JOIN fichefrais on typevehicule.id=fichefrais.idtypevehicule ";
                        break;
                    case innerJoinLigneFraisForfaitFicheFrais:
                        innerJoinString += " INNER JOIN fichefrais on lignefraisforfait.mois=fichefrais.mois ";
                        break;
                    case innerJoinLigneFraisHorsForfaitFicheFrais:
                        innerJoinString += " INNER JOIN fichefrais on lignefraishorsforfait.mois=fichefrais.mois ";
                        break;
                }
            }
            string whereString = Where(colonneConditions, valeurConditions, tablesAmbiguesConditions);
            if (distinct != "")
            {
                commande.CommandText = "SELECT " + distinct + " " + selectString + " FROM " + innerJoinString + whereString;
            }
            else
            {
                commande.CommandText = "SELECT " + selectString + " FROM " + innerJoinString + whereString;
            }
            string requete = ReadRequetes(commande);
            CloseConnection(connexion);
            return requete;
        }

        /// <summary>
        /// Fonction locale à la cette classe permettant de renvoyer le résultat des requêtes sous forme
        /// de string et avec une MySqlCommand en paramètre qui contient ladite requête. 
        /// </summary>
        /// <param name="commande"></param>
        /// <returns></returns>
        protected static string ReadRequetes(MySqlCommand commande)
        {
            MySqlDataReader reader = commande.ExecuteReader();
            int y = 0;
            string requete = "";
            while (reader.Read())
            {
                y = 0;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    requete += reader.GetString(y).ToString() + "\n";
                    y++;
                }
                requete += "-----------------------\n";
            }
            reader.Close();
            return requete;
        }

        /// <summary>
        /// Fonction qui retourne un string d'une condition where en fonction des 
        /// valeurs et colonnes passées en paramètres.
        /// </summary>
        /// <param name="colonnesConditions"></param>
        /// <param name="valeursConditions"></param>
        /// <returns></returns>
        protected static string Where(List<string> colonnesConditions, List<string> valeursConditions)
        {
            string whereString = " WHERE ";
            if (colonnesConditions.Count > 1 && valeursConditions.Count == colonnesConditions.Count)
            {
                for (int i = 0; i < colonnesConditions.Count; i++)
                {
                    if (i == colonnesConditions.Count - 1)
                    {
                        whereString += colonnesConditions[i] + " = " + "'" + valeursConditions[i] + "'";
                    }
                    else
                    {
                        whereString += colonnesConditions[i] + " = " + "'" + valeursConditions[i] + "'" + " AND ";
                    }
                }
            }
            else
            {
                whereString += colonnesConditions[0] + "=" + "'" + valeursConditions[0] + "'";
            }
            return whereString;
        }
        /// <summary>
        /// Fonction qui retourne un string d'une condition where en fonction des 
        /// valeurs et colonnes passées en paramètres(Surcharge de Where pour les conditions ambigues).
        /// </summary>
        /// <param name="colonnesConditions"></param>
        /// <param name="valeursConditions"></param>
        /// <param name="tablesAmbiguesConditions"></param>
        /// <returns></returns>
        protected static string Where(List<string> colonnesConditions, List<string> valeursConditions, List<string> tablesAmbiguesConditions)
        {
            string whereString = " WHERE ";
            int cpt = 0;
            if (colonnesConditions.Count > 1 && valeursConditions.Count == colonnesConditions.Count)
            {
                for (int i = 0; i < colonnesConditions.Count; i++)
                {
                    if ((colonnesConditions[i].Contains("id") || colonnesConditions[i].Contains("mois")) && !colonnesConditions[i].Contains("montantValide"))
                    {
                        if (i == colonnesConditions.Count - 1)
                        {
                            whereString += tablesAmbiguesConditions[cpt] + "." + colonnesConditions[i] + " = " + "'" + valeursConditions[i] + "'";
                            cpt++;
                        }
                        else
                        {
                            whereString += tablesAmbiguesConditions[cpt] + "." + colonnesConditions[i] + " = " + "'" + valeursConditions[i] + "'" + " AND ";
                            cpt++;
                        }

                    }
                    else
                    {
                        if (i == colonnesConditions.Count - 1)
                        {
                            whereString += colonnesConditions[i] + " = " + "'" + valeursConditions[i] + "'";
                        }
                        else
                        {
                            whereString += colonnesConditions[i] + " = " + "'" + valeursConditions[i] + "'" + " AND ";
                        }
                    }
                }
            }
            else
            {
                if ((colonnesConditions[0].Contains("id") || colonnesConditions[0].Contains("mois")) && !colonnesConditions[0].Contains("montantValide"))
                {
                    whereString += tablesAmbiguesConditions[0] + "." + colonnesConditions[0] + "=" + "'" + valeursConditions[0] + "'";
                }
                else
                {
                    whereString += colonnesConditions[0] + "=" + "'" + valeursConditions[0] + "'";
                }
            }
            return whereString;
        }

        /// <summary>
        /// Fonction qui retourne un string d'une requête select en fonction
        /// des colonnes passées en paramètres.
        /// </summary>
        /// <param name="colonnes"></param>
        /// <returns></returns>
        private static string SelectColonnes(List<string> colonnes)
        {
            string selectString = "";
            if (colonnes.Count > 1)
            {
                for (int i = 0; i < colonnes.Count; i++)
                {
                    if (i == colonnes.Count - 1)
                    {
                        selectString += colonnes[i];
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
            return selectString;
        }

        /// <summary>
        /// Fonction qui retourne un string d'une requête select en fonction
        /// des colonnes passées en paramètres.(elle gère les ambiguïtés au niveau
        /// des clés primaires à l'aide du tableau passé en paramètre).
        /// </summary>
        /// <param name="colonnes"></param>
        /// <param name="tablesAmbigues"></param>
        /// <returns></returns>
        private static string SelectColonnesAmbigues(List<string> colonnes, List<string> tablesAmbigues)
        {
            string selectString = "";
            int cpt = 0;
            if (colonnes.Count > 1)
            {

                for (int i = 0; i < colonnes.Count; i++)
                {
                    if ((colonnes[i].Contains("id") || colonnes[i].Contains("mois")) && !colonnes[i].Contains("montantValide"))
                    {
                        if (i == colonnes.Count - 1)
                        {
                            selectString += tablesAmbigues[cpt] + "." + colonnes[i];
                            cpt++;
                        }
                        else
                        {
                            selectString += tablesAmbigues[cpt] + "." + colonnes[i] + ",";
                            cpt++;
                        }
                    }
                    else
                    {
                        if (i == colonnes.Count - 1)
                        {
                            selectString += colonnes[i];
                        }
                        else
                        {
                            selectString += colonnes[i] + ",";
                        }
                    }
                }
            }
            else
            {
                selectString += colonnes[0];
            }
            return selectString;
        }
    }
}