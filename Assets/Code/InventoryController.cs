using UnityEngine;
using System.Collections;

public class InventoryController : MonoBehaviour {
    public bool hasBat;
    public bool hasRock;
    public bool hasMindControl;
    public bool hasGun;
    public bool hasKey;
    public bool hasEnslavedBat;
    public int resourceCount;
    GameObject inventory;

    // Use this for initialization
    void Start ()
    {
        hasBat = false;
        hasRock = false;
        hasMindControl = false;
        hasGun = false;
        hasKey = false;
        hasEnslavedBat = false;
        resourceCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (InventoryActive())
            {
                DestroyInventory();
            } else
            {
                CreateInventory();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                switch (hit.collider.name)
                {
                    case "ExitButton":
                        DestroyInventory();
                        break;
                }
            }
        }
    }

    public bool InventoryActive()
    {
        return inventory;
    }

    void CreateInventory()
    {
        inventory = GameObject.Instantiate(Resources.Load("Prefabs/Inventory") as GameObject);

        if (hasBat) inventory.transform.Find("InventoryBat").GetComponent<SpriteRenderer>().enabled = true;
        else inventory.transform.Find("InventoryBat").GetComponent<SpriteRenderer>().enabled = false;

        if (hasRock) inventory.transform.Find("InventoryRock").GetComponent<SpriteRenderer>().enabled = true;
        else inventory.transform.Find("InventoryRock").GetComponent<SpriteRenderer>().enabled = false;

        if (hasMindControl) inventory.transform.Find("InventoryMindControl").GetComponent<SpriteRenderer>().enabled = true;
        else inventory.transform.Find("InventoryMindControl").GetComponent<SpriteRenderer>().enabled = false;

        if (hasGun) inventory.transform.Find("InventoryRifle").GetComponent<SpriteRenderer>().enabled = true;
        else inventory.transform.Find("InventoryRifle").GetComponent<SpriteRenderer>().enabled = false;

        if (hasKey) inventory.transform.Find("InventoryKey").GetComponent<SpriteRenderer>().enabled = true;
        else inventory.transform.Find("InventoryKey").GetComponent<SpriteRenderer>().enabled = false;

        if (resourceCount > 0)
        {
            inventory.transform.Find("InventoryResource").GetComponent<SpriteRenderer>().enabled = true;
            inventory.transform.Find("InventoryResourceCount").GetComponent<TextMesh>().text = resourceCount.ToString();
        }
        else
        {
            inventory.transform.Find("InventoryResource").GetComponent<SpriteRenderer>().enabled = false;
            inventory.transform.Find("InventoryResourceCount").GetComponent<TextMesh>().text = "";
        }
    }

    void DestroyInventory()
    {
        if (inventory)
        {
            GameObject.Destroy(inventory);
        }
    }

    public void UpdateBat(bool newState)
    {
        hasBat = newState;
    }
    public void UpdateRock(bool newState)
    {
        hasRock = newState;
    }
    public void UpdateMindControl(bool newState)
    {
        hasMindControl = newState;
    }
    public void UpdateGun(bool newState)
    {
        hasGun = newState;
    }
    public void UpdateKey(bool newState)
    {
        hasKey = newState;
    }
    public void UpdateEnslavedBat(bool newState)
    {
        hasEnslavedBat = newState;
    }
    public void UpdateResources(int newState)
    {
        resourceCount = newState;
    }
}
