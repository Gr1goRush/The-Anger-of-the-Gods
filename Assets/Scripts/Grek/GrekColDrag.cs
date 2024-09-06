using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrekColDrag : MonoBehaviour
{
    public bool isCol;

    private void OnCollisionStay2D(Collision2D collision)
    {
        isCol = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isCol = false;
    }
}
