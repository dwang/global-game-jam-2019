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
    private TextMeshProUGUI pointsToWinText;
    [SerializeField]
    private TextMeshProUGUI gameModeText;
    public TransitionManager transitionManager;

    private void Awake()
    {
        ServiceLocator.Instance.AddService(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.RemoveService<MenuManager>();
    }

    public void StartGame()
    {
        transitionManager.TransitionTo("SinglePlayerWave");
    }

    public void GotoMenu(string name)
    {
        for (int i = 0; i < menus.Length; i++)
            menus[i].SetActive(menus[i].name == name);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
