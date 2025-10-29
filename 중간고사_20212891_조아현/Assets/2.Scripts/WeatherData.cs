using UnityEngine;

public class WeatherData : MonoBehaviour
{
    
    public float temperature; // 온도
    public float humidity;    // 습도
    public string cityName;   // 도시 이름

        void Start()
    {
    
        float feelsLikeTemp = temperature - (humidity / 30.0f);

        
        Debug.Log(cityName + "의 온도: " + temperature);
        Debug.Log("습도: " + humidity);
        Debug.Log("체감온도: " + feelsLikeTemp);
    }
}
