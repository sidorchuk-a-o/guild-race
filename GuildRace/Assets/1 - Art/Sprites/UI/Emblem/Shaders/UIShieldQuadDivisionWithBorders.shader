Shader "Custom/UIShieldQuadDivisionWithBorders"
{
    Properties
    {
        [PerRendererData] _MainTex ("Base Texture", 2D) = "white" {}
        _OverlayTex ("Overlay Texture", 2D) = "white" {}
        
        _Color1 ("Sector 1", Color) = (1,0,0,1)
        _Color2 ("Sector 2", Color) = (0,1,0,1)
        _Color3 ("Sector 3", Color) = (0,0,1,1)
        _Color4 ("Sector 4", Color) = (1,1,0,1)
        _Color5 ("Line Color", Color) = (0,0,0,1)
        
        _RotationAngle ("Rotation Angle", Range(0, 360)) = 0
        _LineWidth ("Line Width", Range(0, 0.2)) = 0.02
        
        _Blend ("Overlay Blend", Range(0, 1)) = 0.5
        _Brightness ("Overlay Brightness", Range(0, 2)) = 1.0
        
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
            fixed4 _Color4;
            fixed4 _Color5;
            float _RotationAngle;
            float _LineWidth;
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
                half4 color = tex2D(_MainTex, i.uv) * i.color;
                color.a *= UnityGet2DClipping(i.worldPosition.xy, _ClipRect);
                #ifdef UNITY_UI_CLIP_RECT
                clip(color.a - 0.001);
                #endif

                // Поворот UV координат
                float2 centeredUV = i.uv - 0.5;
                float angleRad = radians(_RotationAngle);
                float2x2 rotationMatrix = float2x2(
                    cos(angleRad), -sin(angleRad),
                    sin(angleRad), cos(angleRad)
                );
                float2 rotatedUV = mul(centeredUV, rotationMatrix);

                // Определение секторов
                bool left = rotatedUV.x < 0;
                bool top = rotatedUV.y > 0;
                fixed4 sectorColor = top ? (left ? _Color1 : _Color2) : (left ? _Color3 : _Color4);

                // Определение линий
                float2 absUV = abs(rotatedUV);
                float lineMask = 
                    step(absUV.x, _LineWidth) +  // Вертикальная линия
                    step(absUV.y, _LineWidth);   // Горизонтальная линия
                
                // Смешивание цветов
                fixed4 finalColor = lerp(sectorColor, _Color5, saturate(lineMask));
                
                // Наложение текстур
                fixed4 texColor = color;
                fixed4 overlay = tex2D(_OverlayTex, i.uv) * _Brightness;
                finalColor = lerp(finalColor * texColor, overlay, _Blend * texColor.a);
                
                return finalColor;
            }
            ENDCG
        }
    }
}