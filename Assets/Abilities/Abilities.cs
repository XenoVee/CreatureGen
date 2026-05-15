using System.Collections.Generic;
public class Ability
{
	protected float value;

	List<Effect> effects;

	public void AddEffect(Effect effect)
	{
		effects.Add(effect);
	}

	public void AddValue(float add)
	{
		value += add;
	}

	public Ability()
	{
		effects = new List<Effect>();
	}

	public void Use(Creature user)
	{
		for (int i = 0; i < effects.Count - 1; i++)
		{
			effects[i].nextHandler = effects[i + 1];
		}
		effects[0]?.Handle(ref value, user);
	}
}