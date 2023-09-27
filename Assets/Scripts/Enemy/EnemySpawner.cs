using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _spawnCount;
    [SerializeField] private BoxCollider2D _spawnArea;

    private void Start()
    {
        var spawnArea = _spawnArea.GetComponent<BoxCollider2D>();
        for (int i = 0; i < _spawnCount; i++)
        {
            var x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.y);
            var y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.x);

            var pos = new Vector2(x, y);
            Instantiate(_enemyPrefab, pos, Quaternion.identity);
        }
    }
}
