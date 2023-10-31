using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ARTrackedImg : MonoBehaviour
{

    public float timer;
    public ARTrackedImageManager trackedImageManager;
    public List<GameObject> objectList = new List<GameObject>();
    private Dictionary<string, GameObject> prefabDic = new Dictionary<string, GameObject>();
    private List<ARTrackedImage> trackedImg = new List<ARTrackedImage>();
    private List<float> trackedTimer = new List<float>();
    
    void Awake()
    {
        foreach(GameObject obj in objectList)
        {
            string tName = obj.name;
            prefabDic.Add(tName, obj);
        }
    }

    
    void Update()
    {
        if(trackedImg.Count > 0)
        {
            List<ARTrackedImage> tNumList = new List<ARTrackedImage>();
            for(var i = 0; i < trackedImg.Count; i++)
            {
                if (trackedImg[i].trackingState==UnityEngine.XR.ARSubsystems.TrackingState.Limited)
                {
                    if (trackedTimer[i] >= timer)
                    {
                        string name = trackedImg[i].referenceImage.name;
                        GameObject tObj = prefabDic[name];
                        tObj.SetActive(false);
                        tNumList.Add(trackedImg[i]);
                    }
                    else
                    {
                        trackedTimer[i] += Time.deltaTime;
                    }
                }
            }

            if (tNumList.Count > 0)
            {
                for (var i = 0; i < tNumList.Count; i++)
                {
                    int num = trackedImg.IndexOf(tNumList[i]);
                    trackedImg.Remove(trackedImg[num]);
                    trackedTimer.Remove(trackedTimer[num]);
                }
            }
        }
    }
    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()

    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }
    
    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            if (!trackedImg.Contains(trackedImage))
            { 
                trackedImg.Add(trackedImage);
                trackedTimer.Add(0);
            }
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            if (!trackedImg.Contains(trackedImage))
            {
                trackedImg.Add(trackedImage);
                trackedTimer.Add(0);
            }
            else
            {
                int num = trackedImg.IndexOf(trackedImage);

                //�߰��� �κ� Limited���°� �ƴҶ� Ÿ�̸Ӹ� 0���� ����

                if (trackedImg[num].trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited)
                {
                }
                else
                {
                    trackedTimer[num] = 0;

                }
            }

            UpdateImage(trackedImage);

        }

    }
    private void UpdateImage(ARTrackedImage trackedImage)
    {
        int num = trackedImg.IndexOf(trackedImage);
        string name = trackedImage.referenceImage.name;

        GameObject tObj = prefabDic[name];

        //�߰��� �κ� Limited���°� �ƴҶ�(Ʈ��ŷ���� �ƴҶ�), �¾�Ƽ��true�� ��ġ ����ȭ�� ���� ����

        if (trackedImg[num].trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited)

        {
        }

        else
        {
            tObj.transform.position = trackedImage.transform.position;
            tObj.transform.rotation = trackedImage.transform.rotation;
            tObj.SetActive(true);
        }
    }
}


