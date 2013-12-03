using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JassTools;

namespace JassToolsTests
{
    [TestClass]
    public class JassObjectMapperTest
    {
        public class personBasic {
            public string name {get;set;}
            public int age {get;set;}
        }

        public class personBasicExtended
        {
            public string name { get; set; }
            public int age { get; set; }
            public int gender { get; set; }
        }

        [TestMethod]
        public void JassToolsMap()
        {

            var personBasic = new personBasic();
            var personBasicExtended = new personBasicExtended();

            personBasic.name = "juan";
            personBasic.age = 10;
            personBasicExtended.gender = 1;

            var mapper = new JassObjectMapper<personBasic,personBasicExtended>();
            mapper.mapProperties(personBasic, personBasicExtended);

            Assert.IsTrue(personBasicExtended.name == personBasic.name);
            Assert.IsTrue(personBasicExtended.age == personBasic.age);

        }
    }
}
