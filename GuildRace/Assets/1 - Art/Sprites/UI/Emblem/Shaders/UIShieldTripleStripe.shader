Shader "Custom/UIShieldTripleStripe"
{
    Properties
    {
        [PerRendererData] _MainTex ("Base Texture", 2D) = "white" {}
        _OverlayTex ("Overlay Texture", 2D) = "white" {}
        
        _Color1 ("First Color", Color) = (1,0,0,1)
        _Color2 ("Second Color", Color) = (0,1,0,1)
        _Color3 ("Third Color", Color) = (0,0,1,1)
        
        _DivisionAngle ("Division Angle", Range(0, 360)) = 0
        _Part1Width ("Part 1 Width", Range(0.1, 0.8)) = 0.33
        _Part2Width ("Part 2 Width", Range(0.1, 0.8)) = 0.33
        
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
            fixed4 _Color3;
            float _DivisionAngle;
            float _Part1Width;
            float _Part2Width;
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
                // Переводим угол в радианы
                float angleRad = radians(_DivisionAngle);
                float2 dir = float2(cos(angleRad), sin(angleRad));
                float2 centeredUV = i.uv - 0.5;
                
                // Проецируем UV на направление деления
                float pos = dot(centeredUV, dir) + 0.5;
                
                // Определяем часть (0-1-2)
                int part = pos < _Part1Width ? 0 : (pos < _Part1Width + _Part2Width ? 1 : 2);
                
                // Выбираем цвет
                fixed4 partColor = part == 0 ? _Color1 : (part == 1 ? _Color2 : _Color3);
                
                // Смешивание с текстурами
                fixed4 texColor = tex2D(_MainTex, i.uv);
                fixed4 overlay = tex2D(_OverlayTex, i.uv) * _Brightness;
                
                fixed4 finalColor = lerp(partColor * texColor, overlay, _Blend * texColor.a);
                return finalColor;
            }
            ENDCG
        }
    }
}