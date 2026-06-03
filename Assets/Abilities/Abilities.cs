using System.Collections.Generic;
using System.Linq;
using TMPro;
public class Ability
{
	protected float value;

	List<AbilityEffect> effects;

	public Ability(float _value)
	{
		this.value = _value;
		effects = new List<AbilityEffect>();
	}
	public void AddEffect(AbilityEffect effect)
	{
		effects.Add(effect);
	}

	public void AddValue(float add)
	{
		value += add;
	}

	public Ability()
	{
		effects = new List<AbilityEffect>();
	}

	public void OrderEffects()
	{
		effects = effects.OrderBy(x => x.priority).ToList();
	}

	public string AbilityDescription(Creature user)
	{
		string ret = "";
		foreach (AbilityEffect effect in effects)
		{
			ret += effect.GetEffect(value, user) + ", ";
		}
		return ret;
	}

	public void Use(Creature user, Creature enemy, TMP_Text combatLogText)
	{
		for (int i = 0; i < effects.Count - 1; i++)
		{
			effects[i].nextHandler = effects[i + 1];
		}
		effects[0]?.Handle(value, user, enemy, combatLogText);
	}
}