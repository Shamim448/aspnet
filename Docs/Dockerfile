FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
MAINTAINER Shamim

ARG DEBIAN_FRONTEND=noneinteractive
RUN apt-get update 
RUN apt-get install -y apache2

WORKDIR /var
EXPOSE 80
CMD apachectl -D FOREGROUND