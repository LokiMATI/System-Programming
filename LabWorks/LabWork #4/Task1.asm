%include "io.inc"

section .text
global main
main:
    mov ebp, esp; for correct debugging
    ;write your code here
    xor eax, eax
    
    PRINT_STRING 'Input string count: '
    GET_UDEC 4, eax
    NEWLINE
    
    MOV ebx, 0
    
    start_while:
    CMP eax, ebx
    JB end_while
        MOV ecx, 0
        start_inner_while:
        CMP ecx, ebx
        JAE end_inner_while
            PRINT_STRING '#'
            INC ecx
            JMP start_inner_while  
        end_inner_while:
        INC ebx
        NEWLINE
        JMP start_while
    end_while:
    ret