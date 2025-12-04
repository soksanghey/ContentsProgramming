using UnityEngine;

public class CubeCreator : MonoBehaviour
{
    public GameObject[] cubePrefabs;
    public void CreateCube()
    {
        var randomIndex = Random.Range(0, cubePrefabs.Length);

        Instantiate(cubePrefabs[randomIndex]); 
    }
}
