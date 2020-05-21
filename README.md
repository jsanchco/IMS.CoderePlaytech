# Previo (Instalacion de SQL Server Express)

-  Instalar **SQL Server Express**
 - Vistar esta opagina para configurar **SQL Server Express**: https://www.dundas.com/support/learning/documentation/installation/how-to-enable-sql-server-authentication
 - Crear un **usuario**: user-> admin password-> admin
 - Luego **Restart SQL**
 - Modificar en Security->Logins->admin [entrar en admin y en ServerRoles añadir el check de sysadmin]
 - **Restart SQL**
 
# Migraciones

 - Establecer como **proyecto de inicio en donde tengamos** instanciado IMS.CoderePlaytech.Domain (cadena de conexión). En mi caso **IMS.CoderePlaytech.WebApi**
- En **'Consola del Administrador de Paquetes'** establecer como **'Proyecto predeterminado'** en donde tengamos instanciados los repositorios. En nuestro caso **'IMS.CoderePlaytech.Infrastructure'**
- Ejecutamos en **'Consola del Administrador de Paquetes'**  -> '***add-migration First_Migration -Context IMS.CoderePlaytech.Infrastructure.EFContextSQL'***
- Se creará en **'IMS.CoderePlaytech.Infrastructure'** una carpeta en donde se estará la migración
- Establecemos el entorno en el que se va a hacer la migración (en 'Consola del Administrador de Paquetes'): $env:ASPNETCORE_ENVIRONMENT='Development'  
	                                        ó
$env:ASPNETCORE_ENVIRONMENT='Production'
- Construimos la base de datos en donde haya indicado la cadena de conexión ejecutando en 'Consola del Administrador de Paquetes' ***'Update-Database -Context IMS.CoderePlaytech.Infrastructure.EFContextSQL'***
- Si queremos eliminar la Migración, ejecutamos en  'Consola del Administrador de Paquetes'  ***'remove-migration -Context SGI.DataEFCoreMySQL.EFContextMySQL'***
