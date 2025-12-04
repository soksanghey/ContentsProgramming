using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 
using TMPro;

public class BubbleController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI 요소")]
    [SerializeField] private Image bubbleImage;
    [SerializeField] private TextMeshProUGUI nameText; // Inspector에서 TextMeshPro 객체가 꼭 연결되어 있어야 합니다!
    
    [Header("커서 설정")]
    public Texture2D subwayCursorTexture; 
    private Vector2 hotSpot = new Vector2(16, 16); 

    // 이 버블이 나타내는 데이터
    private DistrictData districtData;

    // 초기화 함수: 그래프가 생성될 때 한 번 호출됨 -> 이때 이름이 박제됨
    public void Initialize(DistrictData data, float maxDensity)
    {
        this.districtData = data;
        
        // 1. 텍스트 설정 (영구 표시)
        // 상호작용과 관계없이, 버블이 생길 때 이름이 바로 설정됩니다.
        if (nameText != null)
        {
            nameText.text = data.Name; 
        }

        // 2. 크기(Size) 조절
        float normalizedDensity = data.Density / maxDensity; 
        float minPixelSize = 30f; 
        float maxPixelSize = 150f; 

        float finalSize = Mathf.Lerp(minPixelSize, maxPixelSize, normalizedDensity);
        
        RectTransform rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(finalSize, finalSize);

        
        // 3. 이미지 로드 및 색상 적용
        string imageFileName = "district_" + data.Name; 
        Sprite districtSprite = Resources.Load<Sprite>(imageFileName);
        
        if (districtSprite != null && bubbleImage != null)
        {
            bubbleImage.sprite = districtSprite;
            bubbleImage.color = Color.white; 
        }
        else
        {
            Color lowColor = Color.green;
            Color highColor = Color.red;
            if (bubbleImage != null)
            {
                bubbleImage.color = Color.Lerp(lowColor, highColor, normalizedDensity);
            }
        }
    }
    
    // 마우스 오버: 커서 변경
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (subwayCursorTexture != null)
        {
            Cursor.SetCursor(subwayCursorTexture, hotSpot, CursorMode.Auto); 
        }
    }

    // 마우스 나감: 커서 복구
    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); 
    }

    // 클릭: 툴팁 표시
    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 tooltipPosition = transform.position;
        
        GraphController.Instance.ShowTooltip(
            districtData.Name, 
            districtData.Density, 
            districtData.Population, 
            tooltipPosition
        );
    }
}