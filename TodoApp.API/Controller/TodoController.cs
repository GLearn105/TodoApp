using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces;

namespace TodoApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        
        //Async
        private readonly ITodoService _service;

        public TodoController(ITodoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTodoDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTodoDto dto)
        //{
        //    var result = await _service.UpdateAsync(id, dto);
        //    if (result is null) return NotFound();
        //    return Ok(result);
        //}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTodoDto dto)
        {
            try
            {
                var result = await _service.UpdateAsync(id, dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    await _service.DeleteAsync(id);
        //    return NoContent();
        //}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        //sync
    //    private readonly ITodoService _service;

    //    public TodoController(ITodoService service)
    //    {
    //        _service = service;
    //    }

    //    [HttpGet]
    //    public IActionResult GetAll()
    //    {
    //        var todos = _service.GetAll();
    //        return Ok(todos);
    //    }

    //    [HttpGet("{id}")]
    //    public IActionResult GetById(Guid id)
    //    {
    //        var todo = _service.GetById(id);
    //        if (todo == null) return NotFound();
    //        return Ok(todo);
    //    }

    //    [HttpPost]
    //    public IActionResult Create(CreateTodoDto dto)
    //    {
    //        var created = _service.Create(dto);
    //        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    //    }

    //    [HttpPut("{id}")]
    //    public IActionResult Update(Guid id, UpdateTodoDto dto)
    //    {
    //        var updated = _service.Update(id, dto);
    //        if (updated == null) return NotFound();
    //        return Ok(updated);
    //    }

    //    [HttpDelete("{id}")]
    //    public IActionResult Delete(Guid id)
    //    {
    //        var success = _service.Delete(id);
    //        if (!success) return NotFound();
    //        return NoContent();
    //    }
    //}
}
}