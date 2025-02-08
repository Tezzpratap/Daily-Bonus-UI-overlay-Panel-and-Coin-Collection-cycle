using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class DailyBonusManager : MonoBehaviour
{
    public List<Button> dayRewardButtons;   // List for daily bonus buttons
    public List<Text> dayRewardTexts;    // List for daily bonus reward texts
    public DailyBonusData dailyBonusData;   // Reference to daily bonus data scriptable object
    
    private int lastClaimedDay; // Last claimed day
    private DateTime lastClaimedDate;   // Last claimed date
    public ProgressionData progressionData; // ScriptableObject reference
    private void Start()
    {
        LoadDailyBonusProgress(); // Load saved progress

        if (dailyBonusData == null || dailyBonusData.dailyBonusRewards.Count == 0)
        {
            Debug.LogError("DailyBonusData is missing or not configured properly!");    // Check if dailyBonusData is missing or not configured properly
            return;
        }

        SetupDailyBonusListeners(); // Setup listeners for daily bonus buttons
        UpdateDailyBonusLabels();       // Update daily bonus reward texts
        UpdateButtonStates();       // Update button states
    }

    private void SetupDailyBonusListeners() // Method to setup listeners for daily bonus buttons
    {
        for (int i = 0; i < dayRewardButtons.Count; i++)
        {
            int day = i + 1; // Making it Day 1, Day 2, etc.
            dayRewardButtons[i].onClick.AddListener(() => TryCollectDailyReward(day));  // Add listener to collect daily reward
        }
    }

    private void TryCollectDailyReward(int day) // Method to try collecting daily reward
    {
        if (!CanCollectReward(day))
        {
            Debug.Log("Cannot claim reward for Day " + day);    // Check if reward can be claimed
            return;
        }

        CollectDailyReward(day);
    }

    private bool CanCollectReward(int day)  // Method to check if reward can be claimed
    {
        DateTime today = DateTime.Today;    

        if (day != lastClaimedDay + 1) // Enforce linear claiming (Day 1 → Day 2)
        {
            Debug.Log("You must claim rewards in order. Next available: Day " + (lastClaimedDay + 1));  // Check if rewards are claimed in order
            return false;
        }

        if (lastClaimedDate == today) // Prevent multiple claims in one day
        {
            Debug.Log("You already claimed today's reward!");   // Check if reward is already claimed today
            return false;
        }

        return true;
    }

    private void CollectDailyReward(int day)    // Method to collect daily reward
    {
        Debug.Log($"Attempting to collect reward for Day {day}");   // Attempt to collect reward

        // Get the reward as an integer
        int rewardAmount = dailyBonusData.dailyBonusRewards[day - 1];   // Get reward amount from data

        Debug.Log($"Reward amount from data: {rewardAmount}");  // Log reward amount

        WalletManager.Instance.AddCoins(rewardAmount);  // Add reward to wallet

        lastClaimedDay = day;   // Update last claimed day
        lastClaimedDate = DateTime.Today;   // Update last claimed date

        SaveDailyBonusProgress();   // Save daily bonus progress
        UpdateButtonStates();   // Update button states
        UIEventSystem.Instance.TriggerRewardCollected(day);  // Trigger reward collected event

        Debug.Log($"Reward for Day {day} collected successfully!");  // Log successful collection
    }

    private void UpdateDailyBonusLabels()   // Method to update daily bonus reward texts
    {
        for (int i = 0; i < dayRewardTexts.Count; i++)
        {
            if (i < dailyBonusData.dailyBonusRewards.Count && dayRewardTexts[i] != null)    // Check if day reward text is not null and day is within the range of dailyBonusRewards list
            {
                dayRewardTexts[i].text = dailyBonusData.dailyBonusRewards[i] + " Coins";    // Update daily bonus reward text
            }
        }
    }

    private void UpdateButtonStates()   // Method to update button states
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
        progressionData.lastClaimedDay = PlayerPrefs.GetInt("LastClaimedDay", 0);
        progressionData.lastClaimedDate = PlayerPrefs.GetString("LastClaimedDate", "");
    }

    private void SaveDailyBonusProgress()   // Method to save daily bonus progress
    {
        PlayerPrefs.SetInt("LastClaimedDay", lastClaimedDay);   // Save last claimed day
        PlayerPrefs.SetString("LastClaimedDate", lastClaimedDate.ToString());   // Save last claimed date
        PlayerPrefs.Save();
    }
}





/*   below is the last stable code only for the DailyBonusManager.cs file edited last 07 feb 25  
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
        for (int i = 0; i < dayRewardButtons.Count; i++) // i is the day number from 0 to 6
        {
            int day = i; // Assign i to a new variable day
            dayRewardButtons[i].onClick.AddListener(() => CollectDailyReward(day)); // Add listener to collect daily reward
        }
    }

    private void UpdateDailyBonusLabels() // Method to update daily bonus reward texts
    {
        for (int i = 0; i < dayRewardButtons.Count; i++) // i is the day number from 0 to 6
        {
            if (i < dailyBonusData.dailyBonusRewards.Count && dayRewardTexts[i] != null) // Check if day reward text is not null and day is within the range of dailyBonusRewards list
            {
                dayRewardTexts[i].text = dailyBonusData.dailyBonusRewards[i] + " Coins"; // Update daily bonus reward text
            }
        }
    }




    private void CollectDailyReward(int day) // Method to collect daily reward
    {
        if (!isDayCollected[day])
        {
            // Extract numeric value from the string and convert to int
            string rewardString = dailyBonusData.dailyBonusRewards[day]; // Extracts the reward string from the list
            int rewardAmount = int.Parse(rewardString.Split(' ')[0]); // Extracts the number before "Coins"

            WalletManager.Instance.AddCoins(rewardAmount); // Add reward to wallet
            isDayCollected[day] = true; // Mark day as collected

            if (dayRewardButtons[day] != null)
                dayRewardButtons[day].interactable = false; // Disable button after collecting reward

            UIEventSystem.Instance.TriggerRewardCollected(day); // Trigger reward collected event
        }
    }
}

*/
