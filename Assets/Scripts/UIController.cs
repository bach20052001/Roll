using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private int score;

    [SerializeField] private GameObject scoreUI;
    [SerializeField] private GameObject gameoverPanel;

    void Start()
    {
        scoreUI.SetActive(true);
        gameoverPanel.SetActive(false);
        this.RegisterListener(GameEvent.OnEnemyFall, (param) => OnEnemyFallHandler());
        this.RegisterListener(GameEvent.OnPlayerFall, (param) => OnPlayerFallHandler());
    }

    private void OnPlayerFallHandler()
    {
        gameoverPanel.SetActive(true);
    }

    private void OnEnemyFallHandler()
    {
        score++;
        scoreUI.GetComponent<Text>().text = score.ToString();
    }
}
