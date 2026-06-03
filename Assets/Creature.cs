using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Creature
{
	public List<GameObject>		bodyParts = new List<GameObject>();


	public float			maxHealth;
	public float			speed;
	public float			strength;
	public float			abilityPower;

	//public Torso			torso;
	//public Head				head;
	//public Arm				arms;
	//public Leg				legs;

	public List<Ability>			abilities;
	public List<PersistentEffect>	activeEffects;
	public float					currentHealth;

	public int selectedMove;
	TMP_Text combatLogText;
	public Creature(TMP_Text _combatLogText)
	{
		activeEffects = new List<PersistentEffect>();
		combatLogText = _combatLogText;
	}

	public string	creatureName;
	public float IncreaseHealth(float bonus)
	{
		maxHealth += bonus;
		currentHealth += bonus;
		return (maxHealth);
	}
	public float DecreaseHealth(float bonus)
	{
		maxHealth -= bonus;
		return (maxHealth);
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
	
	public void TakeDamage(float damage)
	{
		float actualDamage = HandleDamage(damage);
		currentHealth -= actualDamage;
		combatLogText.text = (creatureName +  " takes " +  actualDamage + " damage \n") + combatLogText.text;
	}

	public void Heal(float amount)
	{
		currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
	}

	private float HandleDamage(float damage)
	{
		if (activeEffects.Count > 0)
		{
			for (int i = 0; i < activeEffects.Count - 1; i++)
			{
				activeEffects[i].nextHandler = activeEffects[i + 1];
			}
			activeEffects[0]?.OnDamageHandle(ref damage);
		}
		return (damage);
	}

	public void EndOfTurn()
	{
		if (activeEffects.Count > 0)
		{
			for (int i = 0; i < activeEffects.Count - 1; i++)
			{
				activeEffects[i].nextHandler = activeEffects[i + 1];
			}
			activeEffects[0]?.EndOfTurnHandle(this);
		}
	}

	public void Move(float x, float y)
	{
		foreach (GameObject gameObject in bodyParts)
		{
			gameObject.transform.position += new Vector3(x, y, gameObject.transform.position.z);
		}
	}

	public void Move(Vector2 pos)
	{
		foreach (GameObject gameObject in bodyParts)
		{
			gameObject.transform.position += new Vector3(pos.x, pos.y, gameObject.transform.position.z);
		}
	}
	public void Move(Vector3 pos)
	{
		foreach (GameObject gameObject in bodyParts)
		{
			gameObject.transform.position += pos;
		}
	}

	public void SetPosition(float x, float y)
	{
		foreach (GameObject gameObject in bodyParts)
		{
			gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
		}
	}

	public void SetPosition(Vector2 pos)
	{
		foreach (GameObject gameObject in bodyParts)
		{
			gameObject.transform.position = new Vector3(pos.x, pos.y, gameObject.transform.position.z);
		}
	}

	public void SetPosition(Vector3 pos)
	{
		foreach (GameObject gameObject in bodyParts)
		{
			gameObject.transform.position = pos;
		}
	}

	public void UseAbility(Creature target, int abilityNum)
	{
		abilities[abilityNum].Use(this, target, combatLogText);
	}

	public string AbilityDescriptions()
	{
		string ret = "";
		int n = 1;
		foreach (Ability ab in abilities)
		{
			ret += n + ") " + ab.AbilityDescription(this) + "\n";
			n++;
		}
		return (ret);
	}
	public Vector2 GetPosition()
	{
		return (new Vector2(bodyParts[0].transform.position.x,
							bodyParts[0].transform.position.y));
	}
}
