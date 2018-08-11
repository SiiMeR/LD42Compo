using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int totalMemory = 1024;
    public float secondsPerMemoryDrain = 1f;

    private PlayerMovement _playerMovement;

    public List<Upgrade> upgrades;
    
    [SerializeField] private TextMeshProUGUI _memoryValueField;
    [SerializeField] private GameObject _upgradeModal;
    

    public int FreeMemory
    {
        get { return _freeMemory; }
        set
        {
            if (value <= 0)
            {
                GameOver();
            }

            _freeMemory = value;

            UpdateMemoryStatus();
        }
    }

    private int _freeMemory;
    private float _memoryDrainTimer;

    private void UpdateMemoryStatus()
    {
        _memoryValueField.text = $"{FreeMemory} / {totalMemory} KB";
    }

    private void GameOver()
    {
        Debug.Log("Game over");
    }

    // Use this for initialization
    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        
        upgrades = new List<Upgrade>();
        FreeMemory = totalMemory;
        _memoryValueField.text = $"{FreeMemory} / {totalMemory} KB";
    }

    // Update is called once per frame
    void Update()
    {
        DrainMemory();
        CheckInteractions();
    }

    private void CheckInteractions()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _upgradeModal.SetActive(!_upgradeModal.activeInHierarchy);

            Time.timeScale = _upgradeModal.activeInHierarchy ? .0f : 1f;
        }
    }

    // drain based on movement
    private void DrainMemory()
    {

        if (_playerMovement._hasMovedThisFrame)
        {
            _memoryDrainTimer += Time.deltaTime;
        }

        if (!(_memoryDrainTimer > secondsPerMemoryDrain)) return;
        
        FreeMemory -= 1;
        _memoryDrainTimer = 0;
    }
}