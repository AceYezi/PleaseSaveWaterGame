using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TrashList")]
public class TrashReciptListSO : ScriptableObject
{
    public List<TrashReciptSO> list;

    public KitchenObjectSO GetOutPut(KitchenObjectSO input)
    {
        foreach (TrashReciptSO recipt in list)
        {
            Debug.Log("Checking input: " + recipt.input.objectName + " against " + input.objectName);
            if (recipt.input == input)
            {
                return recipt.output;
            }
        }
        Debug.Log("No match found for input: " + input.objectName);
        return null;
    }

}
