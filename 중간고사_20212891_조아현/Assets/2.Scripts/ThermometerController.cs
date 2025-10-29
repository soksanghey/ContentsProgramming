using UnityEngine;

public class ThermometerController : MonoBehaviour
{
    
    [Range(0, 50)] 
    public float temperature = 25.0f; // 온도 변수 (초기값 25)
    public float maxHeight = 3.0f;    // 최대 높이 변수 (초기값 3)

    public GameObject thermometerBar; // 높이와 색상이 변할 큐브
    public GameObject ground;         // 색상이 변할 바닥

    
    private Renderer barRenderer;
    private Renderer groundRenderer;

        void Start()
    {
        
        if (thermometerBar != null)
        {
            barRenderer = thermometerBar.GetComponent<Renderer>();
        }
        if (ground != null)
        {
            groundRenderer = ground.GetComponent<Renderer>();
        }
    }

    
    void Update()
    {
       
        if (barRenderer == null || groundRenderer == null)
        {
            return;
        }

        
        float normalizedTemp = Mathf.Clamp01(temperature / 50.0f);
       
        float targetHeight = normalizedTemp * maxHeight;
       
        float finalHeight = Mathf.Max(0.1f, targetHeight);

    
        thermometerBar.transform.localScale = new Vector3(1, finalHeight, 1);
       
        thermometerBar.transform.position = new Vector3(0, finalHeight / 2.0f, 0);


        if (temperature < 15)
        {
            barRenderer.material.color = Color.blue; // 파란색
        }
        else if (temperature < 30) // 15도 이상 30도 미만
        {
            barRenderer.material.color = Color.green; // 녹색
        }
        else // 30도 이상
        {
            barRenderer.material.color = Color.red; // 빨간색
        }

        // [Ground 색상 변경]
        if (temperature < 15)
        {
            groundRenderer.material.color = Color.white; // 하얀색
        }
        else if (temperature < 30) // 15도 이상 30도 미만
        {
            groundRenderer.material.color = new Color(0.6f, 0.4f, 0.2f); // 갈색
        }
        else // 30도 이상
        {
            groundRenderer.material.color = new Color(1.0f, 0.5f, 0.0f); // 주황색
        }
    }
}
