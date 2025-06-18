using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using FMODUnity;

//GPTCODE

public class TypewriterEffect : MonoBehaviour
{
    [System.Serializable]
    public class TextboxData
    {
		
        public TextMeshProUGUI textBox;
        [TextArea] public string fullText;
        public Transform triggerPoint;  // position near the textbox
        public float triggerDistance = 5f;
        [HideInInspector] public bool hasStarted = false;
    }
	public EventReference typeSound;
    public List<TextboxData> textboxes = new List<TextboxData>();
    public Transform player;
    public float letterDelay = 0.05f;

    void Update()
    {
        foreach (TextboxData data in textboxes)
        {
            if (!data.hasStarted && Vector3.Distance(player.position, data.triggerPoint.position) <= data.triggerDistance)
            {
                data.hasStarted = true;
                StartCoroutine(TypeText(data));
            }
        }
    }

	IEnumerator TypeText(TextboxData data)
	{
		data.textBox.text = "";
		foreach (char letter in data.fullText)
		{
			data.textBox.text += letter;

			if (!char.IsWhiteSpace(letter))  // avoid playing sound on spaces
			{
				RuntimeManager.PlayOneShot(typeSound, data.triggerPoint.position);
			}

			yield return new WaitForSeconds(letterDelay);
		}
	}

}
