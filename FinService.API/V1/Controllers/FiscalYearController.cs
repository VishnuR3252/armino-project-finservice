namespace FinService.API.V1.Controllers;

using Microsoft.AspNetCore.Mvc;
using FinService.API.Constants;
using FinService.Application.Interfaces.IServices;
using FinService.Contracts.FiscalYear;

[ApiController]
[Route(ApiConstants.APIPrefix + "/fiscal-year")]
[ApiVersion("1.0")]
public class FiscalYearController(IFiscalYearService fiscalYearService) : ControllerBase
{
    private readonly IFiscalYearService _fiscalYearService = fiscalYearService ?? throw new ArgumentNullException(nameof(fiscalYearService));

    [HttpPost("create")]
    public async Task<IActionResult> AddFiscalYearAsync(FiscalYearDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var savedFiscalYear = await _fiscalYearService.AddFiscalYearAsync(request);
        return StatusCode(StatusCodes.Status201Created, savedFiscalYear);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateFiscalYearAsync(FiscalYearDto request, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var savedFiscalYear = await _fiscalYearService.UpdateFiscalYearAsync(request, id);
        return Ok(savedFiscalYear);
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetAllFiscalYearsAsync()
    {
        var fiscalYears = await _fiscalYearService.GetAllFiscalYearsAsync();
        return Ok(fiscalYears);
    }

    [HttpPatch("delete/{id}")]
    public async Task<IActionResult> DeleteFiscalYearAsync(int id, long userId)
    {
        await _fiscalYearService.DeleteFiscalYearAsync(id, userId);
        return Ok("FISCALYEAR_DELETED_SUCCESSFULLY");
    }
}
