using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject dailyBonusPanel;  // Reference to daily bonus panel
    public GameObject mainMenuPanel;    // Reference to main menu panel
    public UIData uiData; // ScriptableObject reference
    public Button openDailyBonusButton; // Reference to open daily bonus button
    public Button closeDailyBonusButton; // Reference to close daily bonus button

    private void Start()
    {
        AssignButtonListeners(); // Assign button listeners
        UIEventSystem.Instance.OnPanelOpened += ShowPanel; // Subscribe to panel opened event
        UIEventSystem.Instance.OnPanelClosed += HidePanel;  // Subscribe to panel closed event
    }

    private void ApplyUIConfig()
    {
        if (dailyBonusPanel != null)
        {
            Image bg = dailyBonusPanel.GetComponent<Image>();
            if (bg != null) bg.color = uiData.panelBackgroundColor;
        }
    }

    private void AssignButtonListeners()
    {
        if (openDailyBonusButton != null)
        {  
            openDailyBonusButton.onClick.AddListener(() =>
            {
                Debug.Log("Open Daily Bonus Button Clicked!");
                UIEventSystem.Instance.TriggerPanelOpened("DailyBonus");
            });
        }

        if (closeDailyBonusButton != null)
        {
            closeDailyBonusButton.onClick.AddListener(() =>
            {
                Debug.Log("Close Daily Bonus Button Clicked!");
                UIEventSystem.Instance.TriggerPanelClosed("DailyBonus");
            });
        }
    }


    private void ShowPanel(string panelName) // Mehtod to show panel
    {
        if (panelName == "DailyBonus" && dailyBonusPanel != null)
        {
            dailyBonusPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void HidePanel(string panelName) // Method to hide panel
    {
        if (panelName == "DailyBonus" && dailyBonusPanel != null)
        {
            dailyBonusPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    private void OnDestroy() // Unsubscribe from events
    {
        UIEventSystem.Instance.OnPanelOpened -= ShowPanel;
        UIEventSystem.Instance.OnPanelClosed -= HidePanel;
    }
}
