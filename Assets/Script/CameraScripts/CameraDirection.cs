using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CameraDirection : MonoBehaviour {
    public Vector3 target;
    public Vector3 targetCamPos;
    private CameraMovement cm;
    private RulesScript rules;
    private Vector3 dirFromMeToTarget;
    private Quaternion lookRot;
    private float orbitSpeed = 0.001f;

    private float degreesPerSecond = 300f;

    void Start()
    {
        //Offset Camera
        transform.localPosition = new Vector3(-1f, -1f, -11f);

        cm = FindObjectOfType<CameraMovement>();
        rules = FindObjectOfType<RulesScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //resultscreenmode
        //if(rules.resultScreen.activeSelf && transform.localPosition.y > -6.69f)
        if (rules.resultScreen.activeSelf)
        {
            //Zoom
            if (transform.position.y <= 2.001f)
            {
                target = cm.centerPoint;
                dirFromMeToTarget = target - transform.position;
                lookRot = Quaternion.LookRotation(dirFromMeToTarget);
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * (degreesPerSecond / 360f));
                transform.Translate(Vector3.left * Mathf.Clamp(orbitSpeed, 0.001f, 0.9f) * Time.deltaTime);
                orbitSpeed += 0.001f;
            } else //Orbiting around winning Player
            {
                target = cm.centerPoint;
                targetCamPos = target + new Vector3(-1.5f, 2f, -4f);
                //Vector3 targetPosition = Vector3.Lerp(transform.position, new Vector3(this.transform.position.x, 0, target.z), 4f * Time.deltaTime);
                transform.position = Vector3.Lerp(transform.position, targetCamPos, 0.75f * Time.deltaTime);
                //transform.Translate(Vector3.right * Time.deltaTime);

                dirFromMeToTarget = target - transform.position;
                lookRot = Quaternion.LookRotation(dirFromMeToTarget);
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * (degreesPerSecond / 360f));

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
        //Normal Game mode
        else
        {
            //target = cm.centerPoint;
            //Vector3 targetPosition = Vector3.Lerp(transform.position, new Vector3(this.transform.position.x, 0, target.z), 4f * Time.deltaTime);
            if (cm.MaxZDistancePlayers() < 15)
            {
                target = cm.centerPoint;
            }
            else
            {
                target = cm.centerPoint - new Vector3(0, 0, (cm.MaxZDistancePlayers() - 14f) * 0.4f);
            }
            dirFromMeToTarget = target - transform.position;
            //dirFromMeToTarget.x = 0f;
            //dirFromMeToTarget.y = 0f;
            lookRot = Quaternion.LookRotation(dirFromMeToTarget);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * (degreesPerSecond / 360f));

            // Limit lookAt Rotation
            if (Camera.main.transform.localEulerAngles.y < 10)
            {
                Camera.main.transform.localEulerAngles = new Vector3(Mathf.Clamp(Camera.main.transform.localEulerAngles.x, 30f, 80f), Mathf.Clamp(Camera.main.transform.localEulerAngles.y, 0f, 9f), 0);
            }
            else if (Camera.main.transform.localEulerAngles.y > 11)
            {
                Camera.main.transform.localEulerAngles = new Vector3(Mathf.Clamp(Camera.main.transform.localEulerAngles.x, 30f, 80f), Mathf.Clamp(Camera.main.transform.localEulerAngles.y, 350f, 361f), 0);
            }
            //Camera.main.transform.localEulerAngles = new Vector3(Mathf.Clamp(Camera.main.transform.localEulerAngles.x, 40f, 80f), Mathf.Clamp(Camera.main.transform.localEulerAngles.y, maxRotation.x, maxRotation.y), 0);
            //Camera.main.transform.rotation.q
        }
    }

    public void restartCameraDirection()
    {
        transform.localPosition = new Vector3(-1f, -1f, -11f);
        target = cm.centerPoint;
    }
}