using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float range;
    public LayerMask targetLayer;
    public GameObject test;
    public AudioSource source;
    public float fireTimeStep = 1f;

    private PlayerControls controls;
    private float fireTime = 0f;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.1f);
    private LineRenderer laserLine;
    private Camera cam;



    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Fire.performed += ctx => Fire();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        source = GetComponent<AudioSource>();
        laserLine = GetComponent<LineRenderer>();
        cam = GetComponentInParent<Camera>();
    }


    private void Fire()
    {
        if(Time.time >= fireTime)
        {

            StartCoroutine(ShotEffect());

            laserLine.SetPosition(0, transform.position);

            RaycastHit hit;
            RaycastHit lineHit;

            //Gets the center of the screen and returns it as a Vector 3 in the world sapce
            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

            //Gets the end of the ray and sets that position for the line renderer
            if (Physics.Raycast(rayOrigin, transform.forward.normalized * range, out lineHit, range))
            {
                laserLine.SetPosition(1, lineHit.point);
            }

            //Checks to see if ray hits target and destroys it
            if (Physics.Raycast(rayOrigin, transform.forward.normalized * range, out hit, range, targetLayer))
            {
                ITarget target = hit.collider.GetComponent<ITarget>();

                if (target != null)
                {
                    target.TargetHit(hit.point);
                    //Instantiate(test, hit.point, Quaternion.identity);
                }
            }

            fireTime = Time.time + fireTimeStep;
        }
    }

    private IEnumerator ShotEffect()
    {
        laserLine.enabled = true;

        yield return shotDuration;

        laserLine.enabled = false;
    }
}
