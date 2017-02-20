using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMEVENT.Data;

namespace TestLibrary
{
    [TestClass]
    public class LibraryTest
    {
        [TestMethod]
        public void TestSample()
        {
            Dormitory d = new Dormitory();
            Assert.AreEqual(d.IdDormitory, 0);
        }
    }
}
