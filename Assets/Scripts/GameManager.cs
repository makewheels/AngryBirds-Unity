using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Bird> birds;
    public List<Pig> pigs;
    public static GameManager _instance;
    private Vector3 birdOrigionalPosition;

    public GameObject win;
    public GameObject lose;

    public GameObject starLeft;
    public GameObject starMiddle;
    public GameObject starRight;

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
        for (int i = 0; i < birds.Count; i++)
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
                birds[0].transform.rotation = Quaternion.identity;
                initBirds();
            }
            else
            {
                //没鸟了，输了
                lose.SetActive(true);
            }
        }
        else
        {
            //猪没了，赢了
            win.SetActive(true);
        }
    }

    //显示星星
    public void showStars()
    {
        int birdsCount = birds.Count;
        //如果剩一只或者没鸟了，显示一颗星星
        if (birdsCount == 0 || birdsCount == 1)
        {
            starLeft.SetActive(true);
        }
        else if (birdsCount == 2)
        {
            //如果剩两只，显示两颗星星
            starLeft.SetActive(true);
            StartCoroutine("showMiddleStar");
        }
        else
        {
            //如果剩三颗及以上，显示三可星星
            starLeft.SetActive(true);
            StartCoroutine("showMiddleAndRightStar");
        }
    }

    IEnumerator showMiddleStar()
    {
        yield return new WaitForSeconds(0.65f);
        starMiddle.SetActive(true);
    }

   IEnumerator showMiddleAndRightStar()
    {
        yield return new WaitForSeconds(0.65f);
        starMiddle.SetActive(true);
        yield return new WaitForSeconds(0.65f);
        starRight.SetActive(true);
    }
}
