# Zetris (Tetris com um Z)

**O código fonte deste projeto pode ser encontrado 
[neste repositório público](https://github.com/RodrigoPrinheiro/zetris76_bootleg_unity_v2.01.23)**

## Autoria

* ### [Rodrigo Pinheiro](https://github.com/RodrigoPrinheiro) 21802488

  * Lógica do jogo, colisões, rotação de peças, verificação de linhas.
  * Algumas mudanças na classe `GameLoop` (aceitar interface para adicionar
objetos) e criação da Interface `IGameObject`
  * Criação da doxyfile
  * Manutenção de código e resolução de _bugs_ no input.

* ### [Tomás Franco](https://github.com/ThomasFranque) 21803301
  
  * Engine para poder ser usado independentemente do jogo.
  * Sistema de scores.
  * Menus.
  
## Arquitetura da solução

### Descrição da solução
  * Neste projeto o grupo decidiu dividir o trabalho em 2 projetos, dentro
da mesma solução, diferentes. Um dos projetos `GameEngine` trata de conter
todas as classes correspondentes à _engine_, o outro projeto, `Zetris`, trata
de conter todas as classes e funcionamento correspondente com a recriação do
_Tetris_.

#### _Engine_

  * A componente do game engine foi feita de maneira a que não estivesse ligada de forma alguma ao eventual jogo criado, providenciando classes, estruturas e métodos para uso independente.
  * A classe `GameLoop` aceita _Delegates_ para serem chamados durante o _Update_, sendo assim independente do jogo.
  * A estrutura `Vector2` é usada para saber a posição de algo na consola.
  * `IGameObject` define o que é necessário um objeto conter para poder ser lido e usado no `GameLoop`.

#### _Zetris_

  * A recriação do tetris está basicamente condensada na classe `ZetrisBoard`
com várias instâncias de outras classes para compor o objeto final.
A base desta classe principal é um vetor de `bytes` que contém números de `0` a `9`
, `0` sendo uma célula vazia e `9` os limites do tabuleiro. A partir daqui fazemos
uso destes valores para tudo do jogo. As linhas são `8` e as peças são os números
de `1` a `7`.
  * A colisão das peças é então facilmente recriada na consola apenas por comparar
as células que se quer com a que se quer viajar para, sendo isto apenas possível
quando a célula desejada é `0`.
  * A rotação das peças é uma das razões pela qual usamos sempre vetores
de uma única dimensão com um pequeno algoritmo para usar os vetores como se fossem
bi-dimensionais.
    * Dado um vetor de tamanho W * W a posição unidimensional é 
dada da seguinte forma:
        ```csharp
        i = y * W + x
        ```
    * Agora para 90, 180 e 270 graus, a rotação é dada pelo algoritmo:
        ```csharp
        i = (W - 1) * 1 + y - (x * W)
        i = (W - 1) * 2 - (y * W) - x
        i = (W - 1) * 3 - y + (x * W)
        ```

#### Diagrama UML

![Diagrama]

#### Fluxograma

* Loop de jogo do _Tetris_

![Fluxograma]

[Diagrama]:Images/Diagrama.png
[Fluxograma]:Images/Fluxograma.png