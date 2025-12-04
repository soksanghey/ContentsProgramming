using UnityEngine;
using System.Collections;
public class MyCoroutine : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(MyCoroutineMethod());
            }

  IEnumerator MyCoroutineMethod()
{
    Debug.Log("코루틴 시작");
    
    yield return new WaitForSeconds(2f);  // 2초 대기
    
    Debug.Log("2초 후 실행");
}
  
}
