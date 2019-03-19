using UnityEngine;
using UnityEngine.UI;
using TMPro;

[DefaultExecutionOrder(-200)]
public class HUD : MonoBehaviour
{
    static private PlayerHealth playerHealth;
    static private PlayerShooting playerShooting;
    static private RectTransform healtBarBackground;
    static public RectTransform healthBarFill;
    static public TextMeshProUGUI penNumber;
    static public GameObject canvas;
    static private Image background;

    static private HUD current;

    void Awake()
	{
		if (current != null && current != this)
		{
			Destroy(gameObject);
			return;
		}

		current = this;

		DontDestroyOnLoad(gameObject);
	}

    void Start()
    {
        canvas = gameObject;
        healthBarFill = GameObject.Find("HealthBar/Fill").GetComponent<RectTransform>();
        healtBarBackground = GameObject.Find("HealthBar/Background").GetComponent<RectTransform>();
        penNumber = GameObject.Find("Pens/Number").GetComponent<TextMeshProUGUI>();
        background = canvas.GetComponent<Image>();
    }

    void Update()
    {
        penNumber.text = playerShooting.GetPens().ToString();
        if(playerHealth.health > 0)
            healthBarFill.sizeDelta = new Vector2(healtBarBackground.sizeDelta.x / PlayerHealth.initialHealth * playerHealth.health, healtBarBackground.sizeDelta.y);
        else healthBarFill.sizeDelta = new Vector2(0, healtBarBackground.sizeDelta.y);
    }

	static public void SetPlayerHealth(PlayerHealth ph)
	{
        playerHealth = ph;
	}

    static public void SetPlayerShooting(PlayerShooting ps)
	{
        playerShooting = ps;
	}

    static public void SetEnable(bool enable)
    {
        if(canvas)
            canvas.SetActive(enable);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        background.color = new Vector4(background.color.r, background.color.g, background.color.b, 0f);
    }
    
    public void Pause()
    {
        Time.timeScale = 0f;
        background.color = new Vector4(background.color.r, background.color.g, background.color.b, 0.4f);
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        Resume();
        GameManager.ReturnToMenu();
    }
}
