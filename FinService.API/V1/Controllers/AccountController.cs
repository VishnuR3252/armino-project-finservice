namespace FinService.API.V1.Controllers;

using Microsoft.AspNetCore.Mvc;
using FinService.API.Constants;
using FinService.Application.Interfaces.IServices;
using FinService.Contracts.Account;

[ApiController]
[Route(ApiConstants.APIPrefix + "/account")]
[ApiVersion("1.0")]
public class AccountController(IAccountService accountService) : ControllerBase
{
    private readonly IAccountService _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));

    [HttpPost("create")]
    public async Task<IActionResult> AddAccountHeadAsync(AccountHeadDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var savedAccount = await _accountService.AddAccountHeadAsync(request);
        return StatusCode(StatusCodes.Status201Created, savedAccount);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAccountHeadAsync(AccountHeadDto request, long id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var savedAccount = await _accountService.UpdateAccountHeadAsync(request, id);
        return Ok(savedAccount);
    }

    [HttpGet("list/page-number={pageNumber}/page-size={pageSize}")]
    public async Task<IActionResult> GetAllAccountsAsync(int pageNumber = 1, int pageSize = 10)
    {
        var accounts = await _accountService.GetAllAccountHeadAsync(pageNumber, pageSize);
        return Ok(accounts);
    }

    [HttpGet("account-groups")]
    public async Task<IActionResult> GetAccountGroupsWithTypes()
    {
        var accountGroups = await _accountService.GetAccountGroupsWithTypesAsync();
        return Ok(accountGroups);
    }
}
