using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelMenuDataManager : MonoBehaviour
{
    [SerializeField] private PlayerProgress _playerProgress = null;
    [SerializeField] private UI_LevelPackList _levelPackList = null;
    [SerializeField] private LevelPackKuis[] _levelPacks = new LevelPackKuis[0];

    [SerializeField] private TextMeshProUGUI _textKoin = null;

    // Start is called before the first frame update
    void Start()
    {
        //Check apabila tidak berhasil memuat progres
        if (!_playerProgress.MuatProgres())
        {
            //Buat simpanan progres atau ganti dengan yang baru
            _playerProgress.SimpanProgres();
        }

        //Muat semua level pack yang ada di game
        _levelPackList.LoadLevelPack(_levelPacks, _playerProgress.progresData);

        _textKoin.text = $"{_playerProgress.progresData.koin}";
        AudioManager.instance.PlayBGM(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
