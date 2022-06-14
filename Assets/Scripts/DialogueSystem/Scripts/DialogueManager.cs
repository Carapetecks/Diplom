﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml;
using System.IO;

public class DialogueManager : MonoBehaviour {

	public ScrollRect scrollRect;
	public ButtonComponent button;
	public string folder = "Russian"; // подпапка в Resources, для чтения
	public int offset = 100;

	public string fileName, lastName;
	private List<Dialogue> node;
	private Dialogue dialogue;
	private Answer answer;
	public Character character;
	public DialogueTrigger dialogueTrigger;	
	private List<RectTransform> buttons = new List<RectTransform>();
	private float curY, height;
	private static DialogueManager _internal;
	public static bool isQuestDone;
	public GameObject Follower;

	private void Start()
    {
		dialogueTrigger = GameObject.FindGameObjectWithTag("NPC").GetComponent<DialogueTrigger>();
		character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }
    public void DialogueStart(string name)
	{
		if(name == string.Empty) return;
		fileName = name;		
		Load();
		
	}

	public static DialogueManager Internal
	{
		get{ return _internal; }
	}
	void Awake()
	{
		_internal = this;
		button.gameObject.SetActive(false);
		scrollRect.gameObject.SetActive(false);
	}

	void Load()
	{
		scrollRect.gameObject.SetActive(true);

		if(lastName == fileName) // проверка, чтобы не загружать уже загруженный файл
		{
			BuildDialogue(0);
			return;
		}

		node = new List<Dialogue>();

		try // чтение элементов XML и загрузка значений атрибутов в массивы
		{
			TextAsset binary = Resources.Load<TextAsset>(folder + "/" + fileName);
			XmlTextReader reader = new XmlTextReader(new StringReader(binary.text));

			int index = 0;
			while(reader.Read())
			{
				if(reader.IsStartElement("node"))
				{
					dialogue = new Dialogue();
					dialogue.answer = new List<Answer>();
					dialogue.npcText = reader.GetAttribute("npcText");
					node.Add(dialogue);

					XmlReader inner = reader.ReadSubtree();
					while(inner.ReadToFollowing("answer"))
					{
						answer = new Answer();
						answer.text = reader.GetAttribute("text");

						int number;
						if(int.TryParse(reader.GetAttribute("toNode"), out number)) answer.toNode = number; else answer.toNode = 0;

						bool result;
						if(bool.TryParse(reader.GetAttribute("exit"), out result)) answer.exit = result; else answer.exit = false;
						if (bool.TryParse(reader.GetAttribute("testdamage"), out result)) answer.testdamage = result; else answer.testdamage = false;

						node[index].answer.Add(answer);
					}
					inner.Close();

					index++;
				}
			}

			lastName = fileName;
			reader.Close();
		}
		catch(System.Exception error)
		{
			Debug.Log(this + " Ошибка чтения файла диалога: " + fileName + ".xml >> Error: " + error.Message);
			scrollRect.gameObject.SetActive(false);
			lastName = string.Empty;
		}

		BuildDialogue(0);
	}

	void AddToList(bool exit, int toNode, string text, bool isActive, bool testdamage)
	{
		BuildElement(exit, toNode, text, isActive, testdamage);
		curY += height + offset;
		RectContent();
	}

	void BuildElement(bool exit, int toNode, string text, bool isActiveButton, bool testdamage)
	{
		ButtonComponent clone = Instantiate(button) as ButtonComponent;
		clone.gameObject.SetActive(true);
		clone.rect.SetParent(scrollRect.content);
		clone.rect.localScale = Vector3.one;
		clone.text.text = text;
		clone.rect.sizeDelta = new Vector2(clone.rect.sizeDelta.x, clone.text.preferredHeight + offset);
		clone.button.interactable = isActiveButton;
		height = clone.rect.sizeDelta.y;
		clone.rect.anchoredPosition = new Vector2(0, -height/2 - curY);

		if(toNode > 0) SetNextDialogue(clone.button, toNode);
		if(exit) SetExitDialogue(clone.button);
		if (testdamage) TestEvent(clone.button);
		

		buttons.Add(clone.rect);
	}

	void RectContent()
	{
		scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, curY);
		scrollRect.content.anchoredPosition = Vector2.zero;
	}

	void ClearDialogue()
	{
		curY = offset;
		foreach(RectTransform b in buttons)
		{
			Destroy(b.gameObject);
		}
		buttons = new List<RectTransform>();
		RectContent();
	}

	void SetNextDialogue(Button button, int id) // добавляем событие кнопке, для перенаправления на другой узел диалога
	{
		button.onClick.AddListener(() => BuildDialogue(id));
	}

	void SetExitDialogue(Button button) // добавляем событие кнопке, для выхода из диалога
	{
		button.onClick.AddListener(() => CloseDialogue());
	}
	void TestEvent(Button button)
    {
		button.onClick.AddListener(() => TestTakeDamage());

	}
	public void TestTakeDamage()
    {
		if (ScoreText.Score < 500)
		{
			name = "dialogue2";
			dialogueTrigger.fileName = name;			
		}
		if(ScoreText.Score >= 500)
        {
			ScoreText.Score -= 500;
			isQuestDone = true;
			Follower.SetActive(true);
		}
	}
	public void CloseDialogue()
	{
		scrollRect.gameObject.SetActive(false);
		ClearDialogue();
	}

	void BuildDialogue(int current)
	{
		ClearDialogue();
		AddToList(false, 0, node[current].npcText, false, false);
		for (int i = 0; i < node[current].answer.Count; i++)
		{
			AddToList(node[current].answer[i].exit,  node[current].answer[i].toNode, node[current].answer[i].text, true, node[current].answer[i].testdamage);
		}
	}
}
	
class Dialogue
{
	public string npcText;
	public List<Answer> answer;
}


class Answer
{
	public string text;
	public int toNode;
	public bool exit;
	public bool testdamage;
	
}