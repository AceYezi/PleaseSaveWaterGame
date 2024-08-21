using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private GameObject uiParent;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private GameScoreUI gameScoreUI;
    [SerializeField] private GameClockUI gameClockUI;

    private void Start()
    {
        Hide();
        GameManager.Instance.OnGamePause += GameManager_OnGamePause;
        GameManager.Instance.OnGameUnPause += GameManager_OnGameUnPause;

        continueButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ToggleGame();
        });
        menuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameMenuScene);
        });
        settingButton.onClick.AddListener(() =>
        {
            gameScoreUI.Hide();
            gameClockUI.Hide();
            SettingUI.Instance.Show();
        });
    }

    private void GameManager_OnGameUnPause(object sender, System.EventArgs e)
    {
        gameScoreUI.Show();
        gameClockUI.Show();
        Hide();
    }

    private void GameManager_OnGamePause(object sender, System.EventArgs e)
    {
        gameScoreUI.Hide();
        gameClockUI.Hide();
        Show();
    }

    public void Show()
    {
        uiParent.SetActive(true);
    }
    private void Hide()
    {
        uiParent.SetActive(false);
    }

}
