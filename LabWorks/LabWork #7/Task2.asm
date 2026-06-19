%include "io.inc"

section .text
global main
main:
    mov ebp, esp

    sub esp, 16

    GET_DEC 4, [esp + 12]
    GET_DEC 4, [esp + 8]
    GET_DEC 4, [esp + 4]
    GET_DEC 4, [esp]

    push dword [esp]
    push dword [esp + 8]
    push dword [esp + 16]
    push dword [esp + 24]

    call calc_expression
    add esp, 16

    PRINT_DEC 4, eax
    NEWLINE

    add esp, 16
    xor eax, eax
    ret

calc_expression:
    push ebp
    mov ebp, esp

    push ebx
    push ecx

    mov eax, [ebp + 8]
    add eax, [ebp + 12]

    mov ebx, [ebp + 16]
    sub ebx, [ebp + 20]

    imul ebx

    pop ecx
    pop ebx

    mov esp, ebp
    pop ebp
    ret
