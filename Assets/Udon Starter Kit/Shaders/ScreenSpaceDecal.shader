Shader "LyumaShader/ScreenSpaceDecal"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_ScreenDecalTex("Screen Decal Tex", 2D) = "white" {}
		[Enum(ViewSpace,0,WorldSpace,1,ObjectSpace,2)] _UprightMode("Upright Mode", Int) = 0
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Off
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vert

		struct Input
		{
			float3 vertex; 
			float2 texcoord;
			float3 worldPos;
		};
		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT(Input,o);
 
			o.vertex = v.vertex.xyz;
			o.texcoord = v.texcoord.xy;
			o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
		}

		uniform sampler2D _ScreenDecalTex;
		uniform float4 _ScreenDecalTex_ST;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;

		uniform float _UprightMode;

		float4 modifiedComputeScreenPos(float4 pos) {
			float4 o = pos * 0.5f;
			#ifdef USING_STEREO_MATRICES
			o.xy /= unity_StereoScaleOffset[unity_StereoEyeIndex].xx;
			#endif
			o.x /= (_ScreenParams.y / _ScreenParams.x);
    #if UNITY_UV_STARTS_AT_TOP
			o.y *= -1;
    #endif
			o.zw = pos.zw;
			return o;
		}

		float2 CenterEyeScreenPos( float3 vertex )
		{
			#ifdef USING_STEREO_MATRICES
			uint oldIndex = unity_StereoEyeIndex;
			unity_StereoEyeIndex = 0;
			float4 leftClip = UnityObjectToClipPos(vertex);
			float2 leftUV = modifiedComputeScreenPos(leftClip).xy;
			unity_StereoEyeIndex = 1;
			float4 rightClip = UnityObjectToClipPos(vertex);
			float2 rightUV = modifiedComputeScreenPos(leftClip).xy;
			unity_StereoEyeIndex = oldIndex;
			return lerp(leftUV, rightUV, 0.5);
			#else
			float4 leftClip = UnityObjectToClipPos(vertex);
			return modifiedComputeScreenPos(leftClip).xy;
			#endif
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 baseScreenPos = CenterEyeScreenPos( float3(0,0,0) );
			float2 relativeScreenPos = CenterEyeScreenPos( mul( unity_WorldToObject, float4( i.worldPos , 1 ) ).xyz ) - baseScreenPos;
			float2 finalUV = ( ( relativeScreenPos * _ScreenDecalTex_ST.xy ) + _ScreenDecalTex_ST.zw );
			if (_UprightMode > 0.5) {
				float2 upvec = normalize(CenterEyeScreenPos( _UprightMode > 1.5 ? float3( 0,1,0 ) : mul( (float3x3)unity_WorldToObject, float3(0,1,0))) - baseScreenPos );
				finalUV = mul(float2x2(upvec.y, -upvec.x, upvec.x, upvec.y), finalUV);
			}
			finalUV = finalUV * 0.5 + 0.5;
			float4 tex2DNode2 = tex2D( _ScreenDecalTex, finalUV);
			float4 tex2DNode1 = tex2D( _MainTex, ( ( i.texcoord * _MainTex_ST.xy ) + _MainTex_ST.zw ) );
			o.Albedo = ( tex2DNode2 * tex2DNode1 ).rgb;
			o.Alpha = 1;
			
			// o.Albedo = lerp(tex2DNode2.rgb, ( tex2DNode2 * tex2DNode1 ).rgb, 0.9);
			// o.Alpha = tex2DNode1.a * tex2DNode2.a;
			// clip(o.Alpha - 0.1);
		}

		ENDCG
	}
	//Fallback "Diffuse"
//	CustomEditor "ASEMaterialInspector"
}
