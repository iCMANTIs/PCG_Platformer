using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public float hookSpeed = 10f;
    public float pullDuration = 2f;
    public float hookCooldown = 5f;

    private bool isHooking = false;
    private Vector2 hookPoint;
    private float hookTimer;
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;  // 初始时隐藏线
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isHooking && hookTimer <= 0)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePosition - (Vector2)transform.position);

            if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                hookPoint = hit.point;
                StartCoroutine(HookRoutine());
            }
        }

        if (isHooking)
        {
            DrawHookLine();
        }

        hookTimer -= Time.deltaTime;
    }

    private void DrawHookLine()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, hookPoint);
    }

    private IEnumerator HookRoutine()
    {
        isHooking = true;
        float elapsedTime = 0;

        Vector2 initialPosition = transform.position;

        while (elapsedTime < pullDuration)
        {
            transform.position = Vector2.Lerp(initialPosition, hookPoint, elapsedTime / pullDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isHooking = false;
        lineRenderer.enabled = false;
        hookTimer = hookCooldown;
    }
}