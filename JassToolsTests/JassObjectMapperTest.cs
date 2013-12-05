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

        public class personBasicExtended1
        {
            public string name { get; set; }
            public int age { get; set; }
            public int gender { get; set; }
        }

        public class personBasicExtended2
        {
            public string name { get; set; }
            public int age { get; set; }
            public int salary { get; set; }
        }


        [TestMethod]
        public void JassToolsMap()
        {

            var personBasic = new personBasic();
            var personBasicExtended1 = new personBasicExtended1();

            personBasic.name = "juan";
            personBasic.age = 10;
            personBasicExtended1.gender = 1;

            var mapper = new JassProperties<personBasic, personBasic, personBasicExtended1>();
            mapper.map(personBasic, personBasicExtended1);

            Assert.IsTrue(personBasicExtended1.name == personBasic.name);
            Assert.IsTrue(personBasicExtended1.age == personBasic.age);

        }

        [TestMethod]
        public void JassToolsEqual()
        {

            var personBasic = new personBasic();
            var personBasicExtended = new personBasicExtended1();

            personBasic.name = "juan";
            personBasic.age = 10;

            personBasicExtended.name = "juan";
            personBasicExtended.age = 10;
            personBasicExtended.gender = 1;

            var mapper = new JassProperties<personBasic, personBasic, personBasicExtended1>();
            mapper.map(personBasic, personBasicExtended);

            var result = mapper.Compare(personBasic, personBasicExtended);

            Assert.IsTrue(result);

        }
    }
}
