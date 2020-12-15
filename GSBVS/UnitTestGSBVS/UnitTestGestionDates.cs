using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GSBVS;

namespace UnitTestGSBVS
{
    [TestClass]
    public class UnitTestGestionDates : GestionDates
    {
        [TestMethod]
        public void TestGetMoisPrecedent()
        {
            string n = GetMoisPrecedent();
            Assert.AreEqual(n, "11");
        }
        [TestMethod]
        public void TestGetMoisPrecedentPara()
        {
            string n = GetMoisPrecedent(DateTime.Today);
            Assert.AreEqual(n, "11");
        }
        [TestMethod]
        public void TestGetMoisSuivant()
        {
            string n = GetMoisSuivant();
            Assert.AreEqual(n, "01");
        }
        [TestMethod]
        public void TestGetMoisSuivantPara()
        {
            string n = GetMoisSuivant(DateTime.Today);
            Assert.AreEqual(n, "01");
        }
        [TestMethod]
        public void EntreDates2Para()
        {
            DateTime today = DateTime.Today;
            DateTime yesterday = today.AddDays(-1);
            DateTime tomorrow = today.AddDays(1);
            bool b = EntreDates(yesterday, tomorrow);
            Assert.AreEqual(b, true);
        }
        [TestMethod]
        public void EntreDates3Para()
        {
            DateTime today = DateTime.Today;
            DateTime yesterday = today.AddDays(-1);
            DateTime tomorrow = today.AddDays(1);
            bool b = EntreDates(yesterday,today, tomorrow);
            Assert.AreEqual(b, true);
        }
    }
}
