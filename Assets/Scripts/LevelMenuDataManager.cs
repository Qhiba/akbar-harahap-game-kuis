using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelMenuDataManager : MonoBehaviour
{
    [SerializeField] private PlayerProgress _playerProgress = null;
    [SerializeField] private TextMeshProUGUI _tempatKoin = null;

    // Start is called before the first frame update
    void Start()
    {
        _tempatKoin.text = $"{_playerProgress.progresData.koin}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
