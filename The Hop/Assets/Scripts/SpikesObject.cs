using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.transform.tag == "Player")
        {
            GameController.gameController.GameOver();
        }
    }
}
