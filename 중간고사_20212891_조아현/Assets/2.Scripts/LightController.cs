using UnityEngine;
using TMPro; 

public class LightController : MonoBehaviour
{
    public GameObject lightBulb;      // 색상이 변할 구체
    public TextMeshProUGUI statusText;  // 상태를 표시할 텍스트
    public GameObject infoPanel;      // 켜고 끌 정보 패널

    private Renderer bulbRenderer; 

    
    void Start()
    {
        
        if (lightBulb != null)
        {
            bulbRenderer = lightBulb.GetComponent<Renderer>();
        }

        
        SetDark();
    }

    public void SetDark()
    {
        
        if (bulbRenderer != null)
        {
            bulbRenderer.material.color = Color.gray;
        }

        
        if (statusText != null)
        {
            statusText.text = "Brightness: Dark 밝기: 어두움";
        }

        
        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
        }
    }

    
    public void SetBright()
    {
        
        if (bulbRenderer != null)
        {
            bulbRenderer.material.color = Color.white;
        }

        if (statusText != null)
        {
            statusText.text = "Brightness: Dark 밝기: 밝음";
        }

        
        if (infoPanel != null)
        {
            infoPanel.SetActive(true);
        }
    }
}
