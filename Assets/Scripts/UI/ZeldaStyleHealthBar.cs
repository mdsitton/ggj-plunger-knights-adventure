using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeldaStyleHealthBar : MonoBehaviour
{
    public static ZeldaStyleHealthBar instance;

    [SerializeField] GameObject heartCointainerPrefab;

    [SerializeField] List<GameObject> heartContainers;
    int totalHearts;
    float currentHeart;
    HearthContainer currentContainer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        heartContainers = new List<GameObject>();
    }

    public void SetupHearts(int heartIn)
    {
        heartContainers.Clear();
        for (int i = transform.childCount -1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        totalHearts = heartIn;
        currentHeart = (float)totalHearts;

        for (int i = 0; i < totalHearts; i++)
        {
            GameObject newHeart = Instantiate(heartCointainerPrefab, transform);
            heartContainers.Add(newHeart);
            if (currentContainer != null)
            {
                currentContainer.next = newHeart.GetComponent<HearthContainer>();
            }
        }
        currentContainer = heartContainers[0].GetComponent<HearthContainer>();
    }

    public void SetCurrentHealth(float health)
    {
        currentHeart = health;
        currentContainer.SetHeart(currentHeart);
    }

    public void AddHearths(float healthUp)
    {
        currentHeart += healthUp;
        if (currentHeart > totalHearts)
        {
            currentHeart = (float)totalHearts;
        }
        currentContainer.SetHeart(currentHeart);
    }

    public void RemoveHearths(float healthDown)
    {
        currentHeart -= healthDown;
        if (currentHeart < 0)
        {
            currentHeart = 0;
        }
        currentContainer.SetHeart(currentHeart);
    }

    public void AddContainer()
    {
        GameObject newHeart = Instantiate(heartCointainerPrefab, transform);
        currentContainer = heartContainers[heartContainers.Count - 1].GetComponent<HearthContainer>();
        heartContainers.Add(newHeart);

        if (currentContainer != null)
        {
            currentContainer.next = newHeart.GetComponent<HearthContainer>();
        }

        currentContainer = heartContainers[0].GetComponent<HearthContainer>();

        totalHearts++;
        currentHeart = totalHearts;
        SetCurrentHealth(currentHeart);
    }
}
