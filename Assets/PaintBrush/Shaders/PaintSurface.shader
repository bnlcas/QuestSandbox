Shader "Unlit/PaintSurface"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PaintTex ("OutTexture", 2D) = "Black" {}
        _NormalMap ("NormalMap", 2D) = "bump" {}
        _RegionMaskTex ("Region Mask Texture", 2D) = "Black" {}

        _maskSampleCoord ("Mask Sample Coord", Vector) = (0,0,0,0)
        _brushSize ("Brush Size", Float) = 0.1
        _brushColor ("Brush Color", Color) = (0,0,1,1)
        _brushPosition ("Brush Position", Vector) = (0,0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        ZTest Always

        Pass
        {
            CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members uv_bump)
#pragma exclude_renderers d3d11
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog


            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float2 uv_bump : TEXCOORD1;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uv_bump : TEXCOORD1;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            float _brushSize = 0.1;
            float2 _brushPosition = (0,0);
            float4 _brushColor = (0,0,1,1);

            float2 _maskSampleCoord = (0,0);

            sampler2D _PaintTex;
            sampler2D _RegionMaskTex;
            
            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _NormalMap;
            float4 _NormalMap_ST;



            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv_bump = TRANSFORM_TEX(v.uv, _NormalMap);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture 
                fixed4 col = tex2D(_MainTex, i.uv);
                float brush_intensity = (distance(_brushPosition, i.uv) < _brushSize);
                
                float4 uvMaskColor = tex2D(_RegionMaskTex, i.uv);
                float4 maskRegionColor = tex2D(_RegionMaskTex, _maskSampleCoord);
                float region_intensity = (uvMaskColor == maskRegionColor);
                float paint_intensity = region_intensity*brush_intensity;
                col = lerp(col, _brushColor, paint_intensity);

                //fixed4 normal = tex2D(_NormalMap, i.uv_bump);
                //float angle = dot(normal, fixed4(0.0, 0.0, 1.0, 1.0));
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
