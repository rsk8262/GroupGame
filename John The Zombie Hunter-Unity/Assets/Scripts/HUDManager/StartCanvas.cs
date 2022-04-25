/**** 
 * Created by: Akram Taghavi-Burris
 * Date Created: Feb 23, 2022
 * 
 * Last Edited by: NA
 * Last Edited: Feb 23, 2022
 * 
 * Description: Updates start canvas referecing game manager
****/

using UnityEngine;

public class StartCanvas : MonoBehaviour
{
    /*** VARIABLES ***/

    GameManager gm; //reference to game manager

    private void Start()
    {
        gm = GameManager.GM; //find the game manager
    }

    public void GameStart()
    {
        gm.StartGame(); //refenece the StartGame method on the game manager
    }

    public void GameExit()
    {
        gm.ExitGame(); //refenece the ExitGame method on the game manager

    }

}