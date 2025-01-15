using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    // Panels
    public GameObject dailyBonusPanel;      // Panel for daily bonus rewards
    public GameObject mainMenuPanel;       // Panel for the main menu

    // Buttons
    public Button dailyBonusButton;        // Button to open the daily bonus panel
    public Button closeDailyBonusButton;   // Button to close the daily bonus panel
    public Button[] dailyBonusDayButtons;  // Array of buttons for daily rewards
    public Text[] dailyBonusTexts;         // Texts to display daily rewards Values

    // ScriptableObject Reference
    public DailyBonusData dailyBonusData;  // Reference to the ScriptableObject

    // Wallet
    public Text walletText;                // Wallet display text
    private int totalCoins = 0;            // Total coins in the wallet
    private bool[] isDayCollected;         // Tracks if a day's reward is collected

    // Events
    public event Action<int> OnRewardCollected;  // Event triggered when a reward is collected
    public event Action OnPanelOpened;          // Event triggered when the Daily Bonus panel is opened
    public event Action OnPanelClosed;          // Event triggered when the Daily Bonus panel is closed

    private void Start()
    {
        if (dailyBonusData == null || dailyBonusData.dailyBonusRewards.Length == 0)
        {
            Debug.LogError("DailyBonusData is missing or not configured properly!");
            return;
        }

        // Ensure the daily bonus panel is inactive at the start
        dailyBonusPanel.SetActive(false);

        // Initialize collected rewards tracking
        isDayCollected = new bool[dailyBonusData.dailyBonusRewards.Length];

        // Set up listeners
        SetupDailyBonusListeners();

        // Initialize UI
        UpdateWalletText();
        UpdateDailyBonusLabels();

        // Assign button listeners
        if (dailyBonusButton != null)
            dailyBonusButton.onClick.AddListener(() => OnPanelOpened?.Invoke());

        if (closeDailyBonusButton != null)
            closeDailyBonusButton.onClick.AddListener(() => OnPanelClosed?.Invoke());

        // Hook events
        OnPanelOpened += OpenDailyBonusPanel;
        OnPanelClosed += CloseDailyBonusPanel;
    }

    private void SetupDailyBonusListeners()
    {
        for (int i = 0; i < dailyBonusDayButtons.Length; i++)
        {
            int dayIndex = i; // Cache index for the listener
            dailyBonusDayButtons[i].onClick.AddListener(() => OnRewardCollected?.Invoke(dayIndex));
        }

        // Hook reward collection event
        OnRewardCollected += CollectDailyReward;
    }

    private void OpenDailyBonusPanel()
    {
        if (dailyBonusPanel == null)
        {
            Debug.LogError("Daily Bonus Panel is not assigned!");
            return;
        }

        dailyBonusPanel.SetActive(true);
        Time.timeScale = 0; // Optional: Pause the game
        Debug.Log("Daily Bonus Panel is now visible.");
    }

    private void CloseDailyBonusPanel()
    {
        if (dailyBonusPanel == null)
        {
            Debug.LogError("Daily Bonus Panel is not assigned!");
            return;
        }

        dailyBonusPanel.SetActive(false);
        Time.timeScale = 1; // Resume the game
        Debug.Log("Daily Bonus Panel is now hidden.");
    }

    private void CollectDailyReward(int dayIndex)
    {
        if (!isDayCollected[dayIndex])
        {
            totalCoins += dailyBonusData.dailyBonusRewards[dayIndex]; // Add reward to wallet
            isDayCollected[dayIndex] = true;                          // Mark the day as collected
            UpdateWalletText();

            // Disable the button for the day
            if (dailyBonusDayButtons[dayIndex] != null)
                dailyBonusDayButtons[dayIndex].interactable = false;

            Debug.Log($"Reward for Day {dayIndex + 1} collected: {dailyBonusData.dailyBonusRewards[dayIndex]} Coins");
        }
        else
        {
            Debug.LogWarning($"Reward for Day {dayIndex + 1} already collected.");
        }
    }

    private void UpdateWalletText()
    {
        if (walletText != null)
        {
            walletText.text = $"{totalCoins} Coins";
            Debug.Log($"Wallet updated: {totalCoins} Coins");
        }
        else
        {
            Debug.LogError("Wallet Text is not assigned!");
        }
    }

    private void UpdateDailyBonusLabels()
    {
        Debug.Log("Updating Daily Bonus Labels...");
        for (int i = 0; i < dailyBonusDayButtons.Length; i++)
        {
            if (i >= dailyBonusData.dailyBonusRewards.Length)
            {
                Debug.LogWarning($"No reward data for button {i}.");
                continue;
            }

            // Update text on buttons
            if (dailyBonusTexts[i] != null)
            {
                dailyBonusTexts[i].text = $"{dailyBonusData.dailyBonusRewards[i]} Coins";
                Debug.Log($"Day {i + 1}: {dailyBonusData.dailyBonusRewards[i]} Coins");
            }
            else
            {
                Debug.LogWarning($"dailyBonusTexts[{i}] is null!");
            }
        }
    }
}
