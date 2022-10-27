Shader "Unlit/SizeSlider"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _indicatorColor ("Indicator Color", Color) = (1,0,0,1)
        _baseColor ("Base Color", Color) = (1,1,1,1)

        _indicatorUV ("Position", Vector) = (0,0,0,0)

    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "IgnoreProjector"="True" 
            "Queue" = "Transparent"
        }
        LOD 100
        ZWrite Off
        ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha 

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
            float4 _indicatorColor = (0.9,0,0,1);
            float4 _baseColor = (0.9, 0.9, 0.9, 1.0);

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
                float mix = step(_indicatorUV.x, i.uv) * (1.0 - step(_indicatorUV.x + 0.05, i.uv));
                
                float4 col = lerp(_baseColor, _indicatorColor, mix);

                float triangleShape = step(1, ((0.85)*i.uv.x + 0.15)/i.uv.y);
                float4 colShaped = lerp((0,0,0,0), col, triangleShape);
                return colShaped;
            }
            ENDCG
        }
    }
}
