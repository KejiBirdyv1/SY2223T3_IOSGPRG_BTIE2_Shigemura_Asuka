using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public int chosenCharacter;
    public bool selecting;
    public bool chosen;

    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject selectButton;

    private void Start()
    {
        selecting = true;
    }

    public void Play()
    {
        playButton.SetActive(false);
        selectButton.SetActive(true);
        selecting = true;
    }

    public void DefaultSelect()
    {
        chosenCharacter = 0;
        selecting = false;
        chosen = true;
        selectButton.SetActive(false);
    }

    public void SpeedSelect()
    {
        chosenCharacter = 1;
        selecting = false;
        chosen = true;
        selectButton.SetActive(false);
    }

    public void TankSelect()
    {
        chosenCharacter = 2;
        selecting = false;
        chosen = true;
        selectButton.SetActive(false);
    }
}
