namespace FinService.API.V1.Controllers;

using Microsoft.AspNetCore.Mvc;
using FinService.API.Constants;
using FinService.Application.Interfaces.IServices;
using FinService.Contracts.CurrencyExchangeRate;

[ApiController]
[Route(ApiConstants.APIPrefix + "/currency-exchange-rate")]
[ApiVersion("1.0")]
public class CurrencyExchangeRateController(ICurrencyExchangeRateService currencyExchangeRateService) : ControllerBase
{
    private readonly ICurrencyExchangeRateService _currencyExchangeRateService = currencyExchangeRateService ?? throw new ArgumentNullException(nameof(currencyExchangeRateService));

    [HttpPost("create")]
    public async Task<IActionResult> AddCurrencyExchangeRateAsync(CurrencyExchangeRateDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var savedCurrencyExchangeRate = await _currencyExchangeRateService.AddCurrencyExchangeRateAsync(request);
        return StatusCode(StatusCodes.Status201Created, savedCurrencyExchangeRate);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateCurrencyExchangeRateAsync(CurrencyExchangeRateDto request, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var updatedCurrencyExchangeRate = await _currencyExchangeRateService.UpdateCurrencyExchangeRateAsync(request, id);
        return Ok(updatedCurrencyExchangeRate);
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetAllCurrencyExchangeRatesAsync()
    {
        var currencyExchangeRates = await _currencyExchangeRateService.GetAllCurrencyExchangeRatesAsync();
        return Ok(currencyExchangeRates);
    }

    [HttpPatch("delete/{id}")]
    public async Task<IActionResult> DeleteCurrencyExchangeRateAsync(int id, long userId)
    {
        await _currencyExchangeRateService.DeleteCurrencyExchangeRateAsync(id, userId);
        return Ok("CURRENCYEXCHANGERATE_DELETED_SUCCESSFULLY");
    }
}
