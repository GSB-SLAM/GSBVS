using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GSBVS;
using MySqlConnector;
using System.Data;

namespace UnitTestGSBVS
{
    [TestClass]
    public class UnitTestAccesMySql : AccesMySql
    {
        [TestMethod]
        public void TestDBConnection()
        {
            string connection = "Server=localhost;Database=gsb_frais;port=3306;User id=root;password=";
            MySqlConnection connexion = new MySqlConnection(connection);
            MySqlConnection test = DBConnection();
            Assert.AreEqual(connexion.ConnectionString, test.ConnectionString);
        }

        [TestMethod]
        public void TestOpenConnexion()
        {
            MySqlConnection connection = DBConnection();
            connection.Open();
            ConnectionState connect = connection.State;
            MySqlConnection connexion = new MySqlConnection("Server=localhost;Database=gsb_frais;port=3306;User id=root;password=");
            OpenConnection(connexion);
            ConnectionState connectCompare = connexion.State;
            Assert.AreEqual(connectCompare, connect);
        }

        [TestMethod]
        public void TestCloseConnexion()
        {
            MySqlConnection connection = DBConnection();
            connection.Close();
            ConnectionState connect = connection.State;
            MySqlConnection connexion = new MySqlConnection("Server=localhost;Database=gsb_frais;port=3306;User id=root;password=");
            CloseConnection(connexion);
            ConnectionState connectCompare = connexion.State;
            Assert.AreEqual(connectCompare, connect);
        }

        [TestMethod]
        public void TestInsertSql()
        {
            List<string> colonnes = new List<string>();
            colonnes.Add("id");
            colonnes.Add("nom");
            colonnes.Add("prenom");
            List<string> valeurs = new List<string>();
            valeurs.Add("a100");
            valeurs.Add("DeGaule");
            valeurs.Add("Charles");
            string insert = InsertSql("Visiteur", colonnes, valeurs);
            string insertCompare = "Les valeurs 'a100','DeGaule','Charles' ont bien été ajoutées dans la table Visiteur";
            Assert.AreEqual(insertCompare, insert);
        }


        [TestMethod]
        public void TestUpdateSql()
        {
            string tableUpdate = "visiteur";
            List<string> colonnesUpdate = new List<string>();
            string[] tabUpdate = { "ville", "dateembauche" };
            List<string> valeursUpdate = new List<string>();
            string[] tab2Update = { "cvvcvcvcvcv", "01/01/01" };
            colonnesUpdate.AddRange(tabUpdate);
            valeursUpdate.AddRange(tab2Update);
            string update = UpdateSql(tableUpdate, colonnesUpdate, valeursUpdate);
            string updateCompare ="La table visiteur a bien été modifiée";
            Assert.AreEqual(updateCompare, update);
        }

        [TestMethod]
        public void TestUpdate2Sql()
        {
            List<string> colonnes = new List<string>();
            colonnes.Add("id");
            colonnes.Add("nom");
            colonnes.Add("prenom");
            List<string> valeurs = new List<string>();
            valeurs.Add("a101");
            valeurs.Add("2Gaule");
            valeurs.Add("charles");
            List<string> colonnesConditions = new List<string>();
            colonnesConditions.Add("id");
            colonnesConditions.Add("nom");
            colonnesConditions.Add("prenom");
            List<string> valeursConditions = new List<string>();
            valeursConditions.Add("a100");
            valeursConditions.Add("DeGaule");
            valeursConditions.Add("Charles");
            string update = UpdateSql("Visiteur", colonnes, valeurs,colonnesConditions,valeursConditions);
            string updateCompare = "Les valeurs de la table Visiteur ont bien été modifiées";
            Assert.AreEqual(updateCompare, update);
        }

        [TestMethod]
        public void TestDeleteTableSql()
        {
            string table = "Visiteur";
            string delete = DeleteSql(table);
            string deleteCompare = "La table Visiteur a bien été supprimée";
            Assert.AreEqual(deleteCompare, delete);
        }

        [TestMethod]
        public void TestDeleteValeurSql()
        {
            string table = "Visiteur";
            string colonne = "id";
            string valeur = "a131";
            string delete = DeleteSql(table, colonne, valeur);
            string deleteCompare = "La valeur a131 de la table Visiteur a bien été supprimée";
            Assert.AreEqual(deleteCompare, delete);
        }
        [TestMethod]
        public void TestSelectSql()
        {
            string table = "etat";
            List<string> colonnes = new List<string>();
            string[] tab = {"id", "libelle"};
            colonnes.AddRange(tab);
            string select = SelectSql(table, colonnes);
            string selectCompare = "CL\nSaisie clôturée\n" +
                "-----------------------\nCR\nFiche créée, saisie en cours\n-----------------------\nMP\nMise en Paiement"
                +
                "\n-----------------------\nRB\nRemboursée\n-----------------------\nVA\nValidée\n-----------------------"+
                "\n";
            Assert.AreEqual(selectCompare, select);
        }

        [TestMethod]
        public void TestReadRequetes()
        {
            MySqlConnection connexion = DBConnection();
            OpenConnection(connexion);
            MySqlCommand commande = connexion.CreateCommand();
            commande.CommandText = "Select id,nom,prenom from visiteur where id='a100'";
            string read = ReadRequetes(commande);
            CloseConnection(connexion);
            string readCompare = "a100\nDeGaule\nCharles\n-----------------------\n";
            Assert.AreEqual(readCompare, read);
        }

        [TestMethod]
        public void TestWhere()
        {
            List<string> colonnesConditions = new List<string>();
            List<string> valeursConditions = new List<string>();
            string[] tabC = { "id", "nom", "prenom" };
            string[] tabV = { "a100", "DeGaule", "Charles" };
            colonnesConditions.AddRange(tabC);
            valeursConditions.AddRange(tabV);
            string where = Where(colonnesConditions, valeursConditions);
            string whereCompare = " WHERE id = a100 AND  WHERE nom = DeGaule AND  WHERE prenom = Charles";
            Assert.AreEqual(whereCompare, where);
        }
    }
}
