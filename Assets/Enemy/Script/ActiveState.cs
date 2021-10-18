using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveState : MonoBehaviour
{
    [Tooltip("�۲�ʱ����")]
    public float watchWaitTime = 0.3f;
    [Tooltip("�۲�Ļ���(���Զ�����Pi)")]
    public float watchArc = 0.5f;
    [Tooltip("�۲����")]
    public float watchLenght = 2;
    float sqrtWatchLenght;

    /// <summary>
    /// ��̬����
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
                        //�˴������˷����������Ұ,targetΪ�˷������transform
                        //TODO ��������˷����岻ͬ�趨��ͬ״̬
                        break;
                    }
                }
            }
            timer = watchWaitTime;
        }
    }
}
