using System;

public class UIEventSystem
{
    public static UIEventSystem Instance { get; } = new UIEventSystem();

    // Events
    public event Action<int> OnRewardCollected;
    public event Action<string> OnPanelOpened;
    public event Action<string> OnPanelClosed;

    private UIEventSystem() { }

    public void TriggerRewardCollected(int dayIndex)
    {
        OnRewardCollected?.Invoke(dayIndex);
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
