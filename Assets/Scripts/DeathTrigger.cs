﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour
{
    internal void OnDeath()
    {
        SceneManager.LoadScene(1);
    }
}
