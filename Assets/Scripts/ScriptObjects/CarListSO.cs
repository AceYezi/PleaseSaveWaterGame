using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CarDataList", menuName = "CarDataList", order = 2)]
public class CarListSO : ScriptableObject
{
    public List<CarSO> carDataList;
}
