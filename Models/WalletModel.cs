using UnityEngine;

public class WalletModel
{
    public int totalCoins;

    public WalletModel()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();
    }
}
