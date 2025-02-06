using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DailyBonusData", menuName = "ScriptableObjects/DailyBonusData", order = 1)]
public class DailyBonusData : ScriptableObject
{
    public List<string> dailyBonusRewards; // List for daily bonus rewards

    private void OnEnable()
    {
        //dailyBonusRewards = new List<int> { 10, 50, 0, 80, 100, 150, 200 };
        //dailyBonusRewards = new List<string> { "10 Coins", "50 Coins", "0 Coins", "80 Coins", "100 Coins", "150 Coins", "200 Coins" };
        dailyBonusRewards = new List<string>
        {
            "10 Coins",
            "50 Coins",
            GetRandomReward(), //"0 Coins", or random reward
            "80 Coins",
            "100 Coins",
            "150 Coins",
            "200 Coins"
        };
    }

    private string GetRandomReward()
    {
        int randomReward = Random.Range(50, 100); // Random number between 50 and 200
        return randomReward + " Coins";
    }
}



