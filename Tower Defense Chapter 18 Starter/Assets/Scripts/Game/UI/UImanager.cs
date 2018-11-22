using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour {

    public static UImanager Instance;
    public Text txtGold;
    public Text txtWave;
    public Text txtEscapedEnemies;
    public Transform enemyHealthBars;
    public GameObject addTowerWindow;
    public GameObject enemyHealthBarPrefab;
    public GameObject centerWindow;
    public GameObject damageCanvas;
    public GameObject towerInfoWindow;
    public GameObject winGameWindow;
    public GameObject loseGameWindow;
    public GameObject blackBackground;

    void Awake()
    {
        Instance = this;
    }

    private void UpdateTopBar()
    {
        txtGold.text = GameManager.Instance.gold.ToString();
        txtWave.text = "Wave " + GameManager.Instance.waveNumber + " / " + WaveManager.Instance.enemyWaves.Count;
        txtEscapedEnemies.text = "Escaped Enemies " + GameManager.Instance.escapedEnemies + " / " + GameManager.Instance.maxAllowedEscapedEnemies;
    }

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
        //win game screen appears
        blackBackground.SetActive(true);
        winGameWindow.SetActive(true);
    }
    public void ShowLoseScreen()
    {
        //lose game screen appears
        blackBackground.SetActive(true);
        loseGameWindow.SetActive(true);
    }

    public void CreateHealthBarForEnemy(Enemy enemy)
    {
        //Instantiate the healthbar prefab on each enemy
        GameObject healthBar = Instantiate(enemyHealthBarPrefab);
        //Transform the healthbar with the enemy
        healthBar.transform.SetParent(enemyHealthBars, false);
        healthBar.GetComponent<EnemyHealthBar>().enemy = enemy;
    }

    //This is for the center window, AKA as the wave window
    public void ShowCenterWindow(string text)
    {
        //find the component with TxtWave text to display the correct wave
        centerWindow.transform.Find("TxtWave").GetComponent<Text>().
        text = text; // set text
        StartCoroutine(EnableAndDisableCenterWindow()); // start flicker event
    }

    //flicker event for center/wave window
    private IEnumerator EnableAndDisableCenterWindow()
    {
        //flicker three times
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(.4f);
            centerWindow.SetActive(true);
            yield return new WaitForSeconds(.4f);
            centerWindow.SetActive(false);
        }
    }

    //The following code is for the damage canvas
    //It allows the player to visually see that enemies have passed through
    public void ShowDamage()
    {
        StartCoroutine(DoDamageAnimation());
    }
    //Create flicker effect through code
    private IEnumerator DoDamageAnimation()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(.1f);
            damageCanvas.SetActive(true);
            yield return new WaitForSeconds(.1f);
            damageCanvas.SetActive(false);
        }
    }
}
