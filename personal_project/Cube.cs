using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;

    public float rotationSpeed = 10.0f;
    private bool isFadingOut = false;
    private float fadeDuration = 2.0f;
    private float timeElapsed = 0.0f;
    private float baseAlpha = 0.1f;
    private float finalAlpha = 1.0f;

    void Start()
    {
        transform.position = new Vector3(3, 4, 1);
        transform.localScale = Vector3.one * 1.3f;
        
        Material material = Renderer.material;
        SetupMaterialTransparency(material);

        material.color = new Color(0.5f, 1.0f, 0.3f, 0.4f);
    }
    
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, 0.0f, 0.0f);

        float startAlpha = isFadingOut ? finalAlpha : baseAlpha;
        float targetAlpha = isFadingOut ? baseAlpha : finalAlpha;
        float currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / fadeDuration);
        timeElapsed += Time.deltaTime;
        SetMaterialAlpha(currentAlpha);

        if (timeElapsed >= fadeDuration)
        {
            isFadingOut = !isFadingOut;
            timeElapsed = 0.0f;
        }

    }
    private void SetMaterialAlpha(float alpha)
    {
        Color color = Renderer.material.color;
        color.a = Mathf.Clamp01(alpha); // 确保Alpha值在0-1之间
        Renderer.material.color = color;
    }

    // 配置材质的透明渲染模式
    private void SetupMaterialTransparency(Material targetMaterial)
    {
        // 设置渲染模式为 Fade 或 Transparent[3](@ref)
        targetMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        targetMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        targetMaterial.SetInt("_ZWrite", 0); // 关闭深度写入
        targetMaterial.DisableKeyword("_ALPHATEST_ON");
        targetMaterial.EnableKeyword("_ALPHABLEND_ON"); // 启用Alpha混合
        targetMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        targetMaterial.renderQueue = 3000; // 设置为透明渲染队列
    }
}
