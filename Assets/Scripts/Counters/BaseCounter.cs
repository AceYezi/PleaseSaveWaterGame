using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : KitchenObjectHolder
{
    [SerializeField] private GameObject selectedCounter;

    public virtual void Interact(Player player)
    {
        Debug.LogWarning("you don't rewrite the interact method");
    }

    public virtual void InteractOperate(Player player)
    {

    }
    public virtual void InteractOperate2(Player player)
    {

    }

    public void SelectCounter()
    {
        selectedCounter.SetActive(true);
    }
    public void CancleSelect()
    {
        selectedCounter.SetActive(false);
    }
   
}
