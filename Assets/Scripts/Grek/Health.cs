using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Health : MonoBehaviour
{
    [HideInInspector]
    public bool isDamaged = true;
    public float delay = 0.2f;
    public float DamagedDelay=1;
    public SimpleFlash flash;
    [Space]
    public int hp;
    public Image[] images;
    public Sprite PlusHp;
    public Sprite MinusHp;
    public Grek grek;
    public GameObject particles;
    public GameObject EmpyTH;
    public float durate = 1;
    [SerializeField] Ease easeType;
    Vector2[] posititons;
    VibrateManager vibrateManager;
    // Start is called before the first frame update
    private void Start()
    {
        vibrateManager = FindAnyObjectByType<VibrateManager>();
        Invoke(nameof(start2), 0.1f);
    }
    public void start2()
    {
        posititons = new Vector2[images.Length];
        for (int i = 0; i < posititons.Length; i++)
        {
            posititons[i] = images[i].transform.position;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Damage()
    {
        hp -= 1;
        vibrateManager.PlayVibrate();
        GameManager.singenton.SadBG();
        CheckHp();
        StartCoroutine(DamageFlash());
        UpdateHP();
        grek.audioEffect.damage.Play();
    }

    public IEnumerator DamageFlash()
    {
        isDamaged = false;
        grek.anim.SetBool("sad", true);
        for(int i =0;i< DamagedDelay; i++)
        {
            flash.Flash();
            yield return new WaitForSeconds(delay);
        }

        isDamaged = true;
        yield return new WaitForSeconds(5);
        grek.anim.SetBool("sad", false);
    }

    public void UpdateHP()
    {
        for(int i = 0;i< images.Length; i++)
        {
            if(hp > i)
            {
                images[i].sprite = PlusHp;
            }
            else
            {
                images[i].sprite = MinusHp;
            }
        }
    }

    void CheckHp()
    {
        if (hp <= 0)
        {
            GameManager.singenton.GameOver();
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
            vibrateManager.PlayVibration(0.15f);
        }
    }

    public void Treatment(Vector2 pos)
    {
        hp += 1;
        Effect(pos);
        grek.audioEffect.podborHP.Play();
    }

    public void Effect(Vector2 pos)
    {
        int g = Mathf.Clamp(hp,0, posititons.Length);
        GameObject empty = Instantiate(EmpyTH, pos, Quaternion.identity);
        empty.transform.SetParent(Camera.main.transform);
        empty.transform.DOLocalMove(posititons[g-1], durate)
        .SetEase(easeType)
        .OnComplete(() => {
            //executes whenever coin reach target position
            Destroy(empty);
            images[g - 1].sprite = PlusHp;
        });
    }
}
