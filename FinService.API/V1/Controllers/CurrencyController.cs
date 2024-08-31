namespace FinService.API.V1.Controllers;

using Microsoft.AspNetCore.Mvc;
using FinService.API.Constants;
using FinService.Application.Interfaces.IServices;
using FinService.Contracts.Currency;

[ApiController]
[Route(ApiConstants.APIPrefix + "/currency")]
[ApiVersion("1.0")]
public class CurrencyController(ICurrencyService currencyService) : ControllerBase
{
    private readonly ICurrencyService _currencyService = currencyService ?? throw new ArgumentNullException(nameof(currencyService));

    [HttpPost("create")]
    public async Task<IActionResult> AddCurrencyAsync(CurrencyDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var savedCurrency = await _currencyService.AddCurrencyAsync(request);
        return StatusCode(StatusCodes.Status201Created, savedCurrency);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateCurrencyAsync(CurrencyDto request, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var updatedCurrency = await _currencyService.UpdateCurrencyAsync(request, id);
        return Ok(updatedCurrency);
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetAllCurrencysAsync()
    {
        var currencys = await _currencyService.GetAllCurrencysAsync();
        return Ok(currencys);
    }

    [HttpPatch("delete/{id}")]
    public async Task<IActionResult> DeleteCurrencyAsync(int id, long userId)
    {
        await _currencyService.DeleteCurrencyAsync(id, userId);
        return Ok("CURRENCY_DELETED_SUCCESSFULLY");
    }
}
