using UnityEngine;

public class CameraScroller : MonoBehaviour
{
    public int rowPosition;
    public int altePosition;
    public LevelGenerator LevelGenerator;

    // Use this for initialization
    void Start()
    {
        altePosition = -1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, LevelGenerator.LevelSpeed * Time.deltaTime);
        rowPosition = Mathf.RoundToInt(transform.position.z);

        if (rowPosition > altePosition)
        {
            altePosition = rowPosition;
            LevelGenerator.createWorld(rowPosition + LevelGenerator.tiefeLevelStartBasis);
            //Debug.Log("CAMERA ÜBERGIBT REIHE NR.: " + rowPosition);
        }
    }
}