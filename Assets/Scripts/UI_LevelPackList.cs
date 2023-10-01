using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LevelPackList : MonoBehaviour
{
    [SerializeField] private InisialDataGameplay _initGameplay = null;
    [SerializeField] private UI_OpsiLevelPack _tombolLevelPack = null;
    [SerializeField] UI_LevelKuisList _levelList = null;
    [SerializeField] private RectTransform _content = null;
    [Space, SerializeField] private LevelPackKuis[] _levelPacks = new LevelPackKuis[0];

    // Start is called before the first frame update
    void Start()
    {
        LoadLevelPack();

        if (_initGameplay.SaatKalah)
        {
            UI_OpsiLevelPack_EventSaatKlik(_initGameplay.levelPack);
        }

        //Subscribe events
        UI_OpsiLevelPack.EventSaatKlik += UI_OpsiLevelPack_EventSaatKlik;
    }

    //Method untuk memuat semua level pack sebelum ditampilkan
    private void LoadLevelPack()
    {
        foreach (var levelPack in _levelPacks)
        {
            //Membuat salinan objek dari prefab tombol level pack
            var tombol = Instantiate(_tombolLevelPack);
            
            tombol.SetLevelPack(levelPack);

            //Masukkan objek tombol sebagai anak dari objek "content"
            tombol.transform.SetParent(_content);
            tombol.transform.localScale = Vector3.one;
        }
    }

    private void OnDestroy()
    {
        //Unsubscribe events
        UI_OpsiLevelPack.EventSaatKlik -= UI_OpsiLevelPack_EventSaatKlik;
    }

    private void UI_OpsiLevelPack_EventSaatKlik(LevelPackKuis levelPack)
    {
        //Buka Menu Levels
        _levelList.gameObject.SetActive(true);
        _levelList.UnloadLevelPack(levelPack);

        //Tutup Menu Level Packs
        gameObject.SetActive(false);

        _initGameplay.levelPack = levelPack;
    }
}
