namespace ApplicationServices.Npc.Models;

public record NpcTradeResponse(
    string NpcId,
    string NpcName,
    string ItemId,
    int Quantity,
    decimal UnitPrice,
    decimal TotalPrice,
    decimal RemainingCoins,
    string Action);
