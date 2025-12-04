using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int SpawnAmount;// 한번 스폰할 때 몇개 스폰할 것인지.
    public float spawnRate;// 스폰 간격
    public GameObject prefab; // 스폰할 오브젝트

    private float _spawnTimer;

    void Update()
    {
        _spawnTimer += Time.deltaTime;//델타타임-> 모니터가 한 장면을 만들기 위해 걸리는 시간

        if (_spawnTimer >= spawnRate)
        {
            //여기 스폰
            for (var i = 0; i <SpawnAmount; i ++)
            {
                var instance = Instantiate(prefab);
                instance.transform.position = new Vector3(i, 0, 0);

            }
            _spawnTimer = 0;
        }
    }
}
