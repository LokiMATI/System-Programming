%include "io.inc"

section .data
x: dd 0
a: dd 0


section .text
global main
main:
    mov ebp, esp; for correct debugging

    PRINT_STRING 'Input x: '
    GET_DEC 4, x
    NEWLINE
    
    PRINT_STRING 'Input a: '
    GET_DEC 4, a
    NEWLINE
    
    mov eax, [x]
    CMP eax, -10
    JGE second_check
        IMUL eax, eax
        IMUL eax, [a]
    JMP end_if
    second_check:
    CMP eax, 10
    JGE third_operation
        cdq
        xor eax, edx
        sub eax, edx
        MOV ebx, [a]
        IMUL eax, ebx
    JMP end_if
    third_operation:
        MOV ebx, [a]
        SUB ebx, eax
        MOV eax, ebx
    end_if:
    PRINT_DEC 4, eax
    xor eax, eax
    ret