using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
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
    public float spawnRadius;
    public float timeLeft;
    public TextMeshPro scoreText;
    public List<EnemyController> enemies;
    public Transform[] spawnLocations;


    private void Awake()
    {
        ServiceLocator.Instance.AddService(this);
    }

    public IEnumerator StartWave()
    {
        wave++;
        int randAmount = Random.Range(wave / 2, wave);
        int randLocation = Random.Range(0, spawnLocations.Length - 1);
        for (int j = 0; j < enemies.Count; j++)
            for (int i = 0; i < randAmount; i++)
            {
                enemies.Add(Instantiate(enemies[j], spawnLocations[randLocation].position, Quaternion.identity).GetComponent<EnemyController>());
                enemies[enemies.Count - 1].GetComponent<Rigidbody>().AddForce(-spawnLocations[randLocation].position * 20, ForceMode.Impulse);
                yield return new WaitForSeconds(0.5f);
            }
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
