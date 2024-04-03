using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineDoor : MonoBehaviour
{
    //组件获取
    public Animator anim;
    int openID;
    int trapsLayer;
    void Start()
    {
        //组件获取
        anim = GetComponent<Animator>();
        openID = Animator.StringToHash("Open");
        trapsLayer = LayerMask.NameToLayer("Player");
        
    }
    //通过令自己的碰撞体接触到玩家的碰撞体从而触发效果
    private void OnTriggerEnter2D(Collider2D collision)
    {
      if(collision.gameObject.layer == trapsLayer)
      {
        anim.SetTrigger(openID);
      }
    }
}
