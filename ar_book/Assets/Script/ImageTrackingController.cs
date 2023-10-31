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
        // ��ũ��Ʈ�� ó������ Ȱ��ȭ�� ��, ��� �̹��� �������� ��ųʸ��� �߰��մϴ�.
        imageContentDict.Add("Image1", image1ContentPrefab);
        imageContentDict.Add("Image2", image2ContentPrefab);
    }

    private void OnEnable()
    {
        // �̹��� �ν� �̺�Ʈ �ݹ� �Լ��� AR Tracked Image Manager�� ����մϴ�.
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        // ��ũ��Ʈ�� ��Ȱ��ȭ�� ��, �̹��� �ν� �̺�Ʈ �ݹ� �Լ��� �����մϴ�.
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.updated)
        {
            // �̹����� ������Ʈ�Ǹ� �ش� �̹����� Ʈ��ŷ ���¸� Ȯ���մϴ�.
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                // �νĵ� �̹����� �̸��� �����ɴϴ�.
                string imageName = trackedImage.referenceImage.name;

                // �̹��� �̸��� �������� ������ ���� �������� �����ɴϴ�.
                if (imageContentDict.TryGetValue(imageName, out GameObject contentPrefab))
                {
                    // �̹����� �ش��ϴ� ���� �������� �νĵ� �̹����� ������ ��ġ�� ȸ���� �����մϴ�.
                    GameObject contentObject = Instantiate(contentPrefab, trackedImage.transform.position, trackedImage.transform.rotation);

                    // ������ ���� �������� �νĵ� �̹����� �ڽ����� �����Ͽ� �̹����� �Բ� �����̵��� �մϴ�.
                    contentObject.transform.SetParent(trackedImage.transform);

                    // �̹����� �ش��ϴ� ���� �������� ��ųʸ����� �����մϴ�.
                    // (������ �̹����� ���� �����ӿ� ���� ��� �νĵǴ� ��츦 �����ϱ� ����)
                    imageContentDict.Remove(imageName);
                }
            }
        }
    }
}
