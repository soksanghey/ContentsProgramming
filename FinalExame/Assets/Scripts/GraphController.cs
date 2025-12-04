using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Linq; 

public class GraphController : MonoBehaviour
{
    public static GraphController Instance;

    [Header("UI 연결")]
    [SerializeField] private GameObject bubblePrefab; 
    [SerializeField] private Transform contentParent; 

    [Header("툴팁 UI")]
    [SerializeField] private GameObject tooltipPanel; 
    [SerializeField] private TextMeshProUGUI tooltipText; 

    // =================================================================
    // 수동으로 배치한 25개 자치구의 고정 위치 좌표 (최종 반영된 좌표)
    // =================================================================
    private Vector2[] districtPositions = new Vector2[]
    {
        // 1. 종로구
        new Vector2( 0f, 131f ), 
        // 2. 중구
        new Vector2( 0f, 0f ),  
        // 3. 용산구
        new Vector2( -19f, -143f ),
        // 4. 성동구
        new Vector2( 191f, 25f ), 
        // 5. 광진구
        new Vector2( 441f, 59f ),
        // 6. 동대문구
        new Vector2( 323f, 190f ), 
        // 7. 중랑구
        new Vector2( 547f, 283f ),
        // 8. 성북구
        new Vector2( 91f, 246f ),
        // 9. 강북구
        new Vector2( -55f, 380f ),
        // 10. 도봉구
        new Vector2( 127f, 426f ),
        // 11. 노원구
        new Vector2( 345f, 398f ),
        // 12. 은평구
        new Vector2( -547f, 266f ),
        // 13. 서대문구
        new Vector2( -256f, 114f ),
        // 14. 마포구
        new Vector2( -425f, 52f ),
        // 15. 양천구
        new Vector2( -537f, -152f ),
        // 16. 강서구
        new Vector2( -750f, 0f ),
        // 17. 구로구
        new Vector2( -611f, -343f ),
        // 18. 금천구
        new Vector2( -370f, -430f ),
        // 19. 영등포구
        new Vector2( -269f, -220f ),
        // 20. 동작구
        new Vector2( -40f, -268f ),
        // 21. 관악구
        new Vector2( -114f, -408f ),
        // 22. 서초구
        new Vector2( 179f, -318f ),
        // 23. 강남구
        new Vector2( 356f, -216f ),
        // 24. 송파구
        new Vector2( 562f, -142f ),
        // 25. 강동구
        new Vector2( 741f, 0f ) 
    };
    // =================================================================


    void Awake()
    {
        Instance = this;
        
        // 👇 안정성 강화: TooltipText가 Inspector에 연결 안 되었을 때 자식에서 찾아서 연결
        if (tooltipPanel != null && tooltipText == null)
        {
            tooltipText = tooltipPanel.GetComponentInChildren<TextMeshProUGUI>();
            if (tooltipText == null)
            {
                Debug.LogError("[CRITICAL ERROR] TooltipPanel 아래에 TextMeshProUGUI 컴포넌트(TooltipText)를 찾을 수 없습니다.");
            }
        }
        
        // 툴팁 숨김 로직
        if (tooltipPanel != null)
        {
             tooltipPanel.SetActive(false); 
        }
    }

    void Start()
    {
        if (DataManager.Instance.AllDistrictsData.Count > 0)
        {
            VisualizeData(DataManager.Instance.AllDistrictsData);
        }
    }

    // 데이터 리스트를 받아 화면에 버블을 그리는 핵심 함수
    private void VisualizeData(List<DistrictData> dataToVisualize)
    {
        // 기존의 버블 오브젝트 제거 로직
        List<GameObject> childrenToDestroy = new List<GameObject>();
        foreach (Transform child in contentParent)
        {
            childrenToDestroy.Add(child.gameObject);
        }

        foreach (GameObject childGO in childrenToDestroy)
        {
            if (childGO != null)
            {
                Destroy(childGO);
            }
        }

        float maxDensity = DataManager.Instance.MaxDensity;
        int index = 0; 

        // 리스트의 순서대로 버블 생성
        foreach (var data in dataToVisualize)
        {
            if (index >= districtPositions.Length) break; 

            GameObject bubbleGO = Instantiate(bubblePrefab, contentParent);
            BubbleController controller = bubbleGO.GetComponent<BubbleController>();
            
            // 버블 초기화 및 크기/이미지 설정
            controller.Initialize(data, maxDensity);
            
            // 버블의 위치를 미리 정의된 좌표로 설정
            RectTransform rect = bubbleGO.GetComponent<RectTransform>();
            rect.anchoredPosition = districtPositions[index];

            // 오브젝트 이름 변경
            bubbleGO.name = data.Name + "Bubble";

            index++;
        }
    }

    // 정렬 함수들 (유지)
    public void SortByName() 
    {
        List<DistrictData> sortedData = DataManager.Instance.AllDistrictsData
            .OrderBy(d => d.Name)
            .ToList();
        VisualizeData(sortedData);
    }

    public void SortByDensity() 
    {
        List<DistrictData> sortedData = DataManager.Instance.AllDistrictsData
            .OrderByDescending(d => d.Density)
            .ToList();
        VisualizeData(sortedData);
    }
    
    
    // 툴팁 표시 함수 (안정성 강화)
    public void ShowTooltip(string name, float density, int population, Vector3 position)
    {
        // 1. 툴팁 패널이 Null이면 바로 종료 (가장 바깥 오브젝트 확인)
        if (tooltipPanel == null)
        {
            Debug.LogError("[CRITICAL ERROR] TooltipPanel 오브젝트가 GraphController에 연결되지 않았습니다.");
            return;
        }
        
        // 2. 툴팁 텍스트 컴포넌트가 Null이면 종료 (내용을 쓸 수 없음)
        if (tooltipText == null)
        {
            Debug.LogError("[CRITICAL ERROR] TooltipText 컴포넌트가 연결되지 않아 툴팁 내용을 표시할 수 없습니다.");
            tooltipPanel.SetActive(true); // 패널만 띄워서 연결 오류 시각적 확인
            return;
        }

        // 기존 툴팁이 활성화되어 있다면 숨깁니다.
        if (tooltipPanel.activeSelf)
        {
              HideTooltip();
        }

        // 3. 툴팁 내용 포맷 (실제 텍스트 업데이트)
        tooltipText.text = $"{name}\n인구수: {population:N0} 명\n밀집도: {density:F2} 명/㎢";
        
        // 4. 툴팁 위치 및 활성화
        tooltipPanel.transform.position = position + new Vector3(120, 0, 0); 
        tooltipPanel.SetActive(true);
    }
    
    // 툴팁 숨김 함수
    public void HideTooltip()
    {
        if (tooltipPanel != null)
        {
             tooltipPanel.SetActive(false);
        }
    }
}