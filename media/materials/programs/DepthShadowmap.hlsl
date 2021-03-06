// Shadow caster vertex program.
void casterVP(
	float4 pos   			: POSITION,
		
	out float2 outDepth		: TEXCOORD0,
	out float4 outPos		: POSITION,
  uniform float4 texelOffsets,
	uniform float4x4 worldViewProj,	
	uniform float4 depthRange
	)
{
	outPos = mul(worldViewProj, pos);

	// depth info for the fragment.
    outDepth.x = outPos.z;
    outDepth.y = outPos.w;
}


// Shadow caster fragment program for high-precision single-channel textures	
void casterFP(
	float2 depth			: TEXCOORD0,	
	out float4 result		: COLOR,
	uniform float3 pssmSplitPoints
	)
	
{
	
	float finalDepth = depth.x / depth.y;

	// just smear across all components 
	// therefore this one needs high individual channel precision
	result = float4(finalDepth, finalDepth, finalDepth, 1);
}





// Expand a range-compressed vector
float3 expand(float3 v)
{
	return (v - 0.5) * 2;
}



float shadowPCF(sampler2D shadowMap, float4 shadowMapPos, float2 offset)
{
		float adjust = 0.9975; // testing 
		shadowMapPos.z *= adjust;
		
		shadowMapPos = shadowMapPos / shadowMapPos.w;
		float2 uv = shadowMapPos.xy;
		float3 o = float3(offset, -offset.x) * 0.3f;
	   
		// Note: We using 2x2 PCF. Good enough and is alot faster.
		float c =       (shadowMapPos.z <= tex2D(shadowMap, uv.xy - o.xy).r) ? 0.25 : 0; // top left
		c +=            (shadowMapPos.z <= tex2D(shadowMap, uv.xy + o.xy).r) ? 0.25 : 0; // bottom right
		c +=            (shadowMapPos.z <= tex2D(shadowMap, uv.xy + o.zy).r) ? 0.25 : 0; // bottom left
		c +=            (shadowMapPos.z <= tex2D(shadowMap, uv.xy - o.zy).r) ? 0.25 : 0; // top right
		
	   // c += 2.5;
		  
		return (c);
}

/* Normal mapping plus depth shadowmapping receiver programs
*/
void normalMapShadowReceiverVp(
             float4 position    : POSITION,
			 float3 normal		: NORMAL,
			 float2 uv			: TEXCOORD0,
			 float3 tangent     : TANGENT0,
			 
			 // outputs
			 out float4 outPos    	 : POSITION,
			
			 out float4 outShadowUV	 : TEXCOORD0,
			 out float3 oUv	 		 : TEXCOORD1,
			 out float3 oTSLightDir  : TEXCOORD2,			 
			 out float oRenderMethod   : TEXCOORD3,
			
			 out float4 oLightPosition0   : TEXCOORD4,
			 out float4 oLightPosition1   : TEXCOORD5,
		//	 out float4 oLightPosition2   : TEXCOORD6,
			 out float4 outPosition 	  : TEXCOORD7,
			 // parameters
			 uniform float4 lightPosition, // object space
			 uniform float4x4 world,
			 uniform float4x4 worldViewProj,
			 uniform float4x4 texViewProj,
			 uniform float4 lightAttenuation,
			 
			 uniform float4x4 texWorldViewProjMatrix0,
			 uniform float4x4 texWorldViewProjMatrix1
			// uniform float4x4 texWorldViewProjMatrix2
			 
			 
			 )
{
  
  oRenderMethod = -1; // ALL (normal + shadow)
  float lightDist = distance(lightPosition, position);
  
  if(lightPosition.w > 0.5)
  { 
     // non-directional light ???
     if(lightDist > lightAttenuation[0]) // lightDist > attenuationDist
	 {
		// none
		oRenderMethod = 0; // nothing in this pass
	 } else
	 {
		oRenderMethod = (lightAttenuation[0] - lightDist) / lightAttenuation[0]; // no shadow but distance dependent + normal
	 }
     
  }
  
   
	float4 worldPos = mul(world, position);
	outPos = mul(worldViewProj, position);

	// calculate shadow map coords
	outShadowUV = mul(texViewProj, worldPos);

	// pass the main uvs straight through unchanged
	oUv = float3(uv, outPos.z);

	// calculate tangent space light vector
	// Get object space light direction
	// Non-normalised since we'll do that in the fragment program anyway
	float3 lightDir = lightPosition.xyz -  (position * lightPosition.w);

	// Calculate the binormal (NB we assume both normal and tangent are
	// already normalised)
	// NB looks like nvidia cross params are BACKWARDS to what you'd expect
	// this equates to NxT, not TxN
	float3 binormal = cross(tangent, normal);
	
	// Form a rotation matrix out of the vectors
	float3x3 rotation = float3x3(tangent, binormal, normal);
	
	// Transform the light vector according to this matrix
	oTSLightDir = mul(rotation, lightDir);
	
	
	oLightPosition0 = mul(texWorldViewProjMatrix0, position);
  oLightPosition1 = mul(texWorldViewProjMatrix1, position);
 // oLightPosition2 = mul(texWorldViewProjMatrix2, position);
    
    
	
}


