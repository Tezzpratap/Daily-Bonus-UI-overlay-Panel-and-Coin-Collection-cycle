using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Panels
    public GameObject dailyBonusPanel; // Panel for daily bonus rewards
    public GameObject mainMenuPanel;  // Panel for the main menu

    // Buttons
    public Button dailyBonusButton;         // Button to open the daily bonus panel
    public Button closeDailyBonusButton;    // Button to close the daily bonus panel
    public Button[] dailyBonusDayButtons;   // Array of buttons for daily rewards
    public Text[] dailyBonusTexts;          // Texts to display daily rewards

    // ScriptableObject Reference
    public DailyBonusData dailyBonusData;   // Reference to the ScriptableObject

    // Wallet
    public Text walletText;                 // Wallet display text
    private int totalCoins = 0;             // Total coins in the wallet
    private bool[] isDayCollected;          // Tracks if a day's reward is collected

    private void Start()
    {
        Debug.Log("UIManager Start called.");
        Debug.Log($"Daily Bonus Data Assigned: {dailyBonusData != null}");

        if (dailyBonusData == null || dailyBonusData.dailyBonusRewards.Length == 0)
        {
            Debug.LogError("DailyBonusData is missing or not configured properly!");
            return;
        }

        // Ensure the daily bonus panel is inactive at the start
        dailyBonusPanel.SetActive(false);

        // Initialize collected rewards tracking
        isDayCollected = new bool[dailyBonusData.dailyBonusRewards.Length];
        SetupDailyBonusListeners();
        UpdateWalletText();
        UpdateDailyBonusLabels();
    }

    private void SetupDailyBonusListeners()
    {
        for (int i = 0; i < dailyBonusDayButtons.Length; i++)
        {
            int dayIndex = i; // Cache index for the listener
            dailyBonusDayButtons[i].onClick.AddListener(() => CollectDailyReward(dayIndex));
        }
    }

    private void CollectDailyReward(int dayIndex)
    {
        if (!isDayCollected[dayIndex])
        {
            totalCoins += dailyBonusData.dailyBonusRewards[dayIndex]; // Add reward to wallet
            isDayCollected[dayIndex] = true;                          // Mark the day as collected
            UpdateWalletText();

            // Disable the button for the day
            dailyBonusDayButtons[dayIndex].interactable = false;
        }
    }

    private void UpdateWalletText()
    {
        walletText.text = $"{totalCoins} Coins";
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
