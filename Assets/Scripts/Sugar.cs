using UnityEngine;
using UnityEngine.UI;

public class Sugar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int sugarAmount;
    public int numOfSugar;

    public Image[] sugarImages;
    public Sprite sugarFull;
    public Sprite sugarEmpty;

    private void Update()
    {
        if (sugarAmount > numOfSugar)
        {
            sugarAmount = numOfSugar;
        }

        for (int i = 0; i < sugarImages.Length; i++)
        {

            if (i < numOfSugar)
            {
                sugarImages[i].sprite = sugarFull;
            }
            else
            {
                sugarImages[i].sprite = sugarEmpty;
            }

            if (i < sugarAmount)
            {
                sugarImages[i].enabled = true;
            }
            else
            {
                sugarImages[i].enabled = false;
            }
        }
    }
}
