namespace ApplicationServices.Npc.Models;

public record NpcDialogueResponse(
    string NpcId,
    string NpcName,
    string Town,
    string Line,
    string RoleType);
