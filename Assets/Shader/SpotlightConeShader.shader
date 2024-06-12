Shader "Custom/SpotlightConeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 0.87, 0.73, 0.5) // �� ������ ������� ���İ� ����
        _Intensity ("Intensity", Float) = 1.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" }
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
            fixed4 _Color;
            float _Intensity;

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
                // ���� �߽ɿ��� �����ڸ��� ������ ���������� ȿ�� ����
                float alpha = (1.0 - i.uv.y) * _Color.a * _Intensity * 0.5; // ������ �� ���ϰ� ����
                col.rgb = lerp(col.rgb, _Color.rgb, _Intensity);
                col.a *= alpha;
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
