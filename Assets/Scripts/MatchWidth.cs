

using System;
using UnityEngine;
using Cinemachine;

[ExecuteInEditMode]
[RequireComponent(typeof(CinemachineVirtualCamera))]
public class MatchWidth : MonoBehaviour
{

    // Set this to the in-world distance between the left & right edges of your scene.
    [SerializeField] float sceneWidth = 5;

    CinemachineVirtualCamera m_Cam;


    private void Start()
    {
        m_Cam = GetComponent<CinemachineVirtualCamera>();
    }


    void Update()
    {

        float unitsPerPixel = sceneWidth * 2 / Screen.width;

        float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;

        m_Cam.m_Lens.OrthographicSize = desiredHalfHeight;
    }
}
