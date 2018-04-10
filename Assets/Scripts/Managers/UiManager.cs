using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {
    Text countText;
    GameObject endText;
    public GameObject playerPanelPrefab;
    public GameObject healthBarPrefab;
    public GameObject furyBarPrefab;
	// Use this for initialization
	void Start () {
        countText = GameObject.Find(Constants.COUNT_TEXT_NAME).GetComponent<Text>();
        endText = GameObject.Find(Constants.END_TEXT_NAME);
        endText.SetActive(false);
        foreach(Player player in PlayerManager.Instance.alivePlayers)
        {
            GameObject playerPanel = Instantiate(playerPanelPrefab, GameObject.Find(Constants.HEALTH_BAR_GRID_PANEL_NAME).transform);
            GameObject healthBar = Instantiate(healthBarPrefab, playerPanel.transform);
            GameObject furyBar = Instantiate(furyBarPrefab, playerPanel.transform);

            furyBar.name = Constants.TITLE_FURY_BAR + player.playerNumber;
            healthBar.name = Constants.TITLE_HEALTH_BAR + player.playerNumber;

            Slider healthSlider = healthBar.GetComponentInChildren<Slider>();
            Slider furySlider = furyBar.GetComponentInChildren<Slider>();

            Text nameText = playerPanel.GetComponentInChildren<Text>();
            nameText.text = Constants.LABEL_PANEL + player.playerNumber;

            playerPanel.gameObject.name = Constants.TITLE_PANEL_BAR + player.playerNumber;
        }
    }
	
	// Update is called once per frame
	void Update () {

        foreach(Player player in PlayerManager.Instance.alivePlayers)
        {
            Characters playerCharacter = player.charInstantiated.GetComponent<Characters>();
            GameObject healthbar = GameObject.Find(Constants.TITLE_HEALTH_BAR + player.playerNumber);
            GameObject furyBar = GameObject.Find(Constants.TITLE_FURY_BAR + player.playerNumber);
            Slider healthSlider = healthbar.GetComponent<Slider>();
            Slider furySlider = furyBar.GetComponent<Slider>();
            healthSlider.value = playerCharacter.health;
            furySlider.value = playerCharacter.fury;
        }
        if(GameManager.Instance.gameStatus == GameManager.GameState.starting)
        {
            SetupStartCount();
        }
        else if(GameManager.Instance.gameStatus == GameManager.GameState.finished)
        {
            SetupEnd();
        }
	}
    void SetupEnd()
    {
        endText.gameObject.SetActive(true);
    }
    void SetupStartCount()
    {
        float count = countUntilStart();
        countText.text = count.ToString();

        if (count <= 0)
        {
            Destroy(countText);
        }
    }
    float countUntilStart()
    {
        float timeToWait = (GameManager.Instance.gameStartAfter + GameManager.Instance.beginning);
        float value = (Mathf.Ceil(timeToWait - Time.realtimeSinceStartup));
        return value;
    }
}
