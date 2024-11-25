using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitUntil(() => Managers.IsInit);

        CinemachineVirtualCamera vcam = Managers.Camera.VirtualCam;
        CinemachineVirtualCamera vcamCore = Managers.Camera.VirtualCamCore;

        vcam.m_Lens.OrthographicSize = Managers.Data.ConfigDatas[0].CameraSize;
        vcamCore.m_Lens.OrthographicSize = Managers.Data.ConfigDatas[0].CameraSize;
    }

    public void Init()
    {
        CinemachineVirtualCamera vcamCore = Managers.Camera.VirtualCamCore;
        vcamCore.gameObject.SetActive(false);
    }
}
