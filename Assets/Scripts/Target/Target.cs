using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, ITarget
{
    [HideInInspector] private float score;
    private GameManager manager;

    [Header("Target Movement;")]
    public float xSpeed;
    public float ySpeed;
    public float speed;
    public float strength;

    private float ranfomOffSet;
    private Vector3 newPos;
    private Vector3 startPos;

    public AudioSource source;

    
    [SerializeField] private float maxScorecore;
    [SerializeField] private Room room;
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private MeshRenderer[] meshs;


    void Start()
    {
        score = maxScorecore;
        manager = FindObjectOfType<GameManager>();
        room.targets.Add(this);

        startPos = transform.position;
        ranfomOffSet = Random.Range(0f, 2f);
        source = GetComponent<AudioSource>();
    }


    void Update()
    {
        newPos = new Vector3(Bobbing(xSpeed), Bobbing(ySpeed), 0f);
        transform.parent.position = newPos + startPos;

    }

    private float Bobbing(float direction)
    {
        return (Mathf.Sin(Time.time * speed + ranfomOffSet) * strength) * direction;
    }

    public void TargetHit(Vector3 hitPos)
    {
        float distance = Vector3.Distance(transform.position, hitPos);
        score /= distance;
        
        score /= 10f;

        score = Mathf.Round(score);

        if (score > maxScorecore)
            score = maxScorecore;

        if (score < 1f)
            score = 1f;

        source.Play();

        for(int i = 0; i < meshs.Length; i++)
        {
            meshs[i].enabled = false;
        }
        meshs[3].GetComponent<MeshCollider>().enabled = false;

        GetComponent<MeshCollider>().enabled = false;

        room.targets.Remove(this);

        Destroy(transform.parent.gameObject, 3f);
    }
}
