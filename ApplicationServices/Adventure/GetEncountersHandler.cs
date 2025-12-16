using ApplicationServices.Adventure.Results;
using ApplicationServices.Adventure.State;
using ApplicationServices.Authentication;
using ApplicationServices.Contracts.Repositories;
namespace ApplicationServices.Adventure;

public class GetEncountersHandler
{
    private readonly GetCurrentUserHandler _getCurrentUserHandler;
    private readonly ICharacterRepository _characterRepository;

    public GetEncountersHandler(
        GetCurrentUserHandler getCurrentUserHandler,
        ICharacterRepository characterRepository)
    {
        _getCurrentUserHandler = getCurrentUserHandler;
        _characterRepository = characterRepository;
    }

    public async Task<AdventureResult> HandleAsync(string token, Guid characterId, CancellationToken cancellationToken = default)
    {
        var userResult = await _getCurrentUserHandler.HandleAsync(token, cancellationToken);
        if (!userResult.Success || userResult.User is null)
        {
            return AdventureResult.Unauthorized(userResult.Error ?? "Unauthorized");
        }

        var character = await _characterRepository.GetByIdAsync(characterId, cancellationToken);
        if (character is null || character.AccountId != userResult.User.Id)
        {
            return AdventureResult.NotFound("Character not found");
        }

        return AdventureResult.FromSuccess(CharacterStateMapper.FromCharacter(character));
    }
}
