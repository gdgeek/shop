// Simplified Diffuse shader. Differences from regular Diffuse one:
// - no Main Color
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "Voxel/Gray" {
Properties {
}
SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 150

CGPROGRAM
#pragma surface surf Lambert noforwardadd



struct Input {
	float2 uv_MainTex;
	fixed3 color : COLOR;
	float4 vertex : POSITION; 
};

void surf (Input IN, inout SurfaceOutput o) {

	float c = IN.color.r*0.3 + IN.color.g *0.3 + IN.color.b*0.3;
	o.Albedo = fixed3(c,c,c); 
	//o.Albedo = IN.color;
	o.Alpha = 1;
	//o.	 = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
}
ENDCG
}

Fallback "Mobile/VertexLit"
}
