Shader "Custom/BridgeDissolve"
{
   Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _DissolveTex ("Dissolve Texture", 2D) = "white" {}
        _Radius ("Dissolve Radius", Range(0, 5000)) = 10.0
        _CameraPos ("Camera Position", Vector) = (0, 0, 0, 0)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 texcoord : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            sampler2D _DissolveTex;
            float _Radius;
            float3 _CameraPos;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                float3 worldPos = i.worldPos;

                // Вычисляем расстояние от точки на дороге до камеры
                float dist = distance(worldPos, _CameraPos);

                // Применяем текстуру dissolve для добавления шума в эффект растворения
                float dissolve = tex2D(_DissolveTex, i.texcoord).r;

                // Вычисляем прозрачность в зависимости от расстояния до камеры
                float alpha = smoothstep(_Radius, _Radius - 1, dist) * dissolve;

                // Основная текстура с учетом прозрачности
                half4 col = tex2D(_MainTex, i.texcoord);
                col.a *= alpha;

                return col;
            }
            ENDCG
        }
    }
    FallBack "Transparent/Diffuse"
}