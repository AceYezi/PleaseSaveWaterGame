using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxUI : MonoBehaviour
{
    public float rotationSpeed = 1.0f;  // 旋转速度

    void Update()
    {
        // 获取当前Skybox的材质
        Material skyboxMaterial = RenderSettings.skybox;

        if (skyboxMaterial != null)
        {
            // 获取当前的旋转角度
            float rotation = skyboxMaterial.GetFloat("_Rotation");

            // 更新旋转角度
            rotation += rotationSpeed * Time.deltaTime;
            if (rotation > 360f) rotation -= 360f;  // 保持在0到360度范围内

            // 应用新的旋转角度
            skyboxMaterial.SetFloat("_Rotation", rotation);
        }
    }
}
