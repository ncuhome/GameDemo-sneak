using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMyToInterest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NPCManager.Instace.interestObjectList.Add(transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
