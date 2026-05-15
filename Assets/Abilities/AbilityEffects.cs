using UnityEngine;

public interface IHandler
{
	public IHandler nextHandler { get; set; }
	void Handle(ref float value, Creature user);
}

public abstract class Effect : IHandler
{
	public IHandler nextHandler { get; set; }

	public void Handle(ref float value, Creature user)
	{
		Activate(ref value, user);
		nextHandler?.Handle(ref value, user);
	}

	public abstract void Activate(ref float value, Creature user);
}

public class SelfHeal : Effect
{
	public override void Activate(ref float value, Creature user)
	{
		Debug.Log(user.creatureName + " Heals themselves for " + value * (float)(user.abilityPower * 0.5) + " Hit points");
		user.IncreaseHealth(value * (float)(user.abilityPower * 0.5));
	}
}

public class Burn : Effect
{
	public override void Activate(ref float value, Creature user)
	{
		Debug.Log(user.creatureName + " burns their target for " + value * (float)(user.abilityPower * 0.5) + " damage");
	}
}

public class Damage : Effect
{
	public override void Activate(ref float value, Creature user)
	{
		Debug.Log(user.creatureName + " hits their target for " + value * (float)(user.strength * 0.7) + " damage");
	}
}

public class Empower : Effect
{
	public override void Activate(ref float value, Creature user)
	{
		Debug.Log(user.creatureName + " Buffs their Strength and Ability power by " + value * (float)(user.abilityPower * 0.1));
		user.IncreaseStrength(value * (float)(user.abilityPower * 0.1));
		user.IncreaseAbilityPower(value * (float)(user.abilityPower * 0.1));
	}
}

public class Accellerate : Effect
{
	public override void Activate(ref float value, Creature user)
	{
		Debug.Log(user.creatureName + " Buffs their speed by " + value * (float)(user.abilityPower * 0.1));
		user.IncreaseSpeed(value * (float)(user.abilityPower * 0.2));
	}
}
