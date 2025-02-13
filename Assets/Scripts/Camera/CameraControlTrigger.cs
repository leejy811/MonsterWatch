using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;

public class CameraControlTrigger : MonoBehaviour
{
    public bool swapCameras = false;
    public bool panCameraOnContact = false;

    public CinemachineVirtualCamera cameraOnLeft;
    public CinemachineVirtualCamera cameraOnRight;

    public PanDirection panDirection;
    public float panDistance = 3.0f;
    public float panTime = 0.35f;

    private Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(panCameraOnContact)
            {
                CameraManager.instance.PanCameraOnContact(panDistance, panTime, panDirection, false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log(cameraOnLeft + ", " + cameraOnRight);
            Vector2 exitDirection = (collision.transform.position - col.bounds.center).normalized;
            if (swapCameras && cameraOnLeft != null && cameraOnRight != null)
            {
                CameraManager.instance.SwapCamera(cameraOnLeft, cameraOnRight, exitDirection);
            }

            if (panCameraOnContact)
            {
                CameraManager.instance.PanCameraOnContact(panDistance, panTime, panDirection, true);
            }
        }
    }
}


[System.Serializable]
public class CustomInspectorObjects
{

}

public enum PanDirection
{
    Up,
    Down,
    Left,
    Right
}

//[CustomEditor(typeof(CameraControlTrigger))]
//public class MyScriptEditor : Editor
//{
//    CameraControlTrigger cameraControlTrigger;

//    private void OnEnable()
//    {
//        cameraControlTrigger = (CameraControlTrigger)target;
//    }

//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();

//        if(cameraControlTrigger.swapCameras)
//        {
//            cameraControlTrigger.cameraOnLeft = EditorGUILayout.ObjectField("Camera on Left", cameraControlTrigger.cameraOnLeft,
//                typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;

//            cameraControlTrigger.cameraOnLeft = EditorGUILayout.ObjectField("Camera on Right", cameraControlTrigger.cameraOnRight,
//                typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;
//        }

//        if(cameraControlTrigger.panCameraOnContact)
//        {
//            cameraControlTrigger.panDirection = (PanDirection)EditorGUILayout.EnumPopup("Camera Pan Direction",
//                cameraControlTrigger.panDirection);

//            cameraControlTrigger.panDistance = EditorGUILayout.FloatField("Pan Distance", cameraControlTrigger.panDistance);
//            cameraControlTrigger.panTime = EditorGUILayout.FloatField("Pan Time", cameraControlTrigger.panTime);
//        }

//        if (GUI.changed) 
//        { 
//            EditorUtility.SetDirty(cameraControlTrigger);
//        }
//    }
//}
