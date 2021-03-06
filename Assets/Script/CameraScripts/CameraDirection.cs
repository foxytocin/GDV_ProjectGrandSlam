﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CameraDirection : MonoBehaviour {
    public Vector3 target;
    public Vector3 targetCamPos;
    private CameraMovement cm;
    private RulesScript rules;
    private Vector3 vectorFromCamToTarget;
    private Quaternion lookRotation;
    private float orbitSpeed = 0.01f;
    float offsetCamLookAtTarget;

    private float degreesPerSecond = 300f;

    void Start()
    {
        //Offset Camera
        transform.localPosition = new Vector3(-1f, -1f, -11f);

        cm = FindObjectOfType<CameraMovement>();
        rules = FindObjectOfType<RulesScript>();
        offsetCamLookAtTarget = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //resultscreenmode
        if (rules.resultScreen.activeSelf)
        {
            if(cm.roundPlayers == 1)
            {
                transform.position = transform.position;
            }
            else
            {
                //Orbiting
                if (transform.position.y <= 2.1f)
                {
                    target = cm.centerPoint - new Vector3(offsetCamLookAtTarget, 0, 0);
                    vectorFromCamToTarget = target - transform.position;
                    lookRotation = Quaternion.LookRotation(vectorFromCamToTarget);
                    transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * (degreesPerSecond / 360f));
                    transform.Translate(Vector3.left * Mathf.Clamp(orbitSpeed, 0.001f, 0.9f) * Time.deltaTime);
                    orbitSpeed += 0.003f;
                    offsetCamLookAtTarget -= 0.01f;
                    offsetCamLookAtTarget = Mathf.Clamp(offsetCamLookAtTarget, 0.0001f, 2f);

                }
                else //Zoom
                {
                    target = cm.centerPoint - new Vector3(2f, 0, 0);
                    targetCamPos = target + new Vector3(-2.5f, 2f, -4f);
                    transform.position = Vector3.Lerp(transform.position, targetCamPos, 0.75f * Time.deltaTime);

                    vectorFromCamToTarget = target - transform.position;
                    lookRotation = Quaternion.LookRotation(vectorFromCamToTarget);
                    transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * (degreesPerSecond / 360f));

                    if (Camera.main.transform.localEulerAngles.y < 10)
                    {
                        Camera.main.transform.localEulerAngles = new Vector3(Camera.main.transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, 0);
                    }
                    else if (Camera.main.transform.localEulerAngles.y > 11)
                    {
                        Camera.main.transform.localEulerAngles = new Vector3(Camera.main.transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, 0);
                    }
                }
            }
            
        }
        else if(cm.nextRoundAnimation)
        {
            Camera.main.transform.localEulerAngles = new Vector3(30f, 0f, 0f);
        }
        //Normal Game mode
        else
        {
            if (cm.MaxZDistancePlayers() < 15)
            {
                target = cm.centerPoint;
            }
            else
            {
                target = cm.centerPoint - new Vector3(0, 0, (cm.MaxZDistancePlayers() - 14f) * 0.4f);
            }
            vectorFromCamToTarget = target - transform.position;
            lookRotation = Quaternion.LookRotation(vectorFromCamToTarget);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * (degreesPerSecond / 360f));

            // Limit lookAt Rotation
            if (Camera.main.transform.localEulerAngles.y < 10)
            {
                Camera.main.transform.localEulerAngles = new Vector3(Mathf.Clamp(Camera.main.transform.localEulerAngles.x, 30f, 80f), Mathf.Clamp(Camera.main.transform.localEulerAngles.y, 0f, 9f), 0);
            }
            else if (Camera.main.transform.localEulerAngles.y > 11)
            {
                Camera.main.transform.localEulerAngles = new Vector3(Mathf.Clamp(Camera.main.transform.localEulerAngles.x, 30f, 80f), Mathf.Clamp(Camera.main.transform.localEulerAngles.y, 350f, 361f), 0);
            }
        }
    }

    public void restartCameraDirection()
    {
        transform.position = transform.position;
        //StartCoroutine(CamMove());
        transform.localPosition = new Vector3(-1f, -1f, -11f);
        target = cm.centerPoint;
    }

    public IEnumerator CamMove()
    {
        Vector3 pos = transform.position;

        Vector3 t = new Vector3(0f, 50f, 0f);

        while (transform.position.y < t.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, t, 0.3f);
            yield return null;
        }

        //transform.position = Vector3.Lerp(pos, new Vector3(0f, 50f, 0f), 4f * Time.deltaTime);
        //yield return null;
    }
}