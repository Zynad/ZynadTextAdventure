namespace Domain.Core;

public class NpcDialogueTemplate
{
    public List<string> Greetings { get; set; } = new();

    public List<string> QuestOffers { get; set; } = new();

    public List<string> Farewells { get; set; } = new();

    public List<string> RandomLines { get; set; } = new();

    public List<string> TradeOpeners { get; set; } = new();
}
