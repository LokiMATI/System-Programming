section .text
global main
main:
    mov ax, -20
    mov cx, 10
    cwd;xor dx, dx
    idiv cx
    ret