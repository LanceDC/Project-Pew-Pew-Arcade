using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public DoorStatus status;
    public AudioSource source;

    [HideInInspector] public bool isLocked = false;

    private Animator doorAnimator;
    
    void Start()
    {
        doorAnimator = GetComponent<Animator>();

        if(status == DoorStatus.Locked)
        {
            isLocked = true;
        }
        else if(status == DoorStatus.Unlocked)
        {
            isLocked = false;
            AddBoxCollider();
        }
    }

    void Update()
    {
        doorAnimator.SetBool("isOpen", isOpen);

    }

    public void AddBoxCollider()
    {
        BoxCollider box = gameObject.AddComponent<BoxCollider>();
        box.size = new Vector3(5f, 7, 5f);
        box.isTrigger = true;
    }

    public void RemoveBoxCollider()
    {
        if(TryGetComponent(typeof(BoxCollider), out Component component))
        {
            Destroy(component);
        }
    }

    public void ChangeDoorState(bool newState)
    {
        if(newState != isOpen)
        {
            source.Play();
            isOpen = newState;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            ChangeDoorState(true);
        }
    }


    void OnTriggerExit(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            ChangeDoorState(false);
        }
    }
}

public enum DoorStatus
{
    Locked,
    Unlocked
}
