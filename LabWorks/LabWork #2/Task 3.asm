%include "io.inc"

section .data
integer: dd 0

section .text
global main
main:
    mov ebp, esp
    PRINT_STRING 'Input integer: '
    GET_DEC 4, integer
    NEWLINE
    
    mov eax, [integer]
    and eax, 1
    xor eax, 1
    
    PRINT_STRING 'integer is '
    PRINT_DEC 4, eax
    ret