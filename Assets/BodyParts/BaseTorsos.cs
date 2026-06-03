//+ 20 total stats
public class TurtleTorso : Torso
{
	public TurtleTorso()
	{
		BonusHealth = 20;
		BonusSpeed = -10;
		BonusStrength = 4;
		BonusAbility = 6;
		partName = "Turtle Torso";
		spritePath = "Sprites/TurtleTorso";
	}
}
public class PrimateTorso : Torso
{
	public PrimateTorso()
	{
		BonusHealth = 5;
		BonusSpeed = 5;
		BonusStrength = 5;
		BonusAbility = 5;
		partName = "Primate Torso";
		spritePath = "Sprites/PrimateTorso";
	}
}
public class SalamanderTorso : Torso
{
	public SalamanderTorso()
	{
		BonusHealth = 10;
		BonusSpeed = 3;
		BonusStrength = -5;
		BonusAbility = 12;
		partName = "Salamander Torso";
		spritePath = "Sprites/SalamanderTorso";
	}
}