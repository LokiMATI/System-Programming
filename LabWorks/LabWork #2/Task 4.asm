%include "io.inc"

section .text
global main
main:
    mov ebp, esp
    PRINT_STRING 'Input file size in bytes: '
    GET_DEC 4, eax
    NEWLINE
    
    shr eax, 10
    
    PRINT_STRING 'File size in kilobytes: '
    PRINT_DEC 4, eax
    ret