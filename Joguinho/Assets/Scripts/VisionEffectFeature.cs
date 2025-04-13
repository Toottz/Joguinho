using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VisionEffectFeature : ScriptableRendererFeature
{
    class VisionEffectPass : ScriptableRenderPass
    {
        public Material material;
        public RenderTargetIdentifier source;
        public RenderTargetHandle tempTexture;
        public string profilerTag;

        public VisionEffectPass(string tag)
        {
            profilerTag = tag;
            tempTexture.Init("_TempVisionEffectTex");
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (material == null) return;

            CommandBuffer cmd = CommandBufferPool.Get(profilerTag);
            RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
            cmd.GetTemporaryRT(tempTexture.id, opaqueDesc);
            Blit(cmd, source, tempTexture.Identifier(), material);
            Blit(cmd, tempTexture.Identifier(), source);
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }

    VisionEffectPass visionPass;
    public Material visionMaterial;
    public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingTransparents;

    public override void Create()
    {
        visionPass = new VisionEffectPass("VisionEffectPass");
        visionPass.material = visionMaterial;
        visionPass.renderPassEvent = renderPassEvent;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (visionMaterial == null) return;
        visionPass.source = renderer.cameraColorTarget;
        renderer.EnqueuePass(visionPass);
    }
}
