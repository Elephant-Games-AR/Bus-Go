using UnityEngine;

public class BridgeDissolveController : MonoBehaviour
{
    public Transform cameraTransform; // Камера, которая следит за автобусом
    public Material roadMaterial;     // Материал с шейдером dissolve
    public float dissolveRadius = 5f; // Радиус эффекта dissolve

    void Update()
    {
        // Обновляем позицию камеры в шейдере
        roadMaterial.SetVector("_CameraPos", cameraTransform.position);

        // Устанавливаем радиус dissolve эффекта
        roadMaterial.SetFloat("_Radius", dissolveRadius);
    }
}