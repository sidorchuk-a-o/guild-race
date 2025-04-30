Shader "Custom/UIShieldQuadDivision"
{
    Properties
    {
        [PerRendererData] _MainTex ("Base Texture", 2D) = "white" {}
        _OverlayTex ("Overlay Texture", 2D) = "white" {}
        
        _Color1 ("Sector 1", Color) = (1,0,0,1)
        _Color2 ("Sector 2", Color) = (0,1,0,1)
        _Color3 ("Sector 3", Color) = (0,0,1,1)
        _Color4 ("Sector 4", Color) = (1,1,0,1)
        
        _RotationAngle ("Rotation Angle", Range(0, 360)) = 0
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
            fixed4 _Color4;
            float _RotationAngle;
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
                // Поворот UV координат
                float2 centeredUV = i.uv - 0.5;
                float angleRad = radians(_RotationAngle);
                float2x2 rotationMatrix = float2x2(
                    cos(angleRad), -sin(angleRad),
                    sin(angleRad), cos(angleRad)
                );
                float2 rotatedUV = mul(centeredUV, rotationMatrix) + 0.5;

                // Определение секторов
                bool left = rotatedUV.x < 0.5;
                bool top = rotatedUV.y < 0.5;
                
                fixed4 sectorColor;
                if(top) {
                    sectorColor = left ? _Color1 : _Color2;
                } else {
                    sectorColor = left ? _Color3 : _Color4;
                }

                // Смешивание с текстурами
                fixed4 texColor = tex2D(_MainTex, i.uv);
                fixed4 overlay = tex2D(_OverlayTex, i.uv) * _Brightness;
                
                fixed4 finalColor = lerp(sectorColor * texColor, overlay, _Blend * texColor.a);
                return finalColor;
            }
            ENDCG
        }
    }
}