using System.Collections.Generic;
using UnityEngine;

public class TopDownMovementController : MonoBehaviour
{
    private enum ControlMode
    {
        /// <summary>
        /// Up moves the character forward, left and right turn the character gradually and down moves the character backwards
        /// </summary>
        Tank,
        /// <summary>
        /// Character freely moves in the chosen direction from the perspective of the camera
        /// </summary>
        Direct
    }

    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;

    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;

    private bool m_isGrounded;
  
    private Plane playerMovementPlane;

    private RaycastHit floorRaycastHit;

    private Vector3 playerToMouse;

    private void Awake()
    {
        playerMovementPlane = new Plane(transform.up, transform.position + transform.up);
    }

    private void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Transform camera = Camera.main.transform;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            v *= m_walkScale;
            h *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;
        
        Vector3 cursorScreenPosition = Input.mousePosition;

        Vector3 cursorWorldPosition = ScreenPointToWorldPointOnPlane(cursorScreenPosition, playerMovementPlane, Camera.main);

        playerToMouse = cursorWorldPosition - transform.position;

        playerToMouse.y = 0f;

        playerToMouse.Normalize();

        transform.rotation = Quaternion.LookRotation(playerToMouse);

        if (direction != Vector3.zero)
        {

            transform.position += direction * m_moveSpeed * Time.deltaTime;
        }
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
}
