using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private InisialDataGameplay _initGameplay = null;

    [SerializeField] private LevelPackKuis _soalSoal = null;
    [SerializeField] private PlayerProgress _playerProgress = null;
    [SerializeField] private UI_Pertanyaan _tempatPertanyaan = null;
    [SerializeField] private UI_PoinJawaban[] _tempatPilihanJawaban = new UI_PoinJawaban[0];

    [SerializeField] private GameSceneManager gameSceneManager = null;
    [SerializeField] private string sceneMenuLevel = null;

    private int _indexSoal = -1;

    private void Start()
    {
        //Cek apabila tidak berhasil memuat progres
        //if (!_playerProgress.MuatProgres())
        //{
        //    //Buat simpanan progres atau ganti dengan yang baru
        //    _playerProgress.SimpanProgres();
        //}

        _soalSoal = _initGameplay.levelPack;
        _indexSoal = _initGameplay.levelIndex - 1;

        NextLevel();

        //Subscribe events
        UI_PoinJawaban.EventJawabSoal += UI_PoinJawaban_EventJawabSoal;
    }

    private void OnDestroy()
    {
        //Unsubscribe events
        UI_PoinJawaban.EventJawabSoal -= UI_PoinJawaban_EventJawabSoal;
    }

    private void OnApplicationQuit()
    {
        _initGameplay.SaatKalah = false;
    }

    private void UI_PoinJawaban_EventJawabSoal(string jawaban, bool adalahBenar)
    {
        if (adalahBenar)
        {
            _playerProgress.progresData.koin += 20;
        }
    }

    public void NextLevel()
    {
        //Sola index selanjutnya
        _indexSoal++;

        //Jika index melampaui soal terakhir, ulang dari awal
        if (_indexSoal >= _soalSoal.BanyakLevel)
        {
            //_indexSoal = 0;
            gameSceneManager.BukaScene(sceneMenuLevel);
            return;
        }

        //Ambil data Pertanyaan
        LevelSoalKuis soal = _soalSoal.AmbilLevelKe(_indexSoal);

        //Set informasi soal
        _tempatPertanyaan.SetPertanyaan($"Level {_indexSoal + 1}", soal.pertanyaan, soal.hint);

        for (int i = 0; i < _tempatPilihanJawaban.Length; i++)
        {
            UI_PoinJawaban poin = _tempatPilihanJawaban[i];
            LevelSoalKuis.OpsiJawaban opsi = soal.opsiJawaban[i];
            poin.SetJawaban(opsi.jawabanTeks, opsi.adalahBenar);
        }
    }
}
