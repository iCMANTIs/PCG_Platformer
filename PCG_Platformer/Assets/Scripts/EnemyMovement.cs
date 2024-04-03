using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
     [Header("组件")]
    private Rigidbody2D rb;
    [Header("边界点")]
    public Transform leftpoint,rightpoint;
    private float leftx,rightx;
    [Header("移动参数")]
    public float Speed;
    private bool FaceRight=true;//朝向默认向右
   
 //————————————————————————————————————————————————————————————————————————
        void Start()
        {
        rb = GetComponent<Rigidbody2D>();   
        
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        //断绝与子物体的父子关系，以保持位置点的绝对位置，不跟随人物移动
        transform.DetachChildren();
        }


        void Update()
        {
            
        }
     
    void FixedUpdate()
    {
        Movement();
    }

//—————————————————————————————————————————————————————————————————————————

   //移动代码
    void Movement()
    {
      if(FaceRight)
      {
        rb.velocity = new Vector2(Speed , rb.velocity.y);//默认初始向右移动
        if(transform.position.x>rightx)
          {
            transform.localScale = new Vector3(-1,1,1);
            FaceRight=false;
          }
      }
    else
      {
        rb.velocity = new Vector2(-Speed , rb.velocity.y);//向左移动
        if(transform.position.x<leftx)
            {
                transform.localScale = new Vector3(1,1,1);
                FaceRight=true;
            } 
      }
    }
}
