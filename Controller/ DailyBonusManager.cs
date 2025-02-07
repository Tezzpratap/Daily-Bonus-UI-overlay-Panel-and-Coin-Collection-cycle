using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class DailyBonusManager : MonoBehaviour
{
    public List<Button> dayRewardButtons;
    public List<Text> dayRewardTexts;
    public DailyBonusData dailyBonusData;
    
    private int lastClaimedDay;
    private DateTime lastClaimedDate;

    private void Start()
    {
        LoadDailyBonusProgress(); // Load saved progress

        if (dailyBonusData == null || dailyBonusData.dailyBonusRewards.Count == 0)
        {
            Debug.LogError("DailyBonusData is missing or not configured properly!");
            return;
        }

        SetupDailyBonusListeners();
        UpdateDailyBonusLabels();
        UpdateButtonStates();
    }

    private void SetupDailyBonusListeners()
    {
        for (int i = 0; i < dayRewardButtons.Count; i++)
        {
            int day = i + 1; // Making it Day 1, Day 2, etc.
            dayRewardButtons[i].onClick.AddListener(() => TryCollectDailyReward(day));
        }
    }

    private void TryCollectDailyReward(int day)
    {
        if (!CanCollectReward(day))
        {
            Debug.Log("Cannot claim reward for Day " + day);
            return;
        }

        CollectDailyReward(day);
    }

    private bool CanCollectReward(int day)
    {
        DateTime today = DateTime.Today;

        if (day != lastClaimedDay + 1) // Enforce linear claiming (Day 1 → Day 2)
        {
            Debug.Log("You must claim rewards in order. Next available: Day " + (lastClaimedDay + 1));
            return false;
        }

        if (lastClaimedDate == today) // Prevent multiple claims in one day
        {
            Debug.Log("You already claimed today's reward!");
            return false;
        }

        return true;
    }

    private void CollectDailyReward(int day)
    {
        Debug.Log($"Attempting to collect reward for Day {day}");

        // Get the reward as an integer
        int rewardAmount = dailyBonusData.dailyBonusRewards[day - 1];

        Debug.Log($"Reward amount from data: {rewardAmount}");

        WalletManager.Instance.AddCoins(rewardAmount);

        lastClaimedDay = day;
        lastClaimedDate = DateTime.Today;

        SaveDailyBonusProgress();
        UpdateButtonStates();
        UIEventSystem.Instance.TriggerRewardCollected(day);

        Debug.Log($"Reward for Day {day} collected successfully!");
    }

    private void UpdateDailyBonusLabels()
    {
        for (int i = 0; i < dayRewardTexts.Count; i++)
        {
            if (i < dailyBonusData.dailyBonusRewards.Count && dayRewardTexts[i] != null)
            {
                dayRewardTexts[i].text = dailyBonusData.dailyBonusRewards[i] + " Coins";
            }
        }
    }

    private void UpdateButtonStates()
    {
        for (int i = 0; i < dayRewardButtons.Count; i++)
        {
            int day = i + 1;
            if (day <= lastClaimedDay)
                dayRewardButtons[i].interactable = false; // Already claimed
            else if (day == lastClaimedDay + 1)
                dayRewardButtons[i].interactable = true; // Next available reward
            else
                dayRewardButtons[i].interactable = false; // Future days are locked
        }
    }

    private void LoadDailyBonusProgress()
    {
        lastClaimedDay = PlayerPrefs.GetInt("LastClaimedDay", 0);
        string savedDate = PlayerPrefs.GetString("LastClaimedDate", "");

        if (!string.IsNullOrEmpty(savedDate))
        {
            lastClaimedDate = DateTime.Parse(savedDate);
        }
        else
        {
            lastClaimedDate = DateTime.MinValue;
        }
    }

    private void SaveDailyBonusProgress()
    {
        PlayerPrefs.SetInt("LastClaimedDay", lastClaimedDay);
        PlayerPrefs.SetString("LastClaimedDate", lastClaimedDate.ToString());
        PlayerPrefs.Save();
    }
}
