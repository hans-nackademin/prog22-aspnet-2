using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.Helpers.Services;
using WebApi.Models.Dto;

namespace WebApi.Tests;

public class ProductsControllerTests
{
    [Fact]
    public void GetAll__Should_Return_StatusCode_200_And_A_List_Of_Product()
    {
        // Arrange
        var productService = new Mock<IProductService>();
        var productsController = new ProductsController(productService.Object);

        // Act
        var result = (OkObjectResult)productsController.GetAll();


        // Assert
        Assert.Equal(200, result.StatusCode);
        result.Value.Should().BeOfType<Product[]>();
    }
}