using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController[] players;  // Array untuk semua pemain
    private int currentPlayerIndex = 0;  // Untuk mengganti giliran pemain
    public TextMeshProUGUI victoryText;  // Reference ke UI Text yang akan menampilkan pesan kemenangan
    public DiceScript diceScript; // Tambahkan referensi ke DiceScript untuk mengambil nilai dadu

    public DeckManager deckManager;
    void Start()
    {
        victoryText.gameObject.SetActive(false);  // Menyembunyikan pesan kemenangan saat permainan dimulai
    }

    

    // Fungsi untuk menggerakkan pemain setelah dadu digulung
    // Fungsi untuk menggerakkan pemain setelah dadu digulung
    public void RollDiceAndMove(int diceRoll)
    {
        // Pastikan diceRoll sudah valid
        if (diceRoll > 0)
        {
            // Pindahkan pemain sesuai dengan nilai dadu
            players[currentPlayerIndex].MovePlayer(diceRoll);

            // Cek apakah pemain sudah menang
            if (players[currentPlayerIndex].currentPoint == players[currentPlayerIndex].boardPoints.Length - 1)
            {
                ShowVictoryMessage(players[currentPlayerIndex]);
                return; // Menghentikan giliran jika ada yang menang
            }
        }

        deckManager.DrawCard();
        

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
