using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    public bool Change;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] audi = GameObject.FindGameObjectsWithTag("BGMusic");
        if (audi.Length ==1)
        {
            if(!Change)
                DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Change)
            {
                Destroy(audi[0]);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
