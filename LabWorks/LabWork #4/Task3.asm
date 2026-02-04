%include "io.inc"

section .text
global main
main:
    mov ebp, esp; for correct debugging
    
    PRINT_STRING 'Input random value: '
    GET_UDEC 4, ebx
    NEWLINE
    
    start_do_while:
    NEWLINE
    PRINT_STRING 'Input value: '
    GET_UDEC 4, eax
    NEWLINE
    
    CMP eax, ebx
    JE end_do_while
    JA more_then
        PRINT_STRING 'You input a less value'
        JMP start_do_while
    more_then:
        PRINT_STRING 'You input a more value'
        JMP start_do_while
    end_do_while:
    
    PRINT_STRING 'You input a correct value! GOOD!'
    ret