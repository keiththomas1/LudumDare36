using UnityEngine;
using System.Collections;

public class CharWalk : MonoBehaviour {
    public Sprite gunIdle;

    bool currentlyWalking;
    Animator thisAnimator;
    InventoryController inventoryController;
    SoundController soundController;
    InteractiveController interactiveController;
    SpriteRenderer interactionIcon;
    BreakableRock breakableRock;

    float reloadTimer;

	// Use this for initialization
	void Start () {
        currentlyWalking = false;
        thisAnimator = GetComponent<Animator>();
        inventoryController = GameObject.Find("CONTROLLER").GetComponent<InventoryController>();
        soundController = GameObject.Find("CONTROLLER").GetComponent<SoundController>();
        interactiveController = GetComponent<InteractiveController>();
        interactionIcon = transform.Find("InteractionIcon").GetComponent<SpriteRenderer>();
        breakableRock = GameObject.Find("BreakableRock").GetComponent<BreakableRock>();

        reloadTimer = 0.0f;
    }

    // Update is called once per frame
    void Update() {
        if (!inventoryController.InventoryActive() && !interactiveController.OptionsMenuExists())
        {
            CheckInput();
        } else if (interactiveController.OptionsMenuExists())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactiveController.GenerateOptions("");
            }
        }

        if (reloadTimer > 0.0f)
        {
            reloadTimer -= Time.deltaTime;
        }
    }

    void CheckInput() {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            var oldScale = transform.localScale;
            if (oldScale.x < 0.0f)
            {
                oldScale.x = 1.0f;
                transform.localScale = oldScale;
                transform.Translate(new Vector2(-0.1f, 0.0f));
            }
            var oldPosition = transform.position;
            if (oldPosition.x > -2.91f)
            {
                if (!currentlyWalking)
                {
                    currentlyWalking = true;
                    if (inventoryController.hasGun)
                    {
                        thisAnimator.Play("WalkingWithGun");
                    }
                    else
                    {
                        thisAnimator.Play("Walking");
                    }
                }
                oldPosition.x -= .015f;
                transform.position = oldPosition;
            }
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            var oldScale = transform.localScale;
            if (oldScale.x > 0.0f)
            {
                oldScale.x = -1.0f;
                transform.localScale = oldScale;
                transform.Translate(new Vector2(0.1f, 0.0f));
            }
            var oldPosition = transform.position;
            if (oldPosition.x < 2.66f)
            {
                if (!currentlyWalking)
                {
                    currentlyWalking = true;
                    if (inventoryController.hasGun)
                    {
                        thisAnimator.Play("WalkingWithGun");
                    } else
                    {
                        thisAnimator.Play("Walking");
                    }
                }
                oldPosition.x += .015f;
                transform.position = oldPosition;
            }
        } else if (Input.GetKeyDown(KeyCode.Space) &&
            inventoryController.hasGun &&
            reloadTimer <= 0.0f)
        {
            soundController.PlayGunShotSound();
            reloadTimer = 0.5f;
            var bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject);
            var bulletPosition = transform.position;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                thisAnimator.Play("ShootingUp");
                bulletPosition.y += 0.3f;
                bullet.transform.position = bulletPosition;
                bullet.GetComponent<BulletTravel>().UpdateDirection(new Vector2(0.0f, 0.05f));
            }
            else if (transform.localScale.x < 0.0f)
            {
                thisAnimator.Play("ShootingStraight");
                bulletPosition.x += 0.3f;
                bullet.transform.position = bulletPosition;
                bullet.GetComponent<BulletTravel>().UpdateDirection(new Vector2(.05f, 0.0f));
            }
            else
            {
                thisAnimator.Play("ShootingStraight");
                bulletPosition.x -= 0.3f;
                bullet.transform.position = bulletPosition;
                bullet.GetComponent<BulletTravel>().UpdateDirection(new Vector2(-.05f, 0.0f));
            }
        }

        // Interactions
        if (transform.position.x < -2.83f)
        {
            interactionIcon.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                StopWalking();
                interactiveController.GenerateOptions("Rock");
            }
        }
        else if (((transform.localScale.x > 0.0f) &&
            (transform.position.x > -1.73f && transform.position.x < -1.52f)) ||
            ((transform.localScale.x < 0.0f) &&
            (transform.position.x > -1.58f && transform.position.x < -1.37f)))
        {
            if (breakableRock.CanCollectKey())
            {
                interactionIcon.enabled = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    soundController.PlayPickupSound();
                    StopWalking();
                    inventoryController.UpdateKey(true);
                    breakableRock.PickUpKey();
                }
            }
        }
        else if (((transform.localScale.x > 0.0f) &&
            (transform.position.x > -.44f && transform.position.x < -.22f)) ||
            ((transform.localScale.x < 0.0f) &&
            (transform.position.x > -.29f && transform.position.x < -.07f)))
        {
            interactionIcon.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                StopWalking();
                interactiveController.GenerateOptions("ResourceCollector");
            }
        }
        else if (((transform.localScale.x > 0.0f) &&
            (transform.position.x > .25f && transform.position.x < 0.5f)) ||
            ((transform.localScale.x < 0.0f) &&
            (transform.position.x > .4f && transform.position.x < 0.65f)))
        {
            interactionIcon.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                StopWalking();
                interactiveController.GenerateOptions("RifleCase");
            }
        }
        else if (((transform.localScale.x > 0.0f) &&
            (transform.position.x > 1.0f && transform.position.x < 1.25f)) ||
            ((transform.localScale.x < 0.0f) &&
            (transform.position.x > 1.15f && transform.position.x < 1.4f)))
        {
            interactionIcon.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                StopWalking();
                interactiveController.GenerateOptions("LockedCabinet");
            }
        }
        else
        {
            interactionIcon.enabled = false;
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)
            || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.RightArrow)
                && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftArrow))
            {
                StopWalking();
            }
        }
	}

    void StopWalking()
    {
        currentlyWalking = false;
        if (inventoryController.hasGun)
        {
            thisAnimator.Play("GunIdle");
        }
        else
        {
            thisAnimator.Play("Idle");
        }
    }
}
