using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthData
{
    public int currentHealth;
    public int maxHealth;

    public HealthData()
    {
        currentHealth = 0;
        maxHealth = 0;
    }

    public HealthData(int currentHealth, int maxHealth)
    {
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;
    }
}

