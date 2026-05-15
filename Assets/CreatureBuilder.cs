using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.TextCore.Text;

public class CreatureBuilder
{
	private Creature creature;

	public CreatureBuilder(Creature _creature)
	{
		creature = _creature;
	}

	public CreatureBuilder WithBaseHealth(int Health)
	{
		creature.health = Health;
		return (this);
	}
	public CreatureBuilder WithBaseSpeed(int speed)
	{
		creature.speed = speed;
		return (this);
	}
	public CreatureBuilder WithBaseStrength(int strength)
	{
		creature.strength = strength;
		return (this);
	}
	public CreatureBuilder WithBaseAbilityPower(int abilityPower)
	{
		creature.abilityPower = abilityPower;
		return (this);
	}
	public CreatureBuilder WithTorso(Torso torso)
	{
		creature.torso = torso;
		torso.Apply(creature);
		return (this);
	}
	public CreatureBuilder WithArms(Arm arm)
	{
		creature.arms = arm;
		arm.Apply(creature);
		return (this);
	}
	public CreatureBuilder WithLegs(Leg leg)
	{
		creature.legs = leg;
		leg.Apply(creature);
		return (this);
	}
	public CreatureBuilder WithHead(Head head)
	{
		creature.head = head;
		head.Apply(creature);
		return (this);
	}

	public CreatureBuilder WithName(string name)
	{
		creature.creatureName = name;
		return (this);
	}

	public CreatureBuilder WithAbilities(List<Ability> abilities)
	{
		creature.abilities = new List<Ability>(abilities);
		return (this);
	}
	public Creature Build()
	{
		return (creature);
	}

}
