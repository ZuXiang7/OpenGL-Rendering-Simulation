/* Start Header ------------------------------------------------------
Copyright (C) 2020 DigiPen Institute of Technology.
File Name: main.vs
Purpose: This file contains the implementation
for computing the view and light direction and other vertex information.
If it uses normal mapping, it will calculate a TBN matrix and transform
the view and light direction into the TBN space. If it's not
normal mapping, it will stop at view space. Either way, it 
will transform normal to view space because normal mapping uses
normal map for normal and parallax mapping.
Language: C++ Visual Studio 2019 version 16.0.0
Platform: Microsoft Compiler Full Version 192027508, Windows 10
Project: zuxiang.seah_CS300_4
Author: Seah Zu Xiang, zuxiang.seah 390004318
Creation date: 5th August 2020
End Header -------------------------------------------------------*/
#version 330 core

/*  These vertex attributes are in model space */
layout (location = 0) in vec3 pos;
layout (location = 1) in vec3 nrm;
layout (location = 2) in vec3 tan;
layout (location = 3) in vec3 bitan;
layout (location = 4) in vec2 uv;

uniform mat4 mvMat;     /*  model-view matrix for positions */
uniform mat4 nmvMat;    /*  model-view matrix for normals */
uniform mat4 projMat;   /*  projection matrix */

uniform bool lightOn;           /*  whether lighting should be applied */
uniform int  numLights;
uniform vec3 lightPosVF[10];    /*  light pos already in view frame */

uniform bool normalMappingOn;   /*  whether normal mapping should be applied */


out vec2 uvCoord;

/*  Output vectors:
    - If normalMapping is on then these vectors are in tangent space.
    - Otherwise they are in view space
*/
out vec3 lightDir[10];
out vec3 viewDir;
out vec3 normal;


void main(void) 
{
    vec4 posVF = mvMat * vec4(pos, 1.0);

    /*    For object transformation */
    gl_Position = projMat * posVF;

    /*  For object texturing */
    uvCoord = uv;


    if (lightOn)
    {
        normal = normalize(mat3(nmvMat) * nrm);
        viewDir = -posVF.xyz;
        for (int i = 0; i < numLights; ++i)
        {
            lightDir[i] = lightPosVF[i] - vec3(posVF);
        }

        if (normalMappingOn)    
        {
            vec3 T = normalize(vec3(mvMat * vec4(tan,   0.0)));
            vec3 B = normalize(vec3(mvMat * vec4(bitan, 0.0)));
            vec3 N = normal;
            mat3 TBN = transpose(mat3(T, B, N));

            viewDir = TBN * viewDir;
            for (int i = 0; i < numLights; ++i)
            {
                lightDir[i] = TBN * lightDir[i];
            }


        }
        
    }
}