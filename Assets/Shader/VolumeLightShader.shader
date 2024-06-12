Shader "Custom/VolumeLightShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LightColor ("Light Color", Color) = (1, 1, 1, 1)
        _LightIntensity ("Light Intensity", Float) = 1.0
    }
    SubShader
    {
        Tags { "Queue" = "Overlay" }
        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            fixed4 _LightColor;
            float _LightIntensity;

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
                col.rgb *= _LightColor.rgb * _LightIntensity;
                col.a *= _LightIntensity;
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
