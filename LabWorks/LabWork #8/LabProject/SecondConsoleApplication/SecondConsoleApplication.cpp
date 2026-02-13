#include <iostream>

int main()
{
    int n;
    std::cout << "Input n: ";
    std::cin >> n;

    int sum{};
    int i = 1;
    for (; i <= n; i++) {
        sum += i;
    }

    std::cout << "\nSum: " << sum;
}
