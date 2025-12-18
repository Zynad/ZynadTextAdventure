namespace ApplicationServices.Admin.Models;

public record NpcDialogueDto(
    IReadOnlyCollection<string> Greetings,
    IReadOnlyCollection<string> QuestOffers,
    IReadOnlyCollection<string> Farewells,
    IReadOnlyCollection<string> RandomLines,
    IReadOnlyCollection<string> TradeOpeners);
