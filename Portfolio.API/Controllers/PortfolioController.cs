using Microsoft.AspNetCore.Mvc;
using Portfolio.Services;

[ApiController]
[Route("api/[controller]")]
public class PortfolioController : ControllerBase
{
    private readonly IGitHubService _service;

    public PortfolioController(IGitHubService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyPortfolio()
    {
        var result = await _service.GetPortfolioAsync();
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string term, [FromQuery] string lang, [FromQuery] string user)
    {
        var result = await _service.SearchRepositoriesAsync(term, lang, user);
        return Ok(result);
    }
}