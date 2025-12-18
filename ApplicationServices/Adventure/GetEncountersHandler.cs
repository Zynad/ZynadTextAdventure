using ApplicationServices.Adventure.Results;
using ApplicationServices.Authentication;
using ApplicationServices.Characters;
using ApplicationServices.Contracts.Repositories;
namespace ApplicationServices.Adventure;

public class GetEncountersHandler(
    GetCurrentUserHandler getCurrentUserHandler,
    ICharacterRepository characterRepository)
{
    public async Task<AdventureResult> HandleAsync(string token, Guid characterId, CancellationToken cancellationToken = default)
    {
        var userResult = await getCurrentUserHandler.HandleAsync(token, cancellationToken);
        if (!userResult.Success || userResult.User is null)
        {
            return AdventureResult.Unauthorized(userResult.Error ?? "Unauthorized");
        }

        var character = await characterRepository.GetByIdAsync(characterId, cancellationToken);
        if (character is null || character.AccountId != userResult.User.Id)
        {
            return AdventureResult.NotFound("Character not found");
        }

        return AdventureResult.FromSuccess(CharacterMapper.ToStateDto(character));
    }
}
