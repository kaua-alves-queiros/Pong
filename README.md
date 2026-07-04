# Pong - MonoGame

Este é um clássico jogo **Pong** desenvolvido em C# utilizando o framework **MonoGame**.

O projeto está sendo utilizado como uma ferramenta prática para o aprendizado do desenvolvimento de jogos com MonoGame, estruturado conforme as melhores práticas de arquitetura da indústria (separação de conceitos por telas, gerenciador de estados de entrada e entidades dedicadas).

## 🚀 Funcionalidades

- **Menu Principal**: Tela inicial dinâmica com opções interativas de iniciar jogo ou sair.
- **Persistência de Recorde**: Carrega e salva automaticamente a pontuação mais alta (High Score) diretamente no disco do usuário.
- **Movimentação do Jogador**: Controle da raquete usando o teclado (`W` e `S`).
- **Inteligência Artificial (IA)**: Raquete adversária persegue a bola automaticamente de forma suavizada para manter o desafio equilibrado.
- **Aumento Gradual de Dificuldade**: A velocidade da bola aumenta ligeiramente a cada rebatida nas raquetes.
- **Placar**: Contador de pontuação para o Jogador e a IA renderizado via fonte pixel-art dinâmica.

## 📁 Estrutura de Arquivos (Padrão da Indústria)

```
Pong/
├── Core/
│   ├── GameSettings.cs   # Variáveis globais de tela e diretórios do jogo
│   ├── InputManager.cs   # Detecção e controle refinado de cliques de tecla
│   └── SaveManager.cs    # Salvamento e persistência de recorde no disco
├── Entities/
│   ├── Paddle.cs         # Modelo e renderização das raquetes
│   └── Ball.cs           # Lógica física, velocidade e colisão da bola
├── Rendering/
│   └── PixelFontRenderer.cs # Renderizador de texto pixel-art em tempo de execução
├── Screens/
│   ├── IScreen.cs        # Interface padrão de ciclo de vida de telas
│   ├── ScreenManager.cs   # Gerenciador de transições de telas
│   ├── MainMenuScreen.cs # Tela de menu interativo
│   └── GameplayScreen.cs # Tela de gameplay (Loop principal de partida)
├── Game1.cs              # Inicializador e centralizador de fluxos do MonoGame
└── Program.cs            # Ponto de entrada do sistema (.NET Executable)
```

## 🛠️ Tecnologias Utilizadas

- **Linguagem**: C#
- **Framework**: MonoGame (DesktopGL)
- **Plataforma**: Cross-platform (.NET 9.0)

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

### No Menu:
- **W / S** ou **Setas (Cima / Baixo)**: Navegar entre as opções.
- **Enter** ou **Espaço**: Confirmar seleção.

### Na Partida:
- **W**: Mover raquete esquerda para cima.
- **S**: Mover raquete esquerda para baixo.
- **Esc**: Voltar para o Menu Principal.
