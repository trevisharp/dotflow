# sharprocessing
Uma biblioteca de processamento digital de imagens para C# em construção.

## Features

A seguir as features demonstradas sobre essa imagem de exemplo:
![paingaming.jpg](imgs/paingaming.jpg)

### Processamento Pixel-a-Pixel

```
using Sharp.Image;
using Sharp.Image.Processing;

Picture pic = Picture
    .New("paingaming.jpg")
    .ForPixel((r, g, b) => r > 200 ? (r, g, b) : (255, 255, 255));

pic.Save("redtreshold.jpg");
```
![redtreshold.jpg](imgs/redtreshold.jpg)

```
using Sharp.Image;
using Sharp.Image.Processing;

Picture pic = Picture
    .New("paingaming.jpg")
    .ForPixel(p => p > 155 ? 255 : 0); //Grayscale automático

pic.Save("gstreshold.jpg");
```
![gstreshold.jpg](imgs/gstreshold.jpg)

## TODO

 - Tratar exceções adequadamente
 - Acrescentar documentação
 - Add README.md in english language