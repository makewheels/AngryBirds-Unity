using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private bool isClick = false;
    
    private float maxDistance = 1.5f;

    [HideInInspector]
    public SpringJoint2D springJoint2D;
    private Rigidbody2D rigidBody;

    public LineRenderer left;
    public LineRenderer right;
    public Transform leftPosition;
    public Transform rightPosition;

    public GameObject boom;

    private TestMyTrail myTrail;

    //限定每只鸟只能飞一次
    private bool canMove = true;

    private void Awake()
    {
        springJoint2D = GetComponent<SpringJoint2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TestMyTrail>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isClick)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position -= new Vector3(0, 0, Camera.main.transform.position.z);
            float distance = Vector2.Distance(transform.position, rightPosition.position);

            //小鸟拖动的最大距离
            if (distance > maxDistance)
            {
                Vector3 direction = (transform.position - rightPosition.position).normalized;
                direction = direction * maxDistance;
                transform.position = direction + rightPosition.position;
            }
            drawLine();
        }
    }

    private void OnMouseDown()
    {
        if (canMove == false)
        {
            return;
        }
        isClick = true;
        rigidBody.isKinematic = true;
    }

    private void OnMouseUp()
    {
        if (canMove == false)
        {
            return;
        }
        isClick = false;
        Invoke("fly", 0.1f);
        rigidBody.isKinematic = false;
        left.enabled = false;
        right.enabled = false;
        canMove = false;
    }

    //小鸟飞出
    private void fly()
    {
        springJoint2D.enabled = false;
        myTrail.startTrail();
        Invoke("nextBird", 2.5f);
    }

    //画弹弓线
    private void drawLine()
    {
        left.enabled = true;
        right.enabled = true;
        right.SetPosition(0, rightPosition.position);
        right.SetPosition(1, transform.position);
        left.SetPosition(0, leftPosition.position);
        left.SetPosition(1, transform.position);
    }

    //下一只鸟
    private void nextBird()
    {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameManager._instance.nextBird();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //当碰撞时，结束拖影
        myTrail.ClearTrail();
    }
}