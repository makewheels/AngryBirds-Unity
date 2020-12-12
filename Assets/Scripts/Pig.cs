using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    private float maxSpeed =10;
    private float minSpeed =5;
    private SpriteRenderer render;
    public Sprite hurt;
    public GameObject boom;
    public GameObject pigScore;

    public bool isPig = false;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        float velocity = collision2D.relativeVelocity.magnitude;
        //猪的死亡
        if (velocity > maxSpeed)
        {
            PigDead();
        }
        else if(velocity > minSpeed)
        {
            render.sprite = hurt;
        }
    }
    
    //处理猪的后事
    private void PigDead()
    {
        if (isPig)
        {
            GameManager._instance.pigs.Remove(this);
        }
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameObject pigScoreObject
            = Instantiate(pigScore, transform.position + new Vector3(0, transform.position.y + 1.5f, 0), Quaternion.identity);
        Destroy(pigScoreObject, 1.5f);
    }
    

}
