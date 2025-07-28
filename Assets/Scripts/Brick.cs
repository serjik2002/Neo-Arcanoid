using UnityEngine;

public class Brick : MonoBehaviour
{
    public enum BrickType
    {
        Normal,
        Hard,
        Bonus,
        Unbreakable
    }

    [Header("Brick Settings")]
    [SerializeField] private BrickType _brickType = BrickType.Normal;
    [SerializeField] private int _hitsToBreak = 1;

    private int _currentHits;

    private void Start()
    {
        _currentHits = _hitsToBreak;
    }

    public void TakeHit()
    {
        if (_brickType == BrickType.Unbreakable)
            return;

        _currentHits--;

        if (_currentHits <= 0)
        {
            DestroyBrick();
        }
        else
        {
            // Визуально показать повреждение (например, смена цвета)
            Debug.Log($"Brick hit! {_currentHits} hits left.");
        }
    }

    private void DestroyBrick()
    {
        Debug.Log("Brick destroyed!");

        // Тут можно добавить: эффект разрушения, звук, очки, выпадение бонуса и т.д.
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            TakeHit();
        }
    }

    public BrickType Type => _brickType;
}
