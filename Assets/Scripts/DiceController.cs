using UnityEngine;

public class DiceController : MonoBehaviour
{
    public int diceValue; // Nilai dadu terakhir
    public void RollDice()
    {
        // Randomize angka dari 1 sampai 4
        diceValue = Random.Range(1, 5);
        Debug.Log("Dice rolled: " + diceValue);
    }
}
