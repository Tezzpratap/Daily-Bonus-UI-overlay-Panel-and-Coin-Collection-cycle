using UnityEngine;
using UnityEngine.UI;

public class WalletManager : MonoBehaviour
{
    public static WalletManager Instance { get; private set; } // Singleton instance
    
    [SerializeField] private Text walletText; // Reference to wallet text UI element
    private int totalCoins; // Total coins collected by the player

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateWalletText(); // Update wallet text on start
    }

    public void AddCoins(int amount) // Method to add coins to the wallet
    {
        totalCoins += amount;
        UpdateWalletText();
    }

    private void UpdateWalletText() // Method to update wallet text
    {
        if (walletText != null)
        {
            walletText.text = totalCoins + " Coins";
        }
    }
}
