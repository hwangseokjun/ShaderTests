sampler2D main_tex : register(S0);
sampler2D noise_tex : register(S1);

float dissolve_amount;

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 color = tex2D(main_tex, uv);
	float noise_value = tex2D(noise_tex, uv).r;
	
	if (noise_value < dissolve_amount) {
		color.a = 0.0;
	}

	return color;
}
