using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

[ExecuteAlways]
public class MonitorDisplay : MonoBehaviour
{
    public BNG.Button resetButton;
    public TMPro.TextMeshPro monitorText;
    
    public Transform firstLerpTransform;
    public Transform secondLerpTransform;
    public float lerpSpeedFactor = 0.25f;

    Camera startPosCamera;
    Transform playerEyeTransform;
    RenderTexture rt;
    MeshRenderer r;

    bool isResetting = false;
    float resetTime = 0.0f;
    float firstLerpTime = 0.0f;
    float secondLerpTime = 0.0f;
    float lerpDelay = 0.0f;

    Vector3 firstLerpBeginPos;
    Quaternion firstLerpBeginRot;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<MeshRenderer>();

        Resolution res = Screen.currentResolution;
        rt = new RenderTexture(1832, 1920, 1);
        rt.antiAliasing = 4;

        startPosCamera = FindObjectOfType<StartPosCamera>().GetComponent<Camera>();
        playerEyeTransform = FindObjectOfType<PlayerCameraTransform>().transform;
        startPosCamera.targetTexture = rt;
        r.sharedMaterial.SetTexture("_BaseMap", rt);
        r.sharedMaterial.SetFloat("_ResetTime", 0.0f);

        UnityEvent ev = new UnityEvent();
        ev.AddListener(() => 
        {
            isResetting = true; 
            firstLerpBeginPos = playerEyeTransform.position;
            firstLerpBeginRot = playerEyeTransform.rotation;
        });

        resetButton.onButtonDown = ev;
    }

    // Update is called once per frame
    void Update()
    {
        if(isResetting)
        {
            
            if(firstLerpTime < 1.0f)
            {
                firstLerpTime += Time.deltaTime * lerpSpeedFactor;

                playerEyeTransform.position = Vector3.Slerp(firstLerpBeginPos, firstLerpTransform.position, firstLerpTime);
                playerEyeTransform.rotation = Quaternion.Slerp(firstLerpBeginRot, firstLerpTransform.rotation, firstLerpTime);
            }
            else if(resetTime < 1.0f)
            {
                resetTime += Time.deltaTime * lerpSpeedFactor;

                monitorText.alpha = Mathf.Max(0.0f, 1.0f - resetTime);
                r.sharedMaterial.SetFloat("_ResetTime", resetTime);
            }
            else if(secondLerpTime < 1.0f)
            {
                secondLerpTime += Time.deltaTime * lerpSpeedFactor;

                playerEyeTransform.position = Vector3.Slerp(firstLerpTransform.position, secondLerpTransform.position, secondLerpTime);
                playerEyeTransform.rotation = Quaternion.Slerp(firstLerpTransform.rotation, secondLerpTransform.rotation, secondLerpTime);

                Debug.Log("Second lerp");
            }
            else if(lerpDelay < 1.0f)
            {
                lerpDelay += lerpDelay * lerpSpeedFactor;
            }
            else
            {
                firstLerpTime = 0.0f;
                secondLerpTime = 0.0f;
                lerpDelay = 0.0f;
                resetTime = 0.0f;

                monitorText.alpha = 1.0f;
                r.sharedMaterial.SetFloat("_ResetTime", 0.0f);
            }
        }
    }
}