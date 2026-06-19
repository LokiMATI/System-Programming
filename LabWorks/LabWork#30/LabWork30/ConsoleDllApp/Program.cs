using ConsoleDllApp;
Console.WriteLine("Провекра простого числа:");
Console.WriteLine(LibraryImport.is_simple(2));
Console.WriteLine(LibraryImport.is_simple(10));
Console.WriteLine(LibraryImport.is_simple(1));

Console.WriteLine("Проверка массива на простое число:");
Console.WriteLine(LibraryImport.is_simple_array([1, 2, 3, 4, 5, 6, 7, 8, 9, 10], 10));

Console.WriteLine("Подсчёт расстояния:");
var first = new Point { x = 1, y = 1 };
var second = new Point { x = 2, y = 2 };
Console.WriteLine(LibraryImport.calc_def(first, second));
