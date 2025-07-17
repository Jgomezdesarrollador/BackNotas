# BackNotas
Pasos para el debido funcionamiento de la app

docker pull mcr.microsoft.com/mssql/server   /* Descarga de imagen Sql Server en docker

docker run --name sqlserveraudisoft -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=JGadmin123" -p 1433:1433 -d mcr.microsoft.com/mssql/server  /*Crea el contenedor de sql server con usuario sa password=JGadmin123 y se ejecuta en el puerto 1433

dotnet ef database update --project Infrastructure --startup-project API // Actualizar la BD
