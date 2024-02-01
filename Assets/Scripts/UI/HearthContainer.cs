using UnityEngine;
using UnityEngine.UI;

public class HearthContainer : MonoBehaviour
{
    public HearthContainer next;
    [Range(0, 1)] float fill;
    [SerializeField] Image fillImage;

    public void SetHeart(float heartCount)
    {
        fill = heartCount;
        fillImage.fillAmount = fill;
        heartCount--;

        if (next != null)
        {
            next.SetHeart(heartCount);
        }
    }
}
