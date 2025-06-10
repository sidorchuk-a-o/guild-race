Shader "Custom/UITextureBlend"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _OverlayTex ("Overlay Texture", 2D) = "white" {}
        
        _Color1 ("Tint", Color) = (1,1,1,1)
        _Blend ("Blend Amount", Range(0, 1)) = 1.0
        _Brightness ("Brightness", Range(0, 2)) = 1.0
        
        _OverlayScale ("Overlay Scale", Vector) = (1,1,0,0)
        _OverlayOffset ("Overlay Offset", Vector) = (0,0,0,0)
        
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
            "IgnoreProjector"="True"
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

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uvOverlay : TEXCOORD1;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
                float4 worldPosition : TEXCOORD2;
            };

            sampler2D _MainTex;
            sampler2D _OverlayTex;
            float4 _MainTex_ST;
            float2 _OverlayScale;
            float2 _OverlayOffset;
            fixed4 _Color1;
            float _Blend;
            float _Brightness;
            float4 _ClipRect;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPosition = v.vertex;
                
                // Основные UV
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                
                // UV для Overlay с масштабированием и смещением
                o.uvOverlay = (v.uv - 0.5) * _OverlayScale + 0.5 + _OverlayOffset;
                
                o.color = v.color * _Color1;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Проверка маски
                half4 color = tex2D(_MainTex, i.uv) * i.color;
                color.a *= UnityGet2DClipping(i.worldPosition.xy, _ClipRect);
                
                // Если пиксель отсечен маской, возвращаем прозрачный цвет
                #ifdef UNITY_UI_CLIP_RECT
                clip(color.a - 0.001);
                #endif
                
                fixed4 sprite = color;
                fixed4 overlay = tex2D(_OverlayTex, i.uvOverlay) * _Brightness;
                float alphaEdge = smoothstep(0, 0.1, sprite.a);

                fixed4 result = lerp(sprite, overlay, _Blend * alphaEdge);
                result.a = sprite.a;
                
                return result;
            }
            ENDCG
        }
    }
}