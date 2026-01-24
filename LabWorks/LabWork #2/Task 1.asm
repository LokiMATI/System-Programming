%include "io.inc"

section .data
a: dd 0
b: dd 0

section .text
global main
main:
    mov ebp, esp
    PRINT_STRING 'Input a: '
    GET_DEC 4, a
    NEWLINE
    
    PRINT_STRING 'Input b: '
    GET_DEC 4, b
    NEWLINE
    
    mov eax, [a]
    mov ebx, [b]
    xor edx, edx
    div ebx
    
    
    PRINT_STRING 'Output quotient: '
    PRINT_DEC 4, eax
    NEWLINE
    
    PRINT_STRING 'Output quotient: '
    PRINT_DEC 4, edx
    NEWLINE
    ret