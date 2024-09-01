using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target; // Цель, за которой будет следовать камера (например, транспортное средство)
       public Vector3 offset = new Vector3(0f, 5f, -10f); // Смещение камеры относительно цели
       public float smoothTime = 0.3f; // Время сглаживания движения камеры
   
       private Vector3 velocity = Vector3.zero;
   
       void LateUpdate()
       {
           Vector3 targetPosition = target.position + offset;
           transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    
           // Плавное вращение камеры, чтобы она смотрела в том же направлении, что и цель
           Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
           transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothTime);
       }
}
