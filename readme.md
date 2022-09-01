![Build\Test](https://github.com/IceWind2/AlgebraicExpressionCompiler/actions/workflows/dotnet.yml/badge.svg)
# Algebraic expression compiler
AECompiler is a CLI application that generates assembly file which evaluates input expression.
## Requirements
* .NET core 3.0+
* GNU assembler

## Installation
Clone this repository
```sh
git clone https://github.com/IceWind2/AlgebraicExpressionCompiler.git
```
Build with dotnet
```sh
dotnet build
```

## Usage
Use application to create an assembly file. Pass the expression as argument.
```sh
./AECompiler.CLI "1 + 2"
```
Compile assembly file with GNU assembler.
```sh
gcc -nostartfiles -no-pie output.S -o output
```
Run executable.
```sh
./output
```

## !Warning!
Current version have following limitations:
* Number size: 16 bit (-2^15 ... 2^15 - 1)
* Usable operations: +, -, *
