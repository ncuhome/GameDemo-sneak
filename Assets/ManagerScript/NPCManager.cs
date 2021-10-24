using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 管理可移动物体的分类
/// </summary>
public class NPCManager : MonoBehaviour
{
    static public NPCManager Instace = null;
    public List<Transform> interestObjectList = new List<Transform>();
    public List<Transform> guardList = new List<Transform>();
    void Start()
    {
        if (Instace == null) {
            Instace = this;
        } else {
            Debug.LogWarning("多个" + gameObject.name);
            Destroy(gameObject);
        }
    }

    void OnDestroy() {
        if (Instace == this) Instace = null;
    }

    public void AddToGuardList(Transform t) {
        guardList.Add(t);
    }

    public void AddToInterestObject(Transform t) {
        interestObjectList.Add(t);
    }

    public void ClearInterestList() {
        ClearNullInList(interestObjectList);
    }

    public void ClearGuardList() {
        ClearNullInList(guardList);
    }

    void ClearNullInList<T> (List<T> list) {
        int indexA = 0, indexB = 0;
        while (indexB < list.Count) {
            if (list[indexA] != null) {
                indexA++; indexB++;
            } else {
                if (list[indexB] != null) {
                    list[indexA] = list[indexB];
                    list[indexB] = default(T);
                } else {
                    indexB++;
                }
            }
        }
        list.RemoveRange(indexA, indexB - indexA);
    }
}
