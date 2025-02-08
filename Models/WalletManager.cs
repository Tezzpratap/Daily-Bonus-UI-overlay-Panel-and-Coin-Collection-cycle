using UnityEngine;
using UnityEngine.UI;

public class WalletManager : MonoBehaviour
{
    public WalletData walletData; // ScriptableObject reference

    public static WalletManager Instance { get; private set; } // Singleton instance
    
    [SerializeField] private Text walletText; // Reference to wallet text UI element
    private int totalCoins; // Total coins collected by the player

    private void Awake()
    {
        if (Instance == null)
            Instance = this; // Set the singleton instance
        else
            Destroy(gameObject); // Destroy duplicate instances
    }

    private void Start()
    {   
        totalCoins = walletData.initialCoins; // Initialize total coins with initial coins
        UpdateWalletText(); // Update wallet text on start
    }

    public void AddCoins(int amount) // Method to add coins to the wallet
    {
        totalCoins += amount; // Add coins to the total
        UpdateWalletText(); // Update wallet text
    }

    private void UpdateWalletText() // Method to update wallet text
    {
        if (walletText != null)
        {
            walletText.text = totalCoins + " Coins"; // Update wallet text with total coins
        }
    }
}

