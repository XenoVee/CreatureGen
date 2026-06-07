using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CreatureBuilder
{
	private Creature creature;

	public CreatureBuilder(Creature _creature)
	{
		creature = _creature;
	}

	public CreatureBuilder WithBaseHealth(int _health)
	{
		creature.maxHealth = _health;
		creature.currentHealth = _health;
		return (this);
	}

	public CreatureBuilder WithBaseSpeed(int _speed)
	{
		creature.speed = _speed;
		return (this);
	}

	public CreatureBuilder WithBaseStrength(int _strength)
	{
		creature.strength = _strength;
		return (this);
	}

	public CreatureBuilder WithBaseAbilityPower(int _abilityPower)
	{
		creature.abilityPower = _abilityPower;
		return (this);
	}

	public CreatureBuilder WithTorso(Torso _torso, GameObject newObject)
	{
		_torso.Apply(creature, newObject);
		return (this);
	}

	public CreatureBuilder WithArms(Arm _arm, GameObject newObject)
	{
		_arm.Apply(creature, newObject);
		return (this);
	}

	public CreatureBuilder WithLegs(Leg _leg, GameObject newObject)
	{
		_leg.Apply(creature, newObject);
		return (this);
	}

	public CreatureBuilder WithHead(Head _head, GameObject newObject)
	{
		_head.Apply(creature, newObject);
		return (this);
	}

	public CreatureBuilder WithName(string _name)
	{
		creature.creatureName = _name;
		return (this);
	}

	public CreatureBuilder WithAbilities(List<Ability> _abilities)
	{
		creature.abilities = new List<Ability>(_abilities);
		return (this);
	}
		public Creature Build()
	{
		return (creature);
	}
}
