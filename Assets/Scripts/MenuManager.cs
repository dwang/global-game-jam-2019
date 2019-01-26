using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour, IService
{
    public GameObject[] menus;
    [SerializeField]
    private SaveManager saveManager;
    [SerializeField]
    private TextMeshProUGUI pointsToWinText;
    [SerializeField]
    private TextMeshProUGUI gameModeText;

    private void Awake()
    {
        ServiceLocator.Instance.AddService(this);
    }

    private void Start()
    {
        saveManager = ServiceLocator.Instance.GetService<SaveManager>();
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.RemoveService<MenuManager>();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameplaySetup");
    }

    public void GotoMenu(string name)
    {
        for (int i = 0; i < menus.Length; i++)
            menus[i].SetActive(menus[i].name == name);
    }
}
