using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LighteningManager : MonoBehaviour
{
    [SerializeField]GameObject lightening;

    int minArea = 22;
    int maxArea = 40;

    bool strike = true;
    float strikeRate = 1f;
    float strikeTime = 0f;
    float strikeDelay = 0.7f;

    float gameTimer = 0;

    [SerializeField] GameObject gameOver;
    [SerializeField] TextMeshProUGUI timeDISP;
    [SerializeField] TextMeshProUGUI timeDISPMenu;
    [SerializeField] TextMeshProUGUI missedStrikesDISP;
    int missedStrikes = 0;
    float timeSurvived = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (strike) 
        {
            if (strikeTime > strikeRate)
            {
                GameObject spawn = Instantiate(lightening, new Vector3(Random.Range(0, maxArea), Random.Range(0, minArea), 0), Quaternion.identity);
                spawn.GetComponent<Strike>().SetDelay(strikeDelay);
                missedStrikes++;
                strikeTime = 0f;
            }
            else 
            {
                strikeTime += Time.deltaTime;
            }

            if (gameTimer > 5)
            {
                if (strikeDelay > 0.3f)
                {
                    strikeDelay = strikeDelay - 0.1f;
                }
                else 
                {
                    strikeDelay = 0.3f;
                }
                if (strikeRate > 0) 
                {
                    strikeRate = strikeRate - 0.2f;
                }
                else
                {
                    strikeRate = 0;
                }
                gameTimer = 0f;
            }
            else 
            {
                gameTimer += Time.deltaTime;
            }
            timeSurvived += Time.deltaTime;
            timeDISP.text = timeSurvived.ToString("F1");
        }
    }

    public void GameOver() 
    {
        strike = false;
        gameOver.SetActive(true);
        missedStrikesDISP.text = missedStrikes.ToString();
        timeDISPMenu.text = timeSurvived.ToString("F1");
    }

    public void CastStrike(Vector2 myPosition) 
    {
        GameObject spawn = Instantiate(lightening, new Vector3(myPosition.x, myPosition.y, 0), Quaternion.identity);
        spawn.GetComponent<Strike>().SetDelay(strikeDelay);
        missedStrikes++;
    }
}
