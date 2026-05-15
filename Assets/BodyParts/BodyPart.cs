using UnityEngine;

public class BodyPart : MonoBehaviour
{
	[SerializeField] protected float	BonusHealth;
	[SerializeField] protected float	BonusSpeed;
	[SerializeField] protected float	BonusStrength;
	[SerializeField] protected float	BonusAbility;
	[SerializeField] protected string	partName;

	public string getName()
	{
		return partName;
	}
	public virtual void Apply(Creature target)
	{
		target.IncreaseHealth(BonusHealth);
		target.IncreaseSpeed(BonusSpeed);
		target.IncreaseStrength(BonusStrength);
		target.IncreaseAbilityPower(BonusAbility);
	}

	public virtual void Remove(Creature target)
	{
		target.DecreaseHealth(BonusHealth);
		target.DecreaseSpeed(BonusSpeed);
		target.DecreaseStrength(BonusStrength);
		target.DecreaseAbilityPower(BonusAbility);
	}
}
