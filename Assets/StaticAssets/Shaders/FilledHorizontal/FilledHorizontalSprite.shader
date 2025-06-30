
Shader "Custom/FilledHorizontalSpriteWithAlpha"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}
        _FillAmount ("Fill Amount", Range(0, 1)) = 1.0 
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" } 
        Pass
        {
            // ���������� �����-����������
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite On
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // ��������� ������ ��� ������
            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex; 
            float _FillAmount; // �������� ����������

            // ��������� ������
            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color; // �������� ���� � ����� �� SpriteRenderer
                return o;
            }

            // ����������� ������
            half4 frag(v2f i) : SV_Target
            {
                // �������� ���� ������� �� ��������
                half4 texColor = tex2D(_MainTex, i.uv);

                // ��������� ���� � �����, ������� ��� ����� � SpriteRenderer
                texColor *= i.color;

                // ���� ������� ������� �� X ������, ��� _FillAmount, ������ ������� ����������
                if (i.uv.x > _FillAmount)
                {
                    texColor.a = 0; // ������������ ��� ������� �����
                }

                return texColor;
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}