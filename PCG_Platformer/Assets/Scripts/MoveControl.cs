using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
 
public Transform fire;//射线发射点
public LineRenderer lineRenderer;
public float minpos = -0.35f;    //电光效果每个节点的最小值
public float maxpos = 0.35f;     //最大值
int trapsLayer;
    public float LaunchingTime = 1.0f;
    // 持续间隔
    public float IntervalTime = 2.0f;
    // 一个发射周期的时间 = IntervalTime + LaunchingTime
    private float cycleTime;
    // 临时累计时间，一个周期结束后清零
    private float AccTime =-1.0f;
    PlayerController pc;


void Start()
{
    trapsLayer = LayerMask.NameToLayer("Player");
    cycleTime = IntervalTime + LaunchingTime;
}
void shotline()//激光
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(fire.position,Vector2.right);
        //光线投射，返回障碍物
        if (hit && lineRenderer.enabled == true)//如果遇到障碍物且射线打开
        {
            //射线的起始点
            lineRenderer.SetPosition(0, fire.position);
 
            //因为激光只有一个终点，所以障碍物位置为终点
            lineRenderer.SetPosition(1, hit.point);
          
        }
    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      if(collision.gameObject.layer == trapsLayer && AccTime <= 0 && AccTime <= LaunchingTime)
      {
            lineRenderer.enabled = true;
            shotline();     //显示激光
            

      }
      else if(AccTime > LaunchingTime && AccTime <= cycleTime)
      {
        
            lineRenderer.enabled = false;
      }
        AccTime += Time.deltaTime;
        if (AccTime > cycleTime)
        {
            AccTime = 0;
        }
    }
}
