using UnityEngine;

public class TemperatureColor : MonoBehaviour
{
  // 온도 변수
  public float temperature = 20.0f;
    public float MaxHeight = 5.0f;

  public Color coldColor = Color.blue; // 차가운 색상

  public Color normalColor = Color.green; // 보통 색상

  public Color hotColor = Color.red; // 뜨거운 색상

  private Renderer myRenderer;

  void Update()
  {
    // 매 프레임마다 실행 (초당 60번)
    float height = (temperature / 50.0f) * MaxHeight;
    transform.localScale = new Vector3(1, height, 1);

   // Renderer 컴포넌트 가져오기
        myRenderer =  GetComponent<Renderer>();

    // 온도에 따라 색상 결정
    if (temperature < 15.0f)
    {
      myRenderer.material.color = coldColor;
      Debug.Log(temperature + "도: 차가워요! (파란색)");
    }
    else if (temperature < 30.0f)
    {
      myRenderer.material.color = normalColor;
      Debug.Log(temperature + "도: 적당해요! (녹색)");
    }
    else
    {
      myRenderer.material.color = hotColor;
      Debug.Log(temperature + "도: 뜨거워요! (빨간색)");
    }
   
  }
}
