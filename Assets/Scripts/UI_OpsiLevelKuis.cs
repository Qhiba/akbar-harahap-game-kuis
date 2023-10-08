using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_OpsiLevellKuis : MonoBehaviour
{
    public static event System.Action<int> EventSaatKlik;

    [SerializeField] private Button _tombol = null;
    [SerializeField] private TextMeshProUGUI _levelName = null;
    [SerializeField] private LevelSoalKuis _levelKuis = null;

    public bool InteraksiTombol
    {
        get => _tombol.interactable;
        set => _tombol.interactable = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_levelKuis != null)
            SetLevelKuis(_levelKuis, _levelKuis.levelPackIndex);

        //Subscribe event
        _tombol.onClick.AddListener(SaatKlik);
    }

    public void SetLevelKuis(LevelSoalKuis levelKuis, int index)
    {
        _levelName.text = levelKuis.name;
        _levelKuis = levelKuis;

        _levelKuis.levelPackIndex = index;
    }

    private void OnDestroy()
    {
        //Unsubscibe event
        _tombol.onClick.RemoveListener(SaatKlik);
    }

    private void SaatKlik()
    {
        EventSaatKlik?.Invoke(_levelKuis.levelPackIndex);
    }
}
