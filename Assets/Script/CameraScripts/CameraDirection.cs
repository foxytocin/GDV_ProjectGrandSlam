using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CameraDirection : MonoBehaviour {
    public Vector3 target;
    private CameraMovement cm;
    private RulesScript rules;

    private float degreesPerSecond = 300f;
    private int z = 10;

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
        if(rules.resultScreen.activeSelf)
        {
            Debug.Log("Löft");
            target = cm.centerPoint;
            //Vector3 targetPosition = Vector3.Lerp(transform.position, new Vector3(this.transform.position.x, 0, target.z), 4f * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, new Vector3(this.transform.position.x, 2f, target.z-2f), 1f * Time.deltaTime);
        }
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
            Vector3 dirFromMeToTarget = target - transform.position;
            //dirFromMeToTarget.x = 0f;
            //dirFromMeToTarget.y = 0f;
            Quaternion lookRot = Quaternion.LookRotation(dirFromMeToTarget);
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
}