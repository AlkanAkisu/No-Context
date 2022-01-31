using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
using System;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] CinamechineInfo[] cams;
    [SerializeField] CameraTypes currentType = CameraTypes.Bedroom;
    void OnValidate()
    {
        ChangeToSelectedCam(currentType);
    }

    private void ChangeAllToDefault()
    {
        foreach (var cam in cams)
        {
            cam.virtualCamera.Priority = 10;
        }
    }
    private void ChangeToSelectedCam(CameraTypes cameraType)
    {
        currentType = cameraType;
        ChangeAllToDefault();
        Array.Find(cams, (cam) => cam.currentType == cameraType).virtualCamera.Priority = 20;
    }

    public void ChangeToSelected(CameraTypes cameraType)
    {
        ChangeToSelectedCam(cameraType);
    }
    public CinemachineVirtualCamera GetCurrentCinemachine() => Array.Find(cams, (cam) => cam.currentType == currentType).virtualCamera;
}

public enum CameraTypes
{
    Bedroom,
    Kitchen,
    BedMonster,
    Monster,
    Forest1,
    Forest2,
    Bedroom2,
    Kitchen2,
    Bedroom3,
    Kitchen3,
    Bedroom4,
    Kitchen4,
    Encounter1,
    Encounter2,
    Encounter3,





}

[System.Serializable]
public class CinamechineInfo
{
    public CameraTypes currentType;
    public CinemachineVirtualCamera virtualCamera;

}

[System.Serializable]
public class CamEvent : UnityEvent<CameraTypes>
{
}