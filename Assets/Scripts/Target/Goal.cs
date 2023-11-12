using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public float speed;
    public float strength;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.parent.position;
    }

    void Update()
    {
        Vector3 v = new Vector3(0f, Bobbing(1f), 0f);
        transform.parent.position = v + startPos;

        transform.parent.Rotate(0f, 20f * Time.deltaTime, 0f);
    }

    private float Bobbing(float direction)
    {
        return (Mathf.Sin(Time.time * speed) * strength) * direction;
    }


    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            //Save the time with the game number and name
            FindObjectOfType<GameManager>().SaveGame();

            //Load the scene with the scoreboard
            SceneManagment.LoadScene(2);
        }
    }
}
