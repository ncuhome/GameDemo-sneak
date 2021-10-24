using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attackLenght=1.0f;
    /// <summary>
    /// µ–»ÀLayer
    /// </summary>
    const int layer = 1 << 9;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.DrawRay(transform.position, transform.forward * attackLenght, Color.red);
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, attackLenght, layer)) {

            }
        }
    }
}
