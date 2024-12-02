using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour
{
    static Rigidbody rb;
    public static Vector3 diceVelocity;
    public GameManager gameManager; // Tambahkan referensi ke GameManager

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        diceVelocity = rb.velocity;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DiceNumberTextScript.diceNumber = 0;

            // Roll dice dengan fisika
            float dirX = Random.Range(0, 700);
            float dirY = Random.Range(0, 784);
            float dirZ = Random.Range(0, 200);
            transform.position = new Vector3(1, 1, -3);
            transform.rotation = Quaternion.identity;
            rb.AddForce(transform.up * 643);
            rb.AddTorque(dirX, dirY, dirZ);

            // Panggil fungsi RollDiceAndMove setelah dadu berhenti
            StartCoroutine(WaitForDiceToStop());
        }
    }




    private IEnumerator WaitForDiceToStop()
    {
        // Tunggu hingga dadu berhenti bergerak
        yield return new WaitForSeconds(2.8f);  // Tunggu selama 2 detik untuk memastikan dadu berhenti

        // Gunakan nilai dadu yang sudah ditentukan oleh DiceCheckZoneScript
        int diceValue = DiceNumberTextScript.diceNumber;

        if (diceValue > 0) // Pastikan dadu memiliki nilai yang valid
        {
            // Update current point pemain hanya jika nilai dadu valid
            gameManager.RollDiceAndMove(diceValue);  // Gerakkan pemain dengan nilai dadu
        }
        else
        {
            // Jika dadu tidak valid (misalnya nilai 0), roll dice lagi
            Debug.Log("Invalid dice value.");
        }
    }


}

