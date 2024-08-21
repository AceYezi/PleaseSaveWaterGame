using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 角色的Transform
    public float smoothSpeed = 0.125f; // 平滑速度
    public Vector3 offset; // 相机与角色的偏移

    void FixedUpdate()
    {
        // 计算相机的目标位置，只更新x轴
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, transform.position.y, transform.position.z);

        // 使用Lerp函数来平滑移动相机在x轴方向
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 将相机的位置设置为平滑后的位置
        transform.position = smoothedPosition;
    }
}
