using UnityEngine;
using UnityEngine.UI;

public class DailyBonusManager : MonoBehaviour
{
    public Button[] dailyBonusDayButtons;
    public Text[] dailyBonusTexts;
    public DailyBonusData dailyBonusData;

    private bool[] isDayCollected;

    private void Start()
    {
        if (dailyBonusData == null || dailyBonusData.dailyBonusRewards.Length == 0)
        {
            Debug.LogError("DailyBonusData is missing or not configured properly!");
            return;
        }

        isDayCollected = new bool[dailyBonusData.dailyBonusRewards.Length];
        SetupDailyBonusListeners();
        UpdateDailyBonusLabels();
    }

    private void SetupDailyBonusListeners()
    {
        for (int i = 0; i < dailyBonusDayButtons.Length; i++)
        {
            int dayIndex = i;
            dailyBonusDayButtons[i].onClick.AddListener(() => CollectDailyReward(dayIndex));
        }
    }

    private void CollectDailyReward(int dayIndex)
    {
        if (!isDayCollected[dayIndex])
        {
            WalletManager.Instance.AddCoins(dailyBonusData.dailyBonusRewards[dayIndex]);
            isDayCollected[dayIndex] = true;

            if (dailyBonusDayButtons[dayIndex] != null)
                dailyBonusDayButtons[dayIndex].interactable = false;

            UIEventSystem.Instance.TriggerRewardCollected(dayIndex);
        }
    }

    private void UpdateDailyBonusLabels()
    {
        for (int i = 0; i < dailyBonusDayButtons.Length; i++)
        {
            if (i < dailyBonusData.dailyBonusRewards.Length && dailyBonusTexts[i] != null)
            {
                dailyBonusTexts[i].text = $"{dailyBonusData.dailyBonusRewards[i]} Coins";
            }
        }
    }
}
