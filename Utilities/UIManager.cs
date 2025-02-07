using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject dailyBonusPanel;
    public Button openDailyBonusButton;
    public Button closeDailyBonusButton;

    private void Start()
    {
        AssignButtonListeners();
        UIEventSystem.Instance.OnPanelOpened += ShowPanel;
        UIEventSystem.Instance.OnPanelClosed += HidePanel;
    }

    private void AssignButtonListeners()
    {
        openDailyBonusButton.onClick.AddListener(() => UIEventSystem.Instance.TriggerPanelOpened("DailyBonus"));
        closeDailyBonusButton.onClick.AddListener(() => UIEventSystem.Instance.TriggerPanelClosed("DailyBonus"));
    }

    private void ShowPanel(string panelName)
    {
        if (panelName == "DailyBonus")
        {
            dailyBonusPanel.SetActive(true);
        }
    }

    private void HidePanel(string panelName)
    {
        if (panelName == "DailyBonus")
        {
            dailyBonusPanel.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        UIEventSystem.Instance.OnPanelOpened -= ShowPanel;
        UIEventSystem.Instance.OnPanelClosed -= HidePanel;
    }
}
