using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterBarUI : MonoBehaviour
{
    [SerializeField] private Image progressImage;
    // Start is called before the first frame update
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void UpdateWaterProgress(float progress)
    {
        Show();
        progressImage.fillAmount = progress;

        if (progress == 1)
        {
            Invoke("Hide", 1f);
        }

    }
}
