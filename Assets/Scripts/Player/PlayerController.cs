using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private GameObject inputManager;

    private InventoryManager inventoryManager;
    
    private PlayerInput playerInput_;
    private InputAction inventoryAction;

    public bool isPaused;

    private void Awake()
    {
        playerInput_ = inputManager.GetComponent<PlayerInput>();

        inventoryAction = playerInput_.actions["InventoryToggle"];
    }

    private void Start()
    {
        inventoryManager = new InventoryManager();
        uiInventory.SetInventory(inventoryManager);

        uiInventory.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        
        if(itemWorld != null)
        {
            inventoryManager.AddItem(itemWorld.GetItem());
            itemWorld.SelfDestroy();
        }

        if(collision.gameObject.tag == "Checkpoint")
        {
            Checkpoint.checkPointInstance.OpenCraftingMenu();
        }
    }

    private void OnEnable()
    {
        

        inventoryAction.Enable();
        inventoryAction.performed += Pause;
    }

    private void OnDisable()
    {
        inventoryAction.Disable();
    }

    public void Pause(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            OpenInventory();
        }
        else
        {
            CloseInventory();
        }
    }

    public void OpenInventory()
    {
        Time.timeScale = 0f;
        uiInventory.gameObject.SetActive(true);
        isPaused = true;
    }

    public void CloseInventory()
    {
        Time.timeScale = 1f;
        uiInventory.gameObject.SetActive(false);
        isPaused = false;
    }


}
