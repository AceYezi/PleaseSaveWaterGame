using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KitchenObjectHolder : MonoBehaviour
{

    public static event EventHandler OnDrop;
    public static event EventHandler OnPickup;

    [SerializeField] private Transform holdPoint;
    private KitchenObject kitchenObject;

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        if (this.kitchenObject != kitchenObject && kitchenObject != null && this is BaseCounter)
        {
            OnDrop?.Invoke(this, EventArgs.Empty);
        }
        else if (this.kitchenObject != kitchenObject && kitchenObject != null && this is Player)
        {
            OnPickup?.Invoke(this, EventArgs.Empty);
        }
        this.kitchenObject = kitchenObject;
        kitchenObject.transform.localPosition = Vector3.zero;


    }

    public Transform GetHoldPoint()
    {
        return holdPoint;
    }

    public bool IsHaveKitchenObject()
    {
        return kitchenObject != null;
    }


    public void TransferKitchenObject(KitchenObjectHolder sourceHolder, KitchenObjectHolder targetHolder)
    {
        if (sourceHolder.GetKitchenObject() == null)
        {
            Debug.LogWarning("原持有者上没有食材，转移失败");
            return;
        }
        if (targetHolder.GetKitchenObject() != null)
        {
            Debug.LogWarning("目标持有者上有食材，转移失败");
            return;
        }

        targetHolder.AddKitchenObject(sourceHolder.GetKitchenObject());
        sourceHolder.ClearKitchenObject();
    }

    public void AddKitchenObject(KitchenObject kitchenObject)
    {
        kitchenObject.transform.SetParent(holdPoint);
        kitchenObject.transform.localPosition = Vector3.zero;
        SetKitchenObject(kitchenObject);
    }

    public void DestroyKitchenObject()
    {
        Destroy(kitchenObject.gameObject);
        ClearKitchenObject();
    }
    public void CreateKitchenObject(GameObject kitchenObjectPrefab)
    {
        KitchenObject kitchenObject = GameObject.Instantiate(kitchenObjectPrefab, GetHoldPoint()).GetComponent<KitchenObject>();
        SetKitchenObject(kitchenObject);
    }

    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }

    public static void ClearStaticData()
    {
        OnDrop = null;
        OnPickup = null;
    }

}
