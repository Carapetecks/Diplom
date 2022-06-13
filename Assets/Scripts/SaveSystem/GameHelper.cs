using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using System.IO;

public class GameHelper : MonoBehaviour
{
    public GameObject[] prefabs;
    public int x;
    private string path;
    public List<SaveableObject> objects = new List<SaveableObject>();
    private void Awake()
    {
        path = Application.persistentDataPath + "/testsave.xml";
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) Save();
        if (Input.GetKeyDown(KeyCode.L)) Load();
    }
    public void Save()
    {
        XElement root = new XElement("root");
        foreach(SaveableObject obj in objects)
        {
            root.Add(obj.GetElement());
        }
        root.AddFirst(new XElement("score", ScoreText.Score));

        XDocument saveDocument = new XDocument(root);
        File.WriteAllText(path, saveDocument.ToString());
    }
    public void Load()
    {
        XElement root = null;
        if(!File.Exists(path))
        {
            if (File.Exists(Application.persistentDataPath + "/level.xml"))
            {
                root = XDocument.Parse(File.ReadAllText(Application.persistentDataPath + "/level.xml")).Element("root");
            }    
        }
        else
        {
            root = XDocument.Parse(File.ReadAllText(path)).Element("root");
        }
        if (root == null)
        {
            Debug.Log("Level load failed");
            return;
        }
            GenerateScene(root);

    }
    private void GenerateScene(XElement root)
    {
        //foreach (SaveableObject obj in objects)
        //{
        //    obj.DestroySelf();
        //}
        foreach (XElement instance in root.Elements("instance"))
        {
            Vector3 position = Vector3.zero;
            position.x = float.Parse(instance.Attribute("x").Value);
            position.y = float.Parse(instance.Attribute("y").Value);
            position.z = float.Parse(instance.Attribute("z").Value);
            x = UnityEngine.Random.Range(0, prefabs.Length);
            Instantiate(prefabs[x], position, Quaternion.identity);
        }
    }
}
