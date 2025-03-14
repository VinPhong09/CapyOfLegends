using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.UI;

public class EnemiesSpawner : MonoBehaviour
{
    public static EnemiesSpawner Instance;

    [SerializeField] private UnityEvent StageCleared;

    [SerializeField] private List<Enemy> _availableEnemies = new List<Enemy>();
    [SerializeField] private BossFightPreview _bossFightPreview;
    [SerializeField] private List<Boss> _availableBosses = new List<Boss>();
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Button _bossButton;
    [SerializeField] private float DIFFICULT = 1.14f;
    private float difficultyRatio = 1; 

    public List<Enemy> _currentEnemies = new List<Enemy>();

    private int _enemyStageAmount;

    private readonly int MaxEnemiesByStage = 4;
    private readonly int MinEnemiesByStage = 2;

    private readonly float MaxSpawnTimeRange = 1.5f;
    private readonly float MinSpawnTimeRange = 0.6f;

    private readonly float SpawnRangeNum1 = 1.26f;
    private readonly float SpawnRangeNum2 = 1.5f;
    private readonly int SpawnLinesAmount = 3;

    private Coroutine _spawnEnemiesCoroutine;

    private BossFightPreview _currentBossFightPreview;

    public void Initialize()
    {
        Instance = this;
    }

    public void SpawnEnemyByStage()
    {
        if((StageController.Instance.FightNumber >= StageController.Instance.MaxAmountOfFightsPerSubStage) && !StageController.Instance.FarmMode)
        {
            if (_currentBossFightPreview != null)
                return;

            Boss selectedBoss = _availableBosses[Random.Range(0, _availableBosses.Count)];
            BossFightPreview bossFightPreview = Instantiate(_bossFightPreview);
            bossFightPreview.Initialize(selectedBoss.Parameters.BossRenderView, () => {

                Boss boss = Instantiate(selectedBoss);
                boss.Initialize(_spawnPoint.position, () =>
                {
                    _currentEnemies.Remove(boss);

                    if (_currentEnemies.Count <= 0)
                    {
                        StageCleared?.Invoke();
                    }
                }, difficultyRatio);

                boss.transform.position = new Vector3(_spawnPoint.position.x, Random.Range(SpawnRangeNum1, SpawnRangeNum2));
                _currentEnemies.Add(boss);

            });

            _currentBossFightPreview = bossFightPreview;
            return;
        }

        if (_spawnEnemiesCoroutine == null)
            _spawnEnemiesCoroutine = StartCoroutine(SpawnEnemies());
        else
        {
            StopCoroutine(_spawnEnemiesCoroutine);
            _spawnEnemiesCoroutine = StartCoroutine(SpawnEnemies());
        }

        _currentBossFightPreview = null;
    }

    private IEnumerator SpawnEnemies()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(Random.Range(MinSpawnTimeRange, MaxSpawnTimeRange));

        _enemyStageAmount = Random.Range(MinEnemiesByStage, MaxEnemiesByStage);
        
        for (int i = 0; i < _enemyStageAmount; i++)
        {
            Enemy enemy = Instantiate(_availableEnemies[Random.Range(0, _availableEnemies.Count)]);
            enemy.Initialize(_spawnPoint.position, () =>
            {
                _currentEnemies.Remove(enemy);

                if (_currentEnemies.Count <= 0)
                {
                    StageCleared?.Invoke();
                }
            }, difficultyRatio);
            enemy.transform.position = new Vector3(_spawnPoint.position.x, (SpawnRangeNum1 + Random.Range(0 , SpawnLinesAmount) * 0.12f)); 

            _currentEnemies.Add(enemy);

            yield return waitForSeconds;
        }
    }

    public void CheckIfBossWin()
    {
        if (StageController.Instance.FightNumber < StageController.Instance.MaxAmountOfFightsPerSubStage)
        {
            RestartStage();
            return;
        }

        StageController.Instance.SetFarmMode();
        RestartStage();
        _bossButton.gameObject.SetActive(true);
        return;
    }

    public void RestartStage()
    {
        ClearStage();
        SpawnEnemyByStage();
    }

    public void OnSpawnBoss()
    {
        StageController.Instance.FarmMode = false;
        ClearStage();
        ChickensController.Instance.CreateChicken();
    }

    private void ClearStage()
    {
        foreach(Enemy enemy in _currentEnemies)
        {
            Destroy(enemy.gameObject);
        }

        _currentEnemies.Clear();

        if (_currentBossFightPreview != null)
            Destroy(_currentBossFightPreview);
    }

    public void SplashDamageToSpawnedEnemies(IDamage damage)
    {
        for (int i = 0; i < _currentEnemies.Count; i++)
        {
            if (_currentEnemies[i] == null)
                continue;

            _currentEnemies[i].GetDamage(damage);
        }
    }

    public bool TryGetEnemy(out Enemy enemy)
    {
        if (_currentEnemies.Count <= 0)
        {
            enemy = null;
            return false;
        }

        enemy = _currentEnemies.FirstOrDefault();
        return true;
    }

    public bool TryGetRandomEnemy(out Enemy enemy)
    {
        if(_currentEnemies.Count <= 0)
        {
            enemy = null;
            return false;
        }

        enemy = _currentEnemies[Random.Range(0, _currentEnemies.Count)];
        return true;
    }

    public void DifficultyChange()
    {
        difficultyRatio = Mathf.Pow(DIFFICULT, (StageController.Instance.GetSubStage() + (StageController.Instance.GetStage() - 1) * 10));
    }

    public float GetDifficultyRatio()
    {
        return difficultyRatio;
    }
}
