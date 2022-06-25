using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpendingApp.ModelsDTO;
using SpendingApp.Services;

namespace SpendingApp.Controllers
{
    [Authorize]
    [Route("api/categories")]
    [ApiController]
    public class ExpenseCategoryController : ControllerBase
    {
        private readonly IExpenseCategoryService _expenseCategoryService;

        public ExpenseCategoryController(IExpenseCategoryService expenseCategoryService)
        {
            _expenseCategoryService = expenseCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseCategoryVM>>> GetAll()
        {
            var categories = await _expenseCategoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseCategoryVM>> GetById([FromRoute] int id)
        {
            var category = await _expenseCategoryService.GetById(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseCategoryVM>> Create([FromBody] ExpenseCategoryVM category)
        {
            await _expenseCategoryService.Create(category);
            return Created($"/api/categories", null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] ExpenseCategoryVM category)
        {
            await _expenseCategoryService.Update(id, category);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _expenseCategoryService.Remove(id);
            return NoContent();
        }
    }
}
