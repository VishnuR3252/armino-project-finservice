namespace FinService.API.V1.Controllers;

using Microsoft.AspNetCore.Mvc;
using FinService.API.Constants;
using FinService.Application.Interfaces.IServices;
using FinService.Contracts.Journal;
using CommonService.Requests;

[ApiController]
[Route(ApiConstants.APIPrefix + "/journal")]
[ApiVersion("1.0")]
public class JournalController(IJournalService journalService) : ControllerBase
{
    private readonly IJournalService _journalService = journalService ?? throw new ArgumentNullException(nameof(journalService));

    [HttpPost("create")]
    public async Task<IActionResult> AddJournalAsync(JournalDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var savedJournal = await _journalService.AddJournalAsync(request);
        return StatusCode(StatusCodes.Status201Created, savedJournal);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateDraftedJournalAsync(JournalDto request, long id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var response = await _journalService.UpdateDraftedJournalAsync(request, id);
        return Ok(response);
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetAllJournalsAsync([FromQuery] JournalQueryParameters queryParams)
    {
        var journal = await _journalService.GetAllJournalsAsync(queryParams);
        return Ok(journal);
    }

    [HttpPatch("post/{id}")]
    public async Task<IActionResult> PostTheJournalAsync(long id)
    {
        await _journalService.PostTheJournalAsync(id);
        return Ok("JOURANL_POSTED_SUCCESSFULLY");
    }

    [HttpPatch("cancel/{id}")]
    public async Task<IActionResult> CancelTheJournalAsync(long id)
    {
        await _journalService.CancelTheJournalAsync(id);
        return Ok("JOURANL_CANCELLED_SUCCESSFULLY");
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteDraftedJournalAsync(long id)
    {
        await _journalService.DeleteDraftedJournalAsync(id);
        return Ok("JOURANL_DELETED_SUCCESSFULLY");
    }

    [HttpGet("journal-type")]
    public async Task<IActionResult> GetAccountGroupsWithTypes()
    {
        var accountGroups = await _journalService.GetJournalTypeAsync();
        return Ok(accountGroups);
    }

    [HttpGet("journal-number/{journalTypeId}")]
    public async Task<IActionResult> GetJournalNumberByAsync(int journalTypeId)
    {
        var journalNumber = await _journalService.GetJournalNumberByAsync(journalTypeId);
        return Ok(journalNumber);
    }

}
