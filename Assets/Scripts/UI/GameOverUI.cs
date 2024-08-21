using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject uiParent;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameScoreUI gameScoreUI;
    [SerializeField] private GameClockUI gameClockUI;
    [SerializeField] private TextMeshProUGUI plateCountText;
    [SerializeField] private TextMeshProUGUI waterLeftCountText;
    [SerializeField] private List<WaterFaceUP> waterFaceUPInstances;

    private List<PlateContainerCounter> plateContainerCounters = new List<PlateContainerCounter>();

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        Hide();
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        ScoreManager.Instance.OnScoreChanged += ScoreManager_OnScoreChanged;

        // 查找所有 PlateContainerCounter 实例
        plateContainerCounters.AddRange(FindObjectsOfType<PlateContainerCounter>());
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
        if (GameManager.Instance.IsGameOverState())
        {
            gameScoreUI.Hide();
            gameClockUI.Hide();
            Show();
        }
    }

    private void Update()
    {
        int totalPlateCount = 0;

        // 计算所有 PlateContainerCounter 实例中的清洁盘子总数
        foreach (var counter in plateContainerCounters)
        {
            totalPlateCount += counter.GetCleanPlateCount();
        }
        plateCountText.text = totalPlateCount.ToString();

        float totalWaterLeft = 0f;

        // 计算所有 WaterFaceUP 实例中的剩余水量
        foreach (var waterFaceUP in waterFaceUPInstances)
        {
            totalWaterLeft += waterFaceUP.GetTFactor();
        }
        totalWaterLeft *=50;
        int totalWaterLeftInt = Mathf.RoundToInt(totalWaterLeft);
        waterLeftCountText.text = totalWaterLeftInt.ToString();
    }

    private void Show()
    {
        uiParent.SetActive(true);
    }
    private void Hide()
    {
        uiParent.SetActive(false);
    }
}
