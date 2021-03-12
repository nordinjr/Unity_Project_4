using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectPlankton : MonoBehaviour
{

    private GameManager gameLevelManager;
    public int plankVal;
    // Start is called before the first frame update
    void Start()
    {
        gameLevelManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameLevelManager.collPlank(plankVal);
            Destroy(gameObject);
        }
    }
}
