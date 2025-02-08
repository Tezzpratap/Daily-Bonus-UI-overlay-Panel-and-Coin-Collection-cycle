using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DailyRewardData", menuName = "ScriptableObjects/DailyRewardData", order = 1)]
public class DailyBonusData : ScriptableObject
{
    [System.Serializable]
    public class DailyBonusReward
    {
        public int day; // Day number
        public int rewardAmount; // Reward amount in coins
        public bool isRandomReward; // If true, generate a random reward this is the the 3rd or 7th day as there was a option for gift, so i choose a random amount of coin
        public int minRandomValue; // Minimum value for random reward
        public int maxRandomValue; // Maximum value for random reward
    }

    public List<DailyBonusReward> dailyBonusRewards = new List<DailyBonusReward>();

    /// Gets the reward for a specific day.
    public int GetRewardForDay(int day)
    {
        DailyBonusReward rewardData = dailyBonusRewards.Find(r => r.day == day);
        if (rewardData != null)
        {
            if (rewardData.isRandomReward)
            {
                return Random.Range(rewardData.minRandomValue, rewardData.maxRandomValue + 1);
            }
            return rewardData.rewardAmount;
        }
        return 0; // Default reward if not found
    }
}
