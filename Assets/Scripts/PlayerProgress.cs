using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(
    fileName = "Player Progress Baru",
    menuName = "Game Kuis/Player Progress")]

public class PlayerProgress : ScriptableObject
{
    [System.Serializable]
    public struct MainData
    {
        public int koin;
        public Dictionary<string, int> progresLevel;
    }

    [SerializeField] private string _fileName = "contoh.txt";
    
    public MainData progresData = new MainData();

    #region ENKRIPSI SAVE DAN LOAD MENGGUNAKAN BINARY FORMATTER
    public void SimpanProgres()
    {
        //Sample Data
        progresData.koin = 200;
        if (progresData.progresLevel == null) progresData.progresLevel = new();
        progresData.progresLevel.Add("Level Pack 1", 3);
        progresData.progresLevel.Add("Level Pack 3", 5);

        //Informasi penyimpanan data
        string directory = Application.dataPath + "/Temporary/";
        string path = directory + _fileName;

        //Membuat Directory Temporary
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Debug.Log("Directory has been Created: " + directory);
        }

        //Membuat file baru
        if (!File.Exists(path))
        {
            File.Create(path).Dispose();
            Debug.Log("File created: " + path);
        }

        //Menyimpan data ke dalam file menggunakan binary
        var fileStream = File.Open(path, FileMode.Open, FileAccess.Write);

        //Menggunakan BinaryFormatter
        var formatter = new BinaryFormatter();

        fileStream.Flush();
        formatter.Serialize(fileStream, progresData);
        
        //Putuskan aliran memori dengan File
        fileStream.Dispose();

        Debug.Log("Data saved to file: " + path);
    }

    public bool MuatProgres()
    {
        //Informasi penyimpanan data
        string directory = Application.dataPath + "/Temporary/";
        string path = directory + _fileName;

        //Membuat Directory Temporary
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Debug.Log("Directory has been Created: " + directory);
        }

        //Membuat file baru
        if (!File.Exists(path))
        {
            File.Create(path).Dispose();
            Debug.Log("File created: " + path);
        }

        //Buka file dengan Filestream
        var fileStream = File.Open(path, FileMode.OpenOrCreate);

        try
        {
            //Membuat data dari file menggunakan binari formatter
            var formatter = new BinaryFormatter();

            progresData = (MainData)formatter.Deserialize(fileStream);

            //Putuskan aliran memori dengan File
            fileStream.Dispose();

            Debug.Log($"{progresData.koin}; {progresData.progresLevel.Count}");

            return true;
        }
        catch (System.Exception e)
        {
            Debug.Log($"ERROR: Terjadi kesalahan saat memuat progres\n{e.Message}");

            //Putuskan aliran memori dengan File
            fileStream.Dispose();

            return false;
        }        
    }
    #endregion


    #region ENKRIPSI SAVE DAN LOAD MENGGUNAKAN BINARY WRITER
    /*
    public void SimpanProgres()
    {
        //Sample Data
        progresData.koin = 200;
        if (progresData.progresLevel == null) progresData.progresLevel = new();
        progresData.progresLevel.Add("Level Pack 1", 3);
        progresData.progresLevel.Add("Level Pack 3", 5);

        //Informasi penyimpanan data
        string directory = Application.dataPath + "/Temporary/";
        string path = directory + _fileName;

        //Membuat Directory Temporary
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Debug.Log("Directory has been Created: " + directory);
        }

        //Membuat file baru
        if (!File.Exists(path))
        {
            File.Create(path).Dispose();
            Debug.Log("File created: " + path);
        }

        //Menyimpan data ke dalam file menggunakan binary
        var fileStream = File.Open(path, FileMode.Open);
        fileStream.Flush();

        //Menggunakan BinaryWriter
        var writer = new BinaryWriter(fileStream);

        writer.Write(progresData.koin);
        foreach (var i in progresData.progresLevel)
        {
            writer.Write(i.Key);
            writer.Write(i.Value);
        }

        //Putuskan aliran memori dengan File
        writer.Dispose();
        fileStream.Dispose();

        Debug.Log("Data saved to file: " + path);
    }

    public bool MuatProgres()
    {
        //Informasi penyimpanan data
        string directory = Application.dataPath + "/Temporary/";
        string path = directory + _fileName;

        //Membuat Directory Temporary
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Debug.Log("Directory has been Created: " + directory);
        }

        //Membuat file baru
        if (!File.Exists(path))
        {
            File.Create(path).Dispose();
            Debug.Log("File created: " + path);
        }

        //Buka file dengan Filestream
        var fileStream = File.Open(path, FileMode.OpenOrCreate);

        try
        {
            var reader = new BinaryReader(fileStream);
            try
            {
                progresData.koin = reader.ReadInt32();
                if (progresData.progresLevel == null) progresData.progresLevel = new();

                while (reader.PeekChar() != -1)
                {
                    var namaLevelPack = reader.ReadString();
                    var levelKe = reader.ReadInt32();
                    progresData.progresLevel.Add(namaLevelPack, levelKe);
                    Debug.Log($"{namaLevelPack}:{levelKe}");
                }

                //Putuskan aliran memori dengan File
                reader.Dispose();
            }
            catch (System.Exception e)
            {
                Debug.Log($"ERROR: Terjadi kesalahan saat memuat progres\n{e.Message}");

                //Putuskan aliran memori dengan File
                reader.Dispose();
                fileStream.Dispose();

                return false;
            }

            //Putuskan aliran memori dengan File
            fileStream.Dispose();

            Debug.Log($"{progresData.koin}; {progresData.progresLevel.Count}");

            return true;
        }
        catch (System.Exception e)
        {
            Debug.Log($"ERROR: Terjadi kesalahan saat memuat progres\n{e.Message}");

            //Putuskan aliran memori dengan File
            fileStream.Dispose();

            return false;
        }
    }
    */
    #endregion
}
