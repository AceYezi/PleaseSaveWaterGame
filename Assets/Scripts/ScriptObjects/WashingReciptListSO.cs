using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "WashingList")]
public class WashingReciptListSO : ScriptableObject
{
    public List<WashingReciptSO> list;

    public KitchenObjectSO GetOutPut(KitchenObjectSO input)
    {
        foreach (WashingReciptSO recipt in list)
        {
            if (recipt.input == input)
            {
                return recipt.output;
            }
        }
        return null;
    }

    public bool TryGetWashingRecipe(KitchenObjectSO input, out WashingReciptSO washingRecipt)
    {
        foreach (WashingReciptSO recipt in list)
        {
            if (recipt.input == input)
            {
                washingRecipt = recipt; return true;
            }
        }
        washingRecipt = null;
        return false;
    }

}