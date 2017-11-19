using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public void PostLeftWhenBase64DataIsNullReturnsMessage()
        {
            // Arrange
            DiffController controller = new DiffController();

            // Act
            var result = controller.PostLeftV2("1",null);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Text can not be null", result);
        }

        [TestMethod]
        public void PostLeftWhenBase64DataIsNotInBase64FormatReturnsMessage()
        {
            // Arrange
            DiffController controller = new DiffController();

            // Act
            var result_1 = controller.PostLeftV2("1", "123");
            var result_2 = controller.PostLeftV2("1", "1/*4");

            // Assert
            Assert.IsNotNull(result_1);
            Assert.AreEqual("Text must be in base64 format", result_1);
            Assert.IsNotNull(result_2);
            Assert.AreEqual("Text must be in base64 format", result_2);
        }

        [TestMethod]
        public void PostLeftWhenSuccessReturnsMessage()
        {
            // Arrange
            DiffController controller = new DiffController();

            // Act
            var result = controller.PostLeftV2("1", "123456789012");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("123456789012 was saved on left side", result);
        }

        [TestMethod]
        public void PostLeftWhenIdIsNullMessage()
        {
            // Arrange
            DiffController controller = new DiffController();

            // Act
            var result = controller.PostLeftV2(null,null);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Id can not be null", result);
        }

        [TestMethod]
        public void PostRightWhenBase64DataIsNullReturnsMessage()
        {
            // Arrange
            DiffController controller = new DiffController();

            // Act
            var result = controller.PostRightV2("1", null);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Text can not be null", result);
        }

        [TestMethod]
        public void PostRightWhenBase64DataIsNotInBase64FormatReturnsMessage()
        {
            // Arrange
            DiffController controller = new DiffController();

            // Act
            var result_1 = controller.PostRightV2("1", "123");
            var result_2 = controller.PostRightV2("1", "1/*4");

            // Assert
            Assert.IsNotNull(result_1);
            Assert.AreEqual("Text must be in base64 format", result_1);
            Assert.IsNotNull(result_2);
            Assert.AreEqual("Text must be in base64 format", result_2);
        }

        [TestMethod]
        public void PostRightWhenSuccessReturnsMessage()
        {
            // Arrange
            DiffController controller = new DiffController();

            // Act
            var result = controller.PostRightV2("1", "123456789012");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("123456789012 was saved on right side", result);
        }

        [TestMethod]
        public void PostRightWhenIdIsNullMessage()
        {
            // Arrange
            DiffController controller = new DiffController();

            // Act
            var result = controller.PostRightV2(null, null);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Id can not be null", result);
        }
    }
}
