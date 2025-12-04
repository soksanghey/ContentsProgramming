using UnityEngine;
using System.Collections.Generic;
using System.Linq; // Max 값 찾기를 위해 필요

// 1. 데이터 구조: 하나의 자치구 정보를 담는 클래스
public class DistrictData
{
    public string Name;    // 지역명 (A열, 인덱스 0)
    public int Population; // 인구수 (B열, 인덱스 1)
    public float Area;     // 면적 (C열, 인덱스 2)
    public float Density;  // 인구밀도 (D열, 인덱스 3)
}

// 2. 데이터 관리자 스크립트
public class DataManager : MonoBehaviour
{
    // static으로 선언하여 어디서든 쉽게 접근 가능하게 합니다.
    public static DataManager Instance;

    public List<DistrictData> AllDistrictsData { get; private set; } = new List<DistrictData>();
    public float MaxDensity { get; private set; } // 모든 밀도 중 가장 큰 값

    void Awake()
    {
        // 싱글톤 패턴: 단 하나의 DataManager만 존재하도록 합니다.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 파괴되지 않게 (선택 사항)
            LoadAndParseData(); // 데이터 로딩 시작
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadAndParseData()
    {
        // 텍스트 파일(CSV) 로드
        TextAsset dataAsset = Resources.Load<TextAsset>("seoul_density"); 
        if (dataAsset == null)
        {
            Debug.LogError("CSV 파일을 Resources 폴더에서 찾을 수 없습니다! (파일명: seoul_density.txt 확인)");
            return;
        }

        // 전체 텍스트를 줄(Row) 단위로 분리 (줄바꿈 문자로 분리)
        string[] dataRows = dataAsset.text.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        // 각 줄(자치구) 데이터 파싱
        foreach (string row in dataRows)
        {
            // 한 줄을 쉼표(,) 기준으로 컬럼(Column) 단위로 분리
            string[] columns = row.Split(',');

            // 데이터 수가 4개 이상인지 확인
            if (columns.Length >= 4)
            {
                DistrictData district = new DistrictData();
                
                district.Name = columns[0].Trim(); 
                
                // C# 형변환 (TryParse를 사용하여 오류 방지)
                int.TryParse(columns[1].Trim(), out district.Population);
                float.TryParse(columns[2].Trim(), out district.Area);
                float.TryParse(columns[3].Trim(), out district.Density);

                AllDistrictsData.Add(district);
            }
        }
        
        // MaxDensity 계산 (시각화 크기/색상 정규화의 기준)
        if (AllDistrictsData.Count > 0)
        {
            MaxDensity = AllDistrictsData.Max(d => d.Density);
        }
        
        Debug.Log($"[DataManager] 총 {AllDistrictsData.Count}개 구 데이터 로드 완료. MaxDensity: {MaxDensity:F2}");
    }
}