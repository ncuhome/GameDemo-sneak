using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveState : MonoBehaviour
{
    [Tooltip("观察时间间隔")]
    public float watchWaitTime = 0.3f;
    [Tooltip("观察的弧度(会自动乘以Pi)")]
    public float watchArc = 0.5f;
    [Tooltip("观察距离")]
    public float watchLenght = 2;
    float sqrtWatchLenght;

    /// <summary>
    /// 静态区域
    /// </summary>
    public static List<Transform> interestObjectList=new List<Transform>();

    const int layer = (1 << 7) | (1 << 8);
    EnemyNavObj navObj;

    private void Start() {
        watchArc *= Mathf.PI;
        sqrtWatchLenght = watchLenght * watchLenght;
        navObj = GetComponent<EnemyNavObj>();
        StartCoroutine(WatchTimer());
    }

    IEnumerator WatchTimer() {
        float timer = watchWaitTime;
        while (true) {
            while (timer > 0) {
                timer -= Time.deltaTime;
                yield return 0;
            }
            foreach(Transform target in interestObjectList) {
                Vector3 dir = target.position - transform.position;
                if ((target.position - transform.position).sqrMagnitude < sqrtWatchLenght &&
                    Vector3.Dot(transform.forward, dir.normalized) > 0 &&
                    Mathf.Acos(Vector3.Dot(transform.forward,dir.normalized))< watchArc) {
                    if(!Physics.Linecast(transform.position,target.position,(1 << 7))) {
                        navObj.SetInterestPoint(target.position);
                        Debug.Log(target.gameObject.name);
                        //此处代表兴奋物体进入视野,target为兴奋物体的transform
                        //TODO 根据造成兴奋物体不同设定不同状态
                        break;
                    }
                }
            }
            timer = watchWaitTime;
        }
    }
}
