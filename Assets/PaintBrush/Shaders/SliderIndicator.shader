Shader "Unlit/SliderIndicator"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _indicatorColor ("Indicator Color", Color) = (0,0,1,1)
        _indicatorUV ("Position", Vector) = (0,0,0,0)

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float2 _indicatorUV = (0.5,0.5);
            float4 _indicatorColor = (0,0,1,1);
            float4 _baseColor = (0.1,0.1,0.1,1.0);

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                //fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                
                float mix = step(0.5, distance(i.uv, float2(_indicatorUV.x, 0.5)));

                float4 col = lerp(_indicatorColor, _baseColor, mix);
                return col;
            }
            ENDCG
        }
    }
}
