//+0 total stats
public class GorillaArm : Arm
{
	public GorillaArm()
	{
		BonusHealth = 5;
		BonusSpeed = -6;
		BonusStrength = 5;
		BonusAbility = -5;
		partName = "Gorilla Arm";
		spritePath = "Sprites/GorillaArms";
	}
}
public class TentacleArm : Arm
{
	public TentacleArm()
	{
		BonusHealth = 0;
		BonusSpeed = 0;
		BonusStrength = -5;
		BonusAbility = 5;
		partName = "Tentacle Arm";
		spritePath = "Sprites/TentacleArms";
	}
}
public class BirdArm : Arm //also known as a wing :)
{
	public BirdArm()
	{
		BonusHealth = -2;
		BonusSpeed = 6;
		BonusStrength = -2;
		BonusAbility = -2;
		partName = "Bird Arm";
		spritePath = "Sprites/BirdArms";
	}
}