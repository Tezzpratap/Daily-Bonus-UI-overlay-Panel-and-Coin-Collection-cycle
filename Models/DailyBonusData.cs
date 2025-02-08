using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DailyBonusData", menuName = "ScriptableObjects/DailyBonusData", order = 1)]
public class DailyBonusData : ScriptableObject
{
    //public List<string> dailyBonusRewards; // List for daily bonus rewards
    public List<int> dailyBonusRewards; // List for daily bonus rewards

    private void OnEnable()
    {
        //dailyBonusRewards = new List<int> { 10, 50, 0, 80, 100, 150, 200 };
        dailyBonusRewards = new List<int>
        {
            10, //Day 1 reward
            50, //Day 2 reward
            0, //GetRandomReward3rdDay(), // Random coins or gift for day 3
            90, //Day 4 reward
            100, //Day 5 reward
            150, //Day 6 reward
            200,//GetRandomReward7thDay(),  // Random coins or gift for day 7
        };
        //dailyBonusRewards = new List<string> { "10 Coins", "50 Coins", "0 Coins", "80 Coins", "100 Coins", "150 Coins", "200 Coins" };
        /*dailyBonusRewards = new List<string>
        {
            "10 Coins",
            "50 Coins",
            GetRandomReward3rdDay(), //Random coins or gift
            "90 Coins",
            "100 Coins",
            "150 Coins",
            GetRandomReward7thDay(),  // Random coins or gift
        };*/
    }

    private string GetRandomReward3rdDay()
    {
        int randomRewardForDay3 = Random.Range(50, 100); // Random number between 50 and 200
        return randomRewardForDay3 + " Coins";
    }

    private string GetRandomReward7thDay(){
        int randomRewardForDay7 = Random.Range(150, 250); // Random number between 150 and 250
        return randomRewardForDay7 + " Coins";
    }
}



