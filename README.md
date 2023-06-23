# Proyecto Unidad Dos "Sin titulo"

Shooter desarrollado con UtalcaEngine2D_2023 con ligeras modificaciones

## Integrantes

Antonia Donoso y Hector Villalobos

## Objetivos completados

- [x] Su juego debe estar hecho en C#.
- [x] El juego debe ser en tiempo real (debe calcular al menos 30 cuadros por segundo).
- [x] El juego debe ocurrir en un nivel que ocupe al menos 3 pantallas, esté rodeado por murallas y la cámara debe seguir al jugador. Osea, es al menos 3 veces del tamaño del encuadre de la pantalla.
- [X] El jugador debe poder moverse de manera continua y disparar en al menos cuatro direcciones. Moviéndose con WASD y disparando con las flechas (por ejemplo). El jugador debe poder disparar una bala cada 0.3 segundos.
- [x] El juego debe tener pantalla de inicio y fin. Pantalla de inicio debe tener los nombres de los creadores. Todas las pantallas deben armarse usando el motor de videojuego, no pueden crear nuevos forms, ni usar mensajes.
- [x] Los enemigos se activan cuando el jugador está a 1.5 cámaras de distancia.
- [x] Su juego debe tener puntaje que se vea en pantalla, se suma un punto por cada enemigo que muere.

## Por cumplir

- [ ] Debe acompañar su juego con un Readme que explique los miembros del equipo, controles y lo que se logró implementar.
- [ ] Su juego debe tener un motor físico que maneja las colisiones a través de la clase Rigidbody. Los objetos visuales deben tener la misma posición del objeto físico.
- [ ] Su juego debe tener un motor que maneje objetos de la clase GameObject, que tienen un Rigidbody.
- [ ] Debe tener objetos que tienen Rigidbody que detectan cuando otro objeto entra y le hace daño si es la bala del otro bando.
- [ ] El juego debe tener tres tipos de personajes.
- [ ] personajes estáticos que disparan cada 1 segundo 3 balas en distintas direcciones.
- [ ] 1 tipo de personaje que se mueve de manera continua en una dirección hasta chocar con un muro y luego se devuelven, mientras dispara en dirección al jugador.
- [x] 1 tipo de personaje que se mueve 2 segundos hacia el jugador y luego 1.5 segundos se aleja del jugador y que dispara hacia la posición que tendría el jugador en 0.5 segundos si siguiera moviéndose derecho.
- [ ] El jugador debe tener al menos 3 puntos de vida, los enemigos pueden tener vida o morir de un golpe.
- [ ] Debe ralentizar el tiempo cuando mata a un enemigo 2 segundos.
