using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultUI : MonoBehaviour
{
    private const string IS_SHOW = "IsShow";
   [SerializeField]  private Animator deliverySuccessAnimator;


    public void DeliverySuccess()
    {
        deliverySuccessAnimator.gameObject.SetActive(true);
        deliverySuccessAnimator.SetTrigger(IS_SHOW);
    }

}
