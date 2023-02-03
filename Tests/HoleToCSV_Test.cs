using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DrillingHoles;

namespace Tests
{
    [TestClass]
    public class HoleToCSV_Test
    {
        [TestMethod]
        public void HoleToCSV()
        {
            Hole hole =  new Hole(new Point(123.34 , 234.45) , "GWM 5", "NHN + 11,22 m") ;
            IHoleToStingConverter converter = new HoleToCSVStingConverter();
            string expected = "123,34\t234,45\tGWM 5\tNHN + 11,22 m";
            string actual = converter.AsString(hole);
            Assert.AreEqual(expected, actual);
        }
    }
}
