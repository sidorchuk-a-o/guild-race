Shader "Custom/UIShieldRadial"
{
    Properties
    {
        [PerRendererData] _MainTex ("Base Texture", 2D) = "white" {}
        _OverlayTex ("Overlay Texture", 2D) = "white" {}
        
        _Color1 ("First Sector", Color) = (1,0,0,1)
        _Color2 ("Second Sector", Color) = (0,1,0,1)
        _Color3 ("Third Sector", Color) = (0,0,1,1)
        
        _AngleOffset ("Rotation Offset", Range(0, 360)) = 0
        _Sector2Size ("Sector 2 Size", Range(10, 180)) = 120
        _Center ("Center (XY)", Vector) = (0.5, 0.5, 0, 0) // Новый параметр
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
            fixed4 _Color3;
            float _AngleOffset;
            float _Sector2Size;
            float2 _Center; // X/Y координаты центра
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
                // Смещаем UV относительно нового центра
                float2 centeredUV = i.uv - _Center;
                
                // Переводим в полярные координаты
                float angle = atan2(centeredUV.y, centeredUV.x);
                angle = degrees(angle) + 180 + _AngleOffset;
                angle = fmod(angle, 360);

                // Рассчитываем границы секторов
                float sector1Size = (360 - _Sector2Size) * 0.5;
                float sector1End = sector1Size;
                float sector2End = sector1End + _Sector2Size;

                // Определяем сектор
                fixed4 color;
                if(angle < sector1End)
                    color = _Color1;
                else if(angle < sector2End)
                    color = _Color2;
                else
                    color = _Color3;

                // Смешивание с текстурами
                fixed4 texColor = tex2D(_MainTex, i.uv);
                fixed4 overlay = tex2D(_OverlayTex, i.uv) * _Brightness;
                
                fixed4 finalColor = lerp(color * texColor, overlay, _Blend * texColor.a);
                return finalColor;
            }
            ENDCG
        }
    }
}