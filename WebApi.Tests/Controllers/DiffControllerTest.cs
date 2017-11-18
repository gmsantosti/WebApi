using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Repositories;

namespace WebApi.Tests.Controllers
{
    [TestClass]
    public class DiffControllerTest
    {
        [TestMethod]
        public void GetWhenBase64DataAreEqualReturnsMessage()
        {
            // Arrange
            DiffController controller = new DiffController();

            // Act
            var diffRepository = DiffRepository.GetInstance();
            diffRepository.SaveLeft("1", "TestSame");
            diffRepository.SaveRight("1", "TestSame");
            var result = controller.Get("1");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("same value", result);
        }

        [TestMethod]
        public void GetWhenBase64DataHaveDifferentSizesReturnsMessage()
        {
            // Arrange
            DiffController controller = new DiffController();

            // Act
            var diffRepository = DiffRepository.GetInstance();
            diffRepository.SaveLeft("1", "TestLeft");
            diffRepository.SaveRight("1", "TestRight");
            var result = controller.Get("1");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("left and right aren't same size", result);
        }

        [TestMethod]
        public void GetWhenIdDoesntExistReturnsMessage()
        {
            // Arrange
            DiffController controller = new DiffController();

            // Act
            var result = controller.Get("2");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("This Id doesn't exist", result);
        }

        [TestMethod]
        public void GetWhenBase64DataHaveSameSizeAndOneDifferenceReturnsMessage()
        {
            // Arrange
            DiffController controller = new DiffController();

            // Act
            var diffRepository = DiffRepository.GetInstance();
            diffRepository.SaveLeft("1", "TestSameSize1");
            diffRepository.SaveRight("1", "TestSameSize2");
            var result = controller.Get("1");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Difference found in position 12 left side: 1 and right side: 2", result);
        }

        [TestMethod]
        public void GetWhenBase64DataHaveSameSizeAndTwoDifferenceReturnsMessage()
        {
            // Arrange
            DiffController controller = new DiffController();

            // Act
            var diffRepository = DiffRepository.GetInstance();
            diffRepository.SaveLeft("1", "ATestSameSize1");
            diffRepository.SaveRight("1", "BTestSameSize2");
            var result = controller.Get("1");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Difference found in position 0 left side: A and right side: B; Difference found in position 13 left side: 1 and right side: 2", result);
        }



        [TestMethod]
        public void GetById()
        {
            // Arrange
            DiffController controller = new DiffController();

            // Act
            //string result = controller.Get(5);

            // Assert
            //Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            DiffController controller = new DiffController();

            // Act
            // controller.Post("value");

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            DiffController controller = new DiffController();

            // Act
            // controller.Put(5, "value");

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            DiffController controller = new DiffController();

            // Act
            //controller.Delete(5);

            // Assert
        }
    }
}
