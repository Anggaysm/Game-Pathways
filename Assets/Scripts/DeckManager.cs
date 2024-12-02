
using System.Collections.Generic;
using UnityEngine;


public class DeckManager : MonoBehaviour
{
    [Header("Daftar Kartu")]
    public List<DataKartu> deckKartu; // List untuk menyimpan semua kartu.

    private Stack<DataKartu> actualDeck; // Tumpukan (Stack) untuk kartu yang sudah diacak.

    public int BanyakKartu=100;

    void Start()
    {

        actualDeck = new Stack<DataKartu>(); // Inisialisasi Stack.
        ShuffleDeck(); // Panggil fungsi untuk mengacak deck.
        // DisplayTopCard(); // Contoh pengambilan kartu dari deck.
    }

    // Fungsi untuk mengacak deck kartu
    public void ShuffleDeck()
    {
        List<DataKartu> tempDeck = GenerateCard(); // Buat salinan deck untuk diacak.
        while (tempDeck.Count > 0)
        {
            int randomIndex = Random.Range(0, tempDeck.Count); // Pilih indeks acak.
            actualDeck.Push(tempDeck[randomIndex]); // Masukkan kartu ke dalam tumpukan.
            tempDeck.RemoveAt(randomIndex); // Hapus kartu dari daftar sementara.
        }

        Debug.Log("Deck telah diacak!");
    }

    // Fungsi untuk menarik kartu dari deck
    public DataKartu DrawCard()
    {
        if (actualDeck.Count > 0)
        {
            DataKartu drawnCard = actualDeck.Pop(); // Ambil kartu teratas.
            Debug.Log("Kartu yang diambil: " + drawnCard.namaKartu);
            DisplayTopCard(drawnCard);
            return drawnCard;
        }
        else
        {
            Debug.Log("Deck kosong!");
            return null;
        }
    }

    // Contoh fungsi untuk menampilkan kartu teratas
    private void DisplayTopCard(DataKartu topCard)
    {

        if (topCard != null)
        {
            // Lakukan sesuatu dengan kartu, seperti menampilkan sprite atau efek.
            Debug.Log("Menampilkan kartu: " + topCard.namaKartu);
        }
    }
    
        private List<DataKartu> GenerateCard(){
            var templist = new List <DataKartu>();
            for (int i = 0; i < BanyakKartu; i++)
            {
                templist.Add(deckKartu[Random.Range(0,3)]);
            }
            return templist;
        }
    
}

