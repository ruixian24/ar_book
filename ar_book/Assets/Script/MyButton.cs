using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;


public class MyButton : MonoBehaviour
{
    public List<Button> buttons = new List<Button>(); //버튼UI
    public List<Image> images = new List<Image>(); //이미지UI
    Dictionary<Button, Image> dict1 = new Dictionary<Button, Image>(); //버튼을 키로 이미지ui에 값 매칭

    private Image currentImage; // 현재 활성화된 이미지를 추적하기 위한 변수

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

    //버튼이 클릭되었을 때 호출되는 콜백 메서드
    //버튼에 해당하는 이미지 UI를 활성화하고, 현재 활성화된 이미지 UI를 업데이트 한다
    public void OnButtonClick(Button button)
    {
        //현재 활성화된 이미지가 있는지 확인한다
        if (currentImage != null)
        {
            //활성화 된 이미지 UI가 있다면, 해당 이미지의 게임 오브젝트를 비활성화
            currentImage.gameObject.SetActive(false);
        }

        //버튼에 해당하는 이미지 UI를 찾아 온다
        //dict1은 button을 키로 사용하여 image를 가져온다
        //TryGetValue() 딕셔너리에 해당 키를 찾은 경우, 이미지를 반환한다
        //이미지 UI를 찾지 못한 경우 out 매개 변수를 통해 image에 null을 할당한다
        if (dict1.TryGetValue(button, out Image image))
        {

            // 이미지 UI를 상세페이지에 표시
            image.gameObject.SetActive(true);

            // 현재 활성화된 이미지 업데이트
            // 변수에 현재 버튼에 해당하는 이미지를 할당
            //다음에 다른 버튼이 클릭될 때, 현재 활성화된 이미지를 비활성화 하기 위해 사용.
            currentImage = image;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
