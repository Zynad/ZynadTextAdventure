using ApplicationServices.PlayerSettings;

namespace ApplicationServices.Classes;
public abstract class Vocation
{
    public abstract Task SetBaseValues(Player player);
    public string VocationName { get; set; }
}
