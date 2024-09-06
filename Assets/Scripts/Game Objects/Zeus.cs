using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zeus : MonoBehaviour
{
    // Start is called before the first frame update\
    [Range(1,30)]
    public float RangeMin;
    [Range(1, 60)]
    public float RangeMax;
    public Animator anim;
    public GameObject Arrow;
    public Transform target;
    public bool isAttacking = true;
    public AudioSource LightBoltAudi;
    void Start()
    {
        Invoke(nameof(Attack), Random.Range(RangeMin, RangeMax));
    }

    public void Attack()
    {
        if (!isAttacking || !target) return;

        anim.SetTrigger("attack");
        Vector2 pos = target.position;
        pos.y -= 0.8f;
        Instantiate(Arrow, pos, Quaternion.identity);
        Invoke(nameof(Attack), Random.Range(RangeMin, RangeMax));
        float range = Random.Range(0.9f, 1.1f);
        LightBoltAudi.pitch = range;
        LightBoltAudi.Play();
    }
}

