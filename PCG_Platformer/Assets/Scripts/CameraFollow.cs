using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float followDelay = 1.0f;
    public float upperBound = 15.0f; 
    public float lowerBound = 0f; 

    private bool shouldFollow = false;
    private float delayTimer = 0f;

    private void LateUpdate()
    {
        if (transform.position != target.position)
        {

            if (!shouldFollow)
            {
                delayTimer += Time.deltaTime;

                if (delayTimer >= followDelay)
                {
                    shouldFollow = true;
                    delayTimer = 0f;
                }
            }

            if (shouldFollow)
            {
                Vector3 targetPos = new Vector3(transform.position.x, target.position.y, transform.position.z);

                
                targetPos.y = Mathf.Clamp(targetPos.y, lowerBound, upperBound);

                transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
            }
        }
        else
        {
            shouldFollow = false;
            delayTimer = 0f;
        }
    }
}
