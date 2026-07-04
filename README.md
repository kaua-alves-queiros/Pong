# Pong - MonoGame

Este é um clássico jogo **Pong** desenvolvido em C# utilizando o framework **MonoGame**.

O projeto está sendo utilizado como uma ferramenta prática para o aprendizado do desenvolvimento de jogos com MonoGame, aplicando conceitos básicos de game loops, renderização de sprites em memória (2D), detecção de colisão física e lógica de inteligência artificial simples para o oponente.

## 🚀 Funcionalidades

- **Movimentação do Jogador**: Controle da raquete usando o teclado (`W` para subir e `S` para descer).
- **Inteligência Artificial (IA)**: O oponente persegue a bola automaticamente com uma velocidade ligeiramente reduzida para manter o jogo equilibrado.
- **Aumento Gradual de Dificuldade**: A velocidade da bola aumenta a cada rebatida nas raquetes.
- **Placar**: Contador de pontuação para o Jogador e a IA.
- **Gráficos Dinâmicos**: Texturas desenhadas programaticamente em memória, dispensando o uso de arquivos externos de imagem.

## 🛠️ Tecnologias Utilizadas

- **Linguagem**: C#
- **Framework**: MonoGame (XNA)
- **Plataforma**: Cross-platform (.NET Core)

## 🎮 Como Executar o Jogo

1. Certifique-se de ter o [.NET SDK](https://dotnet.microsoft.com/download) instalado.
2. Clone o repositório ou navegue até o diretório do projeto:
   ```bash
   cd Pong
   ```
3. Execute o comando para compilar e iniciar o jogo:
   ```bash
   dotnet run
   ```

## 🕹️ Controles

- **W**: Mover raquete esquerda para cima
- **S**: Mover raquete esquerda para baixo
- **Esc**: Fechar o jogo
