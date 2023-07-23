using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform GetMainCameraTransfor { get => mainCamera.transform; }

    private Camera mainCamera;
    private Transform cameraBody;
    private Transform playerTransform;
    private Vector3 cameraPositionShift = new Vector3(0,3,-4);
    private Vector3 cameraRotationShift = new Vector3(20,0,0);

    public void SetCameraController(Camera _camera, Transform _player)
    {
        mainCamera = _camera;
        playerTransform = _player;

        GameObject g = new GameObject("Camera Body");
        cameraBody = g.transform;

        mainCamera.transform.parent = cameraBody;
        mainCamera.transform.localEulerAngles = Vector3.zero;
        mainCamera.transform.position = Vector3.zero;

        cameraBody.eulerAngles = cameraRotationShift;
        cameraBody.position = cameraPositionShift + _player.position;
    }

    private void Update()
    {
        cameraBody.position = cameraPositionShift + playerTransform.position;
    }
}
