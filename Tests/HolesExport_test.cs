using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

using DrillingHoles;


namespace Tests
{
    [TestClass]
    public class HolesExport_test
    {
        string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\"+ "HolesExport_test.csv";


        [TestInitialize]
        public void Init()
        {
            if ( File.Exists(path) )
                File.Delete(path);
        }

        [TestMethod]
        public void TestHolesExported()
        {
            List<Hole> holes = new List<Hole>()
            {
                  new Hole(new Point(582973.7027, 5929696.8143 ), "MGV3", "NHN + 11,24 m" )
                , new Hole(new Point(582979.9785, 5929692.7757), "MGV4", "NHN + 11,26 m")
            };

            HolesRepository repo = new CSV_HolesRepository(path);
            repo.Add(holes);
            repo.Save();
            Assert.IsTrue(File.Exists(path));
            string[] content =  File.ReadAllLines(path);
            Assert.AreEqual( 2 , content.Length );
        }

        [TestCleanup]
        public void CleanUp()
        {
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}
