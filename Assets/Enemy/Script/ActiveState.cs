using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveState : MonoBehaviour
{
    [Tooltip("观察时间间隔")]
    public float watchWaitTime = 0.3f;
    [Tooltip("观察的角度")]
    public float watchAngle = 60f;
    [Tooltip("观察时使用的射线数量，越高效果越好性能越差")]
    public int watchLine = 5;
    [Tooltip("观察距离")]
    public float watchLenght = 2;

    const int layer = (1 << 7) | (1 << 8);
    EnemyNavObj navObj;

    private void Start() {
        if (watchLine <= 1) {
            watchLine = 2;
        }
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
            //发射射线进行观察 TODO 修改被观察时间
            RaycastHit[] hits;
            //这个角度就先这么写吧…
            //出问题再说
            //出问题了 BUG HERE
            Vector3 eulerAngle = transform.forward;
            eulerAngle.y -= watchAngle / 2;
            float offsetAngle = watchAngle / (watchLine-1);
            bool isInterest = false;
            for(int i = 0; i < watchLine; ++i) {
                hits = Physics.RaycastAll(transform.position, eulerAngle, watchLenght, layer);
                Debug.DrawRay(transform.position, eulerAngle, Color.red, watchWaitTime);
                foreach (RaycastHit hit in hits){
                    int objLayer = hit.collider.gameObject.layer;
                    Debug.Log(hit.collider.gameObject);
                    if (objLayer == 1<<7) {
                        break;
                    } else {
                        isInterest = true;
                        navObj.SetInterestPoint(hit.collider.transform.position);
                        break;
                    }
                }
                if (isInterest) {
                    //感兴趣之后应该做什么呢，我也不知道啊
                    break;
                }else{
                    eulerAngle.y += offsetAngle;
                }
            }
            timer = watchWaitTime;
        }
    }
}
