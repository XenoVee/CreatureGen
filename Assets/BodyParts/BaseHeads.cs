// +8 total stats

public class OrcHead : Head
{
	public OrcHead()
	{
		BonusHealth = 6;
		BonusSpeed = -3;
		BonusStrength = 10;
		BonusAbility = -5;
		partName = "Orc Head";
		spritePath = "Sprites/OrcHead";
	}
}

public class BirdHead : Head
{
	public BirdHead()
	{
		BonusHealth = -2;
		BonusSpeed = 8;
		BonusStrength = -4;
		BonusAbility = 6;
		partName = "Bird Head";
		spritePath = "Sprites/BirdHead";
	}
}

public class BugHead : Head
{
	public BugHead()
	{
		BonusHealth = 2;
		BonusSpeed = 2;
		BonusStrength = 2;
		BonusAbility = 2;
		partName = "Bug Head";
		spritePath = "Sprites/BugHead";
	}
}