using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    
    [Header("动画")]
    private Animator anim;
    [Header("组件")]
    EnemyMovement movement;
    public Collider2D coll;
    [Header("环境监测")]
    public LayerMask Ground;

    void Start()
    {
        anim = GetComponent<Animator>();
        movement =GetComponentInParent<EnemyMovement>();
        coll = GetComponent<Collider2D>();


    }


    void Update()
    {
        SwitchAnim();
    }
    void SwitchAnim()
    {
       if(coll.IsTouchingLayers(Ground))
       {
        anim.SetBool("running",true);
       }
    }
}
