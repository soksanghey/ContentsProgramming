
using UnityEngine;

public class DiatanceCalculator : MonoBehaviour
{
    public GameObject targetGameObject; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var targetGameObjectPosition = targetGameObject.transform.position;
        var distance = Vector3.Distance(transform.position, targetGameObjectPosition);

        if (distance > 3f)
        {

            Debug.Log("3m 밖으로 나갔습니다.");
            targetGameObject.GetComponent <MeshRenderer>().material.color = Color.red;

        }
        else if (distance is < 3f and > 2f)
        {
            Debug.Log("게임오프젝트가" + distance + "m 만큼 떨어져있습니다. 적정거리 입니다.");
            targetGameObject.GetComponent<MeshRenderer>().material.color = Color.green;

        }
        else
        {
            Debug.Log("게임오프젝트가" + distance + "m 만큼 떨어져있습니다.");
             targetGameObject.GetComponent<MeshRenderer>().material.color = Color.white;


        }
    }
}
