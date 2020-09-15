/* Start Header ------------------------------------------------------
Copyright (C) 2020 DigiPen Institute of Technology.
File Name: sphere.vs
Purpose: This file contains the implementation 
for finding the texture coordinate of the vertex to print 
the background.
Platform: Microsoft Compiler Full Version 192027508, Windows 10
Project: zuxiang.seah_CS300_5
Author: Seah Zu Xiang, zuxiang.seah 390004318
Creation date: 21st August 2020
End Header -------------------------------------------------------*/
#version 330 core

uniform mat4 viewMat;

out vec3 texCoord;

void main(void)
{

        //4 back corners of the 2x2x2 box symmetric about the world origin
        vec2[4] vertices = vec2[4](
            vec2(-1.0, -1.0), 
            vec2(1.0, -1.0),
            vec2(-1.0, 1.0),
            vec2(1.0, 1.0)
        );
        //get texture coordinate and postion based on current vertex
        texCoord = transpose(mat3(viewMat)) * vec3(vertices[gl_VertexID], -1.0);
        gl_Position = vec4(vertices[gl_VertexID], 1.0, 1.0);
}