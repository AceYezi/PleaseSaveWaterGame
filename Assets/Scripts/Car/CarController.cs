using System.Collections;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float speed = 5;
    private Transform endPoint;
    [SerializeField] public CarWarningUI carWarningUI;

    public void Setup(Transform end, CarSO carData, CarWarningUI warningUI)
    {
        endPoint = end;
        carWarningUI = warningUI;

        if (GameManager.Instance.IsGamePlayingState())
        {
            StartCoroutine(PlayWarningAndMove());
        }

    }

    private IEnumerator PlayWarningAndMove()
    {

        SoundManager.Instance.PlayCarHornSound();
        carWarningUI.ShowWarning();
        yield return new WaitForSeconds(0.3f);
        SoundManager.Instance.PlayCarHornSound();

        // 等待三秒钟
        yield return new WaitForSeconds(3f);
        carWarningUI.StopWarning();
        // 移动小车
        while (transform.position != endPoint.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);
            yield return null;
        }

        // 移动完成后销毁小车
        Destroy(gameObject);
    }

}
