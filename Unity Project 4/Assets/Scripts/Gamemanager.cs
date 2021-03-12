using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject startButton;
    public GameObject backgroundImage;

    public GameObject canvas;
    public GameObject events;

    public TextMeshProUGUI menuText;
    public TextMeshProUGUI planktonText;

    private int currentLevel = 0;
    private int planktonCount = -1;
    private int targetPlankton = -2;


    public GameObject dialogBox;
    public GameObject dialogText;
    public float typeSpeed = .05f;
    private Coroutine dialogCO;

    public int plank;

    public void collPlank(int numPlank)
    {
        plank += numPlank;
        if (plank % 5 == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        menuText.text = "Jellyfish Game";
        planktonText.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(canvas);
            DontDestroyOnLoad(events);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncPlanktonCount()
    {
        planktonCount++;
        planktonText.text = "Plankton: " + planktonCount + "/" + targetPlankton;
        if (planktonCount == targetPlankton) ChangeLevel();

    }


    public void ChangeLevel()
    {
        if (currentLevel == 0)
        {
            startButton.SetActive(false);
            menuText.text = "";
            StartCoroutine(LoadYourAsyncScene("Level1"));
            planktonCount = 0;
            targetPlankton = 3;
            UpdateCount();
            currentLevel++;
        }
    }

    private void UpdateCount()
    {
        planktonText.text = "Plankton: " + planktonCount + "/" + targetPlankton;
    }

    public void StartButton()
    {
        ChangeLevel();
    }

    IEnumerator ColorLerp(Color endvalue, float duration)
    {
        float time = 0;
        Image sprite = backgroundImage.GetComponent<Image>();
        Color startValue = sprite.color;

        while (time < duration)
        {
            sprite.color = Color.Lerp(startValue, endvalue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        sprite.color = endvalue;
    }

    IEnumerator LoadYourAsyncScene(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        StartCoroutine(ColorLerp(new Color(0, 0, 0, 0), 2));
    }

    public void StartDialog(string text)
    {
        dialogBox.SetActive(true);
        dialogCO = StartCoroutine(TypeText(text));
    }

    public void HideDialog()
    {
        dialogBox.SetActive(false);
        StopCoroutine(dialogCO);
    }

    IEnumerator TypeText(string text)
    {
        dialogText.GetComponent<TextMeshProUGUI>().text = "";
        foreach (char c in text.ToCharArray())
        {
            dialogText.GetComponent<TextMeshProUGUI>().text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
