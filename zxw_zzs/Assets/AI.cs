using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public float differenceTime;
    
    public float maxspeed;
    public float midspeed;
    public float minspeed;
    public Animator animator;
    private float time;
    private int n;//是否随机移动
    private int x;
    private float randomTime;
    public float speed;
    public float Rospeed;
    public float y;
    public float seeDistance;
    public Vector3 dir;
    public GameObject point;
    public float leaveDistance = 30;
    private float disToPoint;
    private Vector3 roToPoint;
    private Vector3 toNeighbor;
    private float disTonei;
    private float rotNei;
    private Vector3 center;
    private float disToCenter;
    private float maxDis = 5;
    private Vector3 roToCenter;

    public bool isPlan;
    public GameObject[] colliders;

    private GameObject[] colliders1;
    public List<GameObject> neighbor;
    public float time1;
    private float cheakInterval = 2f;//检测鱼群的间隔
    public float cheakRadius = 20;//检测鱼群的banjing
    public LayerMask cheakLayer;//检测层
    public Vector3 avDis;
    private int neighborCount;
    public float comfortDistance = 3;
    void Start()
    {
        animator.SetBool("IsSwim", true);
        //point = GameObject.FindWithTag("home");
        n = 1;
        randomTime = Random.Range(0.2f, 1);
        
        colliders = GameObject.FindGameObjectsWithTag("Colider");
        colliders1 = GameObject.FindGameObjectsWithTag("Fish");
    }

    // Update is called once per frame
    void Update()
    {
        disToPoint = Vector3.Distance(this.transform.position, point.transform.position);
        roToPoint = point.transform.position - this.transform.position;
        if (disToPoint > leaveDistance)
        {
            dir = avDis + roToPoint;
            n = 0;
        }
        else
        {
            n = 1;
        }
        time += (Time.deltaTime * n);
        //*****************
        //time1 += Time.deltaTime;
        if (time1 > cheakInterval)
        {
            neighbor.Clear();
            for (int i = 0; i < colliders1.Length; i++)
            {
                neighbor.Add(colliders1[i].gameObject);
            }
            foreach (GameObject s in neighbor)
            {

                if ((s != null) && (s != this.gameObject))
                {
                    avDis += s.transform.forward;
                    center += transform.position;
                }

                disTonei = Vector3.Distance(s.transform.position, this.transform.position);
                toNeighbor = (this.transform.position - s.transform.position);
                rotNei = Vector3.Dot(transform.forward.normalized, toNeighbor.normalized);
                if (rotNei >= 0)
                {
                    speed = Random.Range(midspeed, maxspeed);
                }
                else
                {
                    speed = Random.Range(minspeed, midspeed);
                }
                if ((s != null) && (s != this.gameObject))
                {
                    avDis += s.transform.forward;
                }
                neighborCount++;
                time1 = 0;
            }
            avDis /= neighborCount;
            center /= neighborCount;
            dir = avDis;
            disToCenter = Vector3.Distance(center, this.transform.position);
            roToCenter = center - this.transform.position;
            if (disTonei < comfortDistance)
            {
                dir = avDis + toNeighbor;
                n = 0;
            }
            else
            {
                n = 1;
            }
        
            if (disToCenter > leaveDistance)
            {
                dir = avDis + roToCenter * 10;
                n = 0;
            }
            else
            {
                n = 1;
            }
        }

        //*************
        if (time > (randomTime+differenceTime))
        {
            y = Random.Range(-10, 10);
            if (isPlan == true)
            {
                y = 0;
            }
            dir = new Vector3(Random.Range(-10, 10), y, Random.Range(-10, 10));
            time = 0;
            randomTime = Random.Range(0.2f, 3);
        }
        this.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        Quaternion quaDir = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, quaDir, Time.fixedDeltaTime * Rospeed);
        Ray ray1 = new Ray(this.transform.position, this.transform.forward);
        //Debug.DrawRay(this.transform.position, this.transform.forward, Color.red, seeDistance);
        RaycastHit hit;
        if (Physics.Raycast(ray1, out hit, seeDistance))
        {
            if (hit.collider.gameObject.tag == "Wall")
            {
                float rotdir = Vector3.Dot(transform.forward, hit.normal);
                if (rotdir >= 0)
                {
                    this.transform.Rotate(Vector3.up * 1888 * Time.deltaTime);
                }
                else
                    this.transform.Rotate(Vector3.up * -1555 * Time.deltaTime);
            }

        }
        foreach (var c in colliders)
        {
            float disToFood = Vector3.Distance(c.transform.position, this.transform.position);
            if (c.tag == "Colider" && disToFood < (100 * x))
            {
                // n = 0;
                dir = c.transform.position - this.transform.position;
                x = 100;
            }
            else
            {
                n = 1;
                x = 1;
            }
        }
    }
}
