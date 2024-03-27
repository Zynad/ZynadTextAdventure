namespace ApplicationServices.Locations;
public abstract class BaseLocation
{
    public string Name { get; set; }
    public List<BaseLocation> SubLocations { get; set; }
    public Climate Climate { get; set; }
    public LocationType LocationType { get; set; }
}
