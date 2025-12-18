namespace ApplicationServices.Npc.Models;

public record NpcQuestOfferResponse(
    string NpcId,
    string NpcName,
    string QuestId,
    string Prompt);
