using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveState : MonoBehaviour
{
    [Tooltip("�۲�ʱ����")]
    public float watchWaitTime = 0.3f;
    [Tooltip("�۲�ĽǶ�")]
    public float watchAngle = 60f;
    [Tooltip("�۲�ʱʹ�õ�����������Խ��Ч��Խ������Խ��")]
    public int watchLine = 5;
    [Tooltip("�۲����")]
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
            //�������߽��й۲� TODO �޸ı��۲�ʱ��
            RaycastHit[] hits;
            //����ǶȾ�����ôд�ɡ�
            //��������˵
            //�������� BUG HERE
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
                    //����Ȥ֮��Ӧ����ʲô�أ���Ҳ��֪����
                    break;
                }else{
                    eulerAngle.y += offsetAngle;
                }
            }
            timer = watchWaitTime;
        }
    }
}
