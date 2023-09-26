---
layout: post
title:  "AWS_SQL_Server_CONTAINER_HOSTING_STRATEGY"
date: "2023-09-27"
published: false
---

# AWS SQL Server container hosting and deployment strategy.

## Introduction 

I wanted to use the AWS Elastic Container Registry as I thought I could use my container from there during development in the cloud and deploy later.Strategy

I need to migrate AWS. Why? I want to get out of my apartment to use this container so I can develop on my MacBook Air by day and develop on my home computer in the evening or if it was raining. So I want to develop on more than one machine without having to have 2 different containers on two different computers makes sense?

I want to centralize development in one place. Currently the docker image containing the database sits on my home computer.

## Initializing development

Here's some advise the Bing AI gave me:

> 1. Here’s a simple workflow:
> 
> 2. Develop Locally: Develop your application on your MacBook Air and containerize it using Docker.
> 
> 3. Build Docker Image: Build a Docker image of your application.
> 
> 4. Push to Registry: Push this Docker image to a Docker registry. This could be Docker Hub, AWS ECR, or any other Docker-compatible registry.


