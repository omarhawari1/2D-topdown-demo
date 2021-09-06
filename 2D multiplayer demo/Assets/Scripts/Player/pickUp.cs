using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUp : MonoBehaviour
{
    [SerializeField]private GameObject gun;

    private PlayerScript playerScript;
    private bool alreadyPickedUp = false;

    private void Start() 
    {
        playerScript = transform.parent.gameObject.GetComponent<PlayerScript>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Gun" && alreadyPickedUp == false)
        {
            alreadyPickedUp = true;
            gun.SetActive(true);
            playerScript.gunPickedUp(other.gameObject);
        }
    }
}
