using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTrackingController : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager trackedImageManager;
    //[SerializeField] private GameObject defaultContentPrefab;
    [SerializeField] private GameObject image1ContentPrefab;
    [SerializeField] private GameObject image2ContentPrefab;

    private Dictionary<string, GameObject> imageContentDict = new Dictionary<string, GameObject>();

    private void Awake()
    {
        // 스크립트가 처음으로 활성화될 때, 모든 이미지 콘텐츠를 딕셔너리에 추가합니다.
        imageContentDict.Add("Image1", image1ContentPrefab);
        imageContentDict.Add("Image2", image2ContentPrefab);
    }

    private void OnEnable()
    {
        // 이미지 인식 이벤트 콜백 함수를 AR Tracked Image Manager에 등록합니다.
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        // 스크립트가 비활성화될 때, 이미지 인식 이벤트 콜백 함수를 제거합니다.
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.updated)
        {
            // 이미지가 업데이트되면 해당 이미지의 트래킹 상태를 확인합니다.
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                // 인식된 이미지의 이름을 가져옵니다.
                string imageName = trackedImage.referenceImage.name;

                // 이미지 이름을 기준으로 적절한 가상 콘텐츠를 가져옵니다.
                if (imageContentDict.TryGetValue(imageName, out GameObject contentPrefab))
                {
                    // 이미지에 해당하는 가상 콘텐츠를 인식된 이미지와 동일한 위치와 회전에 생성합니다.
                    GameObject contentObject = Instantiate(contentPrefab, trackedImage.transform.position, trackedImage.transform.rotation);

                    // 생성된 가상 콘텐츠를 인식된 이미지의 자식으로 설정하여 이미지와 함께 움직이도록 합니다.
                    contentObject.transform.SetParent(trackedImage.transform);

                    // 이미지에 해당하는 가상 콘텐츠를 딕셔너리에서 제거합니다.
                    // (동일한 이미지가 여러 프레임에 걸쳐 계속 인식되는 경우를 방지하기 위해)
                    imageContentDict.Remove(imageName);
                }
            }
        }
    }
}
