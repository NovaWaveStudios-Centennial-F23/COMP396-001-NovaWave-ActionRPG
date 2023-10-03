using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    // [Yusuke Kuroki]Need to add more variables for item/weapon/gear etc...
    public int id;
    public string itemName;
    public int value;
    public Sprite icon;
}