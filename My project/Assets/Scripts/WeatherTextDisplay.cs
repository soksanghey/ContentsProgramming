using UnityEngine;
using TMPro;  // TextMeshPro 사용을 위해 필요

public class WeatherTextDisplay : MonoBehaviour
{
    [Header("UI 텍스트 설정")]
    public TextMeshProUGUI temperatureText;  // TextMeshPro 텍스트 컴포넌트

    [Header("날씨 데이터")]
    public float currentTemperature = 25.0f;  // 현재 온도
    public string locationName = "Seoul";       // 지역 이름

    void Start()
    {
        // 게임 시작시 한번 실행
        Debug.Log("날씨 텍스트 시스템 시작!");

        // 텍스트가 연결되었는지 확인
        if (temperatureText == null)
        {
            Debug.LogError("TextMeshPro 컴포넌트가 연결되지 않았습니다!");
        }
        else
        {
            Debug.Log("텍스트 컴포넌트 연결 완료!");
        }
    }

    void Update()
    {
        // 매 프레임마다 실행 - 실시간 업데이트

        // 텍스트가 연결되어 있다면 업데이트
        if (temperatureText != null)
        {
            // 텍스트 내용 업데이트
            temperatureText.text = locationName + " Current Temperature: " + currentTemperature.ToString("F1") + "C";
        }
    }// WeatherTextDisplay 클래스 안에 추가 (Update 함수 아래에)


    public void ChangeToSpring()
    {
        // 봄 설정값들
        currentTemperature = 15.0f;      // 봄 온도: 15도
        locationName = "봄날의 서울";       // 봄 테마 지역명

        // 디버그 메시지로 함수 실행 확인
        Debug.Log("봄으로 변경되었습니다! 온도: " + currentTemperature + "도");


        // ChangeToSpring() 함수 아래에 계속 추가
    }
    public void ChangeToSummer()
    {
        // 여름 설정
        currentTemperature = 30.0f;
        locationName = "여름날의 부산";
        Debug.Log("여름으로 변경! 온도: " + currentTemperature + "도");
    }

    public void ChangeToAutumn()
    {
        // 가을 설정
        currentTemperature = 10.0f;
        locationName = "가을날의 경주";
        Debug.Log("가을으로 변경! 온도: " + currentTemperature + "도");
    }

    public void ChangeToWinter()
    {
        // 겨울 설정
        currentTemperature = -5.0f;
        locationName = "겨울날의 강원도";
        Debug.Log("겨울로 변경! 온도: " + currentTemperature + "도");
    }
   
}

