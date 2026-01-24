section .data
    x dq 2.2
    y dq 3.3
    z dq 0.0

section .text
global main
main:
    mov ebp, esp
    finit
    fld qword [x]
    fld qword [y]
    fxch st1
    ;write your code here
    xor eax, eax
    ret