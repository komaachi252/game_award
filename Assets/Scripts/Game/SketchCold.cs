using UnityEngine;

public class SketchCold : MonoBehaviour
{
    [SerializeField]
    Material mat;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(null, dest, mat);

    }
}
