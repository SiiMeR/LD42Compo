using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float totalMemory = 1024f;
    public float secondsPerMemoryDrain = 1f;

    private PlayerMovement _playerMovement;

    [SerializeField] private TextMeshProUGUI _memoryValueField;

    public float FreeMemory
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

    private float _freeMemory;
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

        FreeMemory = totalMemory;
        _memoryValueField.text = $"{FreeMemory} / {totalMemory} KB";
    }

    // Update is called once per frame
    void Update()
    {
        DrainMemory();
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