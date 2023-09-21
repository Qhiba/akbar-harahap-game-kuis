using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Pertanyaan : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tempatJudulLevel = null;
    [SerializeField] private TextMeshProUGUI _tempatTeks = null;
    [SerializeField] private Image _tempatGambar = null;

    public void SetPertanyaan(string tempatJudulLevel, string teksPertanyaan, Sprite gambarHint)
    {
        _tempatJudulLevel.text = tempatJudulLevel;
        _tempatTeks.text = teksPertanyaan;
        _tempatGambar.sprite = gambarHint;
    }
}
