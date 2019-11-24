﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryTileTrigger : MonoBehaviour
{
    [SerializeField] private string nextLevel;

    private SaveManagerController saver;

    // Start is called before the first frame update
    void Start()
    {
        saver = GetComponent<SaveManagerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            saver.Save();
            SceneManager.LoadScene(nextLevel);
        }
    }
}