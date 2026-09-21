using TMPro;
using UnityEngine;

public class TimeElapsedDisplay : MonoBehaviour
{
    private TMP_Text timeText;

    private void Awake()
    {
        timeText = GetComponent<TMP_Text>();
    }

    float timeElapsed = 0f;
    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        timeText.text = Mathf.FloorToInt(timeElapsed).ToString() + "s";
    }
}
