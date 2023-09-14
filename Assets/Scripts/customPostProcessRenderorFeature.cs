using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public class CustomPostProcessRenderorFeature : ScriptableRendererFeature
{
    [SerializeField]
    Shader m_BloomShader;
    [SerializeField]
    Shader m_CompostieShader;

    private Material m_bloom_material;
    private Material m_compostie_material;

    customPostProcessPass _customPostProcessPass;

    
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(_customPostProcessPass);
    }

    public override void Create()
    {
        m_bloom_material = CoreUtils.CreateEngineMaterial(m_BloomShader);
        m_compostie_material = CoreUtils.CreateEngineMaterial(m_CompostieShader);


        _customPostProcessPass = new customPostProcessPass(m_bloom_material, m_compostie_material);
    }

    public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
    {
        if(renderingData.cameraData.cameraType == CameraType.Game)
        {
            _customPostProcessPass.ConfigureInput(ScriptableRenderPassInput.Depth);
            _customPostProcessPass.ConfigureInput(ScriptableRenderPassInput.Color);
            _customPostProcessPass.SetTarget(renderer.cameraColorTargetHandle, renderer.cameraDepthTargetHandle);
        }
    }

    protected override void Dispose(bool disposing)
    {
        CoreUtils.Destroy(m_bloom_material);
        CoreUtils.Destroy(m_compostie_material);
    }
}
