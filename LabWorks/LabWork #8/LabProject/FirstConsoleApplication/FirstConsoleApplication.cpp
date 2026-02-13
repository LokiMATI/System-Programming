#include <iostream>

int main()
{
    int n;
    std::cout << "Input n: ";
    std::cin >> n;

    int sum{};
    for (int i = 1; i <= n; i++) {
        sum += i;
    }

    std::cout << "\nSum: " << sum;
}
