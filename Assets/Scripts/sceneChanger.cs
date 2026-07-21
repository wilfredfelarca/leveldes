using UnityEngine;

public class sceneChanger : MonoBehaviour
{
    public string loadScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(loadScene);
        }
    }
}
