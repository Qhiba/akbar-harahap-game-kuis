using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(
    fileName = "Inisial Data Gameplay",
    menuName = "Game Kuis/Inisial Data")]

public class InisialDataGameplay : ScriptableObject
{
    public LevelPackKuis levelPack = null;
    public int levelIndex = 0;

    [SerializeField] private bool _saatKalah = false;

    public bool SaatKalah
    {
        get => _saatKalah;
        set => _saatKalah = value;
    }
}
