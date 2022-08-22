namespace AECompiler.Core.CodeGeneration.AssemblyGenerators.Constants
{
    internal static class LinuxConstants
    {
        public const string Header =
@".intel_syntax noprefix
.text
.global _start" + "\n\n";

        public const string Data =
@".data
    zero: .word 0x30
    .word 0x31
    .word 0x32
    .word 0x33
    .word 0x34
    .word 0x35
    .word 0x36
    .word 0x37
    .word 0x38
    .word 0x39" + 
    "\n    line: .string \"\\n\"" +
    "\n    minus: .string \"-\"\n\n";

        public const string OutputFunc =
@"{0}:
    # handling negative numbers
    test    dx, dx
    jns     .print_start
    neg     dx
    neg     ax
    sbb     dx, 0
    push    ax
    push    dx
    mov     eax, 4
    mov     ebx, 1
    lea     ecx, [minus]
    mov     edx, 1
    int     0x80
    pop     dx
    pop     ax

.print_start:
    mov     bx,10
    push    bx

.digit_split:
    mov     cx,ax
    mov     ax,dx
    xor     dx,dx
    div     bx
    xchg    ax,cx
    div     bx
    push    dx
    mov     dx,cx
    or      cx,ax
    jnz     .digit_split
    pop     dx

.print:
    add     edx, edx
    mov     eax, 4
    mov     ebx, 1
    lea     ecx, [zero + edx]
    mov     edx, 1
    int     0x80
    pop     dx
    mov     ebx, 10
    cmp     dx,bx
    jb      .print

    # new line
    mov eax, 4
    mov ebx, 1
    lea ecx, line
    mov edx, 1
    int 0x80

    ret" + "\n\n";

        public const string OutputLabel = "output";

        public const string Start = "_start:\n";

        public const string Store = "mov {0}, {1}\n";

        public const string Add = "add {0}, {1}\n";

        public const string Neg = "neg {0}\n";

        public const string Mul = "imul {0}\n";
        
        public const string Div = "idiv {0}\n";

        public const string Push = "push {0}\n";

        public const string Pop = "pop {0}\n";
        
        public const string Call = "call {0}\n";

        public const string Exit =
@"    mov eax, 1
    mov ebx, 0
    int 0x80" + "\n\n";
    }
}