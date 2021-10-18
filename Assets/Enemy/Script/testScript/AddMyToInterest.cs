using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMyToInterest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ActiveState.interestObjectList.Add(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
