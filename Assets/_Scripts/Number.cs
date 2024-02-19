    using UnityEngine;

    public class Number : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void SetNumber(int amount)
    {
        _spriteRenderer.sprite = _sprites[amount - 1];
    }
}
