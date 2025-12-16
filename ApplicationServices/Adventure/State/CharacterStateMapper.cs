using System.Linq;
using Domain.Core;

namespace ApplicationServices.Adventure.State;

public static class CharacterStateMapper
{
    public static CharacterStateDto FromCharacter(Character character)
    {
        var questStates = character.QuestStates
            .Select(q => new QuestStateDto(q.QuestId, q.Status, q.UpdatedAt))
            .ToList();

        return new CharacterStateDto(
            character.Id,
            character.Name,
            character.Level,
            character.ClassName,
            character.Location,
            character.Inventory.AsReadOnly(),
            questStates.AsReadOnly());
    }
}
