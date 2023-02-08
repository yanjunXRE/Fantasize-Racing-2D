using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIHandler : MonoBehaviour
{//All code is written By Yan Jun with reference to CarUIHandler.cs
    [Header("Car details")]
    public Image itemImage;
    //Get itemData class is used to reference to UseItemUI for player to use specific item
    public ItemData GetItem;
    //Other components
    Animator animator = null;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    //SetupItem is used to add sprite to see which item player uses
    public void SetupItem(ItemData itemData)
    {
        itemImage.sprite = itemData.ItemUISprite;
        GetItem = itemData;
    }



}
