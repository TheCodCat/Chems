using UnityEngine;
using Zenject;

public class InventorySystem : MonoBehaviour, IInitializable
{
    private InventorySlot[] slots;
    [SerializeField] private InventoryItem inventoryPrefab;
    [SerializeField] private InventoryItemObj inventoryObj;
    [Inject]
    public void Construct(InventorySlot[] slots)
    {
        this.slots = slots;
    }
    public void Initialize()
    {
        AddItem(inventoryObj);
        AddItem(inventoryObj);
        AddItem(inventoryObj);
        AddItem(inventoryObj);
        AddItem(inventoryObj);
    }

    public bool AddItem(InventoryItemObj inventoryItemObj)
    {
        foreach (var item in slots)
        {
            var slotItem = item.GetComponentInChildren<InventoryItem>();
            if (slotItem != null && slotItem.itemObj == inventoryItemObj &&
                slotItem.count < slotItem.itemObj.stackCount
                && slotItem.itemObj.isStackable == true)
            {
                slotItem.count += 1;
                slotItem.RefrashCount();
                return true;
            }
        }

            foreach (var item in slots)
        {
            var slotItem = item.GetComponentInChildren<InventoryItem>();
            if(slotItem == null)
            {
                SpawnNewItem(inventoryItemObj, item);
                return true;
            }
        }

        return false;
    }

    public void SpawnNewItem(InventoryItemObj inventoryItemObj, InventorySlot inventorySlot)
    {
        var newItem = Instantiate(inventoryPrefab, inventorySlot.transform);
        newItem.Construct(inventoryItemObj);
    }
}
