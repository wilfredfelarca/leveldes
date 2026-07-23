using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverFunctionality : MonoBehaviour
{
    GameObject go;
    int currentSceneIndex;
    TextMeshProUGUI gameoverText;

    void Awake()
    {
        go = GameObject.Find("gameover");
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        gameoverText = go.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (go != null)
        {
            if (gameoverText.text == "GAME OVER!") 
            {
                if (Input.GetKeyDown(KeyCode.Space)) // Check if the space key is pressed
                {
                    SceneManager.LoadScene(currentSceneIndex);
                }
            }
        }
    }
}
