using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyToSpawn;
    [SerializeField] private int _minEnemyCount;
    [SerializeField] private int _maxEnemyCount;
    [SerializeField] private Player _player;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector3 _minCameraOffset;
    [SerializeField] private Vector3 _maxCameraOffset;
    [SerializeField] private RectTransform _healthBarContainer;

    private float _time;
    private int _waveNumber;
    private List<Enemy> _enemies = new List<Enemy>();

    public bool HasEnemies => _enemies.Count > 0;

    public event UnityAction WaveSpawned;

    private void OnEnable()
    {
        foreach (Enemy enemy in _enemies)
            enemy.Dead += OnCreatureDead;
    }

    private void OnDisable()
    {
        foreach (Enemy enemy in _enemies)
            enemy.Dead -= OnCreatureDead;
    }

    private void Update()
    {
        if (_enemies.Count == 0)
        {
            _time += Time.deltaTime;

            if (_time >= _spawnDelay)
            {
                for (int i = 0; i < Random.Range(_minEnemyCount, _maxEnemyCount); i++)
                {
                    Spawn();
                }

                _waveNumber++;
                WaveSpawned?.Invoke();
                _time = 0;
            }
        }
    }

    public Enemy GetNearestEnemy(Transform requesterTransform)
    {
        if (_enemies.Count > 0)
        {
            float minDistance = float.MaxValue;
            int index = 0;

            for (int i = 0; i < _enemies.Count; i++)
            {
                float distance = Vector3.Distance(requesterTransform.position, _enemies[i].transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    index = i;
                }
            }

            return _enemies[index];
        }
        else
        {
            return null;
        }
    }

    private void Spawn()
    {
        Vector3 spawnPosition = TryToGetGroundPoint(_camera) + GetRandomVector(_minCameraOffset, _maxCameraOffset);
        Enemy enemy = Instantiate(_enemyToSpawn, spawnPosition, Quaternion.identity, transform);
        enemy.Init(_player, _healthBarContainer);
        _enemies.Add(enemy);
        enemy.EnhanceStat(_waveNumber);
        enemy.Dead += OnCreatureDead;
    }

    private Vector3 TryToGetGroundPoint(Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width, Screen.height / 2, 0));
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (var hit in hits)
            if (hit.transform.TryGetComponent(out Ground ground))
                return hit.point;

        Debug.LogError("Ground doesn't hit by cast.");

        return default;
    }

    private Vector3 GetRandomVector(Vector3 minVector, Vector3 maxVector)
    {
        float x = Random.Range(minVector.x, maxVector.x);
        float y = Random.Range(minVector.y, maxVector.y);
        float z = Random.Range(minVector.z, maxVector.z);

        return new Vector3(x, y, z);
    }

    private void OnCreatureDead(Creature creature)
    {
        if (creature is Enemy)
            _enemies.Remove((Enemy)creature);

        creature.Dead -= OnCreatureDead;
    }
}
