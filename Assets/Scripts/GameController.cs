using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject player;
    public int maxResources = 100;

    public GameObject GameUI;
    public Text resourcesText;
    public Text itemText;
    public Text statusText;
    public Text winText;

    public GameObject[] items;

    private int resourcesUsed = 0;
    private int currentItem = 0;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        cycleItems();
    }

    private void cycleItems ()
    {
        var wheel = Input.GetAxis("Mouse ScrollWheel");
        if (wheel > 0f)
        {
            // scroll up
            currentItem++;
            currentItem = currentItem % items.Length;
            updateItemText();
        }
        else if (wheel < 0f)
        {
            // scroll down
            currentItem--;
            if (currentItem < 0)
            {
                currentItem = items.Length - 1;
            }
            currentItem = currentItem % items.Length;
            updateItemText();
        }
        //Debug.Log("Selected Item: " + items[currentItem].GetComponent<SpawnableItem>().name);
    }

    public GameObject getCurrentItem()
    {
        return items[currentItem];
    }

    public bool canSpawn (GameObject item)
    {
        int itemCost = item.GetComponent<SpawnableItem>().cost;
        if (resourcesUsed + itemCost <= maxResources)
        {
            return true;
        } else
        {
            //Debug.Log(resourcesUsed + " used out of " + maxResources);
            return false;
        }
    }

    public void useResources (GameObject item)
    {
        int itemCost = item.GetComponent<SpawnableItem>().cost;
        resourcesUsed += itemCost;
        updateResourcesText();
    }

    public void freeResources (GameObject item)
    {
        int itemCost = item.GetComponent<SpawnableItem>().cost;
        resourcesUsed -= itemCost;
        updateResourcesText();
    }

    public void updateResourcesText()
    {
        resourcesText.text = resourcesUsed + " / " + maxResources;
    }

    public void updateItemText()
    {
        itemText.text = items[currentItem].name;
    }

    public void updateStatus(string message)
    {
        statusText.text = message;
    }

    public void endGame()
    {
        winText.enabled = true;
        Destroy(this);
    }
}
