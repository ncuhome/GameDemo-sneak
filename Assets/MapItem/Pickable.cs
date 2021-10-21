using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public ItemType type;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 8) {
            ItemManager.Instace.AddItem(type);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision) {
       
    }
}
