using UnityEngine;

public class Arm : BodyPart
{
	public override void Apply(Creature target)
	{
		base.Apply(target);
		target.arms = this;

	}

	public override void Remove(Creature target)
	{
		base.Remove(target);
		target.arms = null;

	}
}
