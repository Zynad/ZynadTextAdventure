using ApplicationServices.Authentication;
using ApplicationServices.Characters.Dto;
using ApplicationServices.Characters.Results;
using ApplicationServices.Contracts.Repositories;

namespace ApplicationServices.Characters;

public class GetCharactersHandler(ICharacterRepository characterRepository, GetCurrentUserHandler getCurrentUserHandler)
{
    public async Task<(IReadOnlyCollection<CharacterDto>? Characters, CharacterErrorType? ErrorType, string? Error)> HandleAsync(
        string token,
        CancellationToken cancellationToken = default)
    {
        var userResult = await getCurrentUserHandler.HandleAsync(token, cancellationToken);
        if (!userResult.Success || userResult.User is null)
        {
            return (null, CharacterErrorType.Unauthorized, userResult.Error ?? "Unauthorized");
        }

        var characters = await characterRepository.GetByAccountAsync(userResult.User.Id, cancellationToken);
        var dtos = characters.Select(CharacterMapper.ToCharacterDto).ToList();
        return (dtos, null, null);
    }
}
