using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace Tests
{
    [TestClass]
    public class Regex_Test
    {

        [DataTestMethod]
        [DataRow("NHN + 11,26 m")]
        [DataRow("NON - 11,26 m")]
        public void Test_IsMatch(string content)
        {
            string pattern = @"[a-zA-Z]+\s+\+ [0-9]+,[0-9]+ m";
            bool r = Regex.IsMatch(content, pattern);
            Assert.IsTrue(Regex.IsMatch(content, pattern, RegexOptions.CultureInvariant));
            //Assert.IsTrue(Regex.IsMatch(content, pattern));
        }

        [DataTestMethod]
        [DataRow("NHN + 11,26 m")]
        [DataRow("NHN - 11,26 m")]
        [DataRow("NHN + 11.26 m")]
        //[DataRow("NON - 11,26 m")]
        public void Test_IsDescription(string content)
        {
            string pattern = @"[a-zA-Z]+\s+(\+|\-) [0-9]+[,.][0-9]+ m";
            Assert.IsTrue( Regex.IsMatch(content, pattern)) ;
        }

        [DataTestMethod]
        [DataRow("NHN11,26")]
        [DataRow("NHN 11,26 m")]
        [DataRow("NON + 11,26")]
        public void Test_IsNOT_Description(string content)
        {
            string pattern = "";
            Assert.IsFalse(Regex.IsMatch(content, pattern));
        }

        [DataTestMethod]
        [DataRow("GWM 5")]
        [DataRow("GWM 25")]
        [DataRow("GWM")]
        public void Test_IsID(string content)
        {
            string pattern = "";
            Assert.IsTrue(Regex.IsMatch(content, pattern));
        }
    }
}
