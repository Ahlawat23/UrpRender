using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public class customPostProcessPass : ScriptableRenderPass
{
    RenderTextureDescriptor m_Descriptor;
    private RTHandle m_CameraColorTarget;
    private RTHandle m_CameraDepthTarget;
    

    private Material m_bloom_material;
    private Material m_composite_material;

    const int k_MaxPyramidSize = 16;
    private int[] _BloomMipUp;
    private int[] _BloomMipDown;
    private RTHandle[] m_BloomMipUP;
    private RTHandle[] m_BloomMipDown;
    

    

    public customPostProcessPass(Material bloom_material, Material composite_material)
    {
        m_bloom_material = bloom_material;
        m_composite_material = composite_material;

        renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;

        _BloomMipUp = new int[k_MaxPyramidSize];
        _BloomMipDown = new int[k_MaxPyramidSize];
        m_BloomMipUP = new RTHandle[k_MaxPyramidSize];
        m_BloomMipDown = new RTHandle[k_MaxPyramidSize];


        for (int i = 0; i < k_MaxPyramidSize; i++)
        {
            _BloomMipUp[i] = Shader.PropertyToID("_BloomMipUp" + i);
            _BloomMipDown[i] = Shader.PropertyToID("_BloomMipDown" + i);

            m_BloomMipUP[i] = RTHandles.Alloc(_BloomMipUp[i], name: "_BloomMipUp" + i);
            m_BloomMipDown[i] = RTHandles.Alloc(_BloomMipUp[i], name: "_BloomMipDown" + i);

        }

        
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        
    }

    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        m_Descriptor = renderingData.cameraData.cameraTargetDescriptor;

    }

    public void SetTarget(RTHandle cameraColorTargetHandle, RTHandle cameraDepthTargetHandel)
    {
        m_CameraColorTarget = cameraColorTargetHandle;
        m_CameraDepthTarget = cameraDepthTargetHandel;
    }
}
