using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class UI_PesanLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tempatPesan = null;
    [SerializeField] private GameObject _menuOpsiMenang = null;
    [SerializeField] private GameObject _menuOpsiKalah = null;

    public string Pesan
    {
        get => _tempatPesan.text;
        set => _tempatPesan.text = value;
    }

    private void Awake()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);

        //Subscribe event
        UI_Timer.EventWaktuHabis += UI_Timer_EventWaktuHabis;
        UI_PoinJawaban.EventJawabSoal += UI_PoinJawaban_EventJawabSoal;
    }

    private void OnDestroy()
    {
        //Unubscribe event
        UI_Timer.EventWaktuHabis -= UI_Timer_EventWaktuHabis;
        UI_PoinJawaban.EventJawabSoal -= UI_PoinJawaban_EventJawabSoal;
    }

    private void UI_Timer_EventWaktuHabis()
    {
        Pesan = "Waktu Habis";
        gameObject.SetActive(true);

        _menuOpsiMenang.SetActive(false);
        _menuOpsiKalah.SetActive(true);
    }

    private void UI_PoinJawaban_EventJawabSoal(string jawaban, bool adalahBenar)
    {
        Pesan = $"Jawaban Anda {adalahBenar} (Jawab: {jawaban})";
        gameObject.SetActive(true);

        if (adalahBenar)
        {
            _menuOpsiMenang.SetActive(true);
            _menuOpsiKalah.SetActive(false);
        }
        else
        {
            _menuOpsiMenang.SetActive(false);
            _menuOpsiKalah.SetActive(true);
        }
    }
}
