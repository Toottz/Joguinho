Shader "Unlit/Screen Shader"
{
    Properties
    {
        _Velocidade_Nevoa("Velocidade Nevoa", Float) = 2
        _Piscando_Opacidade("Piscando Opacidade", Range(0, 1)) = 0
        _Velocidade_Piscando("Velocidade Piscando", Float) = 2
        _Croma_Opacidade("Croma Opacidade", Range(0, 1)) = 1
        _Nevoa_Opacidade("Nevoa Opacidade", Range(0, 1)) = 1
        _Cor_Nevoa("Cor Nevoa", Color) = (1, 1, 1, 0)
        _Scale_Nevoa("Scale Nevoa", Float) = 50
        [HideInInspector]_QueueOffset("_QueueOffset", Float) = 0
        [HideInInspector]_QueueControl("_QueueControl", Float) = -1
        [HideInInspector][NoScaleOffset]unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "RenderType"="Transparent"
            "UniversalMaterialType" = "Unlit"
            "Queue"="Transparent"
            "ShaderGraphShader"="true"
            "ShaderGraphTargetId"="UniversalUnlitSubTarget"
        }
        Pass
        {
            Name "Universal Forward"
            Tags
            {
                // LightMode: <None>
            }
        
        // Render State
        Cull Back
        Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
        ZTest LEqual
        ZWrite Off
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 4.5
        #pragma exclude_renderers gles gles3 glcore
        #pragma multi_compile_instancing
        #pragma multi_compile_fog
        #pragma instancing_options renderinglayer
        #pragma multi_compile _ DOTS_INSTANCING_ON
        #pragma vertex vert
        #pragma fragment frag
        
        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>
        
        // Keywords
        #pragma multi_compile _ LIGHTMAP_ON
        #pragma multi_compile _ DIRLIGHTMAP_COMBINED
        #pragma shader_feature _ _SAMPLE_GI
        #pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
        #pragma multi_compile_fragment _ DEBUG_DISPLAY
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define VARYINGS_NEED_VIEWDIRECTION_WS
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_UNLIT
        #define _FOG_FRAGMENT 1
        #define _SURFACE_TYPE_TRANSPARENT 1
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DBuffer.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float3 normalWS;
             float4 texCoord0;
             float3 viewDirectionWS;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float4 uv0;
             float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float4 texCoord0 : INTERP0;
             float3 positionWS : INTERP1;
             float3 normalWS : INTERP2;
             float3 viewDirectionWS : INTERP3;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.texCoord0.xyzw = input.texCoord0;
            output.positionWS.xyz = input.positionWS;
            output.normalWS.xyz = input.normalWS;
            output.viewDirectionWS.xyz = input.viewDirectionWS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.texCoord0 = input.texCoord0.xyzw;
            output.positionWS = input.positionWS.xyz;
            output.normalWS = input.normalWS.xyz;
            output.viewDirectionWS = input.viewDirectionWS.xyz;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _Piscando_Opacidade;
        float _Croma_Opacidade;
        float _Velocidade_Piscando;
        float _Nevoa_Opacidade;
        float4 _Cor_Nevoa;
        float _Scale_Nevoa;
        float _Velocidade_Nevoa;
        CBUFFER_END
        
        // Object and Global properties
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        
        inline float Unity_SimpleNoise_RandomValue_float (float2 uv)
        {
            float angle = dot(uv, float2(12.9898, 78.233));
            #if defined(SHADER_API_MOBILE) && (defined(SHADER_API_GLES) || defined(SHADER_API_GLES3) || defined(SHADER_API_VULKAN))
                // 'sin()' has bad precision on Mali GPUs for inputs > 10000
                angle = fmod(angle, TWO_PI); // Avoid large inputs to sin()
            #endif
            return frac(sin(angle)*43758.5453);
        }
        
        inline float Unity_SimpleNnoise_Interpolate_float (float a, float b, float t)
        {
            return (1.0-t)*a + (t*b);
        }
        
        
        inline float Unity_SimpleNoise_ValueNoise_float (float2 uv)
        {
            float2 i = floor(uv);
            float2 f = frac(uv);
            f = f * f * (3.0 - 2.0 * f);
        
            uv = abs(frac(uv) - 0.5);
            float2 c0 = i + float2(0.0, 0.0);
            float2 c1 = i + float2(1.0, 0.0);
            float2 c2 = i + float2(0.0, 1.0);
            float2 c3 = i + float2(1.0, 1.0);
            float r0 = Unity_SimpleNoise_RandomValue_float(c0);
            float r1 = Unity_SimpleNoise_RandomValue_float(c1);
            float r2 = Unity_SimpleNoise_RandomValue_float(c2);
            float r3 = Unity_SimpleNoise_RandomValue_float(c3);
        
            float bottomOfGrid = Unity_SimpleNnoise_Interpolate_float(r0, r1, f.x);
            float topOfGrid = Unity_SimpleNnoise_Interpolate_float(r2, r3, f.x);
            float t = Unity_SimpleNnoise_Interpolate_float(bottomOfGrid, topOfGrid, f.y);
            return t;
        }
        void Unity_SimpleNoise_float(float2 UV, float Scale, out float Out)
        {
            float t = 0.0;
        
            float freq = pow(2.0, float(0));
            float amp = pow(0.5, float(3-0));
            t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;
        
            freq = pow(2.0, float(1));
            amp = pow(0.5, float(3-1));
            t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;
        
            freq = pow(2.0, float(2));
            amp = pow(0.5, float(3-2));
            t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;
        
            Out = t;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Multiply_float4_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A * B;
        }
        
        
        inline float2 Unity_Voronoi_RandomVector_float (float2 UV, float offset)
        {
            float2x2 m = float2x2(15.27, 47.63, 99.41, 89.98);
            UV = frac(sin(mul(UV, m)));
            return float2(sin(UV.y*+offset)*0.5+0.5, cos(UV.x*offset)*0.5+0.5);
        }
        
        void Unity_Voronoi_float(float2 UV, float AngleOffset, float CellDensity, out float Out, out float Cells)
        {
            float2 g = floor(UV * CellDensity);
            float2 f = frac(UV * CellDensity);
            float t = 8.0;
            float3 res = float3(8.0, 0.0, 0.0);
        
            for(int y=-1; y<=1; y++)
            {
                for(int x=-1; x<=1; x++)
                {
                    float2 lattice = float2(x,y);
                    float2 offset = Unity_Voronoi_RandomVector_float(lattice + g, AngleOffset);
                    float d = distance(lattice + offset, f);
        
                    if(d < res.x)
                    {
                        res = float3(d, offset.x, offset.y);
                        Out = res.x;
                        Cells = res.y;
                    }
                }
            }
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Fraction_float2(float2 In, out float2 Out)
        {
            Out = frac(In);
        }
        
        void Unity_Rectangle_Fastest_float(float2 UV, float Width, float Height, out float Out)
        {
            float2 d = abs(UV * 2 - 1) - float2(Width, Height);
        #if defined(SHADER_STAGE_RAY_TRACING)
            d = saturate((1 - saturate(d * 1e7)));
        #else
            d = saturate(1 - d / fwidth(d));
        #endif
            Out = min(d.x, d.y);
        }
        
        struct Bindings_Grid_f2ff61a18444547458ea8849584f6bec_float
        {
        half4 uv0;
        };
        
        void SG_Grid_f2ff61a18444547458ea8849584f6bec_float(float _width, float2 _tiling, float2 _offset, Bindings_Grid_f2ff61a18444547458ea8849584f6bec_float IN, out float Out_1)
        {
        float2 _Property_372660d10ff02e85a219c963d16063f1_Out_0 = _tiling;
        float2 _Property_cf215d09b9c0dd8cbc94e9ff5fc8cd32_Out_0 = _offset;
        float2 _TilingAndOffset_7ef98bf5d80d3e80920e877b157d97a2_Out_3;
        Unity_TilingAndOffset_float(IN.uv0.xy, _Property_372660d10ff02e85a219c963d16063f1_Out_0, _Property_cf215d09b9c0dd8cbc94e9ff5fc8cd32_Out_0, _TilingAndOffset_7ef98bf5d80d3e80920e877b157d97a2_Out_3);
        float2 _Fraction_00dd85cd0e38fc8ba6e24864e56ff16d_Out_1;
        Unity_Fraction_float2(_TilingAndOffset_7ef98bf5d80d3e80920e877b157d97a2_Out_3, _Fraction_00dd85cd0e38fc8ba6e24864e56ff16d_Out_1);
        float _Property_213c9e6e95ba64828704c5ad7e10ffe4_Out_0 = _width;
        float _Rectangle_60dc71b9aa2e6084b4873c8f50fb6a1b_Out_3;
        Unity_Rectangle_Fastest_float(_Fraction_00dd85cd0e38fc8ba6e24864e56ff16d_Out_1, _Property_213c9e6e95ba64828704c5ad7e10ffe4_Out_0, _Property_213c9e6e95ba64828704c5ad7e10ffe4_Out_0, _Rectangle_60dc71b9aa2e6084b4873c8f50fb6a1b_Out_3);
        Out_1 = _Rectangle_60dc71b9aa2e6084b4873c8f50fb6a1b_Out_3;
        }
        
        void Unity_Add_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A + B;
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void SawtoothWave_float(float In, out float Out)
        {
            Out = 2 * (In - floor(0.5 + In));
        }
        
        void Unity_Ceiling_float(float In, out float Out)
        {
            Out = ceil(In);
        }
        
        void Unity_Clamp_float(float In, float Min, float Max, out float Out)
        {
            Out = clamp(In, Min, Max);
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float3 BaseColor;
            float Alpha;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float2 _Vector2_72e73518e85e4a738e669926bc387259_Out_0 = float2(float(0), IN.TimeParameters.x);
            float2 _TilingAndOffset_d94cd77055544ad4ad1c29e8bef26fce_Out_3;
            Unity_TilingAndOffset_float(IN.uv0.xy, float2 (1, 1), _Vector2_72e73518e85e4a738e669926bc387259_Out_0, _TilingAndOffset_d94cd77055544ad4ad1c29e8bef26fce_Out_3);
            float _Property_230c7f4701c5423c88b16d0e9d6463e6_Out_0 = _Scale_Nevoa;
            float _SimpleNoise_0d1ab8e2d8044f98ba56fd2ef2065c57_Out_2;
            Unity_SimpleNoise_float(_TilingAndOffset_d94cd77055544ad4ad1c29e8bef26fce_Out_3, _Property_230c7f4701c5423c88b16d0e9d6463e6_Out_0, _SimpleNoise_0d1ab8e2d8044f98ba56fd2ef2065c57_Out_2);
            float _Property_d58272d6df1f4cc49c52c3acd5107d9b_Out_0 = _Nevoa_Opacidade;
            float _Multiply_238d383bbb9d44bfb7d31d7112f8c8e3_Out_2;
            Unity_Multiply_float_float(_SimpleNoise_0d1ab8e2d8044f98ba56fd2ef2065c57_Out_2, _Property_d58272d6df1f4cc49c52c3acd5107d9b_Out_0, _Multiply_238d383bbb9d44bfb7d31d7112f8c8e3_Out_2);
            float4 _Property_113f27566522426dbcf6619eb0264f56_Out_0 = _Cor_Nevoa;
            float4 _Multiply_8a0f87028a544397a7662a84b941a760_Out_2;
            Unity_Multiply_float4_float4((_Multiply_238d383bbb9d44bfb7d31d7112f8c8e3_Out_2.xxxx), _Property_113f27566522426dbcf6619eb0264f56_Out_0, _Multiply_8a0f87028a544397a7662a84b941a760_Out_2);
            float _Property_78b89284a9ca497bb98ecb701e793b45_Out_0 = _Croma_Opacidade;
            float4 Color_e3661de21fcc40e4a4a6b33a4c7f0bbf = IsGammaSpace() ? float4(0, 1, 0, 0) : float4(SRGBToLinear(float3(0, 1, 0)), 0);
            float _Voronoi_f4c6fe2c1192457ca0279d1116a00747_Out_3;
            float _Voronoi_f4c6fe2c1192457ca0279d1116a00747_Cells_4;
            Unity_Voronoi_float(IN.uv0.xy, IN.TimeParameters.x, float(0.5), _Voronoi_f4c6fe2c1192457ca0279d1116a00747_Out_3, _Voronoi_f4c6fe2c1192457ca0279d1116a00747_Cells_4);
            float _Power_8a8ab203f96a4dd094c3e6b4dd41403a_Out_2;
            Unity_Power_float(_Voronoi_f4c6fe2c1192457ca0279d1116a00747_Out_3, float(2.96), _Power_8a8ab203f96a4dd094c3e6b4dd41403a_Out_2);
            float _Multiply_5da988a042bf4dc6ac732b077cc60211_Out_2;
            Unity_Multiply_float_float(_Power_8a8ab203f96a4dd094c3e6b4dd41403a_Out_2, 0.55, _Multiply_5da988a042bf4dc6ac732b077cc60211_Out_2);
            Bindings_Grid_f2ff61a18444547458ea8849584f6bec_float _Grid_11ce5ac3460345bc931c0179b1f47ad8;
            _Grid_11ce5ac3460345bc931c0179b1f47ad8.uv0 = IN.uv0;
            float _Grid_11ce5ac3460345bc931c0179b1f47ad8_Out_1;
            SG_Grid_f2ff61a18444547458ea8849584f6bec_float(_Multiply_5da988a042bf4dc6ac732b077cc60211_Out_2, float2 (100, -27.33), float2 (0, 0), _Grid_11ce5ac3460345bc931c0179b1f47ad8, _Grid_11ce5ac3460345bc931c0179b1f47ad8_Out_1);
            float4 _Multiply_979212cbd02e4222b85182ab0d640bd0_Out_2;
            Unity_Multiply_float4_float4(Color_e3661de21fcc40e4a4a6b33a4c7f0bbf, (_Grid_11ce5ac3460345bc931c0179b1f47ad8_Out_1.xxxx), _Multiply_979212cbd02e4222b85182ab0d640bd0_Out_2);
            float4 Color_5d16851cc1b04094bbc5b16b25190f67 = IsGammaSpace() ? float4(1, 0, 0, 0) : float4(SRGBToLinear(float3(1, 0, 0)), 0);
            Bindings_Grid_f2ff61a18444547458ea8849584f6bec_float _Grid_35590392fd804ecaa8f0a04b292774d5;
            _Grid_35590392fd804ecaa8f0a04b292774d5.uv0 = IN.uv0;
            float _Grid_35590392fd804ecaa8f0a04b292774d5_Out_1;
            SG_Grid_f2ff61a18444547458ea8849584f6bec_float(_Multiply_5da988a042bf4dc6ac732b077cc60211_Out_2, float2 (100, -27.33), float2 (0.528, 0), _Grid_35590392fd804ecaa8f0a04b292774d5, _Grid_35590392fd804ecaa8f0a04b292774d5_Out_1);
            float4 _Multiply_1dddf38c56c244ee984db359b67c9713_Out_2;
            Unity_Multiply_float4_float4(Color_5d16851cc1b04094bbc5b16b25190f67, (_Grid_35590392fd804ecaa8f0a04b292774d5_Out_1.xxxx), _Multiply_1dddf38c56c244ee984db359b67c9713_Out_2);
            float4 Color_b03f354cf8ef464398e948085d019cc2 = IsGammaSpace() ? float4(0, 0, 1, 0) : float4(SRGBToLinear(float3(0, 0, 1)), 0);
            Bindings_Grid_f2ff61a18444547458ea8849584f6bec_float _Grid_dfe37d0016524395abc8ad77e211cdf2;
            _Grid_dfe37d0016524395abc8ad77e211cdf2.uv0 = IN.uv0;
            float _Grid_dfe37d0016524395abc8ad77e211cdf2_Out_1;
            SG_Grid_f2ff61a18444547458ea8849584f6bec_float(_Multiply_5da988a042bf4dc6ac732b077cc60211_Out_2, float2 (100, -27.33), float2 (0.265, 0), _Grid_dfe37d0016524395abc8ad77e211cdf2, _Grid_dfe37d0016524395abc8ad77e211cdf2_Out_1);
            float4 _Multiply_c0323c5da81e43a481031a19f6112a23_Out_2;
            Unity_Multiply_float4_float4(Color_b03f354cf8ef464398e948085d019cc2, (_Grid_dfe37d0016524395abc8ad77e211cdf2_Out_1.xxxx), _Multiply_c0323c5da81e43a481031a19f6112a23_Out_2);
            float4 _Add_4a9836bb031d4eb3a1a9828c4d369ccd_Out_2;
            Unity_Add_float4(_Multiply_1dddf38c56c244ee984db359b67c9713_Out_2, _Multiply_c0323c5da81e43a481031a19f6112a23_Out_2, _Add_4a9836bb031d4eb3a1a9828c4d369ccd_Out_2);
            float4 _Add_6fbb2758eba44e0bb7cde1907e5333d6_Out_2;
            Unity_Add_float4(_Multiply_979212cbd02e4222b85182ab0d640bd0_Out_2, _Add_4a9836bb031d4eb3a1a9828c4d369ccd_Out_2, _Add_6fbb2758eba44e0bb7cde1907e5333d6_Out_2);
            float4 _Multiply_1224f13115c5403c887e7f2b6e7e9ccf_Out_2;
            Unity_Multiply_float4_float4((_Property_78b89284a9ca497bb98ecb701e793b45_Out_0.xxxx), _Add_6fbb2758eba44e0bb7cde1907e5333d6_Out_2, _Multiply_1224f13115c5403c887e7f2b6e7e9ccf_Out_2);
            float4 _Add_a47659d581a14afc80ccd1befe0ae444_Out_2;
            Unity_Add_float4(_Multiply_8a0f87028a544397a7662a84b941a760_Out_2, _Multiply_1224f13115c5403c887e7f2b6e7e9ccf_Out_2, _Add_a47659d581a14afc80ccd1befe0ae444_Out_2);
            float _Add_c0e33a4666f340dd88dfe3d92f3dd128_Out_2;
            Unity_Add_float(_Grid_35590392fd804ecaa8f0a04b292774d5_Out_1, _Grid_dfe37d0016524395abc8ad77e211cdf2_Out_1, _Add_c0e33a4666f340dd88dfe3d92f3dd128_Out_2);
            float _Add_0c0fe447ee184b36a503dde3a3900c93_Out_2;
            Unity_Add_float(_Grid_11ce5ac3460345bc931c0179b1f47ad8_Out_1, _Add_c0e33a4666f340dd88dfe3d92f3dd128_Out_2, _Add_0c0fe447ee184b36a503dde3a3900c93_Out_2);
            float _Multiply_35f373ad171841fdabc0703c5d2e7815_Out_2;
            Unity_Multiply_float_float(_Property_78b89284a9ca497bb98ecb701e793b45_Out_0, _Add_0c0fe447ee184b36a503dde3a3900c93_Out_2, _Multiply_35f373ad171841fdabc0703c5d2e7815_Out_2);
            float _Add_1aa555c235d04d7f9b8aa9b25b04a9f7_Out_2;
            Unity_Add_float(_Multiply_238d383bbb9d44bfb7d31d7112f8c8e3_Out_2, _Multiply_35f373ad171841fdabc0703c5d2e7815_Out_2, _Add_1aa555c235d04d7f9b8aa9b25b04a9f7_Out_2);
            float _Property_bf5e6542ef9843168d6f27be9fbb7ae8_Out_0 = _Velocidade_Piscando;
            float _Multiply_cad5fc38ec084fb79bd8ae40c74f07e7_Out_2;
            Unity_Multiply_float_float(IN.TimeParameters.x, _Property_bf5e6542ef9843168d6f27be9fbb7ae8_Out_0, _Multiply_cad5fc38ec084fb79bd8ae40c74f07e7_Out_2);
            float _SawtoothWave_ddfc128e9e9e443d8f5b2e68e49cfd05_Out_1;
            SawtoothWave_float(_Multiply_cad5fc38ec084fb79bd8ae40c74f07e7_Out_2, _SawtoothWave_ddfc128e9e9e443d8f5b2e68e49cfd05_Out_1);
            float _Ceiling_f9ca461dda084426acd79faf7d278516_Out_1;
            Unity_Ceiling_float(_SawtoothWave_ddfc128e9e9e443d8f5b2e68e49cfd05_Out_1, _Ceiling_f9ca461dda084426acd79faf7d278516_Out_1);
            float _Add_f00588f5175b48a6b045b5e1f76c4eff_Out_2;
            Unity_Add_float(_Ceiling_f9ca461dda084426acd79faf7d278516_Out_1, float(0.2), _Add_f00588f5175b48a6b045b5e1f76c4eff_Out_2);
            float _Property_ec999aee71bb4de5be5282e3732e8af2_Out_0 = _Piscando_Opacidade;
            float _Add_dbeb5c4cbc974aae9b4983048afec44c_Out_2;
            Unity_Add_float(_Add_f00588f5175b48a6b045b5e1f76c4eff_Out_2, _Property_ec999aee71bb4de5be5282e3732e8af2_Out_0, _Add_dbeb5c4cbc974aae9b4983048afec44c_Out_2);
            float _Clamp_9beece49630c4e29b2bff9b2d52b6b7b_Out_3;
            Unity_Clamp_float(_Add_dbeb5c4cbc974aae9b4983048afec44c_Out_2, float(0), float(1), _Clamp_9beece49630c4e29b2bff9b2d52b6b7b_Out_3);
            float _Multiply_4e421f19500a4df9a841f2e8f0f3a242_Out_2;
            Unity_Multiply_float_float(_Add_1aa555c235d04d7f9b8aa9b25b04a9f7_Out_2, _Clamp_9beece49630c4e29b2bff9b2d52b6b7b_Out_3, _Multiply_4e421f19500a4df9a841f2e8f0f3a242_Out_2);
            surface.BaseColor = (_Add_a47659d581a14afc80ccd1befe0ae444_Out_2.xyz);
            surface.Alpha = _Multiply_4e421f19500a4df9a841f2e8f0f3a242_Out_2;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
            // FragInputs from VFX come from two places: Interpolator or CBuffer.
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
            output.uv0 = input.texCoord0;
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/UnlitPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
    }
    CustomEditorForRenderPipeline "UnityEditor.ShaderGraphUnlitGUI" "UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset"
    CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
    FallBack "Hidden/Shader Graph/FallbackError"
}