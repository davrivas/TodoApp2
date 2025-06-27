using Microsoft.AspNetCore.Mvc;
using TodoApp.Services;
using TodoApp.Services.Models;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoItemController : ControllerBase
    {
        private readonly ILogger<TodoItemController> _logger;
        private readonly ITodoItemService _todoItemService;

        public TodoItemController(ILogger<TodoItemController> logger,
            ITodoItemService todoItemService)
        {
            _logger = logger;
            _todoItemService = todoItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var todoItems = await _todoItemService.GetAllAsync();
                return Ok(todoItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving todo items");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var todoItem = await _todoItemService.GetByIdAsync(id);
                return Ok(todoItem);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving todo item with ID {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TodoItemInputModel todoItem)
        {
            if (todoItem == null)
            {
                return BadRequest("Todo item cannot be null");
            }
            try
            {
                var id = await _todoItemService.AddAsync(todoItem);
                return CreatedAtAction(nameof(GetById), new { id }, todoItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding todo item");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TodoItemInputModel todoItem)
        {
            if (todoItem == null)
            {
                return BadRequest("Todo item cannot be null");
            }
            try
            {
                var result = await _todoItemService.UpdateAsync(id, todoItem);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating todo item with ID {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _todoItemService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting todo item with ID {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
