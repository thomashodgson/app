FROM mono:latest
ADD . /agiledb-web		  
EXPOSE 9000
ENTRYPOINT ["mono", "/agiledb-web/webserver.exe"]
