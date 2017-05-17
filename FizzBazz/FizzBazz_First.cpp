#include <iostream>
#include <string>

void showFizzBazz(int num);
std::string getFizzBazz(int num);

int main()
{
	const int num = 16;

	showFizzBazz(num);
}

void showFizzBazz(int num)
{
	for (int i = 1; i <= num; i++)
	{
		std::cout << getFizzBazz(i) << '\n';
	}
}

std::string getFizzBazz(int num)
{
	std::string result;

	if (num % 3 == 0) result += "Fizz";
	if (num % 5 == 0) result += "Bazz";

	if (result.empty()) return std::to_string(num);

	return result;
}