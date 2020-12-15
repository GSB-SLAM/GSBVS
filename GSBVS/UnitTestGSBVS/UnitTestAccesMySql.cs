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
        public void TestDBConnection()
        {
            string connection = "Server=localhost;Database=gsb_frais;port=3306;User id=root;password=";
            MySqlConnection connexion = new MySqlConnection(connection);
            MySqlConnection test = DBConnection();
            Assert.AreEqual(connexion.ConnectionString, test.ConnectionString);
        }

        [TestMethod]
        public void TestUpdateSql()
        {
            List<string> colonnes = new List<string>();
            colonnes.Add("id");
            colonnes.Add("nom");
            colonnes.Add("prenom");
            List<string> valeurs = new List<string>();
            valeurs.Add("a100");
            valeurs.Add("DeGaule");
            valeurs.Add("Charles");
            string update = UpdateSql("Visiteur", colonnes, valeurs);
            string updateCompare = "La table Visiteur a bien été modifiée";
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
            string updateCompare = "Les valeurs 'a100' 'DeGaule' 'Charles' de la table Visiteur ont bien été modifiées";
            Assert.AreEqual(updateCompare, update);
        }

        [TestMethod]
        public void TestOpenConnexion()
        {
            MySqlConnection connection = DBConnection();
            connection.Open();
            ConnectionState connect = connection.State;
            MySqlConnection connexion = new MySqlConnection();
            OpenConnection(connexion);
            ConnectionState connectCompare = connexion.State;
            Assert.AreEqual(connectCompare, connect);
        }
    }
}
