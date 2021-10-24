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
    float attackColdDown = 0;
    void Update()
    {
        if (attackColdDown > 0) {
            attackColdDown -= Time.deltaTime;
        }
        if (attackColdDown<=0&&Input.GetKeyDown(KeyCode.Space)) {
            attackColdDown = 0.5f;
            Ray ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(transform.position, transform.forward * attackLenght, Color.red,1f);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, attackLenght, layer)) {
                
                if (hit.transform.gameObject.GetComponent<ActiveState>().BeAttack(transform.position)) attackColdDown -= 0.2f;
            }
        }
    }
}
