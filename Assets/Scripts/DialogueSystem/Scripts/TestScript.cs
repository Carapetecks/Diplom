using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	// пример, получения имени файла диалога и запуска процесса
	private bool CheckEnter = false;
	void Update()
	{
		if (CheckEnter && Input.GetKeyDown(KeyCode.F)) TriggerDialog();
		CloseTrigger();
	}
	void TriggerDialog()
    {
		DialogueTrigger dialogueTrigger = transform.GetComponent<DialogueTrigger>();
		if(dialogueTrigger != null && dialogueTrigger.fileName != string.Empty)
		FindObjectOfType<DialogueManager>().DialogueStart(dialogueTrigger.fileName);		
    }
	private void OnTriggerEnter2D(Collider2D other)
	{

		if (other.CompareTag("Player"))
		{
			CheckEnter = true;
		}
	}
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			CheckEnter = false;
		}
	}
	public void CloseTrigger()
    {
		if(CheckEnter==false)
		FindObjectOfType<DialogueManager>().CloseDialogue();

		
	}
}
