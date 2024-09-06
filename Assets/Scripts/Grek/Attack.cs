using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public bool isAttacking = false;
    public Grek grek;
    public float delay = 0.2f;
    public GameObject Arrow;
    public AudioSource ShotAudi;
    public Transform pivotShot;
    public Transform target;
    public void attack()
    {
        isAttacking = true;
        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        grek.anim.SetTrigger("attack");
        if(pivotShot.position.x > 0)
        {
            grek.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            grek.transform.localScale = new Vector3(1, 1, 1);
        }
        ShotAudi.Play();
        yield return new WaitForSeconds(delay);
        GameObject Object = Instantiate(Arrow, pivotShot.position, Quaternion.identity);
        Object.GetComponent<Arrow>().target = target;
        isAttacking = false;
    }
}
