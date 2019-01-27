using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using TMPro;

public class GameManager : MonoBehaviour, IService
{
    public CameraVFX cameraVFX;
    public CameraController cameraController;
    public Image healthImageFill;
    public Image chargeUpImageFill;

    [Header("Prefabs")]
    public GameObject[] enemyPrefabs;
    public GameObject[] housePrefabs;

    [Header("Waves")]
    public int wave = 1;
    public float timeLeft;
    public TextMeshPro scoreText;
    public List<EnemyController> enemies;


    private void Awake()
    {
        ServiceLocator.Instance.AddService(this);
    }

    public void StartWave()
    {
        wave++;
        int rand = Random.Range(wave / 2, wave);
        for (int j = 0; j < enemies.Count; j++)
            for (int i = 0; i < rand; i++)
                Instantiate(enemies[j], );
    }

    public void EnemyDeath(EnemyController enemy)
    {
        enemies.Remove(enemy);
        if (enemy is ShotgunEnemy)
            
        if (enemies.Count == 0)
            StartWave();
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.RemoveService<GameManager>();
    }
}
