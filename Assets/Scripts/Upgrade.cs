using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Upgrade : MonoBehaviour
{

    public bool isEquipped;
    public int memoryCost = 128;
    
    	
    public string upgradeName;
    
    [TextArea(5,10)]
    public string description;
    
    public TextMeshProUGUI descriptionText;

    public OnAcquireEvent OnAcquire;
    public OnAcquireEvent UnAcquire;
    
    [SerializeField] private GameObject textPanel;

    // Use this for initialization
    void Start()
    {
      //  titleText.SetText(title);
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

    public virtual void RemoveEffectFromPlayer(Player player)
    {
    }

    public virtual void AddEffectToPlayer(Player player)
    {
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().upgrades.Add(this);
            
            textPanel.SetActive(true);
            Time.timeScale = 0.0f;
            StartCoroutine(WaitForKeyPress(KeyCode.Return));
           // OnAcquire.Invoke(other.gameObject.GetComponent<Player>());
        }
    }



    public IEnumerator WaitForKeyPress(KeyCode key)
    {
        yield return new WaitUntil(() => Input.GetKeyDown(key));

        Time.timeScale = 1.0f;
        Destroy(gameObject);
    }

    
    

}

public class OnAcquireEvent : UnityEvent<Player>
{
        
}