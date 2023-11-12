using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Target> targets;
    public Door entranceDoor;
    public Door exitDoor;

    void Update()
    {
        CheckTargets();
    }

    //Whem all of the targets are destroyed in the room, open the exit door
    private void CheckTargets()
    {
        if(targets.Count < 1 && exitDoor)
        {
            exitDoor.ChangeDoorState(true);
            
        }
    }

    //When the player enters the room, close the door behind it
    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            if (entranceDoor != null)
            {
                entranceDoor.RemoveBoxCollider();

                entranceDoor.ChangeDoorState(false);
            }
        }
    }
}
