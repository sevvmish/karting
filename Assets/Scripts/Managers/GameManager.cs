using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject mainCartPrefab;
    [SerializeField] private Joystick joystick;

    private Transform playerTransform;
    private CameraController cameraController;
    private InputController inputController;

    //TODELETE
    private float _timer;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        GameObject g = Instantiate(mainCartPrefab);
        g.name = "Main Player";
        playerTransform = g.transform;
        playerTransform.position = Vector3.zero;

        cameraController = GetComponent<CameraController>();
        cameraController.SetCameraController(GameObject.Find("Main Camera").GetComponent<Camera>(), playerTransform);

        inputController = GetComponent<InputController>();
        inputController.SetInputController(joystick, playerTransform.GetComponent<ArcadeKart>());
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        print(_timer.ToString("f0") + " !!!");
    }


}
