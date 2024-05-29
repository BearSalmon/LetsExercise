using UnityEngine;
using System. Collections;
using TMPro;
public class TeleType : MonoBehaviour
{
    private TextMeshProUGUI m_textMeshPro;
    public TMP_Text text;
    public GameObject nextText;
    IEnumerator Start()
    {
        text.ForceMeshUpdate();
        // Get Reference to TextMeshPro Component if one exists; Otherwise add one.
        m_textMeshPro = gameObject.GetComponent<TextMeshProUGUI>() ?? gameObject.AddComponent<TextMeshProUGUI>();


        int totalVisibleCharacters = m_textMeshPro.textInfo.characterCount; // Get # of Visible Character in text object
        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            m_textMeshPro.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?
            //m_textMeshPro-maxVisibleLines = MaxLines;

            // Once the last character has been revealed, wait 1.0 second and start over.
            if (visibleCount >= totalVisibleCharacters)
            {
                //yield return new WaitForSeconds(1.0f);
                break;
            }

            counter += 1;

            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(1.8f);
        if (nextText != null)
        {
            gameObject.SetActive(false);
            nextText.SetActive(true);
        }
    }
}
