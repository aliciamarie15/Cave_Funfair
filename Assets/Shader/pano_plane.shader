Shader "Custom/MultiPassPanoPlane"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}        // Haupttextur
        _Color("Color", Color) = (1,1,1,1)              // Basisfarbe
        _Amplitude("Amplitude", Float) = 0             // Stärke der Wellen
        _Frequency("Frequency", Float) = 0             // Frequenz der Wellen
        _OffsetSin("OffsetSin", Float) = 0             // Phasenverschiebung der Wellen
        _SecondaryTex("Secondary Texture", 2D) = "white" // Zusätzliche Textur für Pass 2
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        // **Pass 1: Wellenbewegung mit Haupttextur**
        Pass
        {
            Cull Back
            CGPROGRAM
            #pragma target 3.0
            #pragma vertex vert
            #pragma fragment frag

            // Eigenschaften
            sampler2D _MainTex;
            float4 _Color;
            float _Amplitude;
            float _Frequency;
            float _OffsetSin;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            // Vertex-Modifikation für Wellen
            v2f vert(appdata_t v)
            {
                v2f o;
                float wave = sin(_OffsetSin + v.vertex.z * _Frequency) * _Amplitude;
                v.vertex.y += wave; // Bewegung entlang der Y-Achse
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Fragment-Shader für Textur
            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.uv); // Haupttextur
                return texColor * _Color;               // Mit Farbe multiplizieren
            }
            ENDCG
        }

        // **Pass 2: Zusätzliche Textur oder Effekt**
        Pass
        {
            Cull Back
            CGPROGRAM
            #pragma target 3.0
            #pragma vertex vert
            #pragma fragment frag

            // Eigenschaften
            sampler2D _SecondaryTex;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            // Identische Vertex-Transformation wie Pass 1
            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Fragment-Shader für zusätzliche Textur
            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_SecondaryTex, i.uv); // Zusätzliche Textur
                return texColor; // Nur die zweite Textur anzeigen
            }
            ENDCG
        }
    }

    Fallback "Diffuse"
}
