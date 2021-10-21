using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType{
    COIN,
    KEY
}

//临时的，更好的写法有，但没必要在demo写

public class ItemManager : MonoBehaviour
{
    List<int> ItemLeft = new List<int>();
    public List<GameObject> TextUI;
    List<Text> texts = new List<Text>();
    static public ItemManager Instace=null;

    private void Start() {
        if (Instace == null) {
            Instace = this;
        } else {
            Debug.LogWarning("多个" + gameObject.name);
            Destroy(gameObject);
        }
        foreach(GameObject obj in TextUI) {
            Text text = obj.GetComponent<Text>();
            if (text != null) {
                texts.Add(text);
                ItemLeft.Add(0);
            } else {
                Debug.LogError(obj + "Not Have Text Component");
            }
        }
    }

    private void OnDestroy() {
        if (Instace == this) Instace = null;
    }

    public void AddItem(ItemType itemType) {
        ItemLeft[(int)itemType]++;
        UpdateText(itemType);
    }

    public bool AskItem(ItemType itemType) {
        return ItemLeft[(int)itemType] > 0;
    }

    public bool UseItem(ItemType itemType) {
        if(ItemLeft[(int)itemType] > 0) {
            ItemLeft[(int)itemType]--;
            UpdateText(itemType);
            return true;
        }
        return false;
    }

    void UpdateText(ItemType itemType) {
        texts[(int)itemType].text = Enum.GetName(typeof(ItemType), itemType)+":"+ItemLeft[(int)itemType];
    }
}
