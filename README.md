## Descripción

Este proyecto es un trabajo de finanzas para el curso de Finanzas e Ingeniería economica de la UPC

## Conexión a la base de datos

Apoyo del paquete NuGet Pomelo.EntityFrameworkCore.MySql

### Uso:

En la consola del administrador de paquetes escribir:

	Scaffold-DbContext "server=*servidor local o en linea*; port=*numero de puerto*; database=*nombre de la BD*; uid=*nombre del usuario*; password=*contraseña del usuario*;" Pomelo.EntityFrameworkCore.MySql -o Models

