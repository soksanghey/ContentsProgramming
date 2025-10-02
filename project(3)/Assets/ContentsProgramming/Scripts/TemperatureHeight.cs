using UnityEngine;

public class TemperatureHeight : MonoBehaviour
{
    public float temperature = 20.0f; //온도

    public float maxHeight = 5.0f;  // 높이

    private Transform myTransform;

    void Update()
    {

        float height = (temperature / 50.0f) * maxHeight;
        transform.localScale = new Vector3(1, height, 1);

        myTransform = GetComponent<Transform>();


        myTransform.localScale = new Vector3(transform.localScale.x, temperature, transform.localScale.z);

        Debug.Log("온도에 따른 높이 설정 완료 " + height);
    }
}
