%include "io.inc"

section .data
sum: dd 0
amount_deposited: dd 0


section .text
global main
main:
    mov ebp, esp; for correct debugging

    PRINT_STRING 'Input goods price sum: '
    GET_UDEC 4, sum
    NEWLINE
    
    PRINT_STRING 'Input amount deposited: '
    GET_UDEC 4, amount_deposited
    NEWLINE
    
    MOV eax, [sum]
    MOV ebx, [amount_deposited]
    
    CMP eax, ebx
    JNE second_check
        PRINT_STRING 'The payment was successful'
    JMP end_if
    second_check:
    CMP eax, ebx
    JA late_payment
        SUB ebx, eax
        PRINT_STRING 'Here is your change from the purchase: '
        PRINT_UDEC 4, ebx
    JMP end_if
    late_payment:
        SUB eax, ebx
        PRINT_STRING 'You owe more money in the amount of: '
        PRINT_UDEC 4, eax
    end_if:
    xor eax, eax
    ret