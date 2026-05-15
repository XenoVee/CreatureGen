using System.Collections.Generic;
using UnityEngine;


public class AbilityDecorator
{
	public float value;
	public Effect effect;

	public AbilityDecorator(float value, Effect effect)
	{
		this.value = value;
		this.effect = effect;
	}

	public Ability Decorate(Ability ability)
	{
		ability.AddEffect(effect);
		ability.AddValue(value);
		return ability;
	}
}