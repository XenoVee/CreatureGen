using UnityEngine;

public class Head : BodyPart
{
	public override void Apply(Creature target)
	{
		base.Apply(target);
		target.head = this;
	}

	public override void Remove(Creature target)
	{
		base.Remove(target);
		target.head = null;
	}
}
