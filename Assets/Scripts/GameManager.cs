using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Bird> birds;
    public List<Pig> pigs;
    public static GameManager _instance;
    private Vector3 birdOrigionalPosition;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        birdOrigionalPosition = birds[0].transform.position;
        initBirds();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void initBirds()
    {
        for(int i = 0; i < birds.Count; i++)
        {
            if (i == 0)
            {
                birds[i].enabled = true;
                birds[i].springJoint2D.enabled = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].springJoint2D.enabled = false;
            }
        }
    }


    public void nextBird()
    {
        if (pigs.Count > 0)
        {
            if (birds.Count > 0)
            {
                //飞下一只鸟
                birds[0].transform.position = birdOrigionalPosition;
                initBirds();
            }
            else
            {
                //没鸟了，输了
            }
        }
        else
        {
            //猪没了，赢了
        }
    }
}
