using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugText : MonoBehaviour
{
    [SerializeField]
    private RectTransform _Arrow;

    public RectTransform Arrow
    {
        get => _Arrow;
        set => _Arrow = value;
    }
    public TextMeshProUGUI F { get => _F; set => _F = value; }
    public TextMeshProUGUI G { get => _G; set => _G = value; }
    public TextMeshProUGUI H { get => _H; set => _H = value; }
    public TextMeshProUGUI P { get => _P; set => _P = value; }

    [SerializeField]
    private TextMeshProUGUI _F, _G, _H, _P;
}
