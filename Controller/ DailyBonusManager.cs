using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DailyBonusManager : MonoBehaviour
{
    public List<Button> dayRewardButtons;  // List for daily bonus buttons
    public List<Text> dayRewardTexts;  // List for daily bonus reward texts
    public DailyBonusData dailyBonusData; // Reference to daily bonus data scriptable object

    private List<bool> isDayCollected; // List to track collected days

    private void Start()
    {
        if (dailyBonusData == null || dailyBonusData.dailyBonusRewards.Count == 0) // Check if dailyBonusData is missing or not configured properly
        {
            Debug.LogError("DailyBonusData is missing or not configured properly!");
            return;
        }

        isDayCollected = new List<bool>(new bool[dailyBonusData.dailyBonusRewards.Count]);  // Initialize isDayCollected list with false values
        SetupDailyBonusListeners(); 
        UpdateDailyBonusLabels();
    }

    private void SetupDailyBonusListeners() // Method to setup listeners for daily bonus buttons
    {
        for (int i = 0; i < dayRewardButtons.Count; i++)
        {
            int day = i;
            dayRewardButtons[i].onClick.AddListener(() => CollectDailyReward(day));
        }
    }

    private void CollectDailyReward(int day) // Method to collect daily reward
    {
        if (!isDayCollected[day])
        {
            // Extract numeric value from the string and convert to int
            string rewardString = dailyBonusData.dailyBonusRewards[day];
            int rewardAmount = int.Parse(rewardString.Split(' ')[0]); // Extracts the number before "Coins"

            WalletManager.Instance.AddCoins(rewardAmount);
            isDayCollected[day] = true;

            if (dayRewardButtons[day] != null)
                dayRewardButtons[day].interactable = false;

            UIEventSystem.Instance.TriggerRewardCollected(day); // Trigger reward collected event
    }
    }


    private void UpdateDailyBonusLabels() // Method to update daily bonus reward texts
    {
        for (int i = 0; i < dayRewardButtons.Count; i++)
        {
            if (i < dailyBonusData.dailyBonusRewards.Count && dayRewardTexts[i] != null)
            {
                dayRewardTexts[i].text = dailyBonusData.dailyBonusRewards[i] + " Coins";
            }
        }
    }
}
