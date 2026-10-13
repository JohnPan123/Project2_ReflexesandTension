using TMPro;
using UnityEngine;

public class GameOverTextScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultsText;
    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject player;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (GameManager.GetVictoryState()) 
        {
            resultsText.text = "You Won :D";
            player.GetComponent<MeshRenderer>().material.color = GameManager.GetPlayerVictoryHealthColor();
            player.SetActive(true);
        }
        else 
        {
            resultsText.text = "You Lost D:";
            turret.SetActive(true);
        }
    }
}
