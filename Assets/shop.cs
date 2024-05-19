using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class shop : MonoBehaviour
{
    public Item[] tierI;
    public Item[] tierII;
    public Item[] tierIII;
    public float tierIchance;
    public float tierIIchance;
    public float tierIIIchance;
    public ItemBox itemBoxI;
    public ItemBox itemBoxII;
    public ItemBox itemBoxIII;
    public GameObject panel;
    public static bool isActive;

    public static PlayerController playerController;
    public static WeaponManager weaponManager;

    private void Awake()
    {
        weaponManager = FindFirstObjectByType<WeaponManager>();
        playerController = FindFirstObjectByType<PlayerController>();
    }

    void Start()
    {
        FillItems();
        isActive = false;
    }

    public void SwitchShop()
    {
        if (isActive == true)
        {
            panel.SetActive(false);
            Time.timeScale = 1;
            isActive = false;
        }
        else
        {
            ShopActive();
        }
    }


    public void FillItems()
    {
        FillItemBox(itemBoxI);
        FillItemBox(itemBoxII);
        FillItemBox(itemBoxIII);
    }
    public void ShopActive()
    {
        Time.timeScale = 0;
        panel.SetActive(true);
        isActive = true;
    }

    private void FillItemBox(ItemBox box)
    {
        float chance = Random.Range(1, 101);
        if (chance <= tierIchance)
        {
            int randomIndex = Random.Range(0, tierI.Length);
            Item item = tierI[randomIndex];
            box.SetItem(item);
        }
        else if (chance <= tierIchance + tierIIchance)
        {
            int randomIndex = Random.Range(0, tierII.Length);
            Item item = tierII[randomIndex];
            box.SetItem(item);
        }
        else if (chance <= tierIchance + tierIIchance + tierIIIchance)
        {
            int randomIndex = Random.Range(0, tierIII.Length);
            Item item = tierIII[randomIndex];
            box.SetItem(item);
        }
    }

    void Update()
    {

    }
}
[System.Serializable]
public class Item
{
    public string title { get; private set; }
    public Sprite image { get; private set; }

    public float price;
    public string description;
    public GameObject prefab;

    public void Init()
    {
        image = prefab.GetComponentInChildren<SpriteRenderer>().sprite;
        title = prefab.name;
    }
}
 
[System.Serializable]

public class ItemBox
{
    public Image image;
    public TMP_Text title;
    public TMP_Text description;
    public TMP_Text price;
    public Button button;

    private Item item;

    public void SetItem(Item newItem)
    {
        newItem.Init();

        item = newItem;
        image.sprite = item.image;
        title.text = item.title;
        description.text = item.description;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(click);
        price.text = item.price.ToString();
    }

    private void click()
    {
        bool result = shop.playerController.CheckMoney(item.price);
        if (result == true)
        {
          shop.playerController.ChangeMoney(-item.price);

            shop.weaponManager.TakeWeapon(item.prefab);
        }
        else
        {
            Debug.Log("net deneg ti bomj");
        }

      
    }
}

