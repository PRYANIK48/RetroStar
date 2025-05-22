using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Player;

public class CameraController : Singleton<CameraController>
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Start() {
        SetPlayerCameraFollow();
    }

    public void SetPlayerCameraFollow() {
        cinemachineVirtualCamera = FindFirstObjectByType<CinemachineVirtualCamera>();
        cinemachineVirtualCamera.Follow = PlayerEntity.instance.transform;
    }
}
