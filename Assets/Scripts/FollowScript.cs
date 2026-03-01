using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public bool followX = true;
    public bool followY = true;
    public bool followZ = true;

    public bool useSmoothing = true;
    public float smoothSpeed = 5f;
    private Vector3 targetPosition;

    //Скрипт с гибкой настройкой следования за предметом, офсет, и заморозку по координатам, а также плавное перемещение за объектом
    void LateUpdate()
    {
        // Вычисляем целевую позицию
        targetPosition = transform.position;

        if (followX) targetPosition.x = player.position.x + offset.x;

        if (followY) targetPosition.y = player.position.y + offset.y;

        if (followZ) targetPosition.z = player.position.z + offset.z;

        // Плавно или мгновенно
        if (useSmoothing)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = targetPosition;
        }
    }
}