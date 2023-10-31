using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;


public class MyButton : MonoBehaviour
{
    public List<Button> buttons = new List<Button>(); //��ưUI
    public List<Image> images = new List<Image>(); //�̹���UI
    Dictionary<Button, Image> dict1 = new Dictionary<Button, Image>(); //��ư�� Ű�� �̹���ui�� �� ��Ī

    private Image currentImage; // ���� Ȱ��ȭ�� �̹����� �����ϱ� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        int index = 0;
        foreach (Button button in buttons)
        {
            Image image = images[index];
            dict1.Add(button, image);
            button.onClick.AddListener(() => OnButtonClick(button));
            index++;
        }


    }

    //��ư�� Ŭ���Ǿ��� �� ȣ��Ǵ� �ݹ� �޼���
    //��ư�� �ش��ϴ� �̹��� UI�� Ȱ��ȭ�ϰ�, ���� Ȱ��ȭ�� �̹��� UI�� ������Ʈ �Ѵ�
    public void OnButtonClick(Button button)
    {
        //���� Ȱ��ȭ�� �̹����� �ִ��� Ȯ���Ѵ�
        if (currentImage != null)
        {
            //Ȱ��ȭ �� �̹��� UI�� �ִٸ�, �ش� �̹����� ���� ������Ʈ�� ��Ȱ��ȭ
            currentImage.gameObject.SetActive(false);
        }

        //��ư�� �ش��ϴ� �̹��� UI�� ã�� �´�
        //dict1�� button�� Ű�� ����Ͽ� image�� �����´�
        //TryGetValue() ��ųʸ��� �ش� Ű�� ã�� ���, �̹����� ��ȯ�Ѵ�
        //�̹��� UI�� ã�� ���� ��� out �Ű� ������ ���� image�� null�� �Ҵ��Ѵ�
        if (dict1.TryGetValue(button, out Image image))
        {

            // �̹��� UI�� ���������� ǥ��
            image.gameObject.SetActive(true);

            // ���� Ȱ��ȭ�� �̹��� ������Ʈ
            // ������ ���� ��ư�� �ش��ϴ� �̹����� �Ҵ�
            //������ �ٸ� ��ư�� Ŭ���� ��, ���� Ȱ��ȭ�� �̹����� ��Ȱ��ȭ �ϱ� ���� ���.
            currentImage = image;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
