using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager singenton;
    public GameObject GameOverPanel;
    public GameObject WinnerPanel;
    [Header("SettingLevel")]
    public float durationLevel;
    [Header("BonusLevel")]
    public bool[] LightBolt;
    public int MoneyLevel;
    public int moneyPlus = 0;
    [Header("MoneyManager")]
    public GameObject PrefabMoneyAnim;
    public Transform targetMoney;
    Vector2 targetPos;
    [Range(0.5f,0.9f)]
    public float minDurate;
    [Range(0.5f, 2f)]
    public float maxDurate;
    public float spread;
    public TMP_Text textMoneyHPBar;
    [SerializeField] Ease easeType;
    [Header("Additionally")]
    public SpriteRenderer BG;
    public Transform Grek;
    public Zeus zeus;
    public AudioSource WinnerAudi;
    public AudioSource GameOverAudi;
    public AudioSource CoinAudi;
    public TMP_Text textMoney;
    public Image[] LightBoltImage;
    public Sprite LightBoltOn;
    int allLightBoltLast;
    // Start is called before the first frame update
    void Awake()
    {
        singenton = this;
    }
    private void Start()
    {
        PlayerPrefs.SetString("LastLevel", Application.loadedLevelName);
        Application.targetFrameRate = 60;
        for (int i =0;i< 3; i++)
        {
            if(IntToBool(PlayerPrefs.GetInt(Application.loadedLevelName + "lightBolt" + i)))
            {
                allLightBoltLast += 1;
            }
        }
        Invoke(nameof(start2), 0.1f);
    }

    void start2()
    {
        targetPos = targetMoney.position;
    }

    bool IntToBool(int Int)
    {
        if (Int > 0)
        {
            return true;
        }
        return false;
    }

    public void Save()
    {
        int g = 0;
        for (int i = 0; i < 3; i++)
        {
            if (LightBolt[i])
            {
                g += 1;
            }
        }
        if (allLightBoltLast < g)
        {
            for (int i = 0; i < 3; i++)
            {
                if (LightBolt[i])
                {
                    PlayerPrefs.SetInt(Application.loadedLevelName + "lightBolt" + i, 1);
                }
            }
        }
        int money = PlayerPrefs.GetInt("Money");
        money += moneyPlus;
        PlayerPrefs.SetInt("Money", money);
    }
    public void Restart()
    {
        SceneManager.LoadScene(Application.loadedLevel);
    }
    public void GameOver()
    {
        GameOverPanel.SetActive(true);
        Destroy(GameObject.FindGameObjectWithTag("BGMusic"));
        GameOverAudi.Play();
    }
    public void Winner()
    {
        Grek grek = Grek.gameObject.GetComponent<Grek>();
        grek.enabled = false;
        grek.anim.SetTrigger("fly");
        grek.rb.simulated = false;
        StartCoroutine(FlyToWinner());
    }

    IEnumerator FlyToWinner()
    {
        zeus.isAttacking = false;
        for(int i =0;i < 2500; i++)
        {
            Grek.position = Vector2.LerpUnclamped(Grek.position, new Vector2(0, 22.9f+(( Camera.main.transform.position.y) * durationLevel)), 0.5f * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        OpenWinnerMenu();
    }
    IEnumerator MoneyAdding()
    {
        float money = 0;
        CoinAudi.Play();
        float p = (float)moneyPlus / 25f;
        while(money < moneyPlus)
        {
            money += p;
            textMoney.text = ((int)money).ToString();
            yield return new WaitForSeconds(0.1f);
        }

        textMoney.text = moneyPlus.ToString();
    }
    void OpenWinnerMenu()
    {
        Save();
        StartCoroutine(MoneyAdding());
        WinnerAudi.Play();
        Destroy(GameObject.FindGameObjectWithTag("BGMusic"));
        for(int i=0;i< 3; i++)
        {
            if (LightBolt[i])
            {
                LightBoltImage[i].sprite = LightBoltOn;
            }
        }
        WinnerPanel.SetActive(true);
    }

    public void NextLevelButton()
    {
        SceneManager.LoadScene(Application.loadedLevel+1);
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void SetTime(float time)
    {
        Time.timeScale = time;
    }
    public void SadBG()
    {
        if(coroutine!= null)
            StopCoroutine(coroutine);
       coroutine = StartCoroutine(SadingBG());
    }
    Coroutine coroutine;
    public IEnumerator SadingBG()
    {
        Color col = BG.color;
        while(col.a > 0)
        {
            col.a -= 0.02f;
            BG.color = col;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(5);
        while (col.a < 1)
        {
            col.a += 0.02f;
            BG.color = col;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void AddMoney(Vector2 pos, int MoneyPlus)
    {
        for(int i =0;i< 6; i++)
        {
            GameObject coin = Instantiate(PrefabMoneyAnim, pos, Quaternion.identity);
            coin.transform.SetParent(Camera.main.transform);
            coin.transform.position = pos + new Vector2(Random.Range(-spread, spread), 0f);
            float duration = Random.Range(minDurate, maxDurate);
            coin.transform.DOLocalMove(targetPos, duration)
            .SetEase(easeType)
            .OnComplete(() => {
            //executes whenever coin reach target position
            StartCoroutine(AddingMoneyBar());
                Destroy(coin);
            });
        }
        this.moneyPlus += MoneyPlus;
        
    }
    IEnumerator AddingMoneyBar()
    {
        float money = 0;
        float p = (float)moneyPlus / 10;
        while (money < moneyPlus)
        {
            money += p;
            textMoneyHPBar.text = ((int)money).ToString();
            yield return new WaitForSeconds(0.01f);
        }
    }
}
