%include "io64.inc"

section .data
string: db "", 0

section .bss
substring resw 10

section .text
global main
main:
    mov rbp, rsp; for correct debugging
    PRINT_STRING "Input string: "
    GET_STRING string, 10
    NEWLINE
    
    lea edi, [string]
    xor ecx, ecx
    
    while:
    cmp byte [edi + ecx], 0
    je end_while
        inc ecx
        jmp while
    end_while: 
    
    PRINT_STRING "String len: "
    PRINT_UDEC 4, ecx
    
    xor rax, rax
    ret