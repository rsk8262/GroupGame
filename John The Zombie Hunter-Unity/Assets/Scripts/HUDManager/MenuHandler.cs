using TMPro;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{

    GameManager gm; //reference to game manager

    [Header("Canvas Settings")]
    public TMP_Text killScoreUI;
    public TMP_Text bestScoreUI;
    public TMP_Text objectivesRemainingUI;

    private void Start()
    {
        gm = GameManager.GM;
    }

    // Update is called once per frame
    void Update()
    {
        killScoreUI.text = "Kills: " + gm.Score;
        bestScoreUI.text = "Best: " + gm.HighScore;
        objectivesRemainingUI.text = "Chests Remaining: " + (gm.totalObjectives - gm.objectivesCaptured);
    }
}
