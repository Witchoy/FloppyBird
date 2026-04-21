using UnityEngine;
using UnityEngine.InputSystem;

public class FlyBehaviour : MonoBehaviour
{
    [SerializeField] private float velocity = 1.5f;
    [SerializeField] private float rotationSpeed = 10f;

    private bool _flapQueued;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame
            || Keyboard.current.spaceKey.wasPressedThisFrame
            || (Touchscreen.current != null
                && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
           ) _flapQueued = true;
    }

    private void FixedUpdate()
    {
        if (_flapQueued)
        {
            _rigidbody.linearVelocity = Vector2.up * velocity;
            _flapQueued = false;
        }

        _rigidbody.MoveRotation(_rigidbody.linearVelocity.y * rotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Instance.GameOver();
    }
}