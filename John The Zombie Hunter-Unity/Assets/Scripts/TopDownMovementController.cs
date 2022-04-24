using System.Collections.Generic;
using UnityEngine;

public class TopDownMovementController : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 4;
    [SerializeField] private Animator m_animator = null;

    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 5;

    private Plane playerMovementPlane;
    private Vector3 playerToMouse;

    private void Awake()
    {
        playerMovementPlane = new Plane(transform.up, transform.position + transform.up);
    }

    private void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        
        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
        transform.position += transform.right * m_currentH * m_moveSpeed * Time.deltaTime * .75f;

        Vector3 cursorScreenPosition = Input.mousePosition;

        Vector3 cursorWorldPosition = ScreenPointToWorldPointOnPlane(cursorScreenPosition, playerMovementPlane, Camera.main);

        playerToMouse = cursorWorldPosition - transform.position;

        playerToMouse.y = 0f;

        playerToMouse.Normalize();

        transform.rotation = Quaternion.LookRotation(playerToMouse);
        m_animator.SetFloat("InputX", m_currentH);
        m_animator.SetFloat("InputY", m_currentV);
    }

    Vector3 PlaneRayIntersection(Plane plane, Ray ray)
    {
        float dist = 0.0f;
        plane.Raycast(ray, out dist);
        return ray.GetPoint(dist);
    }

    Vector3 ScreenPointToWorldPointOnPlane(Vector3 screenPoint, Plane plane, Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(screenPoint);
        return PlaneRayIntersection(plane, ray);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Chest"))
        {
            GameManager.GM.ObjectiveCaptured();
            if (GameManager.GM.totalObjectives - GameManager.GM.objectivesCaptured == 0)
                GameManager.GM.gameState = GameState.BeatLevel;

            Destroy(other.gameObject);
        }
    }
}
