using UnityEngine;

public class MovePipe : MonoBehaviour
{
    [SerializeField] private float speed = 9f;

    private void Update()
    {
        transform.position += Vector3.left * (speed * Time.deltaTime);

        if (transform.position.x < -20f)
            Destroy(gameObject);
    }
}