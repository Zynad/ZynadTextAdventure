namespace ApplicationServices.Locations;
public abstract class BaseLocation
{
    public string Name { get; set; } = string.Empty;
    public List<BaseLocation> SubLocations { get; set; } = new();
    public Climate Climate { get; set; }
    public LocationType LocationType { get; set; }
}
