using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private float maxTime = 1.5f;
    [SerializeField] private float heightRange = 0.5f;
    [SerializeField] private GameObject pipe;
    private float _timer;

    private void Awake()
    {
        SpawnPipe();
    }

    private void Update()
    {
        if (_timer > maxTime)
        {
            SpawnPipe();
            _timer = 0;
        }

        _timer += Time.deltaTime;
    }

    private void SpawnPipe()
    {
        var position = transform.position + new Vector3(0, Random.Range(-heightRange, heightRange));

        Instantiate(pipe, position, Quaternion.identity);
    }
}