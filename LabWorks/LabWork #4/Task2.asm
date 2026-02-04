%include "io.inc"

section .text
global main
main:
    
    PRINT_STRING 'Input value: '
    GET_UDEC 4, ecx
    NEWLINE
    
    MOV ebx, 1
    
    start_while:
    CMP ecx, ebx
    JB end_while
        MOV eax, ebx
        MUL eax
        CMP eax, ecx
        JG end_while
            PRINT_UDEC 4, eax
            INC ebx
            NEWLINE
            JMP start_while
    end_while:
    ret