using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;

    void Start()
    {
        // Начальное случайное направление
        direction = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
    }

    void Update()
    {
        // Движение вручную
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Получаем нормаль поверхности столкновения
        Vector2 normal = collision.contacts[0].normal;

        // Отражаем вектор движения от нормали
        direction = Vector2.Reflect(direction, normal).normalized;
    }
}
