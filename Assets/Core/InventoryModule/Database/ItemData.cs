using System;
using UnityEditor.Animations;
using UnityEngine;

[Serializable]
public struct ItemData
{
    public string id;

    public string name;
    public string price;
    public Sprite icon;
    public AnimatorController animatorController;
}
