namespace FinService.API.V1.Controllers;

using Microsoft.AspNetCore.Mvc;
using FinService.API.Constants;
using FinService.Application.Interfaces.IServices;
using FinService.Contracts.Tax;

[ApiController]
[Route(ApiConstants.APIPrefix + "/tax")]
[ApiVersion("1.0")]
public class TaxController(ITaxService taxService) : ControllerBase
{
    private readonly ITaxService _taxService = taxService ?? throw new ArgumentNullException(nameof(taxService));

    [HttpPost("create")]
    public async Task<IActionResult> AddTaxAsync(TaxMasterDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var savedTax = await _taxService.AddTaxAsync(request);
        return StatusCode(StatusCodes.Status201Created, savedTax);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateTaxAsync(TaxMasterDto request, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var response = await _taxService.UpdateTaxAsync(request, id);
        return Ok(response);
    }
    
    [HttpGet("list")]
    public async Task<IActionResult> GetAllTaxAsync()
    {
        var tax = await _taxService.GetAllTaxAsync();
        return Ok(tax);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteTaxByIdAsync(int id)
    {
        await _taxService.DeleteTaxByIdAsync(id);
        return Ok("TAX_DELETED_SUCCESSFULLY");
    }
}
