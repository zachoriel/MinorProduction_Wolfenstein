using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public AimAndControlsSetting invertedControls;

    public float speedH;
    public float speedV;

    public float yaw;

    [Tooltip("add pitch for the ability to look in the Y axis")]
    public float pitch;
    public float minPitch;
    public float maxPitch;

    public bool lockCursor = true;
    private bool m_cursorIsLocked = true;

    float ClampAngle(float angle, float from, float to)
    {
        if (angle > 180) angle = 360 - angle;
        angle = Mathf.Clamp(angle, from, to);
        if (angle < 0) angle = 360 + angle;

        return angle;
    }

    void Start()
    {
        //invertedControls = GetComponent<AimAndControlsSetting>();
        speedH = 5f;
    }

    void Update()
    {
        if (invertedControls.invertedMouse == false)
        {
            yaw += Input.GetAxis("Mouse X") * speedH;
        }
        else
        {
            yaw -= Input.GetAxis("Mouse X") * speedH;
        }
        //pitch -= Input.GetAxis("Mouse Y") * speedV;

        //pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.eulerAngles = new Vector3(/*pitch*/0, yaw, 0);
        UpdateCursorLock();
    }

    public void SetCursorLock(bool value)
    {
        lockCursor = value;
        if (!lockCursor)
        {//we force unlock the cursor if the user disable the cursor locking helper
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void UpdateCursorLock()
    {
        //if the user set "lockCursor" we check & properly lock the cursos
        if (lockCursor)
            InternalLockUpdate();
    }

    private void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            m_cursorIsLocked = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            m_cursorIsLocked = true;
        }

        if (m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
