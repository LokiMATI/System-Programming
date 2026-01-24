%include "io.inc"

section .data
x: dd 0
y: dd 0


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
    
    mov eax, [x]
    mov ebx, [y]
    CMP eax, ebx
    JNE else_begin
        PRINT_STRING 'equal'
    JMP end_if
    else_begin:
    PRINT_STRING 'not equal'
    end_if:
    ;write your code here
    xor eax, eax
    ret