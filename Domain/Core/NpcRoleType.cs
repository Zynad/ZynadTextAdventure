using System.Text.Json.Serialization;

namespace Domain.Core;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum NpcRoleType
{
    Guard,
    Vendor,
    QuestGiver,
    Flavor
}
