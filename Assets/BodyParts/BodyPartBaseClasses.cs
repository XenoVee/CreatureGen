using System.Security.Cryptography;
using UnityEditor.Rendering;
using UnityEngine;

public class BodyPart
{
	protected float		BonusHealth;
	protected float		BonusSpeed;
	protected float		BonusStrength;
	protected float		BonusAbility;
	protected string	partName;
	protected string	spritePath;
	protected GameObject gameObject;

	public string GetName()
	{
		return partName;
	}

	public void SetName(string name)
	{
		partName = name;
	}

	public virtual void Apply(Creature target, GameObject newObject)
	{
		target.IncreaseMaxHealth(BonusHealth);
		target.IncreaseSpeed(BonusSpeed);
		target.IncreaseStrength(BonusStrength);
		target.IncreaseAbilityPower(BonusAbility);
		gameObject = newObject;
		SpriteRenderer spriteRenderer = (SpriteRenderer)gameObject.GetComponent("SpriteRenderer");
		Sprite sprite  = Resources.Load<Sprite>(spritePath);
		spriteRenderer.sprite = sprite;
		target.bodyParts.Add(gameObject);
	}

	public virtual void Remove(Creature target)
	{
		target.DecreaseMaxHealth(BonusHealth);
		target.DecreaseSpeed(BonusSpeed);
		target.DecreaseStrength(BonusStrength);
		target.DecreaseAbilityPower(BonusAbility);
		target.bodyParts.Remove(gameObject);
		Object.Destroy(gameObject);
	}
}

public class Head : BodyPart
{
	public override void Apply(Creature target, GameObject newObject)
	{
		base.Apply(target, newObject);
		target.head = this;
	}

	public override void Remove(Creature target)
	{
		base.Remove(target);
		target.head = null;
	}

	public Head Copy()
	{
		Head copy = new Head();

		copy.BonusHealth = this.BonusHealth;
		copy.BonusSpeed = this.BonusSpeed;
		copy.BonusStrength = this.BonusStrength;
		copy.BonusAbility = this.BonusAbility;
		copy.partName = this.partName;
		return (copy);
	}
}

public class Arm : BodyPart
{
	public override void Apply(Creature target, GameObject newObject)
	{
		base.Apply(target, newObject);
		target.arms = this;
	}

	public override void Remove(Creature target)
	{
		base.Remove(target);
		target.arms = null;
	}

	public Arm Copy()
	{
		Arm copy = new Arm();

		copy.BonusHealth = this.BonusHealth;
		copy.BonusSpeed = this.BonusSpeed;
		copy.BonusStrength = this.BonusStrength;
		copy.BonusAbility = this.BonusAbility;
		copy.partName = this.partName;
		return (copy);
	}
}

public class Leg : BodyPart
{
	public override void Apply(Creature target, GameObject newObject)
	{
		base.Apply(target, newObject);
		target.legs = this;
	}

	public override void Remove(Creature target)
	{
		base.Remove(target);
		target.legs = null;
	}

	public Leg Copy()
	{
		Leg copy = new Leg();

		copy.BonusHealth = this.BonusHealth;
		copy.BonusSpeed = this.BonusSpeed;
		copy.BonusStrength = this.BonusStrength;
		copy.BonusAbility = this.BonusAbility;
		copy.partName = this.partName;
		return (copy);
	}
}

public class Torso : BodyPart
{
	public override void Apply(Creature target, GameObject newObject)
	{
		base.Apply(target, newObject);
		target.torso = this;
	}

	public override void Remove(Creature target)
	{
		base.Remove(target);
		target.torso = null;
	}

	public Torso Copy()
	{
		Torso copy = new Torso();

		copy.BonusHealth = this.BonusHealth;
		copy.BonusSpeed = this.BonusSpeed;
		copy.BonusStrength = this.BonusStrength;
		copy.BonusAbility = this.BonusAbility;
		copy.partName = this.partName;
		return (copy);
	}
}
