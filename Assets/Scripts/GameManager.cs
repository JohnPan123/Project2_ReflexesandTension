using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float gameOverTimer = 3f;
    void Update()
    {
        if (!CheckIfPlayerIsAlive() || GetVictoryState())
        {
            gameOverTimer -= Time.deltaTime;

            if (gameOverTimer <= 0)
                ChangeScene("End Scene");
        }
    }
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

    public static void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
