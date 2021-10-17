using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavObj : MonoBehaviour
{
    List<Vector3> patrolList=new List<Vector3>();
    int patrolListIndex;
    bool isTimerRun;
    Vector3 interestPoint;
    bool isPatrol;
    NavMeshAgent agent;

    [Tooltip("Ѳ��·�߸����壬����������������������ֵ���˳��Ѳ�ߣ���ţ�")]
    public GameObject patrolPointObj;
    [Tooltip("Ѳ����ĳ���ص�ͣ�����")]
    public float patrolWaitTime=1f;
    [Tooltip("����Ȥ��ͣ�����")]
    public float interestWaitTime=2f;

    //���Ȱ뾶��С
    const float PATROL_POINT_RADIUS = 0.2f;

    void Start()
    {
        isTimerRun = false;
        agent = GetComponent<NavMeshAgent>();
        isPatrol = true;
        patrolList.Add(transform.position);
        for(int i=0;i<patrolPointObj.transform.childCount; ++i) {
            Transform point = patrolPointObj.transform.GetChild(i);
            patrolList.Add(point.position);
        }
        patrolListIndex = 0;
        agent.SetDestination(patrolList[patrolListIndex]);
    }

    void FixedUpdate()
    {
        //�˷�״̬/Ѳ��״̬
        if (!isPatrol) {
            if(!isTimerRun && agent.remainingDistance < PATROL_POINT_RADIUS) {
                StartCoroutine(interestWaitTimer());
            }
        }
        else if (!isTimerRun && agent.remainingDistance < PATROL_POINT_RADIUS) {
            StartCoroutine(patrolWaitTimer());
        }
    }

    public bool SetInterestPoint(Vector3 point) {
        NavMeshPath path = new NavMeshPath();
        bool canArrive = agent.CalculatePath(point, path);
        if (canArrive) {
            agent.SetPath(path);
            isPatrol = false;
        }
        return canArrive;
    }

    IEnumerator patrolWaitTimer() {
        isTimerRun = true;
        float times = patrolWaitTime;
        while (times > 0) {
            times -= Time.deltaTime;
            yield return 0;
        }
        if (isPatrol) {
            agent.SetDestination(patrolList[patrolListIndex]);
            patrolListIndex = patrolListIndex + 1;
            patrolListIndex %= patrolList.Count;
        }
        isTimerRun = false; 
    }

    IEnumerator interestWaitTimer() {
        isTimerRun = true;
        float times = interestWaitTime;
        while (times > 0) {
            times -= Time.deltaTime;
            yield return 0;
        }
        agent.SetDestination(patrolList[patrolListIndex]);
        isPatrol = true;
        isTimerRun = false;
    }
}
