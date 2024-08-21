using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
  
    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
        {
            if (IsHaveKitchenObject() == false)
            {
                TransferKitchenObject(player, this);
            }
            else
            {

            }
        }
        else
        {
            if (IsHaveKitchenObject() == false)
            {


            }
            else
            {
                TransferKitchenObject(this, player);
            }
        }

    }
}
