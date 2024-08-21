using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class CarSpawner : MonoBehaviour
{
    public CarListSO carDataList; // 所有小车数据的列表
    public Transform spawnPoint; // 小车生成点
    public Transform endPoint; // 小车行驶的终点
    public float minSpawnInterval = 3f; // 生成小车的最小时间间隔
    public float maxSpawnInterval = 10f; // 生成小车的最大时间间隔
    [SerializeField] private CarWarningUI carWarningUI;



    private void Start()
    {
        StartCoroutine(SpawnCar());
    }

    private IEnumerator SpawnCar()
    {
        while (true)
        {
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);


            CarSO carData = carDataList.carDataList[Random.Range(0, carDataList.carDataList.Count)];

            GameObject car = Instantiate(carData.carPrefab, spawnPoint.position, spawnPoint.rotation);



            CarController carController = car.GetComponent<CarController>();
            carController.Setup(endPoint, carData, carWarningUI);
        }
    }
}
