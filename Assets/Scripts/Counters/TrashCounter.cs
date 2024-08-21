using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnObjectTrashed;

    [SerializeField] private TrashReciptListSO trashReciptList;

    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
        {
            player.DestroyKitchenObject();
            OnObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }

    public override void InteractOperate(Player player)
    {

        if(player.IsHaveKitchenObject())
        {

            KitchenObject playerKitchenObject = player.GetKitchenObject();
            KitchenObjectSO input = playerKitchenObject.GetKitchenObjectSo();


            KitchenObjectSO output = trashReciptList.GetOutPut(input);

            if (output != null)
            {

                player.DestroyKitchenObject();
                player.CreateKitchenObject(output.prefab);
            }
            else
            {
                Debug.Log("No output found for: " + input.objectName);
            }
        }
    }

    public static void ClearStaticData()
    {
        OnObjectTrashed = null;
    }
}
