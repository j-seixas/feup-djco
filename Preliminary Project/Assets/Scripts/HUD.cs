using UnityEngine;

public class HUD : MonoBehaviour
{
    static private PlayerHealth playerHealth;
    static private RectTransform healtBarBackground;
    static public RectTransform healthBarFill;
    static private GameObject canvas;

    void Start()
    {
        DontDestroyOnLoad(this);
        canvas = GameObject.Find("Canvas");
        healthBarFill = GameObject.Find("HealthBar/Fill").GetComponent<RectTransform>();
        healtBarBackground = GameObject.Find("HealthBar/Background").GetComponent<RectTransform>();
    }

    void Update()
    {
        if(playerHealth.health > 0)
            healthBarFill.sizeDelta = new Vector2(healtBarBackground.sizeDelta.x / PlayerHealth.initialHealth * playerHealth.health, healtBarBackground.sizeDelta.y);
        else healthBarFill.sizeDelta = new Vector2(0, healtBarBackground.sizeDelta.y);
    }

	static public void SetPlayerHealth(PlayerHealth ph)
	{
        playerHealth = ph;
	}

    static public void SetEnable(bool enable)
    {
        canvas.SetActive(enable);
    }
}
