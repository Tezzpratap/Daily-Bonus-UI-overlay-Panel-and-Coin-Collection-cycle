using System;

public class UIEventSystem
{
    public static UIEventSystem Instance { get; } = new UIEventSystem();

    public event Action<int> OnRewardCollected;
    public event Action<string> OnPanelOpened;
    public event Action<string> OnPanelClosed;

    private UIEventSystem() { }

    public void TriggerRewardCollected(int day)
    {
        OnRewardCollected?.Invoke(day);
    }

    public void TriggerPanelOpened(string panelName)
    {
        OnPanelOpened?.Invoke(panelName);
    }

    public void TriggerPanelClosed(string panelName)
    {
        OnPanelClosed?.Invoke(panelName);
    }
}
