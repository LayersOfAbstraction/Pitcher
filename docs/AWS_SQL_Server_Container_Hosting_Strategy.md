---
layout: post
title:  "My AWS SQL Server Container Hosting Strategy for 2023"
date: "2023-09-27"
published: false
---

# My AWS SQL Server Container Hosting Strategy For 2023.

## Introduction 

I wanted to use the AWS Elastic Container Registry as I thought I could use my container from there during development in the cloud and deploy later.Strategy

I need to migrate AWS. Why? I want to get out of my apartment to use this container so I can develop on my MacBook Air by day and develop on my home computer in the evening or if it was raining. So I want to develop on more than one machine without having to have 2 different containers on two different computers makes sense?

I want to centralize development in one place. Currently the docker image containing the database sits on my home computer.

## Initializing development

Here's some advise the Bing AI gave me in terms of having a good developer workflow:

> 1. Here’s a simple workflow:
> 
> 2. Develop Locally: Develop your application on your MacBook Air and containerize it using Docker.
> 
> 3. Build Docker Image: Build a Docker image of your application.
> 
> 4. Push to Registry: AWS ECR, or any other Docker-compatible registry.

Here's the rest upon further prompting

> 5. Deploy on ECS: Use Amazon Elastic Container Service (ECS) to deploy your application from the Docker image stored in ECR in your environment.

So I realized I could have 2 images. One for development and one for production.

So in a nutshell the both the development and production workflows could look roughly something like this. 

![AWS development environment diagram](docs/../images/AWS_diagram_for_initial_development.png)

So we have solved setting up the initial development environment to host our container. The problem this does solve however is data persistance. AWS has [a great blog](https://docs.aws.amazon.com/AmazonECS/latest/bestpracticesguide/storage.html) about how it works with containers. 

In a nutshell we cannot persist data into the container as containers have their own embedded writeable layer which gets destroyed when the container is terminated. 

Which means we cannot save the container's data inside ECS. We have to store the container's data outside of it elsewhere in another service. We may need this in case we wish to update the database schema and preserve it's data.

## Persisting data to storage in AWS

You only need to then worry about how to manage the updated database schema changes pulled from ECS. We have several options available now. 

1. We could use a third party cloud storage provider.
2. We could use Amazon EFS (Elastic File System)
3. Amazon EBS (Elastic Block Store).
4. FSx for Windows File Server. 

