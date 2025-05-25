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
        _Center ("Center (XY)", Vector) = (0.5, 0.5, 0, 0)
        _Blend ("Overlay Blend", Range(0, 1)) = 0.5
        _Brightness ("Overlay Brightness", Range(0, 2)) = 1.0
        
        // Mask support
        [Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp ("Stencil Comparison", Int) = 8
        _Stencil ("Stencil ID", Int) = 0
        [Enum(UnityEngine.Rendering.StencilOp)] _StencilOp ("Stencil Operation", Int) = 0
        _StencilWriteMask ("Stencil Write Mask", Int) = 255
        _StencilReadMask ("Stencil Read Mask", Int) = 255
        _ColorMask ("Color Mask", Int) = 15
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off
        ZTest [unity_GUIZTestMode]
        ColorMask [_ColorMask]

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"
            #define PI 3.14159265359

            struct appdata
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
                float4 worldPosition : TEXCOORD1;
            };

            sampler2D _MainTex;
            sampler2D _OverlayTex;
            float4 _MainTex_ST;
            fixed4 _Color1;
            fixed4 _Color2;
            fixed4 _Color3;
            float _AngleOffset;
            float _Sector2Size;
            float2 _Center;
            float _Blend;
            float _Brightness;
            float4 _ClipRect;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPosition = v.vertex;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Clip rect masking
                half4 baseColor = tex2D(_MainTex, i.uv) * i.color;
                baseColor.a *= UnityGet2DClipping(i.worldPosition.xy, _ClipRect);
                #ifdef UNITY_UI_CLIP_RECT
                clip(baseColor.a - 0.001);
                #endif

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
                fixed4 sectorColor;
                if(angle < sector1End)
                    sectorColor = _Color1;
                else if(angle < sector2End)
                    sectorColor = _Color3;
                else
                    sectorColor = _Color2;

                // Смешивание с текстурами
                fixed4 texColor = baseColor;
                fixed4 overlay = tex2D(_OverlayTex, i.uv) * _Brightness;
                
                fixed4 finalColor = lerp(sectorColor * texColor, overlay, _Blend * texColor.a);
                return finalColor;
            }
            ENDCG
        }
    }
}