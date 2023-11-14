using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.transform.tag == "Player")
        {
            animator.SetBool("isDead", true);
            PlayerManager.onCoinGrab?.Invoke();
        }
    }

    private void OnGrabDestroyCoin()
    {
        Destroy(gameObject);
    }

}
