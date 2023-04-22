using Gtk;
using System.Text.Json;

namespace Orbit;
public class MassJsonHelper {
	public int Index {get; set;}
	public string Name {get; set;}

	public double MassInGg {get; set;}
	public Vector2d PositionInKm {get; set;}
	public Vector2d VelocityInKmS {get; set;}
	public bool Stationary {get; set;}

    public bool HasTrail {get; set;}
	public int TrailLength {get; set;}
    public int TrailQuality {get; set;}

    public int FollowingIndex {get; set;}
	public int OrbitingBodyIndex {get; set;}

    public bool Satellite {get; set;}
    public double SemiMajorAxisLength {get; set;}
    public int PrecisionPriorityLimitInSeconds {get; set;}

	internal static void SaveMassesToFile(List<Mass> masses, string fileName) {
		MassJsonList JList = new();
		foreach(Mass m in masses) {
			JList.JsonMasses.Add(new MassJsonHelper(m.mi));
		}
		var options = new JsonSerializerOptions { WriteIndented = true };
		string jsonString = JsonSerializer.Serialize(JList, options);
		
		if(fileName.EndsWith(".json")) {
			File.WriteAllText(fileName, jsonString);
		}
		else {
			File.WriteAllText(fileName + ".json", jsonString);
		}
	}
	internal static MassJsonList? LoadMassesFromFile(string fileName) {
		using (StreamReader file = File.OpenText(@fileName)) {
			MassJsonList? masses = JsonSerializer.Deserialize<MassJsonList>(file.ReadToEnd());
			return masses;
		}
		
	}
	internal Mass ToMass() {
		return new Mass(1, new Vector2d(), new Vector2d());
	}
	internal MassJsonHelper(MassInfo mass) : base(){
		Index = mass.index;
		Name = mass.name;

		MassInGg = mass.mass;
		PositionInKm = mass.position;
		VelocityInKmS = mass.velocity;
		Stationary = mass.stationary;

		HasTrail = mass.hasTrail;
		TrailLength = mass.trail.Length / mass.trailQuality;
		TrailQuality = mass.trailQuality;

		FollowingIndex = mass.followingIndex;
		OrbitingBodyIndex = mass.orbitingBodyIndex;

		Satellite = mass.satellite;
		SemiMajorAxisLength = mass.semiMajorAxisLength;
		PrecisionPriorityLimitInSeconds = mass.precisionPriorityLimit;

	}
}
public class MassJsonList {
	public List<MassJsonHelper> JsonMasses {get; set;}
	public MassJsonList() {
		JsonMasses = new();
	}
}