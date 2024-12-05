using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform boomTransform;

    public float cameraZoom = 10f;
    public float sensitivity = 10f;
    public Vector3 offset;
    public float verticalRotationMin, verticalRotationMax;
    public LayerMask avoidLayer;

    public Transform transformToFollow;

    private float _currentHorizontalRotation;
    private float _currentVerticalRotation;
    private float _idealCameraZoom;
    private float _currentCameraZoom;

    void Start()
    {
        _currentHorizontalRotation = transform.localEulerAngles.y;
        _currentVerticalRotation = transform.localEulerAngles.x;
        _currentCameraZoom = cameraZoom;
        _idealCameraZoom = cameraZoom;

        CursorManager.DisableCursor();
    }

    void Update()
    {
        if (!GameManager.Instance.gameActive)
            return;

        // Camera rotation
        _currentHorizontalRotation += Input.GetAxis("Mouse X") * sensitivity;
        _currentVerticalRotation -= Input.GetAxis("Mouse Y") * sensitivity;
        _currentVerticalRotation = Mathf.Clamp(_currentVerticalRotation, verticalRotationMin, verticalRotationMax);

        transform.localEulerAngles = new Vector3(0, _currentHorizontalRotation, 0);
        boomTransform.localEulerAngles = new Vector3(_currentVerticalRotation, 0, 0);

        // Move camera away from walls
        Vector3 directionToCamera = (cameraTransform.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, directionToCamera, out RaycastHit hit, _idealCameraZoom, avoidLayer))
            _currentCameraZoom = hit.distance;
        else
            _currentCameraZoom = _idealCameraZoom;

        transform.position = transformToFollow.position + offset;

        cameraTransform.localPosition = new Vector3(0, 0, -_currentCameraZoom);
    }
}
