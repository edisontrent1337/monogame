#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

matrix WorldViewProjection;
float DistanceToCamera;
float4 CameraPosition;
float4 OldPosition;
matrix World;
matrix View;
matrix Projection;
float4 AmbientColor = float4(1, 1, 1, 1);
float AmbientIntensity = 0.6; // 0.6

float4x4 WorldInverseTranspose;

float Shininess = 100;
float4 SpecularColor = float4(1, 1, 1, 1);
float SpecularIntensity = 1;
float3 ViewVector = float3(1, 0, 0);

float3 DiffuseLightDirection = float3(1, 0, 0);
float4 DiffuseColor = float4(1, 1, 1, 1);
float DiffuseIntensity = 2; // 2

struct VertexShaderInput
{
	float4 Position : SV_POSITION;
	float4 Normal : NORMAL0;
	float4 Color : COLOR0;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float4 DefaultColor : COLOR1;
	float4 WorldPosition : TEXCOORD0;
	float4 Normal : NORMAL0;
};

VertexShaderOutput MainVS(in VertexShaderInput input)
{
	VertexShaderOutput output = (VertexShaderOutput)0;
	float4 worldPosition = mul(input.Position, World);
	//OldPosition = input.Position;
	float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);
	//output.Color = input.Color;
	output.WorldPosition = worldPosition;

	float4 normal = mul(input.Normal, WorldInverseTranspose);
	float lightIntensity = dot(normal, DiffuseLightDirection);
	output.Color = saturate(DiffuseColor * DiffuseIntensity * lightIntensity);
	//output.Color *= input.Color;
	output.DefaultColor = input.Color;
	output.Normal = normal;
	return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
	DistanceToCamera = length(CameraPosition - input.WorldPosition);
	float alpha = clamp(DistanceToCamera/10,0,1);

	float3 light = normalize(DiffuseLightDirection);
	float3 normal = normalize(input.Normal);
	float3 r = normalize(2 * dot(light, normal) * normal - light);
	float3 v = normalize(mul(normalize(ViewVector), World));

	float dotProduct = dot(r, v);
	float4 specular = SpecularIntensity * SpecularColor * max(pow(dotProduct, Shininess), 0) * length(input.Color);

	input.Color = saturate(input.Color + AmbientColor * AmbientIntensity);
	//input.Color = saturate(input.Color + AmbientColor * AmbientIntensity);
	input.Color *= input.DefaultColor;
	input.Color.a = 1 - alpha;

	return input.Color;
}

technique BasicColorDrawing
{
	pass P0
	{
		AlphaBlendEnable = TRUE;
		DestBlend = INVSRCALPHA;
		SrcBlend = SRCALPHA;
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};