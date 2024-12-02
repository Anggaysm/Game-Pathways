using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataKartu", menuName = "Kartu Baru/Data Kartu")]
public class DataKartu : ScriptableObject
{
    public enum TipeKartu { Aman, Skip, Blackhole }

    [Header("Identitas Kartu")]
    public int ID;
    public string namaKartu;
    public string deskripsiKartu;
    public Sprite spriteKartu;
    public TipeKartu tipeKartu;
}