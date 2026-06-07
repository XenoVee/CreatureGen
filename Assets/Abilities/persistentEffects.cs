using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.Rendering.DebugUI;

public interface IEffectHandler
{
	public IEffectHandler nextHandler { get; set; }
	void EndOfTurnHandle(Creature target);
	void OnDamageHandle(ref float value);
}

public class PersistentEffect : IEffectHandler
{
	public			IEffectHandler nextHandler { get; set; }
	public uint		priority;
	protected uint	duration; // duration of 0 means it lasts the whole battle.

	public void EndOfTurnHandle(Creature target)
	{
		EndOfTurnActivate(target);
		nextHandler?.EndOfTurnHandle(target);
	}

	public void OnDamageHandle(ref float damage)
	{
		OnDamageActivate(ref damage);
		nextHandler?.OnDamageHandle(ref damage);
	}

	protected virtual void EndOfTurnActivate(Creature target)
	{
		if (duration != 0)
		{
			duration -= 1;
			if (duration <= 0)
			{
				//You should remove YOURself, NOW!
				target.activeEffects.Remove(this);
			}
		}
	}

	protected virtual void OnDamageActivate(ref float damage)
	{
	}

	public virtual void OnApply()
	{
	}

	public virtual void OnRemove()
	{
	}
}

public class Burning : PersistentEffect
{
	int burnDamage;

	public Burning(uint _burnTimer, int _burnDamage)
	{
		duration = _burnTimer;
		burnDamage = _burnDamage;
		priority = 100;
	}

	protected override void EndOfTurnActivate(Creature target)
	{
		target.TakeDamage(burnDamage);
		base.EndOfTurnActivate(target);
	}

	protected override void OnDamageActivate(ref float damage)
	{
	}

	public override void OnApply()
	{
	}

	public override void OnRemove()
	{
	}
}

public class Vulnerable : PersistentEffect
{
	int bonusDamage;

	public Vulnerable(uint _vulnTimer, int _bonusDamage)
	{
		duration = _vulnTimer;
		bonusDamage = _bonusDamage;
		priority = 20;
	}

	protected override void EndOfTurnActivate(Creature target)
	{
		base.EndOfTurnActivate(target);
	}

	protected override void OnDamageActivate(ref float damage)
	{
		damage += bonusDamage;
	}

	public override void OnApply()
	{
	}

	public override void OnRemove()
	{
	}
}

public class Blocking : PersistentEffect
{
	float damageReduction;

	public Blocking(uint _defTimer, int _damageReduction)
	{
		duration = _defTimer;
		damageReduction = _damageReduction;
		priority = 21;
	}

	protected override void EndOfTurnActivate(Creature target)
	{
		base.EndOfTurnActivate(target);
	}

	protected override void OnDamageActivate(ref float damage)
	{
		int reducer = Mathf.CeilToInt(damage * (damageReduction / 100));
		damage -= reducer;
	}

	public override void OnApply()
	{
	}

	public override void OnRemove()
	{
	}
}