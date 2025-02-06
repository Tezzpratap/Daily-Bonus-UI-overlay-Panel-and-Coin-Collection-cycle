using UnityEngine;
using UnityEngine.UI;

public class WalletManager : MonoBehaviour
{
    public static WalletManager Instance { get; private set; }

    [SerializeField] private Text walletText;
    private int totalCoins;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateWalletText();
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        UpdateWalletText();
    }

    private void UpdateWalletText()
    {
        if (walletText != null)
        {
            walletText.text = $"{totalCoins} Coins";
        }
    }
}
