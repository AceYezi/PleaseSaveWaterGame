using UnityEngine;
using UnityEngine.Audio;

public class WaterFaceUP : MonoBehaviour
{
    //public static WaterFaceUP Instance { get; private set; }

    [SerializeField] private WaterFaceUP water;

    public float riseDuration = 30f; // 上升持续时间，单位：秒
    public float riseHeight = 1f;  // 上升高度

    private Vector3 startPosition;
    private Vector3 endPosition;
    private float elapsedTime = 0.0f;
    private float tFactor;
    private bool isRising = false;
    public int counterID;

    private WarningControl warningControl;
    


    private void Awake()
    {
       // Instance = this;
    }
    private void Start()
    {
        warningControl = GetComponent<WarningControl>();
        // 记录初始位置
        startPosition = transform.position;
        // 计算目标位置
        endPosition = startPosition + new Vector3(0, riseHeight, 0);
    }

    private void FixedUpdate()
    {
        if (isRising && elapsedTime < riseDuration && GameManager.Instance.IsGamePlayingState())
        {
            // 计算插值比例
            elapsedTime += Time.deltaTime;
            tFactor = elapsedTime / riseDuration;

            // 线性插值移动平面
            transform.position = Vector3.Lerp(startPosition, endPosition, tFactor);

        }


    }

    public float GetTFactor()
    {
        return tFactor;
    }

    public void Reset()
    {
       this.transform.position =  startPosition;
        tFactor = 0;
        elapsedTime = 0;

    }
    public void StartAndEndRising()
    {
        if (isRising)
        {
            StopWaterFlowSound();
        }
        else
        {
            PlayWaterFlowSound();
        }
        isRising = !isRising;
    }
    public bool GetIsRising()
    {
        return isRising;
    }

    public void DecreaseWaterLevel(float percentage)
    {
        float decreaseAmount = riseHeight * percentage;
        float newHeight = transform.position.y - decreaseAmount;

        if (newHeight <= startPosition.y + (riseHeight * 0.2f))
        {
            newHeight = startPosition.y;
            tFactor = 0;
            elapsedTime = 0;
        }
        else
        {
            elapsedTime = riseDuration * (newHeight - startPosition.y) / riseHeight;
            tFactor = elapsedTime / riseDuration;
        }

        transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
    }

    private void PlayWaterFlowSound()
    {

        SoundManager.Instance.PlayWaterSound();
        
    }

    private void StopWaterFlowSound()
    {
        SoundManager.Instance.StopWaterSound();
    }

}
