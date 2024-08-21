using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//可以直接用右键创建对象
//在本地实现实例化的保存
[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    public string objectName;
}
