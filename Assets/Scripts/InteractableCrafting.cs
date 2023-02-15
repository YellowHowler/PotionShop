using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCrafting : MonoBehaviour
{
    private SpriteRenderer sp;

    private List<string> items;

    private int maxItems = 3;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        items = new List<string>();
    }

    void Update()
    {
        
    }
    
    public void ChangeSprite(bool transparent)
    {
        if(transparent || items.Count == 0) sp.color = new Color(1f,1f,1f,0.8f + BoolToInt(!transparent)*0.2f);
    }

    public void AddItem(GameObject item, string subType)
    {
        if(items.Count < maxItems) 
        {
            items.Add(subType);
        }
    }

    private int BoolToInt(bool val){
        if (val)
            return 1;
        return 0;
    }
}
