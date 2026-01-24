%include "io.inc"

section .data
a: dd 0
b: dd 0
c: dd 0
x: dd 0

first: dd 0
second: dd 0

section .text
global main
main:
    mov ebp, esp
    PRINT_STRING 'Input a: '
    GET_DEC 4, a
    NEWLINE
    
    PRINT_STRING 'Input b: '
    GET_DEC 4, b
    NEWLINE
    
    PRINT_STRING 'Input c: '
    GET_DEC 4, c
    NEWLINE
    
    PRINT_STRING 'Input x: '
    GET_DEC 4, x
    NEWLINE
    
    ;x^2
    mov eax, [x]
    mov ebx, [x]
    xor edx, edx
    imul ebx
    mov [second], eax
    
    ;a*x^2
    mov eax, [a]
    mov ebx, [second]
    xor edx, edx
    imul ebx
    mov [first], eax
    
    ;b*x
    mov eax, [b]
    mov ebx, [x]
    xor edx, edx
    imul ebx
    mov [second], eax
    
    ;y
    mov eax, [first]
    mov ebx, [second]
    xor edx, edx
    add eax, ebx
    mov ebx, [c]
    add eax, ebx
    
    PRINT_STRING 'Result: '
    PRINT_DEC 4, eax
    NEWLINE
    ret