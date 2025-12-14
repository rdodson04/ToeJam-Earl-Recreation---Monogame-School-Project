public class StageCriteria
{
    public bool HasSand { get; set; }
    public bool HasWater { get; set; }
    public bool HasHighway { get; set; }
    public bool HasHoles { get; set; }
    public int HoleCount { get; set; }
    public int EnemyTypesRequired { get; set; }
    public bool RequiresElevator { get; set; }
    public bool MayContainShipParts { get; set; }
}