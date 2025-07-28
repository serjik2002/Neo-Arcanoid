using UnityEngine;

public class BrickGridGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _brickPrefab;
    [SerializeField] private int _rows = 5;
    [SerializeField] private int _columns = 10;
    [SerializeField] private Vector2 _padding = new Vector2(0.1f, 0.1f);
    [SerializeField] private Transform _cornerA; // Один угол (например, нижний левый)
    [SerializeField] private Transform _cornerB; // Второй угол (например, верхний правый)

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        if (_brickPrefab == null || _cornerA == null || _cornerB == null)
        {
            Debug.LogError("Не все поля заданы!");
            return;
        }

        SpriteRenderer sr = _brickPrefab.GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogError("Prefab должен содержать SpriteRenderer!");
            return;
        }

        // Получаем размеры контейнера из двух углов
        Vector2 min = Vector2.Min(_cornerA.position, _cornerB.position);
        Vector2 max = Vector2.Max(_cornerA.position, _cornerB.position);
        Vector2 containerSize = max - min;
        Vector2 containerOrigin = min;

        Vector2 cellSize = new Vector2(
            (containerSize.x - (_columns - 1) * _padding.x) / _columns,
            (containerSize.y - (_rows - 1) * _padding.y) / _rows
        );

        // Сохраняем aspect ratio кирпича
        Vector2 originalSize = sr.bounds.size;
        float aspect = originalSize.x / originalSize.y;

        float brickWidth = cellSize.x;
        float brickHeight = brickWidth / aspect;

        if (brickHeight > cellSize.y)
        {
            brickHeight = cellSize.y;
            brickWidth = brickHeight * aspect;
        }

        Vector2 brickSize = new Vector2(brickWidth, brickHeight);

        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _columns; col++)
            {
                Vector2 cellPos = new Vector2(
                    col * (cellSize.x + _padding.x),
                    row * (cellSize.y + _padding.y)
                );

                Vector2 brickPos = containerOrigin + cellPos + new Vector2(cellSize.x, cellSize.y) / 2f;

                GameObject brick = Instantiate(_brickPrefab, brickPos, Quaternion.identity, transform);

                // Масштабируем кирпич с сохранением формы
                Vector3 scale = brick.transform.localScale;
                scale.x *= brickWidth / originalSize.x;
                scale.y *= brickHeight / originalSize.y;
                brick.transform.localScale = scale;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_cornerA == null || _cornerB == null) return;

        Gizmos.color = Color.cyan;

        Vector2 min = Vector2.Min(_cornerA.position, _cornerB.position);
        Vector2 max = Vector2.Max(_cornerA.position, _cornerB.position);
        Vector3 topLeft = new Vector3(min.x, max.y, 0);
        Vector3 topRight = new Vector3(max.x, max.y, 0);
        Vector3 bottomRight = new Vector3(max.x, min.y, 0);
        Vector3 bottomLeft = new Vector3(min.x, min.y, 0);

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}
