namespace FinService.API.V1.Controllers;

using Microsoft.AspNetCore.Mvc;
using FinService.API.Constants;
using FinService.Application.Interfaces.IServices;
using FinService.Contracts.Bank;

[ApiController]
[Route(ApiConstants.APIPrefix + "/bank")]
[ApiVersion("1.0")]
public class BankController(IBankService bankService) : ControllerBase
{
    private readonly IBankService _bankService = bankService ?? throw new ArgumentNullException(nameof(bankService));

    [HttpPost("create")]
    public async Task<IActionResult> AddBankAsync(BankDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var savedBank = await _bankService.AddBankAsync(request);
        return StatusCode(StatusCodes.Status201Created, savedBank);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateBankAsync(BankDto request, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var response = await _bankService.UpdateBankAsync(request, id);
        return Ok(response);
    }

    [HttpGet("list/page-number={pageNumber}/page-size={pageSize}")]
    public async Task<IActionResult> GetAllBankAsync(int pageNumber = 1, int pageSize = 10)
    {
        var bank = await _bankService.GetAllBankAsync(pageNumber, pageSize);
        return Ok(bank);
    }


    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteBankByIdAsync(int id)
    {
        await _bankService.DeleteBankByIdAsync(id);
        return Ok("BANK_DELETED_SUCCESSFULLY");
    }
}
