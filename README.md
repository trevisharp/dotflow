# sharprocessing
Uma biblioteca de processamento digital de imagens para C# em construção.

## Features


### Processamento Pixel-a-Pixel

```
using Sharp.Image;
using Sharp.Image.Processing;

Picture pic = Picture
    .New("paingaming.jpg")
    .ForPixel((r, g, b) => r > 200 ? (r, g, b) : (255, 255, 255));

pic.Save("redtreshhold.jpg");
```
![paingaming.jpg](imgs/paingaming.jpg)
![redtreshhold.jpg](imgs/redtreshhold.jpg)

## TODO

 - Tratar exceções adequadamente
 - Acrescentar documentação
 - Add README.md in english language