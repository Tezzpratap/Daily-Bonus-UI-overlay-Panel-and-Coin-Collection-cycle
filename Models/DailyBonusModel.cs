using System;
using System.Collections.Generic;
using UnityEngine;

public class DailyBonusModel
{
    public List<int> dailyRewards;
    public int lastClaimedDay;
    public DateTime lastClaimedDate;

    public DailyBonusModel(DailyBonusData data)
    {
        dailyRewards = new List<int>(data.dailyBonusRewards);
        lastClaimedDay = PlayerPrefs.GetInt("LastClaimedDay", -1);
        lastClaimedDate = DateTime.Parse(PlayerPrefs.GetString("LastClaimedDate", DateTime.MinValue.ToString()));
    }

    public bool CanCollectReward(int day)
    {
        DateTime today = DateTime.Today;
        return (day == lastClaimedDay + 1) && (lastClaimedDate != today);
    }

    public void CollectReward(int day)
    {
        lastClaimedDay = day;
        lastClaimedDate = DateTime.Today;
        PlayerPrefs.SetInt("LastClaimedDay", lastClaimedDay);
        PlayerPrefs.SetString("LastClaimedDate", lastClaimedDate.ToString());
        PlayerPrefs.Save();
    }

    public int GetRewardAmount(int day)
    {
        return (day >= 0 && day < dailyRewards.Count) ? dailyRewards[day] : 0;
    }
}
