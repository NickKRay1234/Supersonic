Shader "Custom/DiagonalLineOverSprite"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _LineColor ("Line Color", Color) = (1, 1, 1, 1)
        _LineThickness ("Line Thickness", Range(0.01, 0.3)) = 0.02
        _Speed ("Speed", Range(0.0, 5)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _LineColor;
            float _LineThickness;
            float _Speed;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float time = _Time.y * _Speed;
                float linePos = frac(time);
                
                float distanceFromLine = abs((uv.x - uv.y) - (linePos - 0.5));
                
                float isLine = step(distanceFromLine, _LineThickness);

                fixed4 lineColor = _LineColor * isLine;
                fixed4 spriteColor = tex2D(_MainTex, uv);
                
                return spriteColor + lineColor * lineColor.a;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}




