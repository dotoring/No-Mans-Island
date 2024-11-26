Shader "Skybox/NightDay_URP"
{
    Properties
    {
        _Texture1("Texture1", 2D) = "white" {}
        _Texture2("Texture2", 2D) = "white" {}
        _Blend("Blend", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "Queue" = "Background" "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" }
        Pass
        {
            Name "Skybox"
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // Constants
            #define PI 3.14159265359

            // Properties
            TEXTURE2D(_Texture1);
            SAMPLER(sampler_Texture1);
            TEXTURE2D(_Texture2);
            SAMPLER(sampler_Texture2);
            float _Blend;

            // Vertex Input
            struct Attributes
            {
                float3 positionOS : POSITION;
            };

            // Interpolated Output
            struct Varyings
            {
                float3 worldDir : TEXCOORD0;
                float4 positionCS : SV_POSITION;
            };

            Varyings Vert(Attributes v)
            {
                Varyings output;

                // 카메라 정보를 스테레오 지원 방식으로 가져오기
                float3 worldPos = TransformObjectToWorld(v.positionOS);
#if defined(USING_STEREO_MATRICES)
                float3 cameraPos = GetCameraPositionWS();
#else
                float3 cameraPos = _WorldSpaceCameraPos; // 비스테레오 모드에서는 일반 카메라 사용
#endif

                output.worldDir = normalize(worldPos - cameraPos);
                output.positionCS = TransformWorldToHClip(worldPos);
                return output;
            }

            float2 ToRadialCoords(float3 worldDir)
            {
                float3 normalizedCoords = normalize(worldDir);
                float latitude = acos(normalizedCoords.y);
                float longitude = atan2(normalizedCoords.z, normalizedCoords.x);
                float2 sphereCoords = float2(longitude, latitude) * float2(0.5 / PI, 1.0 / PI);
                return float2(0.5, 1.0) - sphereCoords;
            }

            half4 Frag(Varyings i) : SV_Target
            {
                float2 uv = ToRadialCoords(i.worldDir);
                half4 tex1 = SAMPLE_TEXTURE2D(_Texture1, sampler_Texture1, uv);
                half4 tex2 = SAMPLE_TEXTURE2D(_Texture2, sampler_Texture2, uv);
                return lerp(tex1, tex2, _Blend);
            }
            ENDHLSL
        }
    }
}
