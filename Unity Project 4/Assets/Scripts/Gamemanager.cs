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

    private int currentLevel = 0;
    public TextMeshProUGUI menuText;

    public GameObject dialogBox;
    public GameObject dialogText;
    public float typeSpeed = .05f;
    private Coroutine dialogCO;

    // Start is called before the first frame update
    void Start()
    {

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
        //else
        //{
        //    Destroy(gameObject);
        //}
    }


    private void ChangeLeve()
    {
        if (currentLevel == 0)
        {
            startButton.SetActive(false);
            menuText.text = "";
            StartCoroutine(LoadYourAsyncScene("Level1"));
            currentLevel++;
        }
    }

    public void StartButton()
    {
        ChangeLeve();
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
