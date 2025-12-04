using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SimpleAPITest : MonoBehaviour
{
    // 테스트용 공개 API (API 키 불필요)
    private const string TEST_URL = "https://jsonplaceholder.typicode.com/posts/1";

    void Start()
    {
        Debug.Log("📡 API 테스트 시작...");
        StartCoroutine(TestAPI());
    }

    IEnumerator TestAPI()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(TEST_URL))
        {
            // 요청 전송
            Debug.Log("⏳ 서버에 요청 중...");
            yield return request.SendWebRequest();

            // 결과 확인
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("✅ 성공!");
                Debug.Log("📦 응답 데이터:\n" + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("❌ 실패: " + request.error);
            }
        }
    }
    
}

[System.Serializable]  // ← 필수!
public class WeatherData
{
    public Main main;           // JSON의 "main" 객체
    public Weather[] weather;   // JSON의 "weather" 배열
    public string name;         // JSON의 "name" 값
}

[System.Serializable]
public class Main
{
    public float temp;          // JSON의 "temp" 값
    public int humidity;        // JSON의 "humidity" 값
}

[System.Serializable]
public class Weather
{
    public string main;         // JSON의 "main" 값
    public string description;  // JSON의 "description" 값
}