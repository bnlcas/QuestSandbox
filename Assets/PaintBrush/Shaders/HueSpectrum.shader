Shader "Unlit/HueSpectrum"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _indicatorColor ("Indicator Color", Color) = (0.8,0,0,1)
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
            
            float3 HSVToRGB( float3 c )
			{
				float4 K = float4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
				float3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
				return c.z * lerp( K.xxx, saturate( p - K.xxx ), c.y );
			}

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
            float4 _indicatorColor = (0.8,0,0,1);
            float2 _indicatorUV = (0.5,0.5);


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
                float3 hsv = float3(i.uv.y, 0.9, 0.9);
                float4 rgb = float4(HSVToRGB(hsv), 1);

                float indicatorMix = step(_indicatorUV.y, i.uv.y) * (1.0 - step(_indicatorUV.y + 0.05, i.uv.y));
                return lerp(rgb, _indicatorColor, indicatorMix);
            }
            ENDCG
        }
    }
}
