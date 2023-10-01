using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_OpsiLevelPack : MonoBehaviour
{
    [SerializeField] private Button _tombol = null;
    [SerializeField] private TextMeshProUGUI _packName = null;
    [SerializeField] private LevelPackKuis _levelPack = null;

    public static event System.Action<LevelPackKuis> EventSaatKlik;

    // Start is called before the first frame update
    void Start()
    {
        if (_levelPack != null)
            SetLevelPack(_levelPack);

        _tombol.onClick.AddListener(SaatKlik);
    }

    public void SetLevelPack(LevelPackKuis levelPack)
    {
        _packName.text = levelPack.name;
        _levelPack = levelPack;
    }

    private void OnDestroy()
    {
        _tombol.onClick.RemoveListener(SaatKlik);
    }

    private void SaatKlik()
    {
        EventSaatKlik?.Invoke(_levelPack);
    }
}
