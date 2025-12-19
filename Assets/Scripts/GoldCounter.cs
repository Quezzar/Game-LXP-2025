using UnityEngine;
using UnityEngine.UIElements;

public class GoldCounter : MonoBehaviour
{

    public int GoldCount = 20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Label goldLabel;
    public UIDocument UIDocument;
    void Start()
    {
        goldLabel = UIDocument.rootVisualElement.Q<Label>("GoldLabel");
    }

    // Update is called once per frame
    void Update()
    {
        goldLabel.text = "Gold: " + GoldCount.ToString();   
    }
}
