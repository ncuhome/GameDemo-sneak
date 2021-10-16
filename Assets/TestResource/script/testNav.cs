using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TestScript {
    public class testNav : MonoBehaviour
    {
        public List<Transform> targetList;
        private int targetIndex;
        NavMeshAgent agent;
        bool isTimerRun = false;
        List<NavMeshPath> pathList;
        void Start()
        {
            GameObject myPoint = new GameObject(gameObject.name + "Spawn");
            myPoint.transform.position = this.transform.position;
            targetList.Add(myPoint.transform);
            targetIndex = targetList.Count-1;
            agent = GetComponent<NavMeshAgent>();
            agent.destination = targetList[targetIndex].position;
        }

        void Update() {
            if (!isTimerRun&&agent.remainingDistance < 0.2f) {
                StartCoroutine(timer());
            }
        }

        IEnumerator timer() {
            isTimerRun = true;
            float times = 0.5f;
            while (times > 0) {
                times -= Time.deltaTime;
                yield return 0;
            }
            Debug.Log(agent.SetDestination (targetList[targetIndex].position));
            targetIndex = targetIndex + 1;
            targetIndex %= targetList.Count;
            isTimerRun=false;
        }
    }
}

