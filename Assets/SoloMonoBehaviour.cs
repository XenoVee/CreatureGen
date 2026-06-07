using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SoloMonoBehaviour : MonoBehaviour
{

	List<Head>					headPool = new ();
	List<Arm>					armPool = new ();
	List<Torso>					torsoPool = new ();
	List<Leg>					legPool = new ();
	List<AbilityEffect>			abilityEffectPool = new ();
	List<Creature>				creatures = new List<Creature>();

	[SerializeField] Vector2Int	baseHealthRange;
	[SerializeField] Vector2Int	baseSpeedRange;
	[SerializeField] Vector2Int	baseStrengthRange;
	[SerializeField] Vector2Int	baseAbilityPowerRange;
	[SerializeField] Vector2Int	abilityAmountRange;
	[SerializeField] Vector2	abilityValueBase;
	[SerializeField] Vector2	abilityAdditionalValuePerEffectRange;
	[SerializeField] Vector2Int	effectsPerAbilityRange;

	[SerializeField] GameObject	emptyCreaturePrefab;
	[SerializeField] TMP_Text	allyHealthText;
	[SerializeField] TMP_Text	enemyHealthText;
	[SerializeField] TMP_Text	abilityDescriptionText;
	[SerializeField] TMP_Text	combatLogText;
	[SerializeField] TMP_Text	gameOverScreenText;
	[SerializeField] int		turnTime;

	private int					cooldown = 0;
	Creature					allyCreature;
	Creature					enemyCreature;
	bool						gameOver = false;

	//.part file delimiters
	char[]						ttlLineDelimiter = new char[] { '\n' };
	char[]						splitTypeName = new char[] { ':', ' ', '\t' };

void Start()
	{
		PreparePools();
		for (int i = 0; i < 2; i++)
		{
			int rngTorso = UnityEngine.Random.Range(0, torsoPool.Count);
			int rngHead = UnityEngine.Random.Range(0, headPool.Count);
			int rngArm = UnityEngine.Random.Range(0, armPool.Count);
			int rngLeg = UnityEngine.Random.Range(0, legPool.Count);

			int rngHealth = (int)UnityEngine.Random.Range(baseHealthRange[0], baseHealthRange[1]);
			int rngStrength = (int)UnityEngine.Random.Range(baseSpeedRange[0], baseSpeedRange[1]);
			int rngSpeed = (int)UnityEngine.Random.Range(baseStrengthRange[0], baseStrengthRange[1]);
			int rngAbilityPower = (int)UnityEngine.Random.Range(baseAbilityPowerRange[0], baseAbilityPowerRange[1]);

			List<Ability> abilities = GenerateAbilities();

			creatures.Add(
				new CreatureBuilder(new Creature(combatLogText))
					.WithBaseHealth(rngHealth)
					.WithBaseStrength(rngStrength)
					.WithBaseSpeed(rngSpeed)
					.WithBaseAbilityPower(rngAbilityPower)
					.WithLegs(legPool[rngLeg], Instantiate(emptyCreaturePrefab))
					.WithTorso(torsoPool[rngTorso], Instantiate(emptyCreaturePrefab))
					.WithHead(headPool[rngHead], Instantiate(emptyCreaturePrefab))
					.WithArms(armPool[rngArm], Instantiate(emptyCreaturePrefab))
					.WithName(i == 0 ? "Your creature" : "Enemy Creature")
					.WithAbilities(abilities)
					.Build());
		}
		allyCreature = creatures[0];
		enemyCreature = creatures[1];
		allyCreature.SetPosition(-7, 0);
		enemyCreature.SetPosition(7, 0);
	}

	void UseMove(Creature user, Creature target)
	{
		user.UseAbility(target, user.selectedMove);
	}

	int SelectEnemyMove()
	{
		return (UnityEngine.Random.Range(0, enemyCreature.abilities.Count()));
	}

	void SelectMove(int move)
	{
		allyCreature.selectedMove = move;
		cooldown += turnTime;
		abilityDescriptionText.text = "";
		enemyCreature.selectedMove = SelectEnemyMove();
		combatLogText.text = "";
	}

	void MoveSelectionInput()
	{
		if (Keyboard.current.digit1Key.IsPressed())
		{
			SelectMove(0);
		}
		else if (Keyboard.current.digit2Key.IsPressed())
		{
			SelectMove(1);
		}
		else if (Keyboard.current.digit3Key.IsPressed())
		{
			SelectMove(2);
		}
		else if (Keyboard.current.digit4Key.IsPressed())
		{
			SelectMove(3);
		}
	}

	void GameOver()
	{
		foreach (Creature creature in creatures)
		{
			foreach (GameObject part in creature.bodyParts)
			{
				part.GetComponent<SpriteRenderer>().enabled = false;
			}
		}
		abilityDescriptionText.text = "";
		gameOver = true;
		if (allyCreature.currentHealth < enemyCreature.currentHealth)
		{
			gameOverScreenText.text = "Defeat!";
		}
		else
		{
			gameOverScreenText.text ="Victory";
		}
	}

	void Update()
	{
		if ((allyCreature.currentHealth <= 0 || enemyCreature.currentHealth <= 0) && !gameOver)
		{
			GameOver();
		}
		allyHealthText.text = allyCreature.currentHealth.ToString() + " / " + allyCreature.maxHealth.ToString() + " HP";
		enemyHealthText.text = enemyCreature.currentHealth.ToString() + " / " + enemyCreature.maxHealth.ToString() + " HP";
		if (!gameOver)
		{

			if (cooldown == 0)
			{
				creatures = creatures.OrderByDescending(x => x.speed).ToList();
				abilityDescriptionText.text = "Abilities:\n" + allyCreature.AbilityDescriptions();
				MoveSelectionInput();
			}
			else
			{
				cooldown--;
				if (cooldown <= 0)
				{
					foreach (Creature creature in creatures)
					{
						creature.EndOfTurn();
					}
				}
			}
			if (cooldown == turnTime)
			{
				UseMove(creatures[0], creatures[1]);
			}
			else if (cooldown == turnTime / 2)
			{
				UseMove(creatures[1], creatures[0]);
			}
		}
	}

	//// definetely not neccesary WASD movement (for testing purposes)
	//float moveSpeed = 0.1f;
	//Vector3 moveVector = new();
	//if (Keyboard.current.aKey.IsPressed())
	//{
	//	moveVector += (new Vector3(-moveSpeed, 0, 0));
	//}
	//else if (Keyboard.current.dKey.IsPressed())
	//{
	//	moveVector += (new Vector3(moveSpeed, 0, 0));
	//}
	//if (Keyboard.current.sKey.IsPressed())
	//{
	//	moveVector += (new Vector3(0, -moveSpeed, 0));
	//}
	//if (Keyboard.current.wKey.IsPressed())
	//{
	//	moveVector += (new Vector3(0, moveSpeed, 0));
	//}
	//moveVector.Normalize();
	//moveVector *= moveSpeed;
	//allyCreature.Move(moveVector);

	// generates a random ability using the effects loaded into the pool
List<Ability> GenerateAbilities()
	{
		List<Ability> list = new List<Ability>();
		int NumAbilities = UnityEngine.Random.Range(abilityAmountRange[0], abilityAmountRange[1] + 1);

		for (int i = 0; i < NumAbilities; i++)
		{
			Ability ability = new Ability(UnityEngine.Random.Range(
				abilityValueBase[0],
				abilityValueBase[1]));
			int NumEffects = (int)UnityEngine.Random.Range(effectsPerAbilityRange[0], effectsPerAbilityRange[1] + 1);
			for (int j = 0; j < NumEffects; j++)
			{
				AbilityDecorator decorator = new AbilityDecorator(
												UnityEngine.Random.Range(abilityAdditionalValuePerEffectRange[0], abilityAdditionalValuePerEffectRange[1]),
												abilityEffectPool[UnityEngine.Random.Range(0, abilityEffectPool.Count())].Copy());
				ability = decorator.Decorate(ability);
			}
			list.Add(ability);
			ability.OrderEffects();
		}
		return (list);
	}

	// attempts to load all body parts and ability effects in .ttl (things to load) file in the ttl folder into the respective pools
	void PreparePools()
	{
		Debug.Log(Application.streamingAssetsPath);
		foreach (var file in Directory.EnumerateFiles(Application.streamingAssetsPath + "/ttl files", "*.ttl"))
		{
			string contents = File.ReadAllText(file);
			foreach (string splitContent in contents.Split(ttlLineDelimiter))
			{
				string[] parts = splitContent.Split(splitTypeName);
				if (parts.Length == 2)
				{
					FindTypes(parts);
				}
			}
		}
	}

	// create an instance of bodypart/effect T and put it into the proper pool
	int AddToPool<T>(string toCreate, List<T> list)
	{
		Type toCreateType = Type.GetType(toCreate, false);
		if (toCreateType == null)
		{
			Error(("Unkown Type: \'" + toCreate + "\'. is the class missing?"));
			return (1);
		}
		T part = (T)Activator.CreateInstance(toCreateType);
		list.Add(part);
		return (0);
	}

	void FindTypes(string[] parts)
	{
		if (parts[0] == "Head")
		{
			AddToPool<Head>(RemoveCarriageReturns(parts[1]), headPool);
		}
		else if (parts[0] == "Torso")
		{
			AddToPool<Torso>(RemoveCarriageReturns(parts[1]), torsoPool);
		}
		else if (parts[0] == "Arm")
		{
			AddToPool<Arm>(RemoveCarriageReturns(parts[1]), armPool);
		}
		else if (parts[0] == "Leg")
		{
			AddToPool<Leg>(RemoveCarriageReturns(parts[1]), legPool);
		}
		else if (parts[0] == "Effect")
		{
			AddToPool<AbilityEffect>(RemoveCarriageReturns(parts[1]), abilityEffectPool);
		}
		else
		{
			Debug.Log("Unkown Type: \'" + parts[0] + "\'");
		}
	}

	string RemoveCarriageReturns(string fuckedUpString) // who keeps putting those there anyway
	{
		return (Regex.Replace(fuckedUpString, @"\r", ""));
	}

	void Error(string message)
	{
		Debug.LogError(RemoveCarriageReturns(message));
	}
}
