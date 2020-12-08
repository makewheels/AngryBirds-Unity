using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private bool isClick = false;
    public Transform rightPosition;
    private float maxDistance = 1.6f;

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
        }
    }

    private void OnMouseDown()
    {
        isClick = true;
    }

    private void OnMouseUp()
    {
        isClick = false;
    }
}