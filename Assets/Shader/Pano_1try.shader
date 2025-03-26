Shader "Custom/CurvedPlaneShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" { }
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            // Eingabestruktur für den Vertex-Shader
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            // Ausgabe für den Fragment-Shader
            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 normal : TEXCOORD1;
                float2 uv : TEXCOORD0;
            };

            // Texturdefinition
            sampler2D _MainTex;
            float4 _MainTex_ST;

            // Vertex-Shader
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex); // Transformation der Vertex-Position
                o.normal = v.normal; // Normalen weitergeben
                o.uv = v.uv; // UV-Koordinaten weitergeben
                return o;
            }

            // Fragment-Shader
            half4 frag(v2f i) : SV_Target
            {
                // Textur abtasten
                half4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
