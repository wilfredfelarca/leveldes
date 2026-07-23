using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Force‑activate all food objects
        ActivateAllFood();
        
    }

    private void ActivateAllFood()
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        int count = 0;

        foreach (GameObject obj in allObjects)
        {
            // Check if it's a scene object (not a prefab) and has the Food tag
            if (obj.scene.IsValid() && obj.CompareTag("Food"))
            {
                obj.SetActive(true);
                count++;
            }
        }
        
    }
}