
namespace LogicService.Dto
{

    public class Instruction
    {
        public string Text { get; set; }
    }

    public class Step
    {
        public int FromIndex { get; set; }
        public int ToIndex { get; set; }
        public double Distance { get; set; }
        public double Time { get; set; }
        public Instruction Instruction { get; set; }
    }

    public class Leg
    {
        public int Distance { get; set; }
        public double Time { get; set; }
        public List<Step> Steps { get; set; }
    }

    public class Geometry
    {
        public string Type { get; set; }
        public List<List<List<double>>> Coordinates { get; set; }
    }

    public class Waypoint
    {
        public List<double> Location { get; set; }
        public int OriginalIndex { get; set; }
    }

    public class Properties
    {
        public string Mode { get; set; }
        public List<Waypoint> Waypoints { get; set; }
        public string Units { get; set; }
        public int Distance { get; set; }
        public string DistanceUnits { get; set; }
        public double Time { get; set; }
        public List<Leg> Legs { get; set; }
    }

    public class Feature
    {
        public string Type { get; set; }
        public Properties Properties { get; set; }
        public Geometry Geometry { get; set; }
    }

    public class Root
    {
        public List<Feature> Features { get; set; }
        public Properties Properties { get; set; }
        public string Type { get; set; }
    }

}
