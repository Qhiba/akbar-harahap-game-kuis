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
    [SerializeField] private string sceneMenuLevel = string.Empty;

    [SerializeField] private PemanggilSuara _pemanggilSuara = null;
    [SerializeField] private AudioClip _suaraMenang = null;
    [SerializeField] private AudioClip _suaraKalah = null;

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
        AudioManager.instance.PlayBGM(1);

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
        _pemanggilSuara.PanggilSuara(adalahBenar ? _suaraMenang : _suaraKalah);

        if (!adalahBenar) return;

        string namaLevelPack = _initGameplay.levelPack.name;
        int levelTerakhir = _playerProgress.progresData.progresLevel[namaLevelPack];

        if (_indexSoal + 2 > levelTerakhir)
        {
            //Tambahkan koin sebagai hadiah dari menyelesaikan soal kuis
            _playerProgress.progresData.koin += 20;

            //Membuka level selanjutnya agar dapat diakses di menu level
            _playerProgress.progresData.progresLevel[namaLevelPack] = _indexSoal + 2;

            _playerProgress.SimpanProgres();
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
