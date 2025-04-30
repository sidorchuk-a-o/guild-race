Shader "Custom/UIShieldDivision"
{
    Properties
    {
        [PerRendererData] _MainTex ("Base Texture", 2D) = "white" {}
        _OverlayTex ("Overlay Texture", 2D) = "white" {}
        
        _Color1 ("First Color", Color) = (1,0,0,1)
        _Color2 ("Second Color", Color) = (0,0,1,1)
        
        _DivisionAngle ("Division Angle (degrees)", Range(0, 360)) = 0
        _DivisionOffset ("Division Offset", Range(-1,1)) = 0
        
        _Blend ("Overlay Blend", Range(0, 1)) = 0.5
        _Brightness ("Overlay Brightness", Range(0, 2)) = 1.0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #define PI 3.14159265359

            struct appdata
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
            sampler2D _OverlayTex;
            float4 _MainTex_ST;
            fixed4 _Color1;
            fixed4 _Color2;
            float _DivisionAngle;
            float _DivisionOffset;
            float _Blend;
            float _Brightness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Вычисляем линию деления
                float angleRad = radians(_DivisionAngle);
                float2 divisionNormal = float2(sin(angleRad), -cos(angleRad));
                float2 centeredUV = i.uv - 0.5;
                bool isFirstPart = dot(centeredUV, divisionNormal) + _DivisionOffset < 0;
                
                // Базовый цвет (деление)
                fixed4 baseColor = isFirstPart ? _Color1 : _Color2;
                fixed4 texColor = tex2D(_MainTex, i.uv);
                fixed4 colorPart = baseColor * texColor;
                
                // Наложение дополнительной текстуры
                fixed4 overlay = tex2D(_OverlayTex, i.uv) * _Brightness;
                fixed4 finalColor = lerp(colorPart, overlay, _Blend * texColor.a);
                
                return finalColor;
            }
            ENDCG
        }
    }
}