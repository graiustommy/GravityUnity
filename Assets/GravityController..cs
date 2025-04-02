using UnityEngine;

public class GravityController : MonoBehaviour
{
    [Header("Gravity Settings")]
    [Tooltip("Сила гравитации (можно изменять в реальном времени)")]
    public float gravityForce = 9.81f;
    
    [Tooltip("Начальная высота шара")]
    public float initialHeight = 5f;
    
    [Tooltip("Материал шара во время падения")]
    public Material fallingMaterial;
    
    [Tooltip("Материал шара после столкновения")]
    public Material collisionMaterial;
    
    private Rigidbody rb;
    private Renderer sphereRenderer;
    private bool hasCollided = false;
    
    void Start()
    {
        // Инициализация компонентов
        rb = GetComponent<Rigidbody>();
        sphereRenderer = GetComponent<Renderer>();
        
        // Установка начальной позиции
        transform.position = new Vector3(0, initialHeight, 0);
        
        // Настройка физики
        rb.useGravity = false; // Мы будем сами управлять гравитацией
    }
    
    void FixedUpdate()
    {
        if (!hasCollided)
        {
            // Применяем силу гравитации
            rb.AddForce(Vector3.down * gravityForce, ForceMode.Acceleration);
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided && collision.gameObject.CompareTag("Ground"))
        {
            hasCollided = true;
            sphereRenderer.material = collisionMaterial;
            
            // Логирование информации о столкновении
            Debug.Log($"Шар столкнулся с поверхностью на скорости {rb.linearVelocity.magnitude} м/с");
        }
    }
    
    // Метод для сброса сцены (можно вызвать из UI)
    public void ResetScene()
    {
        transform.position = new Vector3(0, initialHeight, 0);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        hasCollided = false;
        sphereRenderer.material = fallingMaterial;
    }
}