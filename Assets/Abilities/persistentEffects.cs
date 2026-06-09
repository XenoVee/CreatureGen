using Unity.IO.LowLevel.Unsafe;
using System.Linq;
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
				this.Remove(target);
			}
		}
	}

	public void Apply(Creature target)
	{
		target.activeEffects.Add(this);
		target.activeEffects = target.activeEffects.OrderBy(x => x.priority).ToList();
		this.OnApply();
	}
	public void Remove(Creature target)
	{
		//You should remove YOURself, NOW!
		this.OnRemove(target);
		target.activeEffects.Remove(this);
	}

	protected virtual void OnDamageActivate(ref float damage)
	{
	}

	protected virtual void OnApply()
	{
	}

	protected virtual void OnRemove(Creature target)
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

	protected override void OnApply()
	{
	}

	protected override void OnRemove(Creature target)
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

	protected override void OnApply()
	{
	}

	protected override void OnRemove(Creature target)
	{
	}
}

public class Defending : PersistentEffect
{
	float damageReduction;

	public Defending(uint _defTimer, int _damageReduction)
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

	protected override void OnApply()
	{
	}

	protected override void OnRemove(Creature target)
	{
	}
}

public class TimeBomb : PersistentEffect
{
	int BombDamage;

	public TimeBomb(uint _burnTimer, int _BombDamage)
	{
		duration = _burnTimer;
		BombDamage = _BombDamage;
		priority = 200;
	}

	protected override void EndOfTurnActivate(Creature target)
	{
		base.EndOfTurnActivate(target);
	}

	protected override void OnDamageActivate(ref float damage)
	{
	}

	protected override void OnApply()
	{
	}

	protected override void OnRemove(Creature target)
	{
		target.TakeDamage(BombDamage);
	}
}