%include "io64.inc"

section .data
len: dq 0
position: dq 0
string: db "String", 0

section .bss
substring resw 10

section .text
global main
main:
    mov rbp, rsp; for correct debugging
    PRINT_STRING "Input length: "
    GET_UDEC 8, len
    NEWLINE
    
    PRINT_STRING "Enter position: "
    GET_UDEC 8, position
    NEWLINE
    
    lea rsi, string
    add rsi, [position]
    lea rdi, substring
    mov rcx, [len]
    
    rep movsb
    
    PRINT_STRING [substring]
    
    xor rax, rax
    ret