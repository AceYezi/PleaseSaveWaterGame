using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{

    public static event EventHandler OnWash;

    [SerializeField] private WashingReciptListSO washingReciptList;

    [SerializeField] private ProgressBarUI progressBarUI;
    [SerializeField] private WaterBarUI waterBarUI;

    [SerializeField] private WaterFaceUP water;
    [SerializeField] private GameObject waterFall;
    [SerializeField] private WarningControl warningControl;

    private int washingCount = 0;


    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
        {
            if (IsHaveKitchenObject() == false)
            {
                washingCount = 0;
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

                water.StartAndEndRising();

            }
            else
            {
                TransferKitchenObject(this, player);
                progressBarUI.Hide();
            }
        }
    }
    public override void InteractOperate(Player player)
    {
      if(IsHaveKitchenObject())
        {
            if( washingReciptList.TryGetWashingRecipe(GetKitchenObject().GetKitchenObjectSo(), out WashingReciptSO washingRecipt))
            {
                if(water.GetTFactor()!=0)
                {
                    Wash();

                    progressBarUI.UpdateProgress((float)washingCount / washingRecipt.washingCountMax);


                    if(washingCount == washingRecipt.washingCountMax)
                    {
                        DestroyKitchenObject();
                        CreateKitchenObject(washingRecipt.output.prefab);
                        water.DecreaseWaterLevel(0.2f);
                    }
                }
                else
                {
                    print("cant wash");
                }


            }

        }
    }

    public override void InteractOperate2(Player player)
    {
        waterFall.SetActive(false);
        water.Reset();
        warningControl.StopWarning();
    }
    private void Wash()
    {
        washingCount++;
        OnWash?.Invoke(this,EventArgs.Empty);
    }

    public static void ClearStaticDate()
    {
        OnWash = null;
    }

    private void Update()
    {
        waterBarUI.UpdateWaterProgress(water.GetTFactor());
        if (water.GetIsRising())
        {
            
            if (water.GetTFactor()>0.6)
            {
                warningControl.ShowWarning();
            }
            if (water.GetTFactor() > 0.7)
            {
               waterFall.SetActive(true);
            }
            if (water.GetTFactor() > 0.98)
            {
                warningControl.StopWarning();
                GameManager.Instance.TurnToGameLosePoint();
            }
        }
        else
        {
            // 水位不上升时的逻辑
            warningControl.StopWarning();
            waterFall.SetActive(false);
        }

    }

}
