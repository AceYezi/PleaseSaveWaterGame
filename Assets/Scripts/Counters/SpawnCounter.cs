using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnCounter : BaseCounter
{
    [SerializeField] private Transform clearPoint;
    private Queue<GameObject> itemsQueue = new Queue<GameObject>();
    private int maxItems = 1;

    public bool IsFull()
    {
        return itemsQueue.Count >= maxItems;
    }

    public bool CanReceiveItem()
    {
        return itemsQueue.Count < 1;
    }

    public void ReceiveItem(GameObject item)
    {
        KitchenObject kitchenObject = item.GetComponent<KitchenObject>();
        if (kitchenObject == null)
        {
            Debug.LogWarning("Received GameObject does not have a KitchenObject component.");
            return;
        }

        if (itemsQueue.Count < maxItems)
        {
            itemsQueue.Enqueue(item);
            PositionItems();
        }
        else
        {
            Debug.LogWarning("Clear counter is full.");
        }
    }

    private void PositionItems()
    {
        float offsetY = 0.1f; // Adjust as needed
        int index = 0;
        foreach (GameObject item in itemsQueue)
        {
            item.transform.position = clearPoint.position + Vector3.up * offsetY * index;
            index++;
        }
    }

    [System.Obsolete]
    public GameObject RemoveItem()
    {
        if (itemsQueue.Count == 0) return null;
        GameObject item = itemsQueue.Dequeue();
        PositionItems();

        // Notify the deliver counter that an item has been removed
        DeliveryCounter deliverCounter = FindObjectOfType<DeliveryCounter>();
        if (deliverCounter != null)
        {
            deliverCounter.NotifyClearCounterEmptied();
        }

        return item;
    }

    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
        {
            Debug.LogWarning("Player already has a kitchen object.");
        }
        else
        {
            GameObject item = GetItem();
            if (item != null)
            {
                player.AddKitchenObject(item.GetComponent<KitchenObject>());
                PositionItems();
            }
            else
            {
                Debug.LogWarning("Clear counter is empty.");
            }
        }
    }

    public GameObject GetItem()
    {
        if (itemsQueue.Count > 0)
        {
            return itemsQueue.Dequeue();
        }
        return null;
    }


}
