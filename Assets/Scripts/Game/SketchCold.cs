using UnityEngine;

public class SketchCold : MonoBehaviour
{
   public Material mat;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(null, dest, mat);

    }
}
