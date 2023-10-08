using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_MenuConfirmMessage : MonoBehaviour
{
    [SerializeField] private PlayerProgress _playerProgress = null;
    [SerializeField] private GameObject _pesanCukupKoin = null;
    [SerializeField] private GameObject _pesanTakCukupKoin = null;
    [SerializeField] private TextMeshProUGUI _tempatKoin = null;

    private UI_OpsiLevelPack _tombolLevelPack = null;
    private LevelPackKuis _levelPack = null;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.activeSelf) gameObject.SetActive(false);

        //Subscribe event
        UI_OpsiLevelPack.EventSaatKlik += UI_OpsiLevelPack_EventSaatKlik;
    }

    private void OnDestroy()
    {
        //Unsubsribe event
        UI_OpsiLevelPack.EventSaatKlik -= UI_OpsiLevelPack_EventSaatKlik;
    }

    private void UI_OpsiLevelPack_EventSaatKlik(UI_OpsiLevelPack tombolLevelPack, LevelPackKuis levelPack, bool terkunci)
    {
        //Cek apakah terkunci atau tidak, jika tidak maka abaikan.
        if (!terkunci) return;

        gameObject.SetActive(true);

        //Cek kecukupan koin untuk membeli Level Pack
        if (_playerProgress.progresData.koin < levelPack.Harga)
        {
            //Jika tidak cukup
            _pesanCukupKoin.SetActive(false);
            _pesanTakCukupKoin.SetActive(true);
            return;
        }

        //Jika Cukup
        _pesanCukupKoin.SetActive(true);
        _pesanTakCukupKoin.SetActive(false);

        _tombolLevelPack = tombolLevelPack;
        _levelPack = levelPack;
    }

    public void BukaLevel()
    {
        _playerProgress.progresData.koin -= _levelPack.Harga;
        _playerProgress.progresData.progresLevel[_levelPack.name] = 1;

        _tempatKoin.text = $"{_playerProgress.progresData.koin}";

        _playerProgress.SimpanProgres();
        _tombolLevelPack.BukaLevelPack();
    }
}
