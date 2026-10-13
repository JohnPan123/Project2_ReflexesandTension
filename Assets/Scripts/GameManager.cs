using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    private float gameOverTimer = 3f;
    private static Color playerVictoryHealth;
    void Update()
    {
        if (!CheckIfPlayerIsAlive() || GetVictoryState())
        {
            gameOverTimer -= Time.deltaTime;

            if (gameOverTimer <= 0)
            {
                if (GetVictoryState())
                    SetPlayerVictoryHealthColor(player.GetComponent<MeshRenderer>().material.color);

                ChangeScene("End Scene");
            }
        }
    }
    private void SetPlayerVictoryHealthColor(Color victoryColor)
    {
        playerVictoryHealth = victoryColor;
    }
    public static Color GetPlayerVictoryHealthColor() => playerVictoryHealth;
    //---------------------------------------------------VICTORY AND DEATH STATE STUFF-----------------------------------------------\\
    private static bool victory = false;

    /// <summary>
    /// Sets the variable victory to true.
    /// </summary>
    public static void DeclareVictory() { victory = true; }

    /// <summary>
    /// Sets the variable victory to false.
    /// </summary>
    public static void ResetVictoryState() { victory = false; }

    /// <summary>
    /// Returns the value of the victory variable.
    /// </summary>
    /// <returns></returns>
    public static bool GetVictoryState() => victory;

    private static bool playerDead = false;

    /// <summary>
    /// Sets the playerDead boolean variable to false;
    /// </summary>
    public static void KillThePlayer() { playerDead = true; }
    
    /// <summary>
    /// Sets the playerDead variable to true.
    /// </summary>
    public static void NewGamePlayerAlive() { playerDead = false; }
    
    /// <summary>
    /// Returns the value of the playerDead variable.
    /// </summary>
    /// <returns></returns>
    public static bool CheckIfPlayerIsAlive() => !playerDead;
    //---------------------------------------------------VICTORY AND DEATH STATE STUFF-----------------------------------------------\\

    /// <summary>
    /// Scene change function. Also called a bunch in buttons.
    /// </summary>
    /// <param name="sceneName">This is the scene string name as found in the editor</param>
    public static void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
