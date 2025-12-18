using ApplicationServices.Characters;
using ApplicationServices.Characters.Dto;
using ApplicationServices.Characters.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TextAdventure.Api.Extensions;

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

    /// <summary>
    /// Retrieve the available character presets.
    /// </summary>
    /// <param name="cancellationToken">Request cancellation token.</param>
    [HttpGet("presets")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPresets(CancellationToken cancellationToken)
    {
        var presets = await _getCharacterPresetsHandler.HandleAsync(cancellationToken);
        return Ok(presets);
    }

    /// <summary>
    /// Create a character for the authenticated user.
    /// </summary>
    /// <param name="request">Character creation details.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(CreateCharacterRequestDto request, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var result = await _createCharacterHandler.HandleAsync(token, request, cancellationToken);

        if (result.Success && result.Character is not null)
        {
            var response = new CreateCharacterResponseDto(result.Character);
            return CreatedAtAction(nameof(GetById), new { id = result.Character.Id }, response);
        }

        return TranslateError(result.ErrorType, result.Error);
    }

    /// <summary>
    /// List all characters created by the current user.
    /// </summary>
    /// <param name="cancellationToken">Request cancellation token.</param>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetForUser(CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
        var (characters, errorType, error) = await _getCharactersHandler.HandleAsync(token, cancellationToken);

        if (errorType.HasValue)
        {
            return TranslateError(errorType.Value, error);
        }

        return Ok(characters);
    }

    /// <summary>
    /// Retrieve a character by id for the current user.
    /// </summary>
    /// <param name="id">Character identifier.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var token = Request.GetAccessToken();
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

}
