# TFI_Genotec
Repositorio del proyecto de la materia Trabajo Final de Ingeniería de la Universidad Abierta Interamericana.

Esta aplicación web se desarrolló en Blazor y también incluye una Web API desarrollada en .Net
Tiene integración con base de datos SQL y Firebase para el manejo de datos.
Está integrado a Auth0 para el manejo de Autentificación y Autorización.

La aplicación brinda una solución a las organozaciones y empresas que debe gestionar ensayos clínicos, pudiendo getionar información de los ensayos, los pacientes relacionados a los mismos y los datos de los pacientes.

Además, mediante la Web API se integra con otra solución que usa el paciente para registrar Saturación de Oxígeno (SPO2) en sangre y Latidos por Minuto (LPM).
Esta información se captura desde un dispositivo IoT armado con un Arduino y mediante una aplicación de escritorio que se conecta por Bluetooth es enviado a la Web API y finalmente los datos son almacenados en la base SQL.

Toda la información se puede ver y getionar desde la aplicación web.
