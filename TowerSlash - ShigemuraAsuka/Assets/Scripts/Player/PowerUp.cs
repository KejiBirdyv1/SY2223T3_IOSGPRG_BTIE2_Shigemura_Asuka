using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int randomValue;

    [SerializeField] private int powerupChance;
    [SerializeField] private GameObject playerObject;
    private Player player;

    private void Start()
    {
        player = playerObject.GetComponent<Player>();
    }

    public void PowerupChance()
    {
        randomValue = Random.Range(0, 50);
        Debug.Log(randomValue);

        if (randomValue < powerupChance)
        {
            player.playerLives++;
        }
    }
}
