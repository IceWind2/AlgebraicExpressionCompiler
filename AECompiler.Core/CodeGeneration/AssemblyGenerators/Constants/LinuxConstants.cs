namespace AECompiler.Core.CodeGeneration.AssemblyGenerators.Constants
{
    internal static class LinuxConstants
    {
        public static readonly string Header =
@".intel_syntax noprefix
.text
.global _start" + "\n\n";

        public static readonly string Data =
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
    "\n    minus: .string \"-\"";

        public static readonly string Output =
@"{0}:
    # handling negative numbers
    test    dx, dx              # Is positive?
    jns     .print_start        # Positive number
    neg     dx                  # 
    neg     ax                  #   Negate DX:AX
    sbb     dx, 0               # 
    push    ax                  # Save number
    push    dx
    mov     eax, 4              # 
    mov     ebx, 1              #  
    lea     ecx, [minus]        #   Print sign
    mov     edx, 1              #  
    int     0x80                # 
    pop     dx                  # Restore number
    pop     ax

.print_start:
    mov     bx,10               # CONST
    push    bx                  # Sentinel

.digit_split:
    mov     cx,ax               # Temporarily store LowDividend in CX
    mov     ax,dx               # First divide the HighDividend
    xor     dx,dx               # Setup for division DX:AX / BX
    div     bx                  # -> AX is HighQuotient, Remainder is re-used
    xchg    ax,cx               # Temporarily move it to CX restoring LowDividend
    div     bx                  # -> AX is LowQuotient, Remainder DX=[0,9]
    push    dx                  # Save remainder for now
    mov     dx,cx               # Build true 32-bit quotient in DX:AX
    or      cx,ax               # Is the true 32-bit quotient zero?
    jnz     .digit_split        # No, use as next dividend
    pop     dx                  # First pop

.print:
    add     edx, edx            # To calculate address
    mov     eax, 4              # 
    mov     ebx, 1              #  
    lea     ecx, [zero + edx]   #   Print digit  
    mov     edx, 1              #  
    int     0x80                # 
    pop     dx                  # All remaining pops
    mov     ebx, 10             # Restore sentinel
    cmp     dx,bx               # Was it the sentinel?
    jb      .print              # Not yet

    # new line
    mov eax, 4       # sys call
    mov ebx, 1       # file descriptor
    lea ecx, line    # message
    mov edx, 1       # message length
    int 0x80

    ret" + "\n\n";

        public static readonly string Start = "_start:\n";

        public static readonly string Store = "mov {0}, {1}\n";

        public static readonly string Add = "add {0}, {1}\n";

        public static readonly string Mul = "mul {0}\n";
        
        public static readonly string Div = "div {0}\n";

        public static readonly string Push = "push {0}\n";

        public static readonly string Pop = "pop {0}\n";
        
        public static readonly string Call = "call {0}\n";

        public static readonly string Exit =
@"mov eax, 1
mov ebx, 0
int 0x80" + "\n\n";
    }
}