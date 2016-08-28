using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class InteractiveController : MonoBehaviour {
    List<GameObject> optionObjects;
    InventoryController inventoryController;
    SoundController soundController;

    float endTimer;

    // Use this for initialization
    void Start () {
        optionObjects = new List<GameObject>();
        inventoryController = GameObject.Find("CONTROLLER").GetComponent<InventoryController>();
        soundController = GameObject.Find("CONTROLLER").GetComponent<SoundController>();
        endTimer = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (endTimer > 0.0f)
        {
            endTimer -= Time.deltaTime;
            if (endTimer <= 0.0f)
            {
                SceneManager.LoadScene("End");
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
                    case "Pick up rock":
                        soundController.PlayPickupSound();
                        inventoryController.UpdateRock(true);
                        DestroyDialog();
                        break;
                    case "Break case":
                        soundController.PlayGlassBreak();
                        GameObject.Instantiate(Resources.Load("Prefabs/GunText") as GameObject);
                        inventoryController.UpdateGun(true);
                        GameObject.Find("GunCase").GetComponent<SpriteRenderer>().enabled = false;
                        GameObject.Find("GunCaseCracked").GetComponent<SpriteRenderer>().enabled = true;
                        DestroyDialog();
                        break;
                    case "Open cabinet":
                        soundController.PlayPickupSound();
                        inventoryController.UpdateMindControl(true);
                        GameObject.Find("Locker").GetComponent<SpriteRenderer>().enabled = false;
                        GameObject.Find("LockerOpen").GetComponent<SpriteRenderer>().enabled = true;
                        DestroyDialog();
                        break;
                    case "Enslave bat":
                        soundController.PlayPickupSound();
                        inventoryController.UpdateEnslavedBat(true);
                        GameObject.Instantiate(Resources.Load("Prefabs/BatEnslaved") as GameObject);
                        DestroyDialog();
                        break;
                    case "Feed fruit":
                        if (inventoryController.resourceCount == 19)
                        {
                            soundController.PlayPickupSound();
                            GameObject.Instantiate(Resources.Load("Prefabs/Ladder") as GameObject);
                            endTimer = 3.0f;
                        }
                        inventoryController.UpdateResources(0);
                        DestroyDialog();
                        break;
                    case "Do Nothing":
                        DestroyDialog();
                        break;
                }
            }
        }
    }

    public bool OptionsMenuExists()
    {
        return optionObjects.Count > 0;
    }

    public void GenerateOptions(string location)
    {
        if (OptionsMenuExists())
        {
            DestroyDialog();
        }
        else
        {
            List<string> options = new List<string>();

            switch (location)
            {
                case "Rock":
                    options.Add("Do Nothing");
                    options.Add("Pick up rock");
                    break;
                case "RifleCase":
                    options.Add("Do Nothing");
                    if (inventoryController.hasRock && !inventoryController.hasGun)
                    {
                        options.Add("Break case");
                    }
                    break;
                case "LockedCabinet":
                    options.Add("Do Nothing");
                    if (!inventoryController.hasEnslavedBat)
                    {
                        if (inventoryController.hasKey && !inventoryController.hasMindControl)
                        {
                            options.Add("Open cabinet");
                        }
                        if (inventoryController.hasMindControl && inventoryController.hasBat)
                        {
                            options.Add("Enslave bat");
                        }
                    }
                    break;
                case "ResourceCollector":
                    options.Add("Do Nothing");
                    if (inventoryController.resourceCount > 0)
                    {
                        options.Add("Feed fruit");
                    }
                    break;
                default:
                    break;
            }

            CreateDialog(options);
        }
    }

    void CreateDialog(List<string> options)
    {
        Vector3 location = new Vector3(transform.position.x + 1.3f, transform.position.y, -2.0f);
        foreach(string option in options)
        {
            var dialogBox = GameObject.Instantiate(Resources.Load("Prefabs/DialogBox") as GameObject);
            dialogBox.transform.position = location;
            dialogBox.name = option;

            var dialogText = dialogBox.transform.Find("DialogText").GetComponent<TextMesh>();
            dialogText.text = option;

            optionObjects.Add(dialogBox);

            location.y += 0.4f; 
        }
    }

    void DestroyDialog()
    {
        foreach(GameObject obj in optionObjects)
        {
            GameObject.Destroy(obj);
        }
        optionObjects.Clear();
    }
}
