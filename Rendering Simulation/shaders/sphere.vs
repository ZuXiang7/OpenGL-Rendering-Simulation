/* Start Header ------------------------------------------------------
Copyright (C) 2020 DigiPen Institute of Technology.
File Name: sphere.vs
Purpose: This file contains the implementation 
for calculating the view, normal and position of the considered vertex.
Platform: Microsoft Compiler Full Version 192027508, Windows 10
Project: zuxiang.seah_CS300_5
Author: Seah Zu Xiang, zuxiang.seah 390004318
Creation date: 21st August 2020
End Header -------------------------------------------------------*/
#version 330 core

layout (location = 0) in vec3 pos;
layout (location = 1) in vec3 nrm;
/*  For the sphere, we don't need other vertex attributes */

uniform mat4 mvMat;
uniform mat4 nmvMat;
uniform mat4 projMat;

out vec3 view, normal;

void main(void)
{

    normal = mat3(nmvMat) * nrm;
    //view is vertex position
    view = (mvMat * vec4(pos, 1.0)).xyz;
    gl_Position = projMat * vec4(view, 1.0);
}