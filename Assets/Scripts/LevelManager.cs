using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    private struct DataSoal
    {
        public string judul;
        public string pertanyaan;
        public Sprite hint;

        public string[] jawabanTeks;
        public bool[] adalahBenar;
    }

    [SerializeField] private DataSoal[] _soalSoal = new DataSoal[0];
    [SerializeField] private UI_Pertanyaan _tempatPertanyaan = null;
    [SerializeField] private UI_PoinJawaban[] _tempatPilihanJawaban = new UI_PoinJawaban[0];

    private int _indexSoal = -1;

    private void Start()
    {
        NextLevel();
    }

    public void NextLevel()
    {
        //Sola index selanjutnya
        _indexSoal++;

        //Jika index melampaui soal terakhir, ulang dari awal
        if (_indexSoal >= _soalSoal.Length)
        {
            _indexSoal = 0;
        }

        //Ambil data Pertanyaan
        DataSoal soal = _soalSoal[_indexSoal];

        //Set informasi soal
        _tempatPertanyaan.SetPertanyaan(soal.judul, soal.pertanyaan, soal.hint);

        for (int i = 0; i < _tempatPilihanJawaban.Length; i++)
        {
            UI_PoinJawaban poin = _tempatPilihanJawaban[i];
            poin.SetJawaban(soal.jawabanTeks[i], soal.adalahBenar[i]);
        }
    }
}
