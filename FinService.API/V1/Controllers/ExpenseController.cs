namespace FinService.API.V1.Controllers;

using Microsoft.AspNetCore.Mvc;
using FinService.API.Constants;
using FinService.Application.Interfaces.IServices;
using FinService.Contracts.Expense;
using CommonService.Requests;

[ApiController]
[Route(ApiConstants.APIPrefix + "/expense")]
[ApiVersion("1.0")]
public class ExpenseController(IExpenseService expenseService) : ControllerBase
{
    private readonly IExpenseService _expenseService = expenseService ?? throw new ArgumentNullException(nameof(expenseService));

    [HttpPost("create")]
    public async Task<IActionResult> AddExpenseAsync(ExpenseDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var savedExpense = await _expenseService.AddExpenseAsync(request);
        return StatusCode(StatusCodes.Status201Created, savedExpense);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateDraftedExpenseAsync(ExpenseDto request, long id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var response = await _expenseService.UpdateDraftedExpenseAsync(request, id);
        return Ok(response);
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetAllExpensesAsync([FromQuery] ExpenseQueryParameters queryParams)
    {
        var expense = await _expenseService.GetAllExpensesAsync(queryParams);
        return Ok(expense);
    }

    [HttpPatch("post/{id}")]
    public async Task<IActionResult> PostTheExpenseAsync(long id)
    {
        await _expenseService.PostTheExpenseAsync(id);
        return Ok("EXPENSE_POSTED_SUCCESSFULLY");
    }

    [HttpPatch("cancel/{id}")]
    public async Task<IActionResult> CancelTheExpenseAsync(long id)
    {
        await _expenseService.CancelTheExpenseAsync(id);
        return Ok("EXPENSE_CANCELLED_SUCCESSFULLY");
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteDraftedExpenseAsync(long id)
    {
        await _expenseService.DeleteDraftedExpenseAsync(id);
        return Ok("EXPENSE_DELETED_SUCCESSFULLY");
    }

    [HttpGet("expense-type")]
    public async Task<IActionResult> GetExpenseTypesAsync()
    {
        var expenseTypes = await _expenseService.GetExpenseTypesAsync();
        return Ok(expenseTypes);
    }
    [HttpGet("expense-number/{expenseTypeId}")]
    public async Task<IActionResult> GetExpenseNumberByAsync(int expenseTypeId)
    {
        var expenseNumber = await _expenseService.GetExpenseNumberByAsync(expenseTypeId);
        return Ok(expenseNumber);
    }
}
