using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_pickUp : MonoBehaviour
{

    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, B_Object_Container, fpsCam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    // Start is called before the first frame update
    void Start()
    {
        //Setup
        if (!equipped)
        {
            rb.isKinematic = false;
            coll.isTrigger = false;
        }

        if (equipped)
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();

        //Drop if equipped and "Q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();

    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        //Make object a child of the camera and move it to default position
        transform.SetParent(B_Object_Container);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //Make Rigidbody kinematic and Box Collider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        //Set parent to null
        transform.SetParent(null);

        //Make Rigidbody not kinematic and Box Collider normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        //Object carries momentum of player
        


        //AddForce
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);


    }
}
