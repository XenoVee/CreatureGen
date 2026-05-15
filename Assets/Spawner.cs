using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

	[SerializeField] List<Head>		headList;
	[SerializeField] List<Arm>		armList;
	[SerializeField] List<Torso>	torsoList;
	[SerializeField] List<Leg>		legList;

	[SerializeField] Vector2		baseHealthRange;
	[SerializeField] Vector2		baseSpeedRange;
	[SerializeField] Vector2		baseStrengthRange;
	[SerializeField] Vector2		baseAbilityPowerRange;
	[SerializeField] Vector2		abilityAmountRange;
	[SerializeField] Vector2		abilityValueRange;
	[SerializeField] Vector2		effectsPerAbilityRange;

	[SerializeField] Creature		emptyCreaturePrefab;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		List<Creature> creatures = new List<Creature>();
		for (int i = 0; i < 5; i++)
		{
			int rngTorso = Random.Range(0, torsoList.Count - 1);
			int rngHead = Random.Range(0, headList.Count - 1);
			int rngArm = Random.Range(0, armList.Count - 1);
			int rngLeg = Random.Range(0, legList.Count - 1);

			int rngHealth = (int) Random.Range(baseHealthRange[0], baseHealthRange[1]);
			int rngStrength = (int) Random.Range(baseSpeedRange[0], baseSpeedRange[1]);
			int rngSpeed = (int) Random.Range(baseStrengthRange[0], baseStrengthRange[1]);
			int rngAbilityPower = (int)Random.Range(baseAbilityPowerRange[0], baseAbilityPowerRange[1]);

			List<Ability> abilities = GenerateAbilities();

			creatures.Add(
				new CreatureBuilder(Instantiate(emptyCreaturePrefab))
					.WithBaseHealth(rngHealth)
					.WithBaseStrength(rngStrength)
					.WithBaseSpeed(rngSpeed)
					.WithBaseAbilityPower(rngAbilityPower)
					.WithTorso(torsoList[rngTorso])
					.WithHead(headList[rngHead])
					.WithArms(armList[rngArm])
					.WithLegs(legList[rngLeg])
					.WithName(i.ToString())
					.WithAbilities(abilities)
					.Build()
				);
		}
		foreach ( Creature creature in creatures )
		{
			creature.Poke();
		}
	}
	List<Ability> GenerateAbilities()
	{
		List<Ability> list = new List<Ability>();
		int NumAbilities = (int)Random.Range(abilityAmountRange[0], abilityAmountRange[1]);

		for (int i = 0; i < NumAbilities; i++)
		{
			Ability ability = new Ability();
			int NumEffects = (int)Random.Range(effectsPerAbilityRange[0], effectsPerAbilityRange[1]);
			for (int j = 0; j < NumEffects; j++)
			{
				AbilityDecorator decorator = new AbilityDecorator(Random.Range(abilityValueRange[0], abilityValueRange[1]), RandomEffect());
				ability = decorator.Decorate(ability);
			}
			list.Add(ability);
		}
	return (list);
	}

	Effect RandomEffect()
	{
		int effect = Random.Range(0, 4); // Hardcoded because I only made 5 effects for now; ugly I know but its only proof of concept
		switch (effect)
		{
			case 0:
				return (new SelfHeal());
			case 1:
				return (new Burn());
			case 2:
				return (new Damage());
			case 3:
				return (new Empower());
			case 4:
				return (new Accellerate());
		}
		return (null);
	}
}
