using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TodoApp.Api.Controllers;
using TodoApp.Services;
using TodoApp.Services.Models;

namespace TodoApp.Tests
{
    public class TodoItemControllerTests
    {
        private readonly Fixture _fixture = new();
        private readonly Mock<ITodoItemService> _todoItemServiceMock = new();
        private TodoItemController _controller;

        public TodoItemControllerTests()
        {
            _controller = new TodoItemController(
                Mock.Of<ILogger<TodoItemController>>(),
                _todoItemServiceMock.Object);
        }

        [Fact]
        public async Task GetAll_WhenCalled_ReturnItems()
        {
            // Arrange
            var todoItems = _fixture.Create<List<TodoItemOutputModel>>();

            _todoItemServiceMock
                .Setup(service => service.GetAllAsync())
                .ReturnsAsync(todoItems);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedItems = Assert.IsType<List<TodoItemOutputModel>>(okResult.Value);
            Assert.Equivalent(todoItems, returnedItems);

        }
    }
}