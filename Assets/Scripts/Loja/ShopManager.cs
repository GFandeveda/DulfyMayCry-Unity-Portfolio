using UnityEngine;
using TMPro;
public class ShopManager : MonoBehaviour
{
    [SerializeField] private Item bigHealthStone;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private Item smallHealthStone;

    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Item blueOrb;
    [SerializeField] private TMP_Text redOrbText;
    [SerializeField] private RedOrbs playerRedOrbs;

    [Header("UI")]
    [SerializeField] private GameObject shopPanel;

    [SerializeField] private GameObject interactText;
    private bool playerNearby;
    private bool shopOpen;

    private void Start()
    {
        
        shopPanel.SetActive(false);
        interactText.SetActive(false);
    }

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.R))
        {
            ToggleShop();
        }
    }

    private void ToggleShop()
    {
        shopOpen = !shopOpen;

        shopPanel.SetActive(shopOpen);

        if (shopOpen)
        {
            UpdateRedOrbText();
        }

        Cursor.visible = shopOpen;
        Cursor.lockState = shopOpen
            ? CursorLockMode.None
            : CursorLockMode.Locked;

        Time.timeScale = shopOpen ? 0f : 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            interactText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            interactText.SetActive(false);

            if (shopOpen)
            {
                ToggleShop();
            }
        }
    }
    private void UpdateRedOrbText()
    {
        redOrbText.text = $"Red Orbs: {playerRedOrbs.GetRedOrbs()}";
    }



    public bool BuyItem(Item item)
    {
        if (!playerRedOrbs.SpendRedOrbs(item.price))
        {
            Debug.Log("Red Orbs insuficientes!");
            return false;
        }

        if (!playerInventory.AddItem(item))
        {
            playerRedOrbs.AddRedOrbs(item.price);
            Debug.Log("Inventário cheio!");
            return false;
        }

        UpdateRedOrbText();

        Debug.Log(item.itemName + " comprado!");

        return true;
    }
    public void BuyBlueOrb()
    {
        Debug.Log("playerRedOrbs: " + playerRedOrbs);
        Debug.Log("blueOrb: " + blueOrb);
        Debug.Log("playerHealth: " + playerHealth);

        if (!playerRedOrbs.SpendRedOrbs(blueOrb.price))
        {
            Debug.Log("Red Orbs insuficientes!");
            return;
        }

        playerHealth.IncreaseMaxHealth();

        UpdateRedOrbText();
    }
}