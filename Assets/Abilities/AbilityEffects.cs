using TMPro;
using UnityEngine;

public interface IAbilityHandler
{
	public IAbilityHandler	nextHandler { get; set; }
	void Handle(float value, Creature user, Creature enemy, TMP_Text combatLogText);
}

public abstract class AbilityEffect : IAbilityHandler
{
	public IAbilityHandler	nextHandler { get; set; }
	public uint				priority;

	public void Handle(float value, Creature user, Creature enemy, TMP_Text combatLogText)
	{
		Activate(value, user, enemy, combatLogText);
		nextHandler?.Handle(value, user, enemy, combatLogText);
	}

	public abstract void Activate(float value, Creature user, Creature enemy, TMP_Text combatLogText);
	public abstract AbilityEffect Copy();
	protected abstract int EffectiveValue(float value, Creature user);
	public abstract string GetEffect(float value, Creature user);

}

public class SelfHeal : AbilityEffect
{
	public SelfHeal()
	{ 
		priority = 10;
	}
	public override void Activate(float value, Creature user, Creature enemy, TMP_Text combatLogText)
	{
		
		combatLogText.text = (user.creatureName + " Heals themselves for " + EffectiveValue(value, user) + " Hit points\n") + combatLogText.text;
		user.Heal(EffectiveValue(value, user));
	}

	protected override int EffectiveValue(float value, Creature user)
	{
		return(((int)Mathf.Ceil(value * (float)(user.abilityPower * 0.5))));
	}

	public override string GetEffect(float value, Creature user)
	{
		return("Heal " + EffectiveValue(value, user));
	}

	public override AbilityEffect Copy()
	{
		SelfHeal copy = new SelfHeal();
		return (copy);
	}
}

public class Burn : AbilityEffect
{
	int	baseDuration = 2;

	public Burn()
	{
		priority = 20;
	}

	public override void Activate(float value, Creature user, Creature enemy, TMP_Text combatLogText)
	{
		int duration = (int)Mathf.Round(baseDuration + (float)(user.abilityPower * 0.1));
		combatLogText.text = (user.creatureName + " burns their target for " + EffectiveValue(value, user) + " damage per turn for " + duration + " turns\n") + combatLogText.text;
		new Burning((uint)duration, EffectiveValue(value, user)).Apply(enemy);
	}

	protected override int EffectiveValue(float value, Creature user)
	{
		return ((int)Mathf.Ceil(value * (float)(user.abilityPower * 0.5)));
	}

	public override string GetEffect(float value, Creature user)
	{
		return ("Burn " + EffectiveValue(value, user) + " for " + (int)Mathf.Round(baseDuration + (float)(user.abilityPower * 0.1)) + " turns");
	}

	public override AbilityEffect Copy()
	{
		Burn copy = new Burn();
		return (copy);
	}
}

public class Damage : AbilityEffect
{
	public Damage()
	{
		priority = 15;
	}
	public override void Activate(float value, Creature user, Creature enemy, TMP_Text combatLogText)
	{
		combatLogText.text = (user.creatureName + " hits their target for " + EffectiveValue(value, user) + " damage\n") + combatLogText.text;
		enemy.TakeDamage(EffectiveValue(value, user));
	}

	protected override int EffectiveValue(float value, Creature user)
	{
		return ((int)Mathf.Ceil(value * (float)(user.strength * 0.7)));
	}

	public override string GetEffect(float value, Creature user)
	{
		return ("Deal " + EffectiveValue(value, user) + " damage");
	}

	public override AbilityEffect Copy()
	{
		Damage	copy = new Damage();
		return (copy);
	}
}

public class Empower : AbilityEffect
{
	public Empower()
	{
		priority = 0;
	}

	public override void Activate(float value, Creature user, Creature enemy, TMP_Text combatLogText)
	{
		combatLogText.text = (user.creatureName + " Buffs their Strength and Ability power by " + EffectiveValue(value, user) + "\n") + combatLogText.text;
		user.IncreaseStrength(EffectiveValue(value, user));
		user.IncreaseAbilityPower(EffectiveValue(value, user));
	}

	protected override int EffectiveValue(float value, Creature user)
	{
		return ((int)Mathf.Ceil((value / 2) * (float)(user.abilityPower * 0.4)));
	}
	public override string GetEffect(float value, Creature user)
	{
		return ("gain " + EffectiveValue(value, user) + " strength and ability");
	}

