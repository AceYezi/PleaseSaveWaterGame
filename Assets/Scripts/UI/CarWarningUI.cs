using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWarningUI : MonoBehaviour
{
    [SerializeField] private Animator WarningAnimator;

    public void ShowWarning()
    {
        WarningAnimator.gameObject.SetActive(true);
    }
    public void StopWarning()
    {
        WarningAnimator.gameObject.SetActive(false);
    }
}
