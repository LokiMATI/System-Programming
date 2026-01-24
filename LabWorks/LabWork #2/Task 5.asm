%include "io.inc"

section .data
r: db 0
g: db 0
b: db 0

section .text
global main
main:
    mov ebp, esp
    PRINT_STRING 'Input red: '
    GET_UDEC 1, r
    NEWLINE
    
    PRINT_STRING 'Input green: '
    GET_UDEC 1, g
    NEWLINE
    
    PRINT_STRING 'Input blue: '
    GET_UDEC 1, b
    NEWLINE
    
    XOR eax, eax
    MOVZX eax, byte[r]
    SHL eax, 8
    OR al, byte[g]
    SHL eax, 8
    OR al, byte[b]
    
    PRINT_STRING 'Result: 0x'
    PRINT_HEX 4, eax
    ret