using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    //Untuk memuat target Scene dengan nama.
    //Nama Scene ini case-sesitive
    public void BukaScene(string namaScene)
    {
        SceneManager.LoadScene(namaScene);
    }
}
