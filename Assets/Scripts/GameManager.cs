using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMesh textMesh;
    public int startTime = 8;
    private string time;
    public GameObject textMeshOB;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = textMeshOB.GetComponent<TextMesh>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTime(int hour)
    {
        string newTime = $"0{hour}:00";
        textMesh.text = newTime;
    }
}
