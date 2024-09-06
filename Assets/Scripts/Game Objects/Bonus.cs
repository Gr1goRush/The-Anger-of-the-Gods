using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public enum TypeBonus
    {
        Money,
        LightBolt,
        Hp
    }
    public TypeBonus type;
    public int univers;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (type == TypeBonus.Hp)
        {
            GameManager.singenton.Grek.gameObject.GetComponent<Health>().Treatment(transform.position);
        }
        else if (type == TypeBonus.LightBolt)
        {
            GameManager.singenton.Grek.gameObject.GetComponent<Attack>().attack();
            GameManager.singenton.LightBolt[univers] = true;
            FindAnyObjectByType<VibrateManager>().PlayVibrate();
        }
        else if (type == TypeBonus.Money)
        {
            GameManager.singenton.AddMoney(transform.position,univers);
        }

        Destroy(gameObject);
    }
}
