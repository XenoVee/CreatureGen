using UnityEngine;

public class Leg : BodyPart
{
	public override void Apply(Creature target)
	{
		base.Apply(target);
		target.legs = this;

	}

	public override void Remove(Creature target)
	{
		base.Remove(target);
		target.legs = null;

	}
}
