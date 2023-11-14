using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private Transform platform;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    private bool toRight = true;
    private float speed = 2f;
    void FixedUpdate()
    {
        if (toRight)
        {
            if (Vector2.Distance(platform.position, right.position) < 0.5f)
            {
                toRight = false;
            }
            else platform.position = Vector2.MoveTowards(platform.position, right.position, speed * Time.deltaTime);
        }
        else
        {
            if (Vector2.Distance(platform.position, left.position) < 0.5f)
            {
                toRight = true;
            }
            else platform.position = Vector2.MoveTowards(platform.position, left.position, speed * Time.deltaTime);
        }
    }
}
