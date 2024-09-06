using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public Transform Zeus;
    public float offset;
    public float maxY;
    public float speed = 0.6f;
    public float speedZeus = 0.5f;
    public Transform paralax;
    Vector2 startPosParll;
    public float xOffset=1;
    public bool isAutoMax;
    public float max;
    public float maxZeus;
    // Start is called before the first frame update
    void Start()
    {
        maxY = transform.position.y;
        startPosParll = paralax.position;
        xOffset = GameManager.singenton.durationLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if (!target) return;
        if (isAutoMax) {
            max = 19.7f + (transform.position.y * xOffset);
            maxZeus = 16.5f + (transform.position.y * xOffset);
        }
        if (target.position.y + offset > maxY)
        {
            maxY = target.position.y + offset;
            maxY = Mathf.Clamp(maxY, 0, max);
        }
        float zeusMax = Mathf.Clamp(maxY, 0, maxZeus);
        Zeus.position = Vector3.Lerp(Zeus.position, new Vector3(0, zeusMax, 0), speedZeus);
        transform.position = Vector3.Lerp(transform.position, new Vector3(0, maxY,-10), speed);
        paralax.position = startPosParll + (Vector2)(transform.position * xOffset);
    }
}
