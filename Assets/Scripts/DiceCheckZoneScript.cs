using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZoneScript : MonoBehaviour
{
    Vector3 diceVelocity;

    void Update()
    {
        diceVelocity = DiceScript.diceVelocity;
    }

    void OnTriggerStay(Collider col)
    {
        // Pastikan dadu berhenti sebelum memeriksa sisi
        if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f)
        {
            switch (col.gameObject.name)
            {
                case "SIDE1":
                    DiceNumberTextScript.diceNumber = 1;
                    break;
                case "SIDE2":
                    DiceNumberTextScript.diceNumber = 2;
                    break;
                case "SIDE3":
                    DiceNumberTextScript.diceNumber = 3;
                    break;
                case "SIDE4":
                    DiceNumberTextScript.diceNumber = 4;
                    break;
                default:
                    Debug.LogWarning("Unknown side detected: " + col.gameObject.name);
                    break;
            }
        }
    }



}
