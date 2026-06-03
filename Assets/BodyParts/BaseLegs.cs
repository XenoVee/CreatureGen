// +10 total stats, emphasis on speed changes
//Leg: GrashopperLeg
//Leg:FishLeg
//Leg:MultiLeg
public class GrashopperLeg : Leg
{
	public GrashopperLeg()
	{
		BonusHealth = 5;
		BonusSpeed = +10;
		BonusStrength = -3;
		BonusAbility = -2;
		partName = "Grashopper Leg";
		spritePath = "Sprites/GrasshopperLegs";
	}
}
public class FishLeg : Leg //would probably add a (maybe passive) ability to raise speed when wet
{
	public FishLeg()
	{
		BonusHealth = 10;
		BonusSpeed = -20;
		BonusStrength = 10;
		BonusAbility = 10;
		partName = "Fish Leg";
		spritePath = "Sprites/FishLegs";
	}
}
public class MultiLeg : Leg
{
	public MultiLeg()
	{
		BonusHealth = 0;
		BonusSpeed = +20;
		BonusStrength = -5;
		BonusAbility = -5;
		partName = "Multi Leg";
		spritePath = "Sprites/MultiLegs";
	}
}