using System.Collections.Generic;
using UnityEngine;

public class Torso : BodyPart
{
	public override void Apply(Creature target)
	{
		base.Apply(target);
		target.torso = this;
	}

	public override void Remove(Creature target)
	{
		base.Remove(target);
		target.torso = null;
	}
}
