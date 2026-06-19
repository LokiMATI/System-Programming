%include "io.inc"

section .text
global main
main:
    mov ebp, esp

    GET_DEC 4, eax          

    call double_value       

    PRINT_DEC 4, eax
    NEWLINE

    xor eax, eax
    ret

double_value:
    push ebp
    mov ebp, esp

    shl eax, 1

    mov esp, ebp
    pop ebp
    ret
