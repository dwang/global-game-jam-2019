using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour, IService
{
    public GameObject[] menus;
    public TransitionManager transitionManager;
    public SaveManager saveManager;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        ServiceLocator.Instance.AddService(this);
    }

    private void Start()
    {
        transitionManager = ServiceLocator.Instance.GetService<TransitionManager>();
        saveManager = ServiceLocator.Instance.GetService<SaveManager>();

        scoreText.text = "";
        for (int i = 0; i < saveManager.State.highscores.Length; i++)
            scoreText.text += (i + 1) + ") " + saveManager.State.highscores[i] + "\n";
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
