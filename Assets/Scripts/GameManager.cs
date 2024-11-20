using TMPro;
using UnityEngine;
using UnityEngine.UI;  // Jangan lupa untuk menambahkan namespace ini agar bisa mengakses UI

public class GameManager : MonoBehaviour
{
    public DiceController diceController;
    public PlayerController[] players;  // Array untuk semua pemain
    private int currentPlayerIndex = 0;  // Untuk mengganti giliran pemain
    public TextMeshProUGUI  victoryText;  // Reference ke UI Text yang akan menampilkan pesan kemenangan

    void Start()
    {
        victoryText.gameObject.SetActive(false);  // Menyembunyikan pesan kemenangan saat permainan dimulai
    }

    // Fungsi untuk menggulung dadu dan menggerakkan pemain
    public void RollDiceAndMove()
    {
        diceController.RollDice();  // Roll dadu

        // Gerakkan pemain aktif
        players[currentPlayerIndex].MovePlayer(diceController.diceValue);

        // Cek apakah pemain sudah menang
        if (players[currentPlayerIndex].currentPoint == players[currentPlayerIndex].boardPoints.Length - 1)
        {
            ShowVictoryMessage(players[currentPlayerIndex]);
        }

        // Ganti giliran pemain
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
    }

    // Fungsi untuk menampilkan pesan kemenangan
    void ShowVictoryMessage(PlayerController winner)
    {
        victoryText.gameObject.SetActive(true);  // Menampilkan teks kemenangan
        victoryText.text = "Pemain " + (currentPlayerIndex + 1) + " Menang!";  // Menampilkan teks dengan pemain yang menang
    }
}
