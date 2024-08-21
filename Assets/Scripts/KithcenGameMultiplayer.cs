using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class KithcenGameMultiplayer : NetworkBehaviour
{
    public static KithcenGameMultiplayer Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }


}
