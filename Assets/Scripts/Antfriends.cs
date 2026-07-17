using UnityEngine;


public class Antfriends : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int antAmount;
    public int numOfAnt;

    private void Update()
    {
        if (antAmount > numOfAnt)
        {
            antAmount = numOfAnt;
        }
    }
}
