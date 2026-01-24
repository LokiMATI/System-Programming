%include "io.inc"

section .data
value: dd 0


section .text
global main
main:
    mov ebp, esp; for correct debugging

    PRINT_STRING 'Input value: '
    GET_DEC 4, value
    NEWLINE
    
    mov eax, [value]
    CMP eax, 4
    JL else_begin
        CMP eax, 14
        JG else_begin
        PRINT_STRING 'within range (4..14)'
    JMP end_if
    else_begin:
    PRINT_STRING 'out of range'
    end_if:
    ;write your code here
    xor eax, eax
    ret