using System.Collections.Generic;
using UnityEngine;


public class AbilityDecorator
{
	public float			value;
	public AbilityEffect	effect;

	public AbilityDecorator(float value, AbilityEffect effect)
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