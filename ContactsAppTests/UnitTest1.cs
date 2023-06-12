using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NBPapi.Controllers;

namespace ContactsAppTests;


[TestClass]
public class UnitTest1
{
    [TestMethod]
    public async Task GetByDate_ReturnsAverageRate()
    {
        // Arrange
        var controller = new CurrencyController();

        // Act
        var result = await controller.GetByDate("USD", "2023-06-01") as OkObjectResult;
        var responseBody = result.Value.ToString();

        // Assert
        Assert.IsTrue(responseBody.Contains("average for USD at 2023-06-01"));
    }
    [TestMethod]
    public async Task GetByDate_ReturnsBadRequestOnError()
    {
        // Arrange
        var controller = new CurrencyController();

        // Act
        var result = await controller.GetByDate("XYZ", "2023-06-01") as BadRequestObjectResult;

        // Assert
        Assert.AreEqual("unable to get the response, check if your inputs are corerect", result.Value);
    }

    [TestMethod]
    public async Task GetMinAndMax_ReturnsBadRequestOnError()
    {
        // Arrange
        var controller = new CurrencyController();

        // Act
        var result = await controller.GetMinAndMax("XYZ", 5) as BadRequestObjectResult;

        // Assert
        Assert.AreEqual("unable to get the response, check if your inputs are corerect", result.Value);
    }
    [TestMethod]
    public async Task DiffrenceAskBid_ReturnsRateDifferences()
    {
        // Arrange
        var controller = new CurrencyController();

        // Act
        var result = await controller.DiffrenceAskBid("USD", 3) as OkObjectResult;
        var rateDifferences = result.Value as List<double>;

        // Assert
        Assert.AreEqual(3, rateDifferences.Count);
    }

    [TestMethod]
    public async Task DiffrenceAskBid_ReturnsBadRequestOnError()
    {
        // Arrange
        var controller = new CurrencyController();

        // Act
        var result = await controller.DiffrenceAskBid("XYZ", 3) as BadRequestObjectResult;

        // Assert
        Assert.AreEqual("unable to get the response, check if your inputs are corerect", result.Value);
    }
    [TestMethod]
    public async Task GetByDate_ReturnsOkResult()
    {
        // Arrange
        var controller = new CurrencyController();

        // Act
        var result = await controller.GetByDate("USD", "2023-06-01") as IActionResult;

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }
    [TestMethod]
    public async Task GetMinAndMax_ReturnsOkResult()
    {
        // Arrange
        var controller = new CurrencyController();

        // Act
        var result = await controller.GetMinAndMax("USD", 5) as IActionResult;

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }
    [TestMethod]
    public async Task DiffrenceAskBid_ReturnsOkResult()
    {
        // Arrange
        var controller = new CurrencyController();

        // Act
        var result = await controller.DiffrenceAskBid("USD", 3) as IActionResult;

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }
    [TestMethod]
    public async Task GetByDate_ReturnsBadRequestResult()
    {
        // Arrange
        var controller = new CurrencyController();

        // Act
        var result = await controller.GetByDate("XYZ", "2023-06-01") as IActionResult;

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }
    [TestMethod]
    public async Task GetByDate_ReturnsBadRequestResult_WhenInvalidDate()
    {
        // Arrange
        var controller = new CurrencyController();

        // Act
        var result = await controller.GetByDate("USD", "invalid-date");

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }
}
