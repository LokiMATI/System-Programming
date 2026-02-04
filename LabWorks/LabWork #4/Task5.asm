%include "io64.inc"

section .text
global main
main:
    mov rbp, rsp; for correct debugging
    
start_input:
    PRINT_STRING 'Input integers count: '
    GET_DEC 8, rcx
    NEWLINE
    CMP rcx, 0
    JL start_input
    
    MOV rax, 0
mainloop:
    GET_DEC 8, rdx
    ;NEWLINE
    ADD rax, rdx
    
    loop mainloop

    PRINT_STRING 'Result: '
    PRINT_DEC 8, rax 
    
    ret