void normalMapShadowReceiverFp(
			  float4 shadowUV	: TEXCOORD0,
			  float3 uv			: TEXCOORD1,
			  float3 TSlightDir : TEXCOORD2,			  
			  float renderMethod  : TEXCOORD3,
			 					  
			 //PSSM
			 float4 LightPosition0   : TEXCOORD4,
			 float4 LightPosition1   : TEXCOORD5,
			// float4 LightPosition2   : TEXCOORD6,
             float3  outPosition     : TEXCOORD7,
			  out float4 result	: COLOR,

			  uniform float4 lightColour,
			  uniform float fixedDepthBias,
					  
			  //PSSM
			  uniform float4 invShadowMapSize0,
			  uniform float4 invShadowMapSize1,
			 // uniform float4 invShadowMapSize2,
			  uniform float4 shadow_scene_depth_range,
			  uniform float3 pssmSplitPoints,
			  
			  //Standard		  
			 
			  //PSSM
			  uniform sampler2D shadowMap0 : register(s0),
			  uniform sampler2D shadowMap1 : register(s1),
			 // uniform sampler2D shadowMap2 : register(s2),   
			  
			 
			  uniform sampler2D   normalMap : register(s2),
			  uniform samplerCUBE normalCubeMap : register(s3))
{

 
    float3 lightVec;
	float3 bumpVec;
	float4 vertexColour;	
	
	
	//  oRenderMethod <  0  // ALL (normal + shadow)
    //  oRenderMethod == 0  // nothing in this pass
    //  oRenderMethod >  0  // no shadow but distance dependent  + normal
    if(renderMethod == 0)
    {
		result = float4(0,0,0,1);
	    return;
    }
  	
 	// retrieve normalised light vector, expand from range-compressed
	lightVec = expand(texCUBE(normalCubeMap, TSlightDir).xyz);

	// get bump map vector, again expand from range-compressed
	bumpVec = expand(tex2D(normalMap, uv).xyz);

  // Calculate dot product
    vertexColour = lightColour * dot(bumpVec, lightVec); 
		
	if(renderMethod > 0) // no shadow but distance dependent + normal
	{		
		result = renderMethod * vertexColour; // in this case renderMethod also holds the power [0-1]
		//result = float4(1,1,0,0);
		return;
	}
	
	// this leaves only "renderMethod < 0" which is full normal + shadow rendering
	
	//Shadowing
	float shadowing = 1.0f;
	float ScreenDepth = uv.z;
			
	// poza obszarem cienia	
	/*
	if ( ScreenDepth  >  pssmSplitPoints.z)
	{   
	    // result = float4(1,1,0,1);	
		 result = vertexColour;		
		 return; 
	}*/
	
	shadowUV /= shadowUV.w;
	
	 // shadowUV.z contains lightspace position of current object
	
	if(shadowUV.z > 1.0f) 
	{    
		result = vertexColour;
		return; // this is a fix for focused shadow camera setup
	}
	
  
	
	
		
	if (ScreenDepth <= pssmSplitPoints.y)
	{		
		shadowing = shadowPCF(shadowMap0, LightPosition0, invShadowMapSize0.xy );
		 //result = float4(1,0,0,1);	
		// return;
	}
	else if (ScreenDepth <= pssmSplitPoints.z)
	{		
	    shadowing = shadowPCF(shadowMap1, LightPosition1, invShadowMapSize1.xy );
	    // result = float4(0,1,0,1);	
	    // return;
	}
	
	
	result = float4(vertexColour.xyz * shadowing, 1);
	
	
}




void shadowReceiverFp(             
			  float4 shadowUV	: TEXCOORD0,
			  float3 uv			: TEXCOORD1,
			  float3 TSlightDir : TEXCOORD2,			  
			  float renderMethod  : TEXCOORD3,
			 					  
			 //PSSM
			 float4 LightPosition0   : TEXCOORD4,
			 float4 LightPosition1   : TEXCOORD5,		
             float3  outPosition     : TEXCOORD7,
			  out float4 result	: COLOR,

			  uniform float4 lightColour,
			  uniform float fixedDepthBias,
					  
			  //PSSM
			  uniform float4 invShadowMapSize0,
			  uniform float4 invShadowMapSize1,		
			  uniform float4 shadow_scene_depth_range,
			  uniform float3 pssmSplitPoints,
			  
			  //Standard		  
			 
			  //PSSM
			  uniform sampler2D shadowMap0 : register(s0),
			  uniform sampler2D shadowMap1 : register(s1)	
			  )		
			 
{

 
	float4 vertexColour;	
	
	
	//  oRenderMethod <  0  // ALL (normal + shadow)
    //  oRenderMethod == 0  // nothing in this pass
    //  oRenderMethod >  0  // no shadow but distance dependent  + normal
    if(renderMethod == 0)
    {
		result = float4(0,0,0,1);
	    return;
    }
  
  // Calculate dot product   
	//float3 N = normalize(normal);
	//float3 L = normalize(lightPosition - position.xyz);
	  
    vertexColour = lightColour; //* max(dot(N, L) , 0);  
		
	if(renderMethod > 0) // no shadow but distance dependent + normal
	{		
		result = renderMethod * vertexColour; // in this case renderMethod also holds the power [0-1]
		//result = float4(1,1,0,0);
		return;
	}
	
	// this leaves only "renderMethod < 0" which is full normal + shadow rendering
	
	//Shadowing
	float shadowing = 1.0f;
	float ScreenDepth = uv.z;
			
	shadowUV /= shadowUV.w;
	
    // shadowUV.z contains lightspace position of current object	
	if(shadowUV.z > 1.0f) 
	{    
		result = vertexColour;
		return; // this is a fix for focused shadow camera setup
	}
			
	if (ScreenDepth <= pssmSplitPoints.y)
	{		
		shadowing = shadowPCF(shadowMap0, LightPosition0, invShadowMapSize0.xy );	
	}
	else if (ScreenDepth <= pssmSplitPoints.z)
	{		
	    shadowing = shadowPCF(shadowMap1, LightPosition1, invShadowMapSize1.xy );	   
	}
	
	result = float4(vertexColour.xyz * shadowing, 1);
	
	
}

