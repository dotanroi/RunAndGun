Shader "Skybox/Horizon With Sun Skybox"
{
    Properties
    {
        _TopColor("Top Color", Color) = (0.37, 0.52, 0.73, 0)
        _HorizonColor("Horizon Color", Color) = (0.89, 0.96, 1, 0)
        _BottomColor("Bottom Color", Color) = (0.89, 0.89, 0.89, 0)
        
        _SkyExponent("Exponent", Float) = 8.5

        _SkyIntensity("Sky Intensity", Float) = 1.0

        _SunColor("Sun Color", Color) = (1, 1, 0.9, 1)
        _SunIntensity("Sun Intensity", float) = 2.0

        _SunSize ("Sun Size", Range (0, 1500)) = 750
    }

    CGINCLUDE

    #include "UnityCG.cginc"

    struct appdata
    {
        float4 position : POSITION;
        float3 texcoord : TEXCOORD0;
    };
    
    struct v2f
    {
        float4 position : SV_POSITION;
        float3 texcoord : TEXCOORD0;
    };
    
    half3 _TopColor;
    half3 _HorizonColor;
    half3 _BottomColor;
    
    half _SkyExponent;
    half _SkyIntensity;

    half3 _SunColor;
    half _SunIntensity;

    half _SunSize;
    
    v2f vert(appdata v)
    {
        v2f o;
        o.position = mul(UNITY_MATRIX_MVP, v.position);
        o.texcoord = v.texcoord;
        return o;
    }
    
    half4 frag(v2f i) : COLOR
    {
        float3 v = normalize(i.texcoord);

        float p = v.y;
        float p1 = 1 - pow(min(1, 1 - p), _SkyExponent);
        float p3 = 1 - pow(min(1, 1 + p), 5);
        float p2 = 1 - p1 - p3;

        //half3 c_sky = _TopColor * p1 + _HorizonColor * p2 + half3(0.25,0.25,0.50) * p3;
        half3 c_sky = _TopColor * p1 + _HorizonColor * p2 + half3(0.25,0.25,0.50) * p3;
        half3 c_sun = _SunColor * min(pow(max(0, dot(v, _WorldSpaceLightPos0.xyz)), (1500 - _SunSize)) * 5, 1);
		
        return half4(c_sky * _SkyIntensity + c_sun * _SunIntensity, 0);
    }

    ENDCG

    SubShader
    {
        Tags { "RenderType"="Skybox" "Queue"="Background" }
        Pass
        {
            ZWrite Off
            Cull Off
            Fog { Mode Off }
            CGPROGRAM
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
    }
}