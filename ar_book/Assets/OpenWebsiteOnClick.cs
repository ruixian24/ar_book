using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWebsiteOnClick : MonoBehaviour
{
    public string targetSiteUrl = "https://www.google.com/maps/place/%EB%8F%99%EC%95%88%EA%B5%90%ED%9A%8C/data=!4m10!1m2!2m1!1z64-Z7JWI6rWQ7ZqM!3m6!1s0x357cbb6df98f2895:0x5b8d2dfc62c8de75!8m2!3d37.5925823!4d127.0561051!15sCgzrj5nslYjqtZDtmoySARNwcmVzYnl0ZXJpYW5fY2h1cmNo4AEA!16s%2Fg%2F1w6r66rn?authuser=0&entry=ttu";

    public void OnButtonClick()
    {
        Application.OpenURL(targetSiteUrl);
    }
}
