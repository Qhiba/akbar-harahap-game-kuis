using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_PesanLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tempatPesan = null;

    public string Pesan
    {
        get => _tempatPesan.text;
        set => _tempatPesan.text = value;
    }

    private void Awake()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}
