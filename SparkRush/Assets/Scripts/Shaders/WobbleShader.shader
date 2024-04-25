Shader "Custom/WobbleShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WobbleSpeed ("Wobble Speed", Float) = 1.0
        _WobbleStrength ("Wobble Strength", Float) = 0.1
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert vertex:vert
        
        struct Input
        {
            float2 uv_MainTex;
        };

        sampler2D _MainTex;
        float _WobbleSpeed;
        float _WobbleStrength;

        void vert(inout appdata_full v)
        {
            float wobble = sin(_Time.y * _WobbleSpeed) * _WobbleStrength; // Calculate wobble amount based on time
            v.vertex.x += wobble; // Apply wobble to the vertex position along the x-axis
        }

        void surf(Input IN, inout SurfaceOutput o)
        {
            o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
