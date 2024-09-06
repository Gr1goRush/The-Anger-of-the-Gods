using UnityEngine;
using UnityEngine.Events;
using AppsFlyerSDK;
using System;
using System.Collections.Generic;
public class AppsFlyerObjectScript : MonoBehaviour,
IAppsFlyerConversionData
{
    [SerializeField] private string _devKey, _appID;
    [SerializeField] private bool _isDebug;
    // deepLinkParamsDictionary contains all the deep link parameters as keys
    public static Dictionary<string, object> DeepLinkParamsDictionary = null;
    public static UnityEvent OnDeepLinkProcessingSuccesfullyDone;
    private void Start()
    {
        if (OnDeepLinkProcessingSuccesfullyDone == null)
            OnDeepLinkProcessingSuccesfullyDone = new UnityEvent();
        AppsFlyer.setIsDebug(_isDebug);
        AppsFlyer.initSDK(_devKey, _appID, this);
        AppsFlyer.OnDeepLinkReceived += OnDeepLink;
        AppsFlyer.startSDK();
    }
    private void OnDeepLink(object sender, EventArgs args)
    {
        var deepLinkEventArgs = args as DeepLinkEventsArgs;
        switch (deepLinkEventArgs.status)
        {
            case DeepLinkStatus.FOUND:
                if (deepLinkEventArgs.isDeferred())
                    AppsFlyer.AFLog("OnDeepLink", "This is a deferred deep link");
                else
                    AppsFlyer.AFLog("OnDeepLink", "This is a direct deep link");
#if UNITY_IOS && !UNITY_EDITOR
if (deepLinkEventArgs.deepLink.ContainsKey("click_event") &&
deepLinkEventArgs.deepLink["click_event"] != null) {
DeepLinkParamsDictionary =
deepLinkEventArgs.deepLink["click_event"] as Dictionary<string, object>;
OnDeepLinkProcessingSuccesfullyDone?.Invoke();
}
if(_isDebug) {
if (DeepLinkParamsDictionary != null) {
foreach (KeyValuePair<string, object> entry in
DeepLinkParamsDictionary)
Debug.Log($"Key: {entry.Key}, Value: {entry.Value}");
} else
Debug.Log("Deep link parameters dictionary is null.");
}
#endif
                break;
            case DeepLinkStatus.NOT_FOUND:
                AppsFlyer.AFLog("OnDeepLink", "Deep link not found");
                break;
            default:
                AppsFlyer.AFLog("OnDeepLink", "Deep link error");
                break;
        }
    }
    public void onConversionDataSuccess(string popoxc)
    {
        AppsFlyer.AFLog("didReceiveConversionData", popoxc);
        Dictionary<string, object> convData =
        AppsFlyer.CallbackStringToDictionary(popoxc);
        string aghsd = "";
        if (convData.ContainsKey("campaign"))
        {
            object conv = null;
            if (convData.TryGetValue("campaign", out conv))
            {
                string[] list = conv.ToString().Split('_');
                if (list.Length > 0)
                {
                    aghsd = "&";
                    for (int a = 0; a < list.Length; a++)
                    {
                        aghsd += string.Format("sub{0}={1}", (a + 1), list[a]);
                        if (a < list.Length - 1)
                            aghsd += "&";
                    }
                }
            }
        }
        PlayerPrefs.SetString("glrobo", aghsd);
    }
    public void onConversionDataFail(string error)
    {
        AppsFlyer.AFLog("didReceiveConversionDataWithError", error);
        PlayerPrefs.SetString("glrobo", "");
    }
    public void onAppOpenAttribution(string attributionData)
    {
        AppsFlyer.AFLog("onAppOpenAttribution", attributionData);
        PlayerPrefs.SetString("glrobo", "");
    }
    public void onAppOpenAttributionFailure(string error)
    {
        AppsFlyer.AFLog("onAppOpenAttributionFailure", error);
        PlayerPrefs.SetString("glrobo", "");
    }
    private void OnDisable()
    {
        AppsFlyer.OnDeepLinkReceived -= OnDeepLink;
    }
}