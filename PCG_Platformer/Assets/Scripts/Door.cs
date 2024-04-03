using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform goToPos;//声明去往的坐标
    private Transform playerPos;//声明游戏角色的坐标
 
    void Start()
    {
        playerPos = GameObject.FindWithTag("Player").transform;//获取游戏角色的坐标
    }

    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            playerPos.transform.position = goToPos.transform.position;
        }
    }
}