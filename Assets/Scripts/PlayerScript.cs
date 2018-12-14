using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public GameObject gameController;

    public int playerRange = 5;

    private GameController controller;
    private GameObject currentItem;
    private GameObject itemInHands;

	// Use this for initialization
	void Start ()
    {
        controller = gameController.GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Spawn
        if (Input.GetKeyDown(KeyCode.E))
        {
            spawnObject();
        }
        
        //Delete
        if (Input.GetKeyDown(KeyCode.R))
        {
            deleteObject();
        }

        //Grab
        if (Input.GetMouseButtonDown(0))
        {
            grabObject();
        }

        //Freeze Object
        if (Input.GetMouseButtonDown(1))
        {
            freezeObject();
        }

        //Interact with Object
        if (Input.GetKeyDown(KeyCode.F))
        {
            interactObject();
        }

        //If you have an item in your hands, move it with the camera
        if (itemInHands != null)
        {
            //Vector3 newPos = this.transform.position + (this.transform.forward * 3);
            Vector3 newPos = Camera.main.transform.position + (Camera.main.transform.forward * 3);
            //itemInHands.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
            itemInHands.transform.SetPositionAndRotation(newPos, Camera.main.transform.rotation);
        }
    }

    void interactObject()
    {
        if (itemInHands == null)
        {
            GameObject o = playerRaycast(playerRange);
            InteractiveItem i;
            if (o != null)
            {
                i = o.GetComponent<InteractiveItem>();
                Debug.Log(o + " " + i);
                if (o != null && i != null)
                {
                    Debug.Log("interacted with " + i.name);
                    i.Interact();
                }
            }
        }
        else
        {
            itemInHands = null;
        }
    }

    void grabObject()
    {
        if (itemInHands == null)
        {
            GameObject o = playerRaycast(playerRange);
            if (o != null && o.GetComponent<SpawnableItem>() != null && o.GetComponent<SpawnableItem>().grabbable)
            {
                Debug.Log("grabbed " + o.name);
                itemInHands = o;
            }
        } else
        {
            itemInHands = null;
        }
    }

    void freezeObject()
    {
        GameObject o;
        if (itemInHands != null)
        {
            o = itemInHands;
        } else
        {
            o = playerRaycast(playerRange);
        }
        Rigidbody rb = o.GetComponent<Rigidbody>();
        SpawnableItem si = o.GetComponent<SpawnableItem>();
        if (rb != null && si != null)
        {
            if (o == itemInHands)
            {
                itemInHands = null;
            }
            if (rb.isKinematic == false)
            {
                rb.isKinematic = true;
            } else
            {
                rb.isKinematic = false;
            }
        }
    }

    void spawnObject ()
    {
        if (itemInHands == null)
        {
            GameObject item = getCurrentItem();
            if (controller.canSpawn(item))
            {
                Vector3 newPos = Camera.main.transform.position + (Camera.main.transform.forward * 3);
                controller.updateStatus("spawned " + item.GetComponent<SpawnableItem>().name);
                Instantiate(item, newPos, Camera.main.transform.rotation);
                controller.useResources(item);
            }
            else
            {
                Debug.Log("Can't spawn this object. Need more resources!");
                controller.updateStatus("not enough resources");
            }
        }
    }

    void deleteObject()
    {
        GameObject o = playerRaycast(playerRange);
        SpawnableItem sp = o.GetComponent<SpawnableItem>();
        if (o != null && sp != null)
        {
            Debug.Log("deleted " + o.name);
            controller.freeResources(o);
            controller.updateStatus("deleted " + sp.name);
            Destroy(o);
        }
    }

    GameObject getCurrentItem ()
    {
        return controller.getCurrentItem();
    }

    //r = range
    GameObject playerRaycast(int r)
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, r))
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }
}
