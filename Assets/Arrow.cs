using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Transform target;
    public GameObject Particles;
    public float Range;
    public float speed = 0.5f;
    // Update is called once per frame
    private void Start()
    {
        transform.SetParent(target);
        Vector2 dir = target.position-transform.position;
        dir.Normalize();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        Destroy(gameObject, 0.15f);
    }
    void FixedUpdate()
    {
        if(Vector2.Distance(transform.position, target.position) <= Range)
        {
            Destroy(gameObject);
        }
        transform.Translate(-transform.up* speed);
    }

    private void OnDisable()
    {
        Instantiate(Particles, transform.position, Quaternion.identity).transform.SetParent(target);
    }
}
