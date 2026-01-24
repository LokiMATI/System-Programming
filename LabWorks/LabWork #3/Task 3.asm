%include "io.inc"

section .data
x: dd 0
y: dd 0
z: dd 0


section .text
global main
main:
    mov ebp, esp; for correct debugging

    PRINT_STRING 'Input first value: '
    GET_DEC 4, x
    NEWLINE
    
    PRINT_STRING 'Input second value: '
    GET_DEC 4, y
    NEWLINE
    
    PRINT_STRING 'Input third value: '
    GET_DEC 4, z
    NEWLINE
    
    mov eax, [x]
    mov ebx, [y]
    
    CMP eax, ebx
    JGE second_check
        MOV eax, ebx
    
    second_check:
    MOV ebx, [z]
    CMP eax, ebx
    JGE end_if
        MOV eax, ebx
    end_if:
    PRINT_DEC 4, eax
    xor eax, eax
    ret