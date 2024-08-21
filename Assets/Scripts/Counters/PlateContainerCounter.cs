using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateContainerCounter : BaseCounter
{
    [SerializeField] private ContainerCounterVisual containerCounterVisual;
    [SerializeField] private ResultUI resultUI;

    private int cleanPlatesCount = 0;

    // Start is called before the first frame update
    public override void Interact(Player player)
    {
        //
        if (player.IsHaveKitchenObject())
        {
            if (IsHaveKitchenObject() == false)
            {

                if (player.GetKitchenObject().TryGetComponent<PlateKitchenObject>
            (out PlateKitchenObject plateKitchenObject))
                {
                    SoundManager.Instance.PlaySuccessSound();
                    resultUI.DeliverySuccess();
                    ScoreManager.Instance.AddScore(1);
                    player.DestroyKitchenObject();
                    cleanPlatesCount += 1;
                }

                else if (player.GetKitchenObject().TryGetComponent<PlateGroupKitchenObject>
                    (out PlateGroupKitchenObject plateGroupKitchenObject))
                {
                    SoundManager.Instance.PlaySuccessSound();
                    resultUI.DeliverySuccess();
                    ScoreManager.Instance.AddScore(7);
                    player.DestroyKitchenObject();
                    cleanPlatesCount += 5;

                }

                else
                {

                }
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
                
            }
        }
    }

    public  int GetCleanPlateCount()
    {
        return cleanPlatesCount;
    }


}
