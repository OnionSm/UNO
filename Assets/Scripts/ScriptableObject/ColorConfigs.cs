using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ColorConfigs", menuName = "Colors/ColorConfigs")]
public class ColorConfigs : ScriptableObject
{
    public List<ColorConfig> all_color_configs;
}

[System.Serializable]
public class ColorConfig
{
    public CardColor type_color;
    public string color;

    public ColorConfig Clone()
    {
        return new ColorConfig
        {
            type_color = this.type_color,
            color = this.color
        };
    }
}
