using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ShopItemsGenerator : MonoBehaviour
{

    public Text TotalFish;

    public static ShopItemsGenerator Instance;

    [System.Serializable]
    public class ShopItem {
        public string itemName;
        public int itemCost;
        public bool bought;
    }

    private List<GameObject> objects;

    [SerializeField]
    public List<ShopItem> shopList;

    public static int listCount;
    public GameObject itemPrefab;
    public static float positionDifference = 150f;
    float currrentItemSpawnPosition = 0f;

    void Awake() {
        objects = new List<GameObject>();
        listCount = shopList.Count;
        Instance = this;
    }

    void Start() {
        GenerateItems();
    }

    void GenerateItems() {
        foreach(ShopItem item in shopList) {
           objects.Add(GenerateItem(item));
        }
        RefreshUI();
    }

    //Function Generating objects defined in inspector in Unity 
    GameObject GenerateItem(ShopItem it) {
        GameObject item = Instantiate(itemPrefab);

        item.transform.SetParent(transform);
        Text[] texts = item.GetComponentsInChildren<Text>();
        texts[0].text = it.itemName;
        if(it.bought == false) {
            texts[1].text = string.Format("COSTS: {0}", it.itemCost.ToString());
        }
        item.transform.localPosition = new Vector3(currrentItemSpawnPosition, 0, 0);
        currrentItemSpawnPosition += positionDifference;

        return item;
    }

    //Function which refreshes UI after Buying some items
    public void RefreshUI() {
        int i = 0;
        foreach (GameObject item in objects) {
            Text[] texts = item.GetComponentsInChildren<Text>();
            if (GameState.upgradesBought[i] == true) {
                texts[1].text = string.Format("BOUGHT");
            }
            i++;
            item.transform.localScale = new Vector3(1, 1, 1);
        }
        TotalFishInGameUpdate.updateFish();
    }
}
