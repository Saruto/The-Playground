using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowrunCalculator : MonoBehaviour {

	[SerializeField] int GunSkillDice;
	[SerializeField] int GunPower;
	public enum DamageCode { NoDamage, L, M, S, D }
	[SerializeField] DamageCode GunDamageCode;
	[SerializeField] int GunTargetNumber;


	[SerializeField] int TargetBallisticArmor;
	[SerializeField] int TargetBodyDice;



    // Start is called before the first frame update
    void Start() {
		// --- Number of Attacker Successes --- //
		// Calculate the average number of dice that come up a success.
		float chanceForOneDieToBeSuccessful = GetChanceForSingleDieToBeatTargetNumber(GunTargetNumber);
		float chanceForOneSuccess = 1 - Mathf.Pow(((1 - chanceForOneDieToBeSuccessful)), GunSkillDice);
		float averageNumberOfAttackerSuccesses = chanceForOneDieToBeSuccessful * GunSkillDice;
		Debug.Log("Attacker Chance to Get at least 1 Success against a Target Number of " + GunTargetNumber + ": " + chanceForOneSuccess);
		Debug.Log("Average Number of Attacker Successes against a Target Number of " + GunTargetNumber + ": " + averageNumberOfAttackerSuccesses);


		// --- Number of Defender Successes --- //
		// Subtract ballistic armor from power.
		int defenderTargetNumber = GunPower - TargetBallisticArmor;
		float averageNumberOfDefenderSuccesses = GetChanceForSingleDieToBeatTargetNumber(defenderTargetNumber) * TargetBodyDice;
		Debug.Log("Average Number of Defender Successes against a Target Number of " + defenderTargetNumber + " (GunPower minus Armor): " + averageNumberOfDefenderSuccesses);

		
		// --- Calculate Damage Results --- //
		float averageSuccesses = averageNumberOfAttackerSuccesses - averageNumberOfDefenderSuccesses;
		Debug.Log("Average Successes Total (attacker == positive): " + averageSuccesses);

		DamageCode finalDamageCode = GunDamageCode;
		while(Mathf.Abs(averageSuccesses) > 2) {
			// Stage damage up
			if(averageSuccesses > 0) {
				finalDamageCode = (finalDamageCode == DamageCode.D ? DamageCode.D : finalDamageCode + 1);
				averageSuccesses -= 2;
			}
			// Stage damage down.
			else {
				finalDamageCode = (finalDamageCode == DamageCode.NoDamage ? DamageCode.NoDamage : finalDamageCode - 1);
				averageSuccesses += 2;
			}
		}

		Debug.Log("Average Damage on Hit: " + finalDamageCode);
	}

    // Update is called once per frame
    void Update()
    {
        
    }


	// Returns the chance that a single die will beat a particular target number.
	float GetChanceForSingleDieToBeatTargetNumber(int targetNumber) {
		// If the target number < 6, figure out the number of sixes we need to roll on a single dice to get a success
		int numberOfSixesNeeded = targetNumber / 6;
		// Find the target number of the last roll on the dice we need.
		int modifiedTargetNumber = targetNumber - numberOfSixesNeeded * 6;
		// A value of 0 is the same as a value of 1 for the purpose of the calculation here.
		modifiedTargetNumber = (modifiedTargetNumber == 0 ? 1 : modifiedTargetNumber);
		// The chance to get the last roll correct.
		float chanceToMakeLastRoll = (6 - (modifiedTargetNumber - 1)) / 6f;

        // Calculate total chance for a single die to beat the target number.
		if(numberOfSixesNeeded != 0) {
			return chanceToMakeLastRoll * numberOfSixesNeeded * (1f / 6f);
		} else {
			return chanceToMakeLastRoll;
		}
	}
}
