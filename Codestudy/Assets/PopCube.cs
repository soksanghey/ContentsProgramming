using UnityEngine;

public class PopCube : MonoBehaviour
{
    public GameObject targetGameObject;

    public void Pop()
    {
        var targetGameObjectPosition = targetGameObject.transform.position;
        var distance = Vector3.Distance(targetGameObjectPosition, transform.position);

        if (distance > 2f && distance < 3f)
        {
            targetGameObject.SetActive(false);

        }
        else
        {
            Debug.Log("잘못된 위치이빈다.");
        }
    }
}
