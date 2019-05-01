using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemsGenerator : MonoBehaviour
{
    public List<string> shopList;
    public static int listCount;
    public GameObject itemPrefab;
    public static float positionDifference = 150f;
    float currrentItemSpawnPosition = 0f;

    void Awake() {
        GenerateItems();
        listCount = shopList.Count;
    }

    void GenerateItems() {
        foreach(string item in shopList) {
            GenerateItem(item);
        }
    }

    void GenerateItem(string name) {
        GameObject item = Instantiate(itemPrefab);
        Text text = item.GetComponentInChildren<Text>();
        item.transform.SetParent(transform);
        text.text = name;
        item.transform.localPosition = new Vector3(currrentItemSpawnPosition, 0, 0);
        currrentItemSpawnPosition += positionDifference;
    }
}
