using ApplicationServices.Authentication;
using ApplicationServices.Characters.Results;
using ApplicationServices.Contracts.Repositories;

namespace ApplicationServices.Characters;

public class GetCharacterDetailsHandler(
    ICharacterRepository characterRepository,
    GetCurrentUserHandler getCurrentUserHandler)
{
    public async Task<CharacterResult> HandleAsync(Guid characterId, string token, CancellationToken cancellationToken = default)
    {
        var userResult = await getCurrentUserHandler.HandleAsync(token, cancellationToken);
        if (!userResult.Success || userResult.User is null)
        {
            return CharacterResult.Unauthorized(userResult.Error ?? "Unauthorized");
        }

        var character = await characterRepository.GetByIdAsync(characterId, cancellationToken);
        if (character is null || character.AccountId != userResult.User.Id)
        {
            return CharacterResult.NotFound("Character not found");
        }

        return CharacterResult.FromSuccess(CharacterMapper.ToCharacterDto(character));
    }
}
