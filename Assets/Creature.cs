using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
	public float			health;
	public float			speed;
	public float			strength;
	public float			abilityPower;

	public Torso			torso;
	public Head				head;
	public Arm				arms;
	public Leg				legs;

	public List<Ability>	abilities;

	public string	creatureName;
	public float IncreaseHealth(float bonus)
	{
		health += bonus;
		return (health);
	}
	public float DecreaseHealth(float bonus)
	{
		health -= bonus;
		return (health);
	}
	public float IncreaseSpeed(float bonus)
	{
		speed += bonus;
		return (speed);
	}
	public float DecreaseSpeed(float bonus)
	{
		speed -= bonus;
		return (speed);
	}
	public float IncreaseStrength(float bonus)
	{
		strength += bonus;
		return (strength);
	}
	public float DecreaseStrength(float bonus)
	{
		strength -= bonus;
		return (strength);
	}
	public float IncreaseAbilityPower(float bonus)
	{
		abilityPower += bonus;
		return (abilityPower);
	}
	public float DecreaseAbilityPower(float bonus)
	{
		abilityPower -= bonus;
		return (abilityPower);
	}

	public void Poke()
	{
		Debug.Log("Creature \"" + creatureName + "\":\nstats:"
			+ "\n\tHealth: " + health
			+ "\n\tSpeed: " + speed
			+ "\n\tStrength: " + strength
			+ "\n\tAbilityPower: " + abilityPower + "\nBody parts: "
			+ "\n\tHead: " + head.getName()
			+ "\n\tTorso: " + torso.getName()
			+ "\n\tArms: " + arms.getName()
			+ "\n\tLegs: " + legs.getName()
			);
		if (abilities?.Count > 0)
		{
			foreach (Ability ability in abilities)
			{
				Debug.Log("Creature " + creatureName + " Uses ability: ");
				ability.Use(this);
			}
		}
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
