namespace Domain.Core;

public class NpcDialogueTemplate
{
    public List<string> Greetings { get; set; } = [];

    public List<string> QuestOffers { get; set; } = [];

    public List<string> Farewells { get; set; } = [];

    public List<string> RandomLines { get; set; } = [];

    public List<string> TradeOpeners { get; set; } = [];
}
