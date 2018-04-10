using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    //Parent qui contiendra tout nos personnages
    public Transform charListParent;
    public Transform playerPanelParent;
    //Personnages a afficher
    public GameObject[] characters;
    
    public CharSelectUser[] users;
    public GameObject[] tokens;
    public GameObject[] panels;

    public int userNumber;
    // Use this for initialization
    void Start()
    {
        userNumber = SupplyDepot.playerNumber;
        ShowCharSelect();
        InitializeUsers();
    }
    void ShowCharSelect()
    {
        for(int i = 0; i < characters.Length; i++)
        {
            characters[i] = Instantiate(characters[i], charListParent);
        }
    }
    void InitializeUsers()
    {
        users = new CharSelectUser[userNumber];
        for (int i = 0; i < users.Length; i++)
        {
            // We instantiate all our users
            users[i] = new CharSelectUser(0, Instantiate(tokens[i], characters[1].transform), characters, Instantiate(panels[i], playerPanelParent));
        }
    }
    // Update is called once per frame
    void Update()
    {
        foreach(CharSelectUser user in users)
        {
            Move(user);
            if(Input.GetButtonDown("SelectP" + user.player.playerNumber))
            {
                SelectCharacter(user);
            }
        }

        if (Input.GetButtonDown("Start"))
        {
            SetupAndStartGame();
        }
    }
    public void SetupAndStartGame()
    {
        //Set playerList in SupplyDepot
        SupplyDepot.players = new Player[users.Length];
        for (int i = 0; i < users.Length; i++)
        {
            SupplyDepot.players[i] = users[i].player;
        }
        //Launch the game
        SceneManager.LoadScene("level");
    }
    public void Move(CharSelectUser user)
    {
        // Players numbers start at 1, we dont want that for our arrays
        int indexFromPlayer = user.player.playerNumber - 1;

        float horizontal = 0;

        //Check if we want to move
        if (Input.GetButtonDown(Constants.PLAYERS_HORIZONTAL_INPUT + user.player.playerNumber))
        {
            horizontal = Input.GetAxisRaw(Constants.PLAYERS_HORIZONTAL_INPUT + user.player.playerNumber);
        }
        user.MoveCursor(horizontal);
    }
    public void SelectCharacter(CharSelectUser user)
    {
        user.ChooseChar();
    }

    //This class is meant to represent a user in our CharSelect, thats why it's a inner class , we will not use it in another scene;
    [System.Serializable]
    public class CharSelectUser
    {
        public Player player;

        // They have a cursor to choose a character
        public int cursor;
        // And a token wich display the current position of the cursor
        public GameObject token;
        public GameObject panel;
        //Every user know the list of player
        public GameObject[] characters;

        public CharSelectUser(int currentCursor, GameObject token, GameObject[] characters, GameObject panel)
        {
            this.player = new Player();
            this.cursor = currentCursor;
            this.token = token;
            this.characters = characters;
            this.panel = panel;
        }

        public void MoveCursor(float direction)
        {
            //Move the cursor
            if (direction > 0 && cursor < characters.Length -1)
            {
                this.cursor += 1;
            }
            else if (direction < 0 && cursor > 0)
            {
                this.cursor -= 1;
            }
            this.token.transform.SetParent(characters[cursor].transform);
            this.token.transform.localPosition = new Vector3(0, 0, 0);
        }

        public void ChooseChar()
        {
            this.player.chooseChar(this.token.GetComponentInParent<SelectableChar>().linkedCharPrefab);
            //Show the selected player
            panel.GetComponent<Image>().sprite = token.GetComponentInParent<SelectableChar>().linkedCharSprite;
        }

    }
}
