using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    static private PlayerHealth playerHealth;
    static private PlayerShooting playerShooting;
    static private RectTransform healtBarBackground;
    static public RectTransform healthBarFill;
    static public TextMeshProUGUI penNumber;
    static private GameObject canvas;

    void Start()
    {
        DontDestroyOnLoad(this);
        canvas = GameObject.Find("Canvas");
        healthBarFill = GameObject.Find("HealthBar/Fill").GetComponent<RectTransform>();
        healtBarBackground = GameObject.Find("HealthBar/Background").GetComponent<RectTransform>();
        penNumber = GameObject.Find("Pens/Number").GetComponent<TextMeshProUGUI>();
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
        canvas.SetActive(enable);
    }
}
