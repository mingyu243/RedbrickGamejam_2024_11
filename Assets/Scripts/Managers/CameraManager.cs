using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // 시네머신 카메라 바꿀 수 있게 해서 보스 뜰 때나,
    // 넥서스 터질 때 카메라 연출 할 예정?

    [SerializeField] Camera _minimapCam;
    [Space]
    [SerializeField] CinemachineVirtualCamera _virtualCam;

    public Camera MinimapCam => _minimapCam;
    public CinemachineVirtualCamera VirtualCam => _virtualCam;
}
