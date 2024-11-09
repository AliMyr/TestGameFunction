using UnityEngine;
using System.Collections.Generic;

public class CharacterSpawnController : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private CharacterFactory characterFactory;

    private float timeBetweenEnemySpawn;
    private float spawnTimer;
    private float gameTimer;
    private int maxActiveEnemies;
    private List<Character> activeEnemies = new List<Character>();

    private void Start()
    {
        // �������� ������������� gameData � characterFactory
        if (gameData == null)
        {
            Debug.LogError("GameData is not assigned in CharacterSpawnController.");
            return;
        }

        if (characterFactory == null)
        {
            Debug.LogError("CharacterFactory is not assigned in CharacterSpawnController.");
            return;
        }

        timeBetweenEnemySpawn = gameData.TimeBetweenEnemySpawn;
        maxActiveEnemies = 1; // ��������� ���������� ������
        spawnTimer = timeBetweenEnemySpawn;
        gameTimer = 0;
    }

    private void Update()
    {
        gameTimer += Time.deltaTime;
        spawnTimer -= Time.deltaTime;

        // ������ ������ ����������� ������������ ���������� ������
        if (gameTimer >= 60f)
        {
            gameTimer = 0;
            maxActiveEnemies++;
        }

        // ������� ������, ���� �� ���������� ������������ ���������� �������� ������
        if (spawnTimer <= 0 && activeEnemies.Count < maxActiveEnemies)
        {
            SpawnEnemy();
            spawnTimer = timeBetweenEnemySpawn;
        }

        // ������� ������, ������� ���� �����
        activeEnemies.RemoveAll(enemy => enemy == null || !enemy.gameObject.activeSelf);
    }

    private void SpawnEnemy()
    {
        // ��������, ��� characterFactory ��������� ���������������
        if (characterFactory == null)
        {
            Debug.LogError("CharacterFactory is not assigned, cannot spawn enemies.");
            return;
        }

        Character enemy = characterFactory.GetCharacter(CharacterType.DefaultEnemy);
        Vector3 playerPosition = characterFactory.Player.transform.position;
        enemy.transform.position = new Vector3(playerPosition.x + GetOffset(), 0, playerPosition.z + GetOffset());
        enemy.gameObject.SetActive(true);
        enemy.Initialize();

        if (enemy.LiveComponent != null)
        {
            enemy.LiveComponent.OnCharacterDeath += OnEnemyDeath;
        }

        activeEnemies.Add(enemy);
    }

    private float GetOffset()
    {
        bool isPlus = Random.Range(0, 100) % 2 == 0;
        float offset = Random.Range(gameData.MinSpawnOffset, gameData.MaxSpawnOffset);
        return isPlus ? offset : -offset;
    }

    private void OnEnemyDeath(Character deadEnemy)
    {
        // �������� �� null ����� ����������� ��������
        if (deadEnemy == null)
        {
            Debug.LogWarning("Attempted to handle death of a null enemy.");
            return;
        }

        deadEnemy.gameObject.SetActive(false);
        characterFactory.ReturnCharacter(deadEnemy);
    }
}