	public override AbilityEffect Copy()
	{
		Empower	copy = new Empower();
		return (copy);
	}
}

public class Accellerate : AbilityEffect
{
	public Accellerate()
	{
		priority = 5;
	}

	public override void Activate(float value, Creature user, Creature enemy, TMP_Text combatLogText)
	{
		combatLogText.text = (user.creatureName + " Buffs their speed by " + EffectiveValue(value, user) + "\n") + combatLogText.text;
		user.IncreaseSpeed(EffectiveValue(value, user));
	}

	protected override int EffectiveValue(float value, Creature user)
	{
		return ((int)Mathf.Ceil((value / 2) * (float)(user.abilityPower * 0.5)));
	}

	public override string GetEffect(float value, Creature user)
	{
		return ("gain " + EffectiveValue(value, user) + " speed");
	}
	public override AbilityEffect Copy()
	{
		Accellerate	copy = new Accellerate();
		return (copy);
	}
}

public class MakeVulnerable : AbilityEffect
{
	int baseDuration = 1;

	public MakeVulnerable()
	{
		priority = 25;
	}

	public override void Activate(float value, Creature user, Creature enemy, TMP_Text combatLogText)
	{
		int duration = (int)Mathf.Round(baseDuration + (float)(user.abilityPower * 0.1));
		combatLogText.text = (user.creatureName + " weakens their target for " + duration + " turns to take " + EffectiveValue(value, user) + " extra damage\n") + combatLogText.text;
		new Vulnerable((uint)duration, EffectiveValue(value, user)).Apply(enemy);
	}

	protected override int EffectiveValue(float value, Creature user)
	{
		return ((int)Mathf.Ceil((value / 4) * (float)(user.abilityPower * 0.2)));
	}

	public override string GetEffect(float value, Creature user)
	{
		return ("weaken " + EffectiveValue(value, user) + " for " + (int)Mathf.Round(baseDuration + (float)(user.abilityPower * 0.1)) + " turns");
	}

	public override AbilityEffect Copy()
	{
		MakeVulnerable	copy = new MakeVulnerable();
		return (copy);
	}
}

public class Defend : AbilityEffect
{
	int baseDuration = 1;

	public Defend()
	{
		priority = 25;
	}

	public override void Activate(float value, Creature user, Creature enemy, TMP_Text combatLogText)
	{
		int	duration = (int)Mathf.Round(baseDuration + (float)(user.abilityPower * 0.1));
		combatLogText.text = (user.creatureName + " defends for " + duration + " turns to take " + EffectiveValue(value, user) + " percent less damage\n") + combatLogText.text;
		new Defending((uint)duration, EffectiveValue(value, user)).Apply(user);
	}

	protected override int EffectiveValue(float value, Creature user)
	{
		return ((int)Mathf.Ceil((value * 5) * (float)(user.abilityPower * 0.3)));
	}

	public override string GetEffect(float value, Creature user)
	{
		return ("defend " + EffectiveValue(value, user) + " percent, for " + (int)Mathf.Round(baseDuration + (float)(user.abilityPower * 0.1)) + " turns");
	}

	public override AbilityEffect Copy()
	{
		Defend copy = new Defend();
		return (copy);
	}
}

public class ApplyTimeBomb : AbilityEffect
{
	int baseDuration = 4;

	public ApplyTimeBomb()
	{
		priority = 50;
	}

	public override void Activate(float value, Creature user, Creature enemy, TMP_Text combatLogText)
	{
		int duration = baseDuration;
		combatLogText.text = (user.creatureName + " will detonate its opponent after " + duration + " turns for " + EffectiveValue(value, user) + " damage\n") + combatLogText.text;
		new TimeBomb((uint)duration, EffectiveValue(value, user)).Apply(enemy);
	}

	protected override int EffectiveValue(float value, Creature user)
	{
		return ((int)Mathf.Ceil((value * 6) * (float)(user.strength * 0.3)));
	}

	public override string GetEffect(float value, Creature user)
	{
		return ("deal " + EffectiveValue(value, user) + " damage after " + baseDuration + " turns");
	}

	public override AbilityEffect Copy()
	{
		ApplyTimeBomb copy = new ApplyTimeBomb();
		return (copy);
	}
}