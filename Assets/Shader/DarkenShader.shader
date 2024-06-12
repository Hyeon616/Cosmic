Shader "Custom/DarkenShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LightTex ("Light Texture", 2D) = "black" {} // This texture will store the light effect
        _DarkColor ("Dark Color", Color) = (0, 0, 0, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            ColorMask RGB

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
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
            sampler2D _LightTex;
            fixed4 _DarkColor;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 lightCol = tex2D(_LightTex, i.uv);
                return col * lightCol + _DarkColor * (1 - lightCol.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}