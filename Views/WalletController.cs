using UnityEngine;

public class WalletController : MonoBehaviour
{
    private WalletModel model;
    private WalletView view;

    private void Start()
    {
        model = new WalletModel();
        view = FindObjectOfType<WalletView>();
        view.UpdateWalletDisplay(model.totalCoins);
    }

    public void AddCoins(int amount)
    {
        model.AddCoins(amount);
        view.UpdateWalletDisplay(model.totalCoins);
    }
}
