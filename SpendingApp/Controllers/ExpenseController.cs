using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpendingApp.ModelsDTO;
using SpendingApp.Services;

namespace SpendingApp.Controllers
{
    [Authorize]
    [Route("api/expense")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromBody] string name)
        {
            var expenses = await _expenseService.GetAll(name);
            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var expense = await _expenseService.GetById(id);
            return Ok(expense);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateExpenseVM expense)
        {
            await _expenseService.Create(expense);
            return Created($"/api/expense", null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreateExpenseVM expense)
        {
            await _expenseService.Update(id, expense);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _expenseService.Remove(id);
            return NoContent();
        }
    }
