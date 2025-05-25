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
            float _DivisionAngle;
            float _Part1Width;
            float _Part2Width;
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

                // Переводим угол в радианы
                float angleRad = radians(_DivisionAngle);
                float2 dir = float2(cos(angleRad), sin(angleRad));
                float2 centeredUV = i.uv - 0.5;
                
                // Проецируем UV на направление делени€
                float pos = dot(centeredUV, dir) + 0.5;
                
                // Определяем часть (0-1-2)
                int part = pos < _Part1Width ? 0 : (pos < _Part1Width + _Part2Width ? 1 : 2);
                
                // Выбираем цвет
                fixed4 partColor = part == 0 ? _Color1 : (part == 1 ? _Color2 : _Color3);
                
                // Смешивание с текстурами
                fixed4 texColor = color;
                fixed4 overlay = tex2D(_OverlayTex, i.uv) * _Brightness;
                
                fixed4 finalColor = lerp(partColor * texColor, overlay, _Blend * texColor.a);
                return finalColor;
            }
            ENDCG
        }
    }
}