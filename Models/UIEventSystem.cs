using System;

public class UIEventSystem
{
    public static UIEventSystem Instance { get; } = new UIEventSystem(); // Singleton instance

    public event Action<int> OnRewardCollected; // Event to trigger when reward is collected
    public event Action<string> OnPanelOpened; // Event to trigger when panel is opened
    public event Action<string> OnPanelClosed; // Event to trigger when panel is closed

    private UIEventSystem() { } // Private constructor to prevent instantiation

    public void TriggerRewardCollected(int day) // Method to trigger reward collected event
    {
        OnRewardCollected?.Invoke(day); // Invoke the event
    }

    public void TriggerPanelOpened(string panelName) // Method to trigger panel opened event
    {
        OnPanelOpened?.Invoke(panelName); // Invoke the event
    }

    public void TriggerPanelClosed(string panelName) // Method to trigger panel closed event
    {
        OnPanelClosed?.Invoke(panelName); // Invoke the event
    }
}
