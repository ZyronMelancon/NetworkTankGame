using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health",menuName = "Health")]
public class HealthScriptable : ScriptableObject
{
    public int m_Health;


    public void TakeDamage(int amount)
    {
        m_Health -= amount;
    }
}