using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.singenton.Winner();
    }
}
