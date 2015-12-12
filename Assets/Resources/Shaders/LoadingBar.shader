// Upgrade NOTE: replaced 'glstate.matrix.mvp' with 'UNITY_MATRIX_MVP'

Shader "Resources/Shaders/LoadingBar"
{
	Properties {
    _Color ("Color", Color) = (1,1,1,1)
    _MainTex ("Main Tex (RGBA)", 2D) = "white" {}
    _Progress ("Progress", Range(0.0,1.0)) = 0.0
	}
	 
	SubShader {
    	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
    	//ZTest Always - testing this to remove always ontop
    	Blend SrcAlpha OneMinusSrcAlpha
        Pass {
	 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			 
			uniform sampler2D _MainTex;
			uniform float4 _Color;
			uniform float _Progress;
			uniform float4 _MainTex_ST;
			 
			struct v2f {
			    float4 pos : POSITION;
			    float2 uv : TEXCOORD0;
			};
			 
			v2f vert (appdata_base v)
			{
			    v2f o;
			    o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			    o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);// TRANSFORM_UV(0);
			 
			    return o;
			}
			 
			half4 frag( v2f i ) : COLOR
			{
			    half4 color = tex2D( _MainTex, i.uv);
			    if(i.uv.x > (1.0 - _Progress))
			    {
			    	if(color.a != 0)
			    	{
			    		color.a = 1;
			    	}
			    }else
			    {
			    	color.a = 0;
			    }
			    
			    return tex2D(_MainTex, i.uv) * color * _Color;
			}
			 
			ENDCG
	    }
	}
}
