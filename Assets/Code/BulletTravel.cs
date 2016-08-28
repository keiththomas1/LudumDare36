using UnityEngine;
using System.Collections;

public class BulletTravel : MonoBehaviour {
    public Vector2 direction = new Vector2(0.0f, 0.0f);
    InventoryController inventoryController;
    SoundController soundController;

    // Use this for initialization
    void Start ()
    {
        inventoryController = GameObject.Find("CONTROLLER").GetComponent<InventoryController>();
        soundController = GameObject.Find("CONTROLLER").GetComponent<SoundController>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction);
	}

    public void UpdateDirection(Vector2 _direction)
    {
        direction = _direction;
        Debug.Log(direction);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "WallLeft"
            || col.gameObject.name == "WallRight"
            || col.gameObject.name == "WallTop")
        {
            soundController.PlayGunMissSound();
            GameObject.Destroy(this.gameObject);
        }

        if (col.gameObject.name == "Bat(Clone)")
        {
            soundController.PlayGunHitSound();
            inventoryController.UpdateBat(true);
            GameObject.Destroy(col.gameObject);
            GameObject.Destroy(this.gameObject);
        }

        if (col.gameObject.name == "BatEnslaved(Clone)")
        {
            soundController.PlayGunHitSound();
            var resourceCount = col.gameObject.GetComponent<BatCollection>().resourceCount;
            inventoryController.UpdateResources(resourceCount);
            inventoryController.UpdateEnslavedBat(false);
            inventoryController.UpdateBat(false);
            GameObject.Destroy(col.gameObject);
            GameObject.Destroy(this.gameObject);
        }

        var breakableRock = col.gameObject.GetComponent<BreakableRock>();
        if (col.gameObject.name == "BreakableRock" && !breakableRock.Broken())
        {
            soundController.PlayGunHitSound();
            breakableRock.BreakMore();
            GameObject.Destroy(this.gameObject);
        }
    }
}
