using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class NewUIButtonEvent : MonoBehaviour
{

    public VideoPlayer videoPlayer;

    public void OnClickButton()
    {
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();
    }

}
