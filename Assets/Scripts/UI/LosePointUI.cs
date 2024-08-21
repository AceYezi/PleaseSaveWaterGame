using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LosePointUI : MonoBehaviour
{
    [SerializeField] private int counterID;
    [SerializeField] private GameObject uiParent;
    [SerializeField] private TextMeshProUGUI loseScoreText;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnScoreLost += GameManager_OnScoreLost;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreLost -= GameManager_OnScoreLost;
        }
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameLosePointState())
        {
            
        }
        else if (GameManager.Instance.IsGameOverState() || GameManager.Instance.IsGameCongratuateState())
        {
            Hide();
        }
    }
    private void GameManager_OnScoreLost(int receivedCounterID, int lostScore)
    {
        if (receivedCounterID == counterID)  // 只更新对应水池的UI
        {
            print("11111");
            loseScoreText.text = lostScore.ToString();
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
