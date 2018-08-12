using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private const float FADETIME = 1.5f;
    
    public int totalMemory = 1024;
    public float secondsPerMemoryDrain = 1f;

    private PlayerMovement _playerMovement;

    public List<Upgrade> upgrades;
    
    public List<GameObject> deathScreens;
    public GameObject deathBG;
    public GameObject deathReturnToContd;
    public GameObject lastDeathScreen;


    [SerializeField] private TextMeshProUGUI _memoryValueField;
    [SerializeField] private GameObject _upgradeModal;
    

    public int FreeMemory
    {
        get { return _freeMemory; }
        set
        {
            if (value <= 0)
            {
                StartCoroutine(GameOver());
            }

            _freeMemory = value;

            UpdateMemoryStatus();
        }
    }

    private int _freeMemory;
    private float _memoryDrainTimer;
    private Animator _animatorController;
    
    private void UpdateMemoryStatus()
    {
        _memoryValueField.text = $"{FreeMemory} / {totalMemory} KB";
    }


    public IEnumerator GameOver()
    {
        _playerMovement.enabled = false;
        _animatorController.SetTrigger("Die");

        yield return new WaitForSeconds(_animatorController.GetCurrentAnimatorStateInfo(0).length+_animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime);
  
        
        lastDeathScreen.GetComponent<TextMeshProUGUI>().text = $"Game over!\n" +
                                                               $"You lasted for <color=red>{Time.timeSinceLevelLoad}</color> seconds.\n" +
                                                               $"Press ESC to quit or Return to restart";
        
        Time.timeScale = 0f;
        var timer = 0f;

        while ((timer += Time.unscaledDeltaTime) < FADETIME)
        {
            var c = deathBG.GetComponent<Image>().color;

            c.a = Mathf.Lerp(0f, 1f, timer / FADETIME);

            deathBG.GetComponent<Image>().color = c;
            
            yield return null;
        }

        for (var index = 0; index < deathScreens.Count; index++)
        {
            
            var deathScreen = deathScreens[index];
            yield return StartCoroutine(FadeIn(deathScreen.GetComponent<TextMeshProUGUI>()));

            StartCoroutine(FadeIn(deathReturnToContd.GetComponent<TextMeshProUGUI>()));
            
           // yield return new WaitForSeconds(0.5f);

            
            yield return new WaitUntil(() =>
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    return true;
                }
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    Time.timeScale = 1.0f;
                    Application.Quit();
                }

                return false;

            });
            

            StartCoroutine(FadeOut(deathReturnToContd.GetComponent<TextMeshProUGUI>()));

            
            yield return StartCoroutine(FadeOut(deathScreen.GetComponent<TextMeshProUGUI>()));
        }

        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
    public IEnumerator FadeIn(TextMeshProUGUI text)
    {
        var timer = 0f;

        while ((timer += Time.unscaledDeltaTime) < FADETIME)
        {
            var c = text.color;

            c.a = Mathf.Lerp(0f,1f, timer / FADETIME);

            text.color = c;
            
            yield return null;
        }
    }

    public IEnumerator FadeOut(TextMeshProUGUI text)
    {
        var timer = 0f;

        while ((timer += Time.unscaledDeltaTime) < FADETIME)
        {
            var c = text.color;

            c.a = Mathf.Lerp(1f,0f, timer / FADETIME);

            text.color = c;
            
            yield return null;
        }
    }


    // Use this for initialization
    void Start()
    {
        _animatorController = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        
        _upgradeModal.SetActive(false);
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