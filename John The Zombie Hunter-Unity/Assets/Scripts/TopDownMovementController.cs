using System.Collections.Generic;
using UnityEngine;

public class TopDownMovementController : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 4;
    [SerializeField] private Animator m_animator = null;

    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;
    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;
    
    private Plane playerMovementPlane;
    private Vector3 playerToMouse;

    private void Awake()
    {
        playerMovementPlane = new Plane(transform.up, transform.position + transform.up);
    }

    private void FixedUpdate()
    {
        m_animator.SetBool("Grounded", true);

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        bool walk = Input.GetKey(KeyCode.LeftShift);

        if (v < 0)
        {
            if (walk) { v *= m_backwardsWalkScale; }
            else { v *= m_backwardRunScale; }
        }
        else if (walk)
        {
            v *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;

        Vector3 cursorScreenPosition = Input.mousePosition;

        Vector3 cursorWorldPosition = ScreenPointToWorldPointOnPlane(cursorScreenPosition, playerMovementPlane, Camera.main);

        playerToMouse = cursorWorldPosition - transform.position;

        playerToMouse.y = 0f;

        playerToMouse.Normalize();

        transform.rotation = Quaternion.LookRotation(playerToMouse);

        m_animator.SetFloat("MoveSpeed", m_currentV);

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
            Destroy(other.gameObject);
        }
    }
}
