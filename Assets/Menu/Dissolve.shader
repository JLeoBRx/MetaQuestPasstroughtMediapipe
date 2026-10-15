Shader "JLeo/Dissolve"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)

        _NoiseTex ("Dissolve Noise", 2D) = "white" {}
        _Dissolve ("Dissolve", Range(0,1)) = 0

        _EdgeColor ("Edge Color", Color) = (0,0.5,1,1)
        _EdgeWidth ("Edge Width", Range(0,0.2)) = 0.05
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
            "Queue"="Geometry"
        }

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _NoiseTex;
            float4 _NoiseTex_ST;

            fixed4 _Color;

            float _Dissolve;
            fixed4 _EdgeColor;
            float _EdgeWidth;

            v2f vert(appdata v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 mainColor = tex2D(_MainTex, i.uv);
                float noise = tex2D(_NoiseTex, i.uv).r;

                // Área que deve desaparecer
                if (noise < _Dissolve)
                {
                    discard;
                }

                // Borda do dissolve
                float edge = smoothstep(
                    _Dissolve,
                    _Dissolve + _EdgeWidth,
                    noise
                );

                fixed4 finalColor = mainColor * _Color;

                // Brilho na borda
                finalColor.rgb = lerp(
                    _EdgeColor.rgb,
                    finalColor.rgb,
                    edge
                );

                return finalColor;
            }

            ENDCG
        }
    }
}