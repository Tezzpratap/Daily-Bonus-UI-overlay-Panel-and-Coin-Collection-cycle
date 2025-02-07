using UnityEngine;

public class DailyBonusController : MonoBehaviour
{
    private DailyBonusModel model;
    private DailyBonusView view;
    private WalletController walletController;

    private void Start()
    {
        DailyBonusData data = Resources.Load<DailyBonusData>("DailyBonusData");
        model = new DailyBonusModel(data);
        view = FindObjectOfType<DailyBonusView>();
        walletController = FindObjectOfType<WalletController>();

        view.UpdateRewardDisplay(model);
        SetupButtonListeners();
    }

    private void SetupButtonListeners()
    {
        for (int i = 0; i < view.dayRewardButtons.Count; i++)
        {
            int day = i + 1;
            view.dayRewardButtons[i].onClick.AddListener(() => TryCollectDailyReward(day));
        }
    }

    private void TryCollectDailyReward(int day)
    {
        if (!model.CanCollectReward(day))
        {
            Debug.Log("Cannot claim reward for Day " + day);
            return;
        }

        int rewardAmount = model.GetRewardAmount(day);
        walletController.AddCoins(rewardAmount);
        model.CollectReward(day);
        view.UpdateRewardDisplay(model);
        UIEventSystem.Instance.TriggerRewardCollected(day);
    }
}
