// WalletData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "WalletData", menuName = "ScriptableObjects/WalletData")]
public class WalletData : ScriptableObject
{
    public int initialCoins = 0;
    public string currencySymbol = "Coins";
}
