using UnityEngine;
using VContainer;

public class PlayerBehaviour : MonoBehaviour
{
    private PlayerController playerController;

    [Inject]
    public void Construct(PlayerController controller)
    {
        playerController = controller;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerController.MovePlayer();
        }
    }
}
