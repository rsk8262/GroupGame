using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int health = 100;
    public HealthBarPlayer healthBar;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ApplyDamage(int points)
    {
        health -= points;
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            GameManager.GM.gameState = GameState.LostLevel;
        }
    }
}
