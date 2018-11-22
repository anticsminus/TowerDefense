﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour {
    //1
    public static UImanager Instance;
    //2
    public GameObject addTowerWindow;
    //3
    public Text txtGold;
    public Text txtWave;
    public Text txtEscapedEnemies;
    public Transform enemyHealthBars;
    public GameObject enemyHealthBarPrefab;
    public GameObject centerWindow;
    //1
    public GameObject towerInfoWindow;
    public GameObject winGameWindow;
    public GameObject loseGameWindow;
    public GameObject blackBackground;
    void Awake()
    {
        Instance = this;
    }
    //2
    private void UpdateTopBar()
    {
        txtGold.text = GameManager.Instance.gold.ToString();
        txtWave.text = "Wave " + GameManager.Instance.waveNumber + " / " +
        WaveManager.Instance.enemyWaves.Count;
        txtEscapedEnemies.text = "Escaped Enemies " +
        GameManager.Instance.escapedEnemies + " / " +
        GameManager.Instance.maxAllowedEscapedEnemies;
    }
    //3
    public void ShowAddTowerWindow(GameObject towerSlot)
    {
        addTowerWindow.SetActive(true);
        addTowerWindow.GetComponent<AddTowerWindow>().
        towerSlotToAddTowerTo = towerSlot;
        UtilityMethods.MoveUiElementToWorldPosition(addTowerWindow.GetComponent<RectTransform>(), towerSlot.transform.position);
    }

    public void Update()
    {
        UpdateTopBar();
    }
    public void ShowTowerInfoWindow(Tower tower)
    {
        towerInfoWindow.GetComponent<TowerInfoWindow>().tower = tower;
        towerInfoWindow.SetActive(true);
        UtilityMethods.MoveUiElementToWorldPosition(towerInfoWindow.
        GetComponent<RectTransform>(), tower.transform.position);
    }
    public void ShowWinScreen()
    {
        blackBackground.SetActive(true);
        winGameWindow.SetActive(true);
    }
    public void ShowLoseScreen()
    {
        blackBackground.SetActive(true);
        loseGameWindow.SetActive(true);
    }
    //1
    public void CreateHealthBarForEnemy(Enemy enemy)
    {
        //2
        GameObject healthBar = Instantiate(enemyHealthBarPrefab);
        //3
        healthBar.transform.SetParent(enemyHealthBars, false);
        //4
        healthBar.GetComponent<EnemyHealthBar>().enemy = enemy;
    }
    public void ShowCenterWindow(string text)
    {
        centerWindow.transform.Find("TxtWave").GetComponent<Text>().
        text = text;
        StartCoroutine(EnableAndDisableCenterWindow());
    }
    //2
    private IEnumerator EnableAndDisableCenterWindow()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(.4f);
            centerWindow.SetActive(true);
            yield return new WaitForSeconds(.4f);
            centerWindow.SetActive(false);
        }
    }
}
