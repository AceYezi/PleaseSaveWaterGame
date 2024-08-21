using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameScoreUI : MonoBehaviour
{
    [SerializeField] private GameObject uiParent;
    [SerializeField] private TextMeshProUGUI scoreText;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        ScoreManager.Instance.OnScoreChanged += ScoreManager_OnScoreChanged;
        Hide();
    }


    private void OnDestroy()
    {
        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
        ScoreManager.Instance.OnScoreChanged -= ScoreManager_OnScoreChanged;
    }

    private void ScoreManager_OnScoreChanged(int newScore)
    {
        scoreText.text = newScore.ToString();
    }


    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGamePlayingState())
        {
            Show();
        }
        else if (GameManager.Instance.IsGameOverState())
        {
            Hide();
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
