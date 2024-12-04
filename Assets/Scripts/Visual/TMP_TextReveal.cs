using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class TMP_TextReveal : MonoBehaviour
{
    [SerializeField] TMP_Text tmp_Text;
    [SerializeField] string revealText;
    [SerializeField] float revealDuration;

    StringBuilder stringBuilder = new StringBuilder();
    private bool isRevealed = false;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        CheckPlayer();
    }

    private void CheckPlayer()
    {
        if (!isRevealed)
        {
            if(Vector3.Distance(player.transform.position, transform.position) < 20f)
            {
                isRevealed = true;
                RevealText();
            }
        }
    }

    public void RevealText()
    {

        StopAllCoroutines();
        stringBuilder.Clear();
        StartCoroutine(TextRevealRoutine());
    }

    private IEnumerator TextRevealRoutine()
    {
        int charCount = revealText.Length;
        float timeStep = revealDuration / charCount;
        for (int i = 0; i < charCount; i++)
        {
            char nextChar = revealText[i];
            stringBuilder.Append(nextChar);
            tmp_Text.text = stringBuilder.ToString();
            yield return new WaitForSeconds(timeStep);
        }
    }
}
