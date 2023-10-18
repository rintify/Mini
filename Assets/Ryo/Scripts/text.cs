
using UnityEngine;
using TMPro;
public class text : MonoBehaviour
{
    private TextMeshProUGUI textframe;
    // Start is called before the first frame update
    void Start()
    {
         textframe = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
        textframe.text="SCORE "+" " + count.number + "/" + movement.enemy;
    }
}
