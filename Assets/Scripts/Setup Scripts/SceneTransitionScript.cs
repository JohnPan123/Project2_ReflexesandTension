using UnityEngine;

public class SceneTransitionScript : MonoBehaviour
{
    private float gameOverTimer = 3f;
    void Update()
    {
        if (!GameManager.CheckIfPlayerIsAlive() || GameManager.GetVictoryState())
        {
            gameOverTimer -= Time.deltaTime;

            if (gameOverTimer <= 0) 
                GameManager.ChangeScene("End Scene");
        }
    }
}
