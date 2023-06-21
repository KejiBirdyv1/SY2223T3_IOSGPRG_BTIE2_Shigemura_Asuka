using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static int score;

    [SerializeField] private TextMeshProUGUI playerLivesUI;
    [SerializeField] private GameObject slider;
    [SerializeField] private Slider dashGaugeSlider;
    [SerializeField] private Image dashGaugeFill;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI finalScoreUI;
    [SerializeField] private Button dashGaugeButton;
    [SerializeField] private GameObject chooseCharObject;
    [SerializeField] private GameObject gameObj;

    private CharacterSelect chooseChar;
    private Player player;
    private Dash dash;
    private GameManager game;

    private void Start()
    {
        chooseChar = chooseCharObject.GetComponent<CharacterSelect>();
        game = gameObj.GetComponent<GameManager>();
        deathMenu.SetActive(false);
    }

    private void Update()
    {
        if (!chooseChar.selecting)
        {
            slider.SetActive(true);
            dashGaugeSlider.gameObject.SetActive(true);
            dashGaugeFill.gameObject.SetActive(true);
            scoreUI.gameObject.SetActive(true);
            playerLivesUI.gameObject.SetActive(true);

            dash = game.playerObj.GetComponent<Dash>();
            player = game.playerObj.GetComponent<Player>();
            playerLivesUI.text = "Lives: " + player.playerLives;

            scoreUI.text = "Score: " + score;

            dashGaugeSlider.value = dash.dashGauge;

            if (player.isDead)
            {
                scoreUI.gameObject.SetActive(false);
                playerLivesUI.gameObject.SetActive(false);
                slider.SetActive(false);
                deathMenu.SetActive(true);
                finalScoreUI.text = "Score: " + score;
            }
        }
        else
        {
            slider.SetActive(false);
            dashGaugeSlider.gameObject.SetActive(false);
            dashGaugeFill.gameObject.SetActive(false);
            scoreUI.gameObject.SetActive(false);
            playerLivesUI.gameObject.SetActive(false);
        }
    }

    public void DashGaugePressed()
    {
        if (dash.dashGauge == 1)
        {
            dash.isDash = true;
        }
    }
}
