using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class Track : MonoBehaviour
{
   
    public ARTrackedImageManager manager;
    public List<GameObject> list1 = new List<GameObject>();
    public List<AudioClip> list2 = new List<AudioClip>();

    Dictionary<string, GameObject> dict1 = new Dictionary<string, GameObject>();
    Dictionary<string, AudioClip> dict2 = new Dictionary<string, AudioClip>();


    void Start()
    {
        foreach (GameObject obj in list1)
        {
            dict1.Add(obj.name, obj);

        }
        foreach (AudioClip obj in list2)
        {
            dict2.Add(obj.name, obj);
        }
    }

    private void OnEnable()
    {
        manager.trackedImagesChanged += OnChanged; //델리게이트(함수를 복사)
    }
    //trackedImagesChanged = 카메라 상에서 이미지가 새로 나오거나 없어지거나 움직일때 자동으로 이미지가 불린다.
    //여기에 Onchanged함수가 붙어있다 = tracked ImageChanged될때 함수가 실행된다.


    private void OnDisable()
    {
        manager.trackedImagesChanged -= OnChanged;
    }

    void OnChanged(ARTrackedImagesChangedEventArgs e)
    {
        foreach (ARTrackedImage t in e.added)
        {
            UpdateImage(t);
            UpdateSound(t);
        }
        foreach (ARTrackedImage t in e.updated)
        {
            UpdateImage(t);
        }
    }

    void UpdateImage(ARTrackedImage t)
    {
        string name = t.referenceImage.name;

        GameObject obj = dict1[name];

        obj.transform.position = t.transform.position;
        obj.transform.rotation = t.transform.rotation;
        obj.SetActive(true);
    }

    void UpdateSound(ARTrackedImage t)
    {
        string name = t.referenceImage.name;

        AudioClip sound = dict2[name];
        GetComponent<AudioSource>().PlayOneShot(sound);
    }

}
