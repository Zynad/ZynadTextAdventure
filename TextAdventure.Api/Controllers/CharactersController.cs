using ApplicationServices.Characters;
using ApplicationServices.Characters.Requests;
using ApplicationServices.Characters.Results;
using Microsoft.AspNetCore.Mvc;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
    private readonly GetCharacterPresetsHandler _getCharacterPresetsHandler;
    private readonly CreateCharacterHandler _createCharacterHandler;
    private readonly GetCharactersHandler _getCharactersHandler;
    private readonly GetCharacterDetailsHandler _getCharacterDetailsHandler;

    public CharactersController(
        GetCharacterPresetsHandler getCharacterPresetsHandler,
        CreateCharacterHandler createCharacterHandler,
        GetCharactersHandler getCharactersHandler,
        GetCharacterDetailsHandler getCharacterDetailsHandler)
    {
        _getCharacterPresetsHandler = getCharacterPresetsHandler;
        _createCharacterHandler = createCharacterHandler;
        _getCharactersHandler = getCharactersHandler;
        _getCharacterDetailsHandler = getCharacterDetailsHandler;
    }

    [HttpGet("presets")]
    public async Task<IActionResult> GetPresets(CancellationToken cancellationToken)
    {
        var presets = await _getCharacterPresetsHandler.HandleAsync(cancellationToken);
        return Ok(presets);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCharacterRequest request, CancellationToken cancellationToken)
    {
        var token = ExtractBearerToken();
        var result = await _createCharacterHandler.HandleAsync(token, request, cancellationToken);

        if (result.Success && result.Character is not null)
        {
            return CreatedAtAction(nameof(GetById), new { id = result.Character.Id }, result.Character);
        }

        return TranslateError(result.ErrorType, result.Error);
    }

    [HttpGet]
    public async Task<IActionResult> GetForUser(CancellationToken cancellationToken)
    {
        var token = ExtractBearerToken();
        var (characters, errorType, error) = await _getCharactersHandler.HandleAsync(token, cancellationToken);

        if (errorType.HasValue)
        {
            return TranslateError(errorType.Value, error);
        }

        return Ok(characters);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var token = ExtractBearerToken();
        var result = await _getCharacterDetailsHandler.HandleAsync(id, token, cancellationToken);

        if (result.Success && result.Character is not null)
        {
            return Ok(result.Character);
        }

        return TranslateError(result.ErrorType, result.Error);
    }

    private IActionResult TranslateError(CharacterErrorType errorType, string? message)
    {
        return errorType switch
        {
            CharacterErrorType.Unauthorized => Unauthorized(new { message = message ?? "Unauthorized" }),
            CharacterErrorType.Conflict => Conflict(new { message = message ?? "Conflict" }),
            CharacterErrorType.NotFound => NotFound(new { message = message ?? "Not found" }),
            _ => BadRequest(new { message = message ?? "Invalid request" })
        };
    }

    private string ExtractBearerToken()
    {
        if (Request.Headers.TryGetValue("Authorization", out var header)
            && header.ToString().StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            return header.ToString()[7..].Trim();
        }

        return string.Empty;
    }
}
