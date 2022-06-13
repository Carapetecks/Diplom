using System.Xml.Linq;
using UnityEngine;

public class SaveableObject : MonoBehaviour
{
   
    private GameHelper gameHelper;
    private void Awake()
    {
        gameHelper = FindObjectOfType<GameHelper>();
    }
    private void Start()
    {
        gameHelper.objects.Add(this);
    }
    private void OnDestroy()
    {
        gameHelper.objects.Remove(this);
    }
    public XElement GetElement()
    {
        XAttribute x = new XAttribute("x", transform.position.x);
        XAttribute y = new XAttribute("y", transform.position.y);
        XAttribute z = new XAttribute("z", transform.position.z);

        XElement element = new XElement("instance", name, x, y, z);

        return element;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
