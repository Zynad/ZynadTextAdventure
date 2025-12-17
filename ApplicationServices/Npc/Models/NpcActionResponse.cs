namespace ApplicationServices.Npc.Models;

public record NpcActionResponse(
    string NpcId,
    string NpcName,
    string Action,
    int Roll,
    int Difficulty,
    int Modifier,
    bool Success,
    decimal Coins);
