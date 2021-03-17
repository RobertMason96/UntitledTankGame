using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShade : MonoBehaviour
{
    [SerializeField]
    private Vector2 cellSize = new Vector2(4, 4);
    public Color colour = new Color(1, 1, 1, 1);
    private Material _material;
    // Start is called before the first frame update
    void Awake()
    {
        _material = new Material(Shader.Find("Custom/BlackWhiteShader"));
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        _material.SetFloat("_ScreenWidth", Screen.width);
        _material.SetFloat("_ScreenHeight", Screen.height);
        _material.SetFloat("_CellSizeX", cellSize.x);
        _material.SetFloat("_CellSizeY", cellSize.y);
        _material.SetColor("_Colour", colour);
        Graphics.Blit(source, destination, _material);
    }
}
