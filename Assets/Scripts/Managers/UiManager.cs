using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {
    Text countText;
    GameObject endText;
    public GameObject healthBarPrefab;
	// Use this for initialization
	void Start () {
        countText = GameObject.Find(Constants.COUNT_TEXT_NAME).GetComponent<Text>();
        endText = GameObject.Find(Constants.END_TEXT_NAME);
        endText.SetActive(false);

        Debug.Log(PlayerManager.Instance.numberPlayerToSpawn);

        foreach(GameObject player in PlayerManager.Instance.players)
        {
            GameObject healthbar = Instantiate(healthBarPrefab, GameObject.Find(Constants.HEALTH_BAR_GRID_PANEL_NAME).transform);
            Slider healthSlider = healthbar.GetComponent<Slider>();
            Text nameText = healthSlider.gameObject.GetComponentInChildren<Text>();
            Debug.Log(player.GetComponent<Player>().status);
            nameText.text = Constants.TITLE_HEALTH_BAR + player.GetComponent<Player>().playerNumber;
            healthSlider.gameObject.name = Constants.TITLE_HEALTH_BAR + player.GetComponent<Player>().playerNumber;
        }
    }
	
	// Update is called once per frame
	void Update () {

        foreach(GameObject player in PlayerManager.Instance.players)
        {
            Player playerAttachedTo = player.GetComponent<Player>();
            GameObject healthbar = GameObject.Find(Constants.TITLE_HEALTH_BAR + playerAttachedTo.playerNumber);
            Slider healthSlider = healthbar.GetComponent<Slider>();
            healthSlider.value = playerAttachedTo.health;
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
