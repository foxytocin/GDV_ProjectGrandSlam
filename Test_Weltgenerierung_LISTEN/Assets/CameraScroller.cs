using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroller : MonoBehaviour
{
   
    public int rowPosition;
    public int altePosition;
    private bool create;
    public LevelGenerator LevelGenerator;

    // Use this for initialization
    void Start()
    {
        altePosition = -1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, 5f * Time.deltaTime);
        rowPosition = (int)Mathf.Round(transform.position.z);

        if (rowPosition > altePosition)
        {
            altePosition = rowPosition;
            LevelGenerator.createWorld(rowPosition);
            //Debug.Log("CAMERA ÜBERGIBT REIHE NR.: " + rowPosition);
        }
    }
}