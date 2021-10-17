using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavObj : MonoBehaviour
{
    List<Vector3> patrolList=new List<Vector3>();
    int patrolListIndex;
    bool isTimerRun;
    List<Vector3> interestList=new List<Vector3>();
    bool isExcited;
    NavMeshAgent agent;

    [Tooltip("巡逻路线父物体，将会以其中子物体的命名字典序顺序巡逻（大概）")]
    public GameObject patrolPointObj;
    [Tooltip("巡逻在某个地点停留多久")]
    public float waitTime;

    //巡逻地点半径大小
    const float PATROL_POINT_RADIUS = 0.2f;

    void Start()
    {
        isTimerRun = false;
        agent = GetComponent<NavMeshAgent>();
        isExcited = false;
        patrolList.Add(transform.position);
        for(int i=0;i<patrolPointObj.transform.childCount; ++i) {
            Transform point = patrolPointObj.transform.GetChild(i);
            patrolList.Add(point.position);
        }
        patrolListIndex = 0;
        agent.SetDestination(patrolList[patrolListIndex]);
    }

    void Update()
    {
        if (isExcited) {
            //TODO 发现兴奋点后作出的反应
        }
        else if (!isTimerRun && agent.remainingDistance < PATROL_POINT_RADIUS) {
            StartCoroutine(WaitTimer());
        }
    }

    IEnumerator WaitTimer() {
        isTimerRun = true;
        float times = waitTime;
        while (times > 0) {
            times -= Time.deltaTime;
            yield return 0;
        }
        Debug.Log(agent.SetDestination(patrolList[patrolListIndex]));
        patrolListIndex = patrolListIndex + 1;
        patrolListIndex %= patrolList.Count;
        isTimerRun = false; 
    }
}
