using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerObj;
    public GameObject wallObj;

    private Player player;
    private WallMotion wall;
    private CharacterSelect chooseChar;

    [SerializeField] private GameObject wallPos;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private List<GameObject> playerCharacters;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject playerSpawn;
    [SerializeField] private GameObject chooseCharObject;

    private void Start()
    {
        chooseChar = chooseCharObject.GetComponent<CharacterSelect>();
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (!chooseChar.selecting)
        {
            Time.timeScale = 1;
        }

        if (chooseChar.chosen)
        {
            wallObj = Instantiate(wallPrefab, wallPos.transform.position, Quaternion.identity);
            wall = wallObj.GetComponent<WallMotion>();
            playerObj = Instantiate(playerCharacters[chooseChar.chosenCharacter], playerSpawn.transform.position, Quaternion.identity);
            player = playerObj.GetComponent<Player>();
            chooseChar.chosen = false;
        }

        if (chooseChar.selecting)
        {
            return;
        }

        if (player.playerLives <= 0)
        {
            player.isDead = true;
            Time.timeScale = 0;
        }

        if (player.dash != null)
        {
            HandleDash();
        }
    }

    public void Retry()
    {
        UIController.score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void HandleDash()
    {
        if (player.dash.isDash)
        {
            if (player.dash.dashTimer > 0)
            {
                wall.scrollSpeed = Time.unscaledTime;
                player.dash.dashTimer -= Time.deltaTime;
            }

            if (player.dash.dashTimer <= 0)
            {
                player.dash.dashGauge = 0;
                wall.scrollSpeed = 2.25f;
                player.dash.dashTimer = 3f;
                player.dash.isDash = false;
            }
        }
    }
}
