using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JassTools;

namespace JassToolsTests
{
    [TestClass]
    public class JassObjectMapperTest
    {
        public class personBasic {
            public string name;
            public int age;
        }

        public class personBasicExtended
        {
            public string name;
            public int age;
            public int gender;
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
            mapper.map(personBasic, personBasicExtended);

        }
    }
}
