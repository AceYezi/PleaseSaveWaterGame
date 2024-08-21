using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameClockUI : MonoBehaviour
{
   [SerializeField] private GameObject uiParent;
   [SerializeField] private Image progressImage;
   [SerializeField] private TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnStateChanged += Instance_OnStateChanged;
        Hide();
    }
    private void Update()
    {
        if(GameManager.Instance.IsGamePlayingState())
        {
            progressImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();
            timeText.text = Mathf.CeilToInt(GameManager.Instance.GetGamePlayingTimer()).ToString();
        }

    }

    private void Instance_OnStateChanged(object sender, System.EventArgs e)
    {
        if(GameManager.Instance.IsGamePlayingState())
        {
            Show();
        }

    }

    public void Show()
    {
        uiParent.SetActive(true);
    }
    public void Hide()
    {
        uiParent.SetActive(false);
    }
}
