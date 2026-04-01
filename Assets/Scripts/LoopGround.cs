using UnityEngine;

public class LoopGround : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float width = 6f;

    private bool _canMove;

    private SpriteRenderer _renderer;
    private Vector2 _startSize;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();

        _startSize = _renderer.size;
    }

    private void Update()
    {
        if (_canMove)
        {
            _renderer.size = new Vector2(_renderer.size.x + speed * Time.deltaTime, _renderer.size.y);
            if (_renderer.size.x > width) _renderer.size = _startSize;
        }
    }

    public void StartMoving()
    {
        _canMove = true;
    }

    public void StopMoving()
    {
        _canMove = false;
    }
}