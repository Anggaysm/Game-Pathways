using System.Collections;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    public Transform[] boardPoints;  // Array untuk titik papan
    public int currentPoint = 0;     // Titik saat ini (dimulai dari Point_1 yang diwakili oleh index 0)
    public float flyHeight = 3f;     // Tinggi roket terbang sebelum mendarat
    public float flyDuration = 0.6f; // Durasi pergerakan naik, horizontal, dan turun
    private Vector3 originalScale;   // Simpan skala asli
    private float shrinkFactor = 0.5f; // Faktor pengecilan ukuran roket
    public float stayInAirTime = 0.1f;  // Waktu tambahan di udara (0.3 detik)

    void Start()
    {
        // Mencari semua objek dengan tag "BoardPoint"
        GameObject[] boardObjects = GameObject.FindGameObjectsWithTag("BoardPoint");

        // Menyortir objek berdasarkan urutan nama seperti "Point_1", "Point_2", dst.
        System.Array.Sort(boardObjects, (a, b) => 
        {
            int aIndex = GetPointIndex(a.name);
            int bIndex = GetPointIndex(b.name);
            return aIndex.CompareTo(bIndex);
        });

        boardPoints = new Transform[boardObjects.Length];
        for (int i = 0; i < boardObjects.Length; i++)
        {
            boardPoints[i] = boardObjects[i].transform;
        }

        // Debug untuk memastikan array terisi dengan benar
        Debug.Log("Jumlah Titik Papan: " + boardPoints.Length);
        transform.position = boardPoints[currentPoint].position;  // Pastikan roket dimulai di titik pertama
        originalScale = transform.localScale; // Menyimpan ukuran asli roket
    }

    // Fungsi untuk mendapatkan angka dari nama objek "Point_1" menjadi 1
    int GetPointIndex(string pointName)
    {
        string[] splitName = pointName.Split('_');
        if (splitName.Length == 2 && int.TryParse(splitName[1], out int index))
        {
            return index;
        }
        return 0;
    }

    // Fungsi untuk memindahkan player ke titik yang sesuai
    public void MovePlayer(int diceRoll)
    {
        int targetPoint = currentPoint + diceRoll;  // Menambahkan angka dadu ke currentPoint

        if (targetPoint < boardPoints.Length)
        {
            currentPoint = targetPoint;
            StartCoroutine(MoveToPoint(boardPoints[currentPoint].position));

            AdjustPositionIfOverlap(); // Mengecek jika ada pemain di titik yang sama
        }
    }

    // Fungsi untuk menyesuaikan posisi jika ada pemain lain di titik yang sama
    void AdjustPositionIfOverlap()
    {
        PlayerController[] allPlayers = FindObjectsOfType<PlayerController>();

        var overlappingPlayers = allPlayers.Where(player => player.currentPoint == this.currentPoint).ToArray();

        if (overlappingPlayers.Length > 1)
        {
            for (int i = 0; i < overlappingPlayers.Length; i++)
            {
                float offsetZ = i * 0.2f; // Mengatur jarak antar pemain pada sumbu Z
                overlappingPlayers[i].transform.position = boardPoints[currentPoint].position + new Vector3(0, 0, offsetZ);
                overlappingPlayers[i].transform.localScale = originalScale * shrinkFactor;
            }
        }
        else
        {
            transform.localScale = originalScale;
        }
    }

    // Coroutine untuk pergerakan roket yang smooth: naik, bergerak, dan mendarat
    IEnumerator MoveToPoint(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        Vector3 midPoint = new Vector3(targetPosition.x, startPosition.y + flyHeight, targetPosition.z); // Titik terbang

        float elapsedTime = 0f;

        // Fase 1: Naik ke atas dengan ease-in
        while (elapsedTime < flyDuration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / flyDuration);  // Ease-in
            transform.position = Vector3.Lerp(startPosition, midPoint, t);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position), 360 * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Fase 2: Gerak ke posisi target secara horizontal dengan ease-out
        elapsedTime = 0f; // Reset waktu untuk fase kedua
        while (elapsedTime < flyDuration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / flyDuration);  // Ease-out
            transform.position = Vector3.Lerp(midPoint, targetPosition, t);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position), 360 * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Fase 3: Mendarat dengan posisi yang benar
        transform.position = targetPosition; // Pastikan posisi mendarat di tempat yang tepat
        transform.rotation = Quaternion.LookRotation(targetPosition - transform.position); // Pastikan rotasi selesai menghadap ke tujuan akhir
    }
}
