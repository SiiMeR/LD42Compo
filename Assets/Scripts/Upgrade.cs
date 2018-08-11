using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Upgrade : MonoBehaviour
{

    public bool isEquipped;
    public int memoryCost = 128;
    
    public string title;
    
    [TextArea(5,10)]
    public string description;
    
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    // TODO upgradefactory??

    public OnAcquireEvent OnAcquire;
    public OnAcquireEvent UnAcquire;
    
    [SerializeField] private GameObject textPanel;

    

    // Use this for initialization
    void Start()
    {
        titleText.SetText(title);
        descriptionText.SetText(description);
        
        
        if (OnAcquire == null)
        {
            OnAcquire = new OnAcquireEvent();
        }
        OnAcquire.AddListener(AddEffectToPlayer);

        if (UnAcquire == null)
        {
            UnAcquire = new OnAcquireEvent();
        }
        UnAcquire.AddListener(RemoveEffectFromPlayer);
    }

    private void RemoveEffectFromPlayer(Player player)
    {
        player.gameObject.GetComponent<PlayerMovement>().MaxJumpHeight -= 1.0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnAcquire.Invoke(other.gameObject.GetComponent<Player>());
        }
    }

    private void AddEffectToPlayer(Player player)
    {
        player.gameObject.GetComponent<PlayerMovement>().MaxJumpHeight += 1.0f;
        textPanel.SetActive(true);
        Time.timeScale = 0.0f;
        StartCoroutine(WaitForKeyPress(KeyCode.Return));
    }

    private IEnumerator WaitForKeyPress(KeyCode key)
    {
        yield return new WaitUntil(() => Input.GetKeyDown(key));

        Time.timeScale = 1.0f;
        Destroy(gameObject);
    }

    
    

}