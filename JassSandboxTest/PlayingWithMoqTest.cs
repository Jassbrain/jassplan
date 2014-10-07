using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace JassToolsTests
{
    [TestClass]
    public class PlayingWithMoqTest
    {
        public class personBasic {
            public string name {get;set;}
            public int age {get;set;}
        }

        [TestMethod]

        public void play1() { 

         // Mock a product
            var newProduct = new Mock<IProduct>();
            newProduct.SetupGet(p => p.Id).Returns(1);
            newProduct.SetupGet(p => p.Name).Returns("Bushmills");
        

         // Mock product repository
            var productRepository = new Mock<IProductRepository>();
           productRepository
           .Setup(p => p.Get(1))
           .Returns(newProduct.Object);

        }
   
    }
}
