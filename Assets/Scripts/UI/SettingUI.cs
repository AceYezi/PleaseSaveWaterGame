using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    public static SettingUI Instance { get; private set; }
    [SerializeField] private GameObject uiParent;
    [SerializeField] private Button SFXButton;
    [SerializeField] private TextMeshProUGUI soundButtonText;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI musicButtonText;
    [SerializeField] private Button closeButton;


    private void Awake()
    {
          Instance = this;     
    }

    private void Start()
    {
 

        Hide();
        UpdateVisual();
        SFXButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    public void Show()
    {
        uiParent.SetActive(true);
    }
    private void Hide()
    {
        uiParent.SetActive(false) ;
    }

    private void UpdateVisual()
    {
        soundButtonText.text = "SFX:" + SoundManager.Instance.GetVolume();
        musicButtonText.text = "MUSIC:" + MusicManager.Instance.GetVolume();

    }
}
