using DG.Tweening;
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

    private float _timer;

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
        if (_timer > 0.1f)
        {
            cameraBody.DOMove(playerTransform.position
            + playerTransform.right * cameraPositionShift.x
            + playerTransform.up * cameraPositionShift.y
            + playerTransform.forward * cameraPositionShift.z, 0.1f);

            cameraBody.DOLookAt(playerTransform.position + Vector3.up * 1.7f, 0.1f);
        }
        else
        {
            _timer += Time.deltaTime;
        }

        //cameraBody.position = cameraPositionShift + playerTransform.position;

        /*
        cameraBody.position = playerTransform.position
            + playerTransform.right * cameraPositionShift.x
            + playerTransform.up * cameraPositionShift.y
            + playerTransform.forward * cameraPositionShift.z;            

        cameraBody.LookAt(playerTransform);
        cameraBody.localEulerAngles = new Vector3(cameraRotationShift.x, cameraBody.localEulerAngles.y, 0);
        */
    }
}
