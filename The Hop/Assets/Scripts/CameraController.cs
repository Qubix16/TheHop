using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private Transform death;
    void Update()
    {
        if (GameController.gameController.GetGameState() != GameState.GS_GAME_OVER)
        {
            if (player.position.y > this.transform.position.y)
            {
                this.transform.position = new Vector3(this.transform.position.x, player.position.y, this.transform.position.z);
                PlayerManager.onBestHeight?.Invoke((int)this.transform.position.y);
            }

            if (player.position.x <= left.position.x)
            {
                player.position = new Vector2(right.position.x, player.position.y);
            }
            else if (player.position.x >= right.position.x)
            {
                player.position = new Vector2(left.position.x, player.position.y);
            }

            if (player.position.y < death.position.y)
            {
                GameController.gameController.GameOver();
            }
        }

    }
}